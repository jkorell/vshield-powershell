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
            get { return "CSC"; }
        }
    }


    

}
