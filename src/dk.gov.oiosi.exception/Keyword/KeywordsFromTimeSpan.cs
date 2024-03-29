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
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.exception.Keyword {
    /// <summary>
    /// Get keywords from a given timespan.
    /// </summary>
    public class KeywordsFromTimeSpan {
        /// <summary>
        /// Gets the keywords from the given timespan.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeywords(TimeSpan timeSpan) {
            Dictionary<string, string> keywords = new Dictionary<string, string>();
            GetKeywords(keywords, timeSpan);
            return keywords;
        }

        /// <summary>
        /// Add the keywords from the given timespan to the given dictionary.
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="timeSpan"></param>
        public static void GetKeywords(Dictionary<string, string> keywords, TimeSpan timeSpan) {
            keywords.Add("timespantostring", timeSpan.ToString());
            keywords.Add("timespanticks", timeSpan.Ticks.ToString());
        }
    }
}
