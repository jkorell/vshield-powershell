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
        public string intIpAddress
        {
            get { return _intIpAddress; }
            set { _intIpAddress = value; }
        }

        private string _intPort;
        [Parameter(Position = 3, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string intPort
        {
            get { return _intPort; }
            set { _intPort = value; }
        }

        private string _extIpAddress;
        [Parameter(Position = 4, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string extIpAddress
        {
            get { return _extIpAddress; }
            set { _extIpAddress = value; }
        }

        private string _extPort;
        [Parameter(Position = 5, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string extPort
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


        protected override void ProcessRecord()
        {
            try
            {

            }
            catch (Exception e) { WriteObject("C-Sharp Exception: " + e); }
        }

    }
}
