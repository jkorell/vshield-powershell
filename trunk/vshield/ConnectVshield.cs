using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Management.Automation;
using System.ComponentModel;
using RestSharp;

using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace vshield
{
    [Cmdlet("Connect", "VShield")]
    public class ConnectVshield : Cmdlet
    {

        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }


        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        private string _Server;
        [Parameter(Position = 0,Mandatory = true)]
        public string Server
        {
            get { return _Server; }
            set { _Server = value; }
        }

        private string _User;
        [Parameter(Position = 1, Mandatory = true)]
        public string User
        {
            get { return _User; }
            set { _User = value; }
        }

        private string _Password;
        [Parameter(Position = 2, Mandatory = true)]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }


        protected override void ProcessRecord()
        {
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = _Server;
                client.Authenticator = new HttpBasicAuthenticator(_User, _Password);

                WriteObject(client);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
