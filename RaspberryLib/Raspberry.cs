using System;
using System.Collections.Generic;

namespace RaspberryLib
{
    public class Raspberry
    {
        public List<Pin> Pins { get; set; }

        public Raspberry()
        {
            this.Pins = new List<Pin>();
            foreach (PinCode pinCode in Enum.GetValues(typeof(PinCode)))
            {
                Pin p = new Pin();
                p.Code = pinCode;
                p.Direction = PinDirectiion.UNKNOWN;
                p.Value = PinValue.UNKNOWN;
            }
        }

        public void ReadFromPin(PinCode pin, PinValue value)
        {

        }

        public void WriteToPin(PinCode pin, PinDirectiion pinDirectiion, PinValue value)
        {
            string pinNumber = ((int)pin).ToString();
            string pinDirection = pinDirectiion.ToString().ToLower();
            string pinValue = ((int)value).ToString();

            string command = "";
            command += string.Format("sudo -i");
            command.Bash();
            command += string.Format("echo \"{0}\" > /sys/class/gpio/export", pinNumber);
            command.Bash();
            command += string.Format("echo \"{0}\" > /sys/class/gpio/gpio{1}/direction", pinDirection, pinNumber);
            command.Bash();
            command += string.Format("echo \"{0}\" > /sys/class/gpio/gpio{1}/value", pinValue,pinNumber) + Environment.NewLine;
            command.Bash();
        }
    }

    public static class ShellHelper
    {
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}
