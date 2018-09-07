using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Acrelec.Library.Logger;
using ECRUtilATLLib;
using System.IO;
using System.Reflection;

namespace RunPCDCheck
{
    class Program
    {
     
        static void Main(string[] args)
        {
           
            //object initiations
            InitPCDCall initPCDCall = new InitPCDCall();
            TerminalIPAddress terminalIPAddress = new TerminalIPAddress();
            StatusClass terminalStatus = new StatusClass();

            StringBuilder logDetails = new StringBuilder();

            logDetails.Append($"Date/Time: {DateTime.Now}.\n");

            //Set terminalIp Address
            terminalIPAddress.IPAddressIn = ConfigurationManager.AppSettings.Get("IPAddress");
            terminalIPAddress.SetIPAddress();

            if (terminalIPAddress.DiagRequestOut == "0")
            {
                logDetails.Append("PED IP Address set Correctly\n");
            }
            else
            {
                logDetails.Append("PED IP Address NOT set Correctly\n");
            }

            //check status at Idle
            terminalStatus.GetTerminalState();


            if (terminalStatus.StateOut == 1)
            {
                logDetails.Append("PED at Idle for PCD initialisation\n");
            }
            else
            {
                logDetails.Append("PED NOT at Idle for PCD initialisation\n");
            }

            //Launch the PCD call
            initPCDCall.Launch();

            if (initPCDCall.DiagRequestOut == "0")
            {
                logDetails.Append("The PED PCD initiation request is sent to the PED\n");
            }
            else
            {
                logDetails.Append("The PED PCD initiation request was NOT sent to the PED\n");
            }

            File.WriteAllText(ConfigurationManager.AppSettings.Get("LogPath"), logDetails.ToString());
        }
       
    }

}

