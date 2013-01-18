using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.test.unit {
    public class Print {

        public static void Started(string testName) {
            Console.WriteLine("{0} {1} Started", DateTime.Now, testName);
        }

        public static void Completed(string testName) {
            Console.WriteLine("{0} {1} Completed", DateTime.Now, testName);
        }

        public static void ExceptionMessage(object instance, Exception ex) {
            Out("{0} has thrown exception {1}", instance, ex.Message);
        }

        public static void Out(string message, params object[] args) {
            string buildMessage = String.Format(message, args);
            Console.WriteLine("{0} {1}", DateTime.Now, buildMessage);
        }
    }
}
