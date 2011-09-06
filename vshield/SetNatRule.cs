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
    [Cmdlet("Set", "NatRule")]
    public class SetNatRule : Cmdlet
    {

        /// <summary>
        /// SetCertificatePolicy()
        /// RemoteCertiticateValidate() 
        /// Allows any SSL certificate to be used.
        /// From the web, don't have the URL.
        /// </summary>
        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        private RestClient _Client;
        [Parameter(Position = 0, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public RestClient Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        private string _InternalPortGroupMofId;
        [Parameter(Position = 1, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string InternalPortGroupMofId
        {
            get { return _InternalPortGroupMofId; }
            set { _InternalPortGroupMofId = value; }
        }

        private string _intIpAddress;
        [Parameter(Position = 2, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string IntIpAddress
        {
            get { return _intIpAddress; }
            set { _intIpAddress = value; }
        }

        private string _intPort;
        [Parameter(Position = 3, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string IntPort
        {
            get { return _intPort; }
            set { _intPort = value; }
        }

        private string _extIpAddress;
        [Parameter(Position = 4, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ExtIpAddress
        {
            get { return _extIpAddress; }
            set { _extIpAddress = value; }
        }

        private string _extPort;
        [Parameter(Position = 5, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ExtPort
        {
            get { return _extPort; }
            set { _extPort = value; }
        }
        private string _Protocol;
        [Parameter(Position = 6, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Protocol
        {
            get { return _Protocol; }
            set { _Protocol = value; }
        }

        private VShieldEdgeConfig _NatRules;
        [Parameter(Position = 7, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public VShieldEdgeConfig NatRules
        {
            get { return _NatRules; }
            set { _NatRules = value; }
        }

        private VShieldEdgeConfig InitObject()
        {
            VShieldEdgeConfig vsec = new VShieldEdgeConfig();
            NATConfig natConfig = new NATConfig();
            NATRule natRule = new NATRule();
            PortInfo ipi = new PortInfo();
            PortInfo epi = new PortInfo();
            IpInfo iip = new IpInfo();
            IpInfo eip = new IpInfo();

            natRule.externalIpAddress = eip;
            natRule.externalPort = epi;
            natRule.internalIpAddress = iip;
            natRule.internalPort = ipi;

            if (_NatRules.NATConfig.Count > 0)
            {
                _NatRules.NATConfig.Add(natRule);
                return _NatRules;
            }
            else
            {
                natConfig.Add(natRule);
                vsec.NATConfig = natConfig;
                return vsec;
            }
            
        }

        private VShieldEdgeConfig SetObject()
        {
            VShieldEdgeConfig vsec = InitObject();
            int count = vsec.NATConfig.Count;
            vsec.NATConfig[count - 1].protocol = _Protocol;

            string[] intIpArray = ParseRange(_intIpAddress);
            string[] intPortArray = ParseRange(_intPort);
            string[] extIpArray = ParseRange(_extIpAddress);
            string[] extPortArray = ParseRange(_extPort);

            if (intIpArray.Length > 1)
            {


                vsec.NATConfig[count - 1].internalIpAddress.IpRange = new IpRange();
                vsec.NATConfig[count - 1].internalIpAddress.IpRange.rangeStart = intIpArray[0];
                vsec.NATConfig[count - 1].internalIpAddress.IpRange.rangeEnd = intIpArray[1];

            }
            else
            {
                vsec.NATConfig[count - 1].internalIpAddress.ipAddress = _intIpAddress;
            }

            if (intPortArray.Length > 1)
            {
                vsec.NATConfig[count - 1].internalPort.PortRange = new PortRange();
                vsec.NATConfig[count - 1].internalPort.PortRange.rangeStart = intPortArray[0];
                vsec.NATConfig[count - 1].internalPort.PortRange.rangeEnd = intPortArray[1];

            }
            else
            {
                vsec.NATConfig[count - 1].internalPort.port = _intPort;
            }

            if (extIpArray.Length > 1)
            {
                vsec.NATConfig[count - 1].externalIpAddress.IpRange = new IpRange();
                vsec.NATConfig[count - 1].externalIpAddress.IpRange.rangeStart = extIpArray[0];
                vsec.NATConfig[count - 1].externalIpAddress.IpRange.rangeEnd = extIpArray[1];
            }
            else
            {
                vsec.NATConfig[count - 1].externalIpAddress.ipAddress = _extIpAddress;
            }

            if (extPortArray.Length > 1)
            {
                vsec.NATConfig[count - 1].externalPort.PortRange = new PortRange();
                vsec.NATConfig[count - 1].externalPort.PortRange.rangeStart = extPortArray[0];
                vsec.NATConfig[count - 1].externalPort.PortRange.rangeEnd = extPortArray[1];

            }
            else
            {
                vsec.NATConfig[count - 1].externalPort.port = _extPort;
            }
            return vsec;
        }


        /// <summary>
        /// ParseRange()
        /// If a paramater is returned with '-' extract the IP Addresses.
        /// </summary>
        /// <param name="range"></param>
        /// <returns>string[]</returns>
        private string[] ParseRange(string range)
        {
            string[] bufArray;
            if (range.Contains('-'))
            {
                bufArray = range.Split(new char[] { ' ', '-' });
                return new string[] { bufArray[0], bufArray[bufArray.Length - 1] };
            }
            return new string[] { range };
        }

        protected override void ProcessRecord()
        {
            try
            {

                StringBuilder requestResource = new StringBuilder();
                VShieldXmlSerialzation xmlSerial = new VShieldXmlSerialzation();
                var request = new RestRequest(Method.POST);
                SetCertificatePolicy();

                string xmlString = xmlSerial.SerializeObject(SetObject());

                requestResource.AppendFormat("api/1.0/network/{0}/dnat/rules", _InternalPortGroupMofId);
                request.Resource = requestResource.ToString();
                request.AddParameter("application/xml", xmlString, ParameterType.RequestBody);
                var rr_natrule = _Client.Execute(request);

                //WriteObject(xmlString);
                WriteWarning(rr_natrule.ErrorMessage);
                WriteWarning(rr_natrule.StatusDescription);
                WriteWarning(rr_natrule.Content);

            }
            catch (Exception e) { WriteObject("C-Sharp Exception: " + e); }
        }

    }
}
