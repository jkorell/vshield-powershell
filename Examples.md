# Introduction #
Quick examples of what the library can currently do.

# Details #

```
# Connect to the vShield Management appliance.  This isn’t a real connect it just creates the client object and I figured it would
# be easier to just do this once then every get or set call.
$connect = Connect-VShield -Server "https://10.53.2.103" -User "admin" -Password "default"
 
#Create a firewall rule
#Get the current firewall rules.
$firewallRules = Get-FirewallRule -Client $connect -InternalPortGroupMofId "dvportgroup-2735"
#The API requires that when you add a rule that you send all existing rules back.
Set-FirewallRule -Client $connect -InternalPortGroupMofId "dvportgroup-2735" -DstIp "10.53.2.246" -DstPort "445" -SrcIp "any" -SrcPort "any" -Action "allow" -Direction "both" -Protocol "tcp" -FirewallRules $firewallRules
 
 
#Create a firewall rule with a IP range
Add-PSSnapin vshieldsnapin
$connect = Connect-VShield -Server "https://10.53.2.103" -User "admin" -Password "default"
$firewallRules =Get-FirewallRule -Client $connect -InternalPortGroupMofId "dvportgroup-2735"
Set-FirewallRule -Client $connect -InternalPortGroupMofId "dvportgroup-2735" -DstIp "10.53.2.1-10.53.2.254" -DstPort "80" -SrcIp "any" -SrcPort "any" -Action "allow" -Direction "both" -Protocol "tcp" -FirewallRules $firewallRules
 
#Create a destination NAT rule
Add-PSSnapin vshieldsnapin
$connect = Connect-VShield-Server "https://10.53.2.103" -User "admin" -Password "default"
$natRule = Get-NatRule -Client $connect -InternalPortGroupMofId "dvportgroup-2735"
Set-NatRule -Client $connect -InternalPortGroupMofId "dvportgroup-2735" -IntIpAddress "192.168.0.3" -IntPort "3389" -ExtIpAddress "10.53.12.227" -ExtPort "3389" -Protocol "tcp" -NatRules $natRule

```