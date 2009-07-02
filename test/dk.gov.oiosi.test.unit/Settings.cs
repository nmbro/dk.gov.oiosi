using System;
using System.IO;

namespace dk.gov.oiosi.test.unit {
    internal class Settings
    {
        public static DirectoryInfo OutputDirectory {
            get { return new DirectoryInfo(Path.Combine("Output", RandomName));}
        }

        public static FileInfo CreateRandomPath(string filename) {
            return new FileInfo(Path.Combine(OutputDirectory.FullName, filename));
        }

        public static string RandomName {
            get {
                var randomName = DateTime.Now.Ticks.ToString().Substring(0, 8) + Guid.NewGuid().ToString().Substring(0, 10) + new Random(100).Next(0, 1000);
                return randomName;
            }
        }

    }
}