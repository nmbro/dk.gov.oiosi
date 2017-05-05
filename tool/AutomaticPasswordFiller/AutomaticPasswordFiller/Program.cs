using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Automation;

namespace dk.gov.oiosi.tool.AutomaticPasswordFiller
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new Exception("Require two parameter. First is the keepRunning file, and the second must be the password!");
            }

            new Program(args[0], args[1]);
        }

        public Program(string file, string password)
        {
            if (string.IsNullOrEmpty(file))
            {
                throw new Exception("The file is no valid - NullOrEmpty! ");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("The password is no valid - NullOrEmpty! ");
            }

            if (File.Exists(file))
            {
                this.SatisfyEverySafeNetTokenPasswordRequest(file, password);
            }
        }

        public void SatisfyEverySafeNetTokenPasswordRequest(string file, string password)
        {
            //// Source: http://stackoverflow.com/questions/17927895/automate-extended-validation-ev-code-signing
            int count = 0;
            Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent, AutomationElement.RootElement, TreeScope.Children, (sender, e) =>
            {
                AutomationElement element = sender as AutomationElement;
                if (element.Current.Name == "Token Logon")
                {
                    WindowPattern pattern = (WindowPattern)element.GetCurrentPattern(WindowPattern.Pattern);
                    pattern.WaitForInputIdle(10000);
                    AutomationElement edit = element.FindFirst(TreeScope.Descendants, new AndCondition(
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit),
                        new PropertyCondition(AutomationElement.NameProperty, "Token Password:")));

                    AutomationElement ok = element.FindFirst(TreeScope.Descendants, new AndCondition(
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                        new PropertyCondition(AutomationElement.NameProperty, "OK")));

                    if (edit != null && ok != null)
                    {
                        count++;
                        ValuePattern vp = (ValuePattern)edit.GetCurrentPattern(ValuePattern.Pattern);
                        vp.SetValue(password);
                        Console.WriteLine("SafeNet window (count: " + count + " window(s)) detected. Setting password...");

                        InvokePattern ip = (InvokePattern)ok.GetCurrentPattern(InvokePattern.Pattern);
                        ip.Invoke();
                    }
                    else
                    {
                        Console.WriteLine("SafeNet window detected but not with edit and button...");
                    }
                }
            });

            bool fileExist = true;
            do
            {
                Thread.Sleep(500);
                if (!File.Exists(file))
                {
                    fileExist = false;
                }
            }
            while (fileExist);

            Automation.RemoveAllEventHandlers();
        }
    }
}
