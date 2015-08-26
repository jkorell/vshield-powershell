VMware vShield PowerShell CmdLets

VMware currently has no easy method to create firewall and NAT rules on a vShield Edge appliance.  Along with C# and RestSharp I have created a PowerShell snap-in library to do just that.  I have already noticed that I could have wrote this in other ways, maybe if I get time I will fix it but for now feedback is very welcome.

So I would recommend PowerShell v2.0 and requires .Net Framework 3.5.

C:\%windir%\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe" /i vshield.dll
Open PowerShell or VMware’s PowerCLI
Confirm that the snapin is registered by running "Get-PSSnapin –Registered"
Then add the snapin "Add-PSSnapin vshieldsnapin"