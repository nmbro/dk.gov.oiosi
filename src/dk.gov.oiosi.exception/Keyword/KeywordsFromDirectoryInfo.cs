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
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dk.gov.oiosi.exception.Keyword {

    /// <summary>
    /// From a DirectoryInfo object, returns the dictionary name and full name as keywords
    /// </summary>
    public class KeywordsFromDirectoryInfo {

        /// <summary>
        /// From a DirectoryInfo object, returns the dictionary name and full name as keywords
        /// </summary>
        /// <param name="directoryInfo">The directory information object</param>
        /// <returns>Returns a dictionary with the extracted keywords</returns>
        public static Dictionary<string, string> GetKeywords(DirectoryInfo directoryInfo) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeywords(keywords, directoryInfo);
            return keywords;
        }

        /// <summary>
        /// From a DirectoryInfo object, get the dictionary name and full name, and inserts them as
        /// keywords into the 'keywords' dictionary
        /// </summary>
        /// <param name="keywords">The keyword dictionary to add the directory keywords to</param>
        /// <param name="directoryInfo">The directory information object</param>
        public static void GetKeywords(Dictionary<string, string> keywords, DirectoryInfo directoryInfo) {
            keywords.Add("directoryname", directoryInfo.Name);
            keywords.Add("directoryfullname", directoryInfo.FullName);
        }
    }
}
