﻿using System;
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
    [Cmdlet("Get", "FirewallRule")]
    public class GetFirewallRule : Cmdlet
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
        /// <summary>
        /// Main section of Get-FirewallRule
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                StringBuilder requestResource       = new StringBuilder();
                var request                         = new RestRequest();
                SetCertificatePolicy();
                
                requestResource.AppendFormat("api/1.0/network/{0}/firewall/rules", _InternalPortGroupMofId);
                
                request.Resource                    = requestResource.ToString();
                var rr_fwrule                       = _Client.Execute<VShieldEdgeConfig>(request);

                WriteObject(rr_fwrule.Data);
            }
            catch (Exception e) { WriteObject("C-Sharp Exception: " + e); }
        }
    }
}