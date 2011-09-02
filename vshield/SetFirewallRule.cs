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

    [Cmdlet("Set", "FirewallRule")]
    public class SetFirewallRule : Cmdlet
    {

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
        public RestClient Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        private string _InternalPortGroupMofId;
        [Parameter(Position = 1, Mandatory = true)]
        public string InternalPortGroupMofId
        {
            get { return _InternalPortGroupMofId; }
            set { _InternalPortGroupMofId = value; }
        }

        private string _DstIp;
        [Parameter(Position = 2, Mandatory = true)]
        public string DstIp
        {
            get { return _DstIp; }
            set { _DstIp = value; }
        }

        private string _DstPort;
        [Parameter(Position = 3, Mandatory = true)]
        public string DstPort
        {
            get { return _DstPort; }
            set { _DstPort = value; }
        }

        private string _SrcIp;
        [Parameter(Position = 4, Mandatory = true)]
        public string SrcIp
        {
            get { return _SrcIp; }
            set { _SrcIp = value; }
        }

        private string _SrcPort;
        [Parameter(Position = 5, Mandatory = true)]
        public string SrcPort
        {
            get { return _SrcPort; }
            set { _SrcPort = value; }
        }

        private string _Action;
        [Parameter(Position = 6, Mandatory = true)]
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        private string _Direction;
        [Parameter(Position = 7, Mandatory = true)]
        public string Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }
        private string _Protocol;
        [Parameter(Position = 8, Mandatory = true)]
        public string Protocol
        {
            get { return _Protocol; }
            set { _Protocol = value; }
        }

        private VShieldEdgeConfig _FirewallRules;
        [Parameter(Position = 9, Mandatory = true)]
        public VShieldEdgeConfig FirewallRules
        {
            get { return _FirewallRules; }
            set { _FirewallRules = value; }
        }



        private VShieldEdgeConfig InitObject()
        {
            //Are there rules in the existing config?
            

            VShieldEdgeConfig vsec = new VShieldEdgeConfig();
            FirewallConfig fwconf = new FirewallConfig();
            FirewallRule fwrule = new FirewallRule();
            PortInfo dpi = new PortInfo();
            PortInfo spi = new PortInfo();
            IpInfo dip = new IpInfo();
            IpInfo sip = new IpInfo();

            fwrule.destinationIpAddress = dip;
            fwrule.destinationPort = dpi;
            fwrule.ruleId = 0;
            fwrule.sourceIpAddress = sip;
            fwrule.sourcePort = spi;

            if (_FirewallRules.FirewallConfig.Count > 0)
            {
                _FirewallRules.FirewallConfig.Add(fwrule);
                return _FirewallRules;
            }
            else
            {
                fwconf.Add(fwrule);
                vsec.FirewallConfig = fwconf;
                return vsec;
            }
        }
        private VShieldEdgeConfig SetObject()
        {
            VShieldEdgeConfig fwconf = InitObject();
            int count = fwconf.FirewallConfig.Count;
            fwconf.FirewallConfig[count-1].action = _Action;
            fwconf.FirewallConfig[count-1].direction = _Direction;
            fwconf.FirewallConfig[count-1].protocol = _Protocol;
            fwconf.FirewallConfig[count-1].destinationIpAddress.ipAddress = _DstIp;
            fwconf.FirewallConfig[count-1].destinationPort.port = _DstPort;
            fwconf.FirewallConfig[count-1].sourceIpAddress.ipAddress = _SrcIp;
            fwconf.FirewallConfig[count-1].sourcePort.port = _SrcPort;
            return fwconf;
        }

        protected override void ProcessRecord()
        {
            try
            {
                SetCertificatePolicy();
                
                
                VShieldXmlSerialzation xmlSerial = new VShieldXmlSerialzation();

                string xmlString = xmlSerial.SerializeObject(SetObject());


                StringBuilder requestResource = new StringBuilder();
                requestResource.AppendFormat("api/1.0/network/{0}/firewall/rules", _InternalPortGroupMofId);
                var request = new RestRequest(Method.POST);
                request.Resource = requestResource.ToString();

                request.AddParameter("application/xml", xmlString, ParameterType.RequestBody);

                var rr_fwrule = _Client.Execute(request);
                WriteDebug(rr_fwrule.ErrorMessage);
                WriteDebug(rr_fwrule.StatusDescription);
                WriteDebug(rr_fwrule.Content);

                
            }
            catch (Exception e)
            {
                WriteObject("C-Sharp Exception: " + e);
            }
        }
    }
}
