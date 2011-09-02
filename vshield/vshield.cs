using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Management.Automation;
using System.ComponentModel;

using RestSharp;



namespace vshield
{
    [RunInstaller(true)]
    public class vshieldsnapin : PSSnapIn
    {
        public override string Description
        {
            get { return "Snapin to provide vshield operations"; }
        }

        public override string Name
        {
            get { return "vshieldsnapin"; }
        }

        public override string Vendor
        {
            get { return "Vendor"; }
        }
    }
}

//Add-PSSnapin vshieldsnapin
//$connect = Connect-VShield -Server "https://10.53.2.103" -User "admin" -Password "default"
//$firewallRules = Get-FirewallRule -Client $connect -InternalPortGroupMofId "dvportgroup-2735"
//Set-FirewallRule -Client $connect -InternalPortGroupMofId "dvportgroup-2735" -DstIp "10.53.2.1-10.53.2.254" -DstPort "80" -SrcIp "any" -SrcPort "any" -Action "allow" -Direction "both" -Protocol "tcp" -FirewallRules $firewallRules
