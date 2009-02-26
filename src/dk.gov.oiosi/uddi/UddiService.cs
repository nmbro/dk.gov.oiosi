using System;
using System.Collections.Generic;
using System.Net.Mail;
using dk.gov.oiosi.common;
using dk.gov.oiosi.security;
using dk.gov.oiosi.uddi.category;

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
        private readonly List<UddiBinding> bindings;

        public UddiService(businessService service, List<UddiBinding> uddiBindings) {
            if (service == null) throw new ArgumentNullException("service");
            if (uddiBindings == null) throw new ArgumentNullException("uddiBindings");
            
            this.service = service;
            bindings = uddiBindings;
        }

        public List<UddiBinding> Bindings {
            get { return bindings; }
        }

        public DateTime ActivationDateUTC {
            get { return GetActivationDateUTC(); }
        }

        public CertificateSubject CertificateSubject {
            get { return GetCertificateSubject(); }
        }

        public DateTime ExpirationDateUTC {
            get { return GetExpirationDateUtcFormat(); }
        }

        public UddiId NewerVersion {
            get { return GetNewerVersion(); }
        }

        public MailAddress ContactMail {
            get { return GetContactMail(); }
        }

        public Uri TermsOfUseUri {
            get { return GetTermsOfUseUrl(); }
        }

        public Version Version {
            get { return GetVersion(); }
        }

        private DateTime GetActivationDateUTC() {
            keyedReference activationDate = UddiCategory.GetOptionalCategoryByIdentifier(service.categoryBag, activationDateId);
            if (activationDate == null) return DateTime.MinValue;
            return GetDatetimeFromLifetimeDates(activationDate.keyValue, false);
        }

        private CertificateSubject GetCertificateSubject() {
            keyedReference certificateReference = UddiCategory.GetMandatoryCategoryByIdentifier(service.categoryBag, certificateSubjectId);
            return new CertificateSubject(certificateReference.keyValue);
        }

        private DateTime GetExpirationDateUtcFormat() {
            keyedReference expirationDate = UddiCategory.GetOptionalCategoryByIdentifier(service.categoryBag, expirationDateId);
            if (expirationDate == null) return DateTime.MaxValue;
            return GetDatetimeFromLifetimeDates(expirationDate.keyValue, false);
        }

        private UddiId GetNewerVersion() {
            keyedReference newVersKeyref = UddiCategory.GetOptionalCategoryByIdentifier(service.categoryBag, newerVersionId);
            if (newVersKeyref == null) return null;
            if (String.IsNullOrEmpty(newVersKeyref.keyValue)) return null;
            return IdentifierUtility.GetUddiIDFromString(newVersKeyref.keyValue);
        }

        private Uri GetTermsOfUseUrl() {
            keyedReference termsOfUseUrl = UddiCategory.GetOptionalCategoryByIdentifier(service.categoryBag, termsOfUseUrlId);
            if (termsOfUseUrl == null) return null;
            if (String.IsNullOrEmpty(termsOfUseUrl.keyValue)) return null;

            return new Uri(termsOfUseUrl.keyValue);
        }

        private MailAddress GetContactMail() {
            keyedReference contactMail = UddiCategory.GetOptionalCategoryByIdentifier(service.categoryBag, contactMailId);
            if (contactMail == null) return null;
            if (String.IsNullOrEmpty(contactMail.keyValue)) return null;

            return new MailAddress(contactMail.keyValue);
        }

        private Version GetVersion() {
            keyedReference majorVersion = UddiCategory.GetOptionalCategoryByIdentifier(service.categoryBag, versionMajorId);
            if (majorVersion == null) return null;

            keyedReference minorVersion = UddiCategory.GetOptionalCategoryByIdentifier(service.categoryBag, versionMinorId);
            keyedReference revision = UddiCategory.GetOptionalCategoryByIdentifier(service.categoryBag, versionRevisionId);


            int majorInt = 0;
            int minorInt = 0;
            int revisionInt = 0;

            majorInt = Int32.Parse(majorVersion.keyValue);
            if (minorVersion != null) minorInt = Int32.Parse(minorVersion.keyValue);
            if (revision != null) revisionInt = Int32.Parse(revision.keyValue);

            return new Version(majorInt, minorInt, revisionInt);
        }

        public IEnumerable<UddiBinding> GetBindingsSupportingAnyProfileAndRole(List<UddiId> profileUddiIds, string roleIdentifier) {
            foreach (UddiBinding binding in bindings) {
                if (binding.SupportsAnyProfileAndRole(profileUddiIds, roleIdentifier)) {
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
            DateTime nowUTC = DateTime.UtcNow;

            return !(nowUTC > ActivationDateUTC && nowUTC < ExpirationDateUTC);
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
