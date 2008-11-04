/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using dk.gov.oiosi.uddi.category;

namespace dk.gov.oiosi.uddi.ars {

    /// <summary>
    /// A user control that finds the possible enitites, given a certain lookup type
    /// </summary>
    [DefaultEvent("SelectedIndexChanged")]
    public partial class LookupTModelComboBox : UserControl {
        private LookupType _arsLookupType = LookupType.httpServiceRegistrationReference;
        private RegistrationConformanceClaimCode _registrationConformance = RegistrationConformanceClaimCode.oiosi1_1;

        /// <summary>
        /// Raises the SelectedIndexChanged event
        /// </summary>
        public event EventHandler SelectedIndexChanged;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public LookupTModelComboBox() {

            InitializeComponent();           
        }

        /// <summary>
        /// Determines what type of entities to lookup
        /// </summary>
        [
        Browsable(true),
        CategoryAttribute("Uddi"),
        DescriptionAttribute("Determines the lookup type"),
        ]
        public LookupType ArsLookupType {
            get {
                return _arsLookupType;
            }
            set { _arsLookupType = value; }
        }

        /// <summary>
        /// Determines what registration conformance claim to use
        /// </summary>
        [
        Browsable(true),
        CategoryAttribute("Uddi"),
        DescriptionAttribute("Determines the conformance claim"),
        ]
        public RegistrationConformanceClaimCode RegistrationConformance {
            get { return _registrationConformance; }
            set { _registrationConformance = value; }
        }

        /// <summary>
        /// Returns the text of the combo box
        /// </summary>
        public override string Text {
            get {
                if (!DesignMode) {
                    return comboBox1.Text;
                } else {
                    return "";
                }
            }
            set {
                DataTable list = (DataTable) comboBox1.DataSource;
                for (int i=0; i<list.Rows.Count; i++)
                    if (list.Rows[i]["Display"].ToString().ToUpper().Equals(value.ToUpper()))
                        comboBox1.SelectedIndex = i;
            }
        }

        /// <summary>
        /// Gets the guid of the selected entity
        /// </summary>
        public string Id {
            get {
                if (!DesignMode) {
                    if (comboBox1.SelectedIndex == 0) return "";
                    else
                        return comboBox1.SelectedValue.ToString();
                }
                else {
                    return "";
                }
            }
            set {
                for (int i = 0; i < comboBox1.Items.Count;i++) {
                    DataRowView row = (DataRowView) comboBox1.Items[i];
                    if (row[1].ToString().ToUpper().Equals(value.ToUpper())) {
                        comboBox1.SelectedIndex = i;
                        break;
                    }
                    comboBox1.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Returns the matching porttype guid for a binding
        /// </summary>
        public string portTypeId {
            get {
                if (!DesignMode) {
                    if (Id.Length > 0 && ArsLookupType != LookupType.procesDefinitionReference) {
                        OasisBindingRegistration binding = null;
                        try {
                            binding = OasisBindingRegistration.Get(new UddiGuidId(Id, true));
                            return binding.PortTypeDefinitionReference.Value;
                        }
                        catch {
                            return "";
                        }
                    }
                    else {
                        return "";
                    }

                }
                else {
                    return "";
                }
            }
        }

        private void LookupTModelComboBox_Load(object sender, EventArgs e) {
            if (!DesignMode)
                LoadCombobox();
        }

        private void LoadCombobox() {

            //2. based on the registrationconformanceclaim
            switch (RegistrationConformance) {

                //3. based on the lookup type, find possible values
                case RegistrationConformanceClaimCode.oiosi1_1:

                    switch (ArsLookupType) {

                        case LookupType.httpServiceRegistrationReference:
                            comboBox1.Items.Clear();
                            GetServiceRegistrationData();
                            break;
                        case LookupType.smtpServiceRegistrationReference:
                            comboBox1.Items.Clear();
                            GetServiceRegistrationData();
                            break;
                        case LookupType.procesDefinitionReference:
                            comboBox1.Items.Clear();
                            GetProcesDefinitionData();
                            break;
                        case LookupType.businessEntity:
                            comboBox1.Items.Clear();
                            GetBusinessEntityData();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        private void GetProcesDefinitionData() {

            //1. initialize datatable
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("Display", typeof(string)));
            list.Columns.Add(new DataColumn("Id", typeof(string)));
            list.Rows.Add(list.NewRow());
            list.Rows[0][0] = "vælg entitet";
            list.Rows[0][1] = "";

            //2. get data from uddi
            List<tModelInfo> tList = ArsLookup.FindArsProcessDefinition("%");

            //3. set datatable with uddidata
            for (int i = 0; i < tList.Count; i++) {
                list.Rows.Add(list.NewRow());
                list.Rows[i + 1][0] = tList[i].name.Value;
                list.Rows[i + 1][1] = tList[i].tModelKey;
            }
            
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "Display";
            comboBox1.ValueMember = "Id";
        }

        /// <summary>
        /// finds posible http oasisbindingregistrations
        /// </summary>
        private void GetServiceRegistrationData() {

            //1. initialize datatable
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("Display", typeof(string)));
            list.Columns.Add(new DataColumn("Id", typeof(string)));
            list.Rows.Add(list.NewRow());
            list.Rows[0][0] = "vælg entitet";
            list.Rows[0][1] = "";

            //2. get data from uddi
            List<tModelInfo> tList = null;
            switch (ArsLookupType) {
                case LookupType.httpServiceRegistrationReference:
                    tList = ArsLookup.FindArsServiceDefinition("%", UddiOrgWsdlCategorizationTransportCode.http.ToString());
                    break;
                case LookupType.smtpServiceRegistrationReference:
                    tList = ArsLookup.FindArsServiceDefinition("%", UddiOrgWsdlCategorizationTransportCode.smtp.ToString());
                    break;
                default:
                    break;
            }
            

            //3. set datatable with uddidata
            for (int i = 0; i < tList.Count; i++) {
                list.Rows.Add(list.NewRow());
                list.Rows[i + 1][0] = tList[i].name.Value;
                list.Rows[i + 1][1] = tList[i].tModelKey;
            }

            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "Display";
            comboBox1.ValueMember = "Id";
        }

        private void GetBusinessEntityData() {

            //1. initialize datatable
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("Display", typeof(string)));
            list.Columns.Add(new DataColumn("Id", typeof(string)));
            list.Rows.Add(list.NewRow());
            list.Rows[0][0] = "vælg entitet";
            list.Rows[0][1] = "";

            //2. get data from uddi
            List<businessInfo> tList = ArsLookup.FindArsBusinessEntities("%");

            //3. set datatable with uddidata
            for (int i = 0; i < tList.Count; i++) {
                list.Rows.Add(list.NewRow());
                list.Rows[i + 1][0] = tList[i].name[0].Value;
                list.Rows[i + 1][1] = tList[i].businessKey;
            }

            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "Display";
            comboBox1.ValueMember = "Id";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.SelectedIndexChanged != null) SelectedIndexChanged(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Describes the available modes of the user control
    /// </summary>
    public enum LookupType {

        /// <summary>
        /// Defines that the lookup is a http oasis binding reference
        /// </summary>
        httpServiceRegistrationReference,

        /// <summary>
        /// Defines that the lookup is a smtp oasis binding reference
        /// </summary>
        smtpServiceRegistrationReference,

        /// <summary>
        /// Defines that the lookup is a proces definition reference
        /// </summary>
        procesDefinitionReference,

        /// <summary>
        /// Defines that the lookup is a businessentity reference
        /// </summary>
        businessEntity
    }
}
