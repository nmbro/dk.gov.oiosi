using System;
using System.Collections.Generic;
using System.Net.Mail;
using dk.gov.oiosi.common;
using dk.gov.oiosi.security;

namespace dk.gov.oiosi.uddi
{
    internal class UddiService
    {
        private const string activationDateId = "uddi:B5449299-B951-4266-9952-4C4470970782";
        private const string certificateSubjectId = "uddi:e6feac92-0aee-4824-ae02-882760609e8a";
        private const string expirationDateId = "uddi:2a8d2edc-cd1e-453f-9777-b623c80aaba5";
        private const string newerVersionId = "uddi:23c1ed31-ee5c-4a13-983e-92a0815e6120";
        private const string contactMailId = "uddi:5194201c-fc02-4d2e-8224-910939ac384d";
        private const string termsOfUseUrlId = "uddi:8cc1e1c1-d0f4-4bbe-90e0-90cd1976d944";
        private const string versionMajorId = "uddi:9c119cc4-cca5-4d98-bee2-5fe3999506e9";
        private const string versionMinorId = "uddi:ba4afec4-f6c1-4205-8212-5eb0472000f9";
        private const string versionRevisionId = "uddi:8e7ce808-0279-4042-8088-133667744c6f";

        private readonly businessService service;
        private readonly UddiCategoryBag categoryBag;
        private readonly List<UddiBinding> bindings;

        public UddiService(businessService service, List<UddiBinding> uddiBindings) {
            if (service == null) throw new ArgumentNullException("service");
            if (uddiBindings == null) throw new ArgumentNullException("uddiBindings");
            
            this.service = service;
            this.categoryBag = new UddiCategoryBag(service.categoryBag);
            bindings = uddiBindings;
        }

        public List<UddiBinding> Bindings {
            get { return bindings; }
        }

        public IEnumerable<UddiBinding> GetBindingsSupportingOneOrMoreProfileAndRole(List<UddiId> profileUddiIds, string roleIdentifier) {
            foreach (UddiBinding binding in bindings) {
                if (binding.SupportsOneOrMoreProfileAndRole(profileUddiIds, roleIdentifier)) {
                    yield return binding;
                }
            }
        }

        /// <summary>
        /// Returns true if this service is inactive or expired, according to its UDDI registration.
        /// All registrations on the UDDI are assumed to follow danish time zone conventions
        /// </summary>
        /// <returns>Returns true if this service is inactive or expired, according to its UDDI registration.</returns>
        public bool IsInactiveOrExpired() {
            DateTime activationDate = GetActivationDateUtc();
            DateTime expirationDate = GetExpirationDateUtc();
            DateTime nowUTC = DateTime.UtcNow;
            return !(nowUTC > activationDate && nowUTC < expirationDate);
        }

        public DateTime GetActivationDateUtc() {
            keyedReference activationDate;
            if (!this.categoryBag.TryGetKeyedReference(activationDateId, out activationDate)) {
                return DateTime.MinValue;
            }
            return GetDatetimeFromLifetimeDates(activationDate.keyValue, false);
        }

        public CertificateSubject GetCertificateSubject() {
            keyedReference certificateReference;
            if (!this.categoryBag.TryGetKeyedReference(certificateSubjectId, out certificateReference)) {
                return null;
            }
            return new CertificateSubject(certificateReference.keyValue);
        }

        public DateTime GetExpirationDateUtc() {
            keyedReference expirationDate;
            if (!this.categoryBag.TryGetKeyedReference(expirationDateId, out expirationDate)) {
                return DateTime.MinValue;
            }
            return GetDatetimeFromLifetimeDates(expirationDate.keyValue, false);
        }

        public UddiId GetNewerVersion() {
            keyedReference newVersKeyref;
            if (!this.categoryBag.TryGetKeyedReference(newerVersionId, out newVersKeyref)) {
                return null;
            }
            if (String.IsNullOrEmpty(newVersKeyref.keyValue)) return null;
            return IdentifierUtility.GetUddiIDFromString(newVersKeyref.keyValue);
        }

        public Uri GetTermsOfUseUrl() {
            keyedReference termsOfUseUrl;
            if (!this.categoryBag.TryGetKeyedReference(termsOfUseUrlId, out termsOfUseUrl)) {
                return null;
            }
            if (String.IsNullOrEmpty(termsOfUseUrl.keyValue)) return null;
            return new Uri(termsOfUseUrl.keyValue);
        }

        public MailAddress GetContactMail() {
            keyedReference contactMail;
            if (!this.categoryBag.TryGetKeyedReference(contactMailId, out contactMail)) {
                return null;
            }
            if (String.IsNullOrEmpty(contactMail.keyValue)) return null;
            return new MailAddress(contactMail.keyValue);
        }

        public Version GetVersion() {
            keyedReference majorVersion;
            this.categoryBag.TryGetKeyedReference(versionMajorId, out majorVersion);

            keyedReference minorVersion;
            this.categoryBag.TryGetKeyedReference(versionMinorId, out minorVersion);

            keyedReference revisionVersion;
            this.categoryBag.TryGetKeyedReference(versionRevisionId, out revisionVersion);

            int majorInt = 0;
            int minorInt = 0;
            int revisionInt = 0;

            if (!string.IsNullOrEmpty(majorVersion.keyValue)) {
                majorInt = Int32.Parse(majorVersion.keyValue);
            }
            if (!string.IsNullOrEmpty(minorVersion.keyValue)) {
                minorInt = Int32.Parse(minorVersion.keyValue);
            }
            if (!string.IsNullOrEmpty(revisionVersion.keyValue)) {
                revisionInt = Int32.Parse(revisionVersion.keyValue);
            }
            return new Version(majorInt, minorInt, revisionInt);
        }

        /// <summary>
        /// Returns a DateTime representation of either an endpoint expiration
        /// or activation date, in UTC
        /// </summary>
        /// <param name="datestring">The date of an endpoint expiration or activation</param>
        /// <param name="getActivationDate">True if the date represents an activation date,
        /// false if expiration date. This influences which default is returned, if no date
        /// is present in the string (DateTime.MinValue for activation date, 
        /// DateTime.MaxValue for expiration time</param>
        /// <returns>
        /// Returns a DateTime representation of either an endpoint expiration
        /// or activation date, in UTC
        /// </returns>
        private static DateTime GetDatetimeFromLifetimeDates(string datestring, bool getActivationDate) {
            if (String.IsNullOrEmpty(datestring)) {
                // Then set default date:
                if (getActivationDate) return DateTime.MinValue;
                return DateTime.MaxValue;
            }
            
            if (datestring.Length < 20) {
                // Heuristic, treat as local date:
                return DateTime.Parse(datestring);
            }

            // Heuristic, treat as UTC date:
            return DateTime.Parse(datestring, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        }
    }
}
