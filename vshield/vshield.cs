/*
 *  vshield-powershell
 *   Copyright (C) <2011>  <Joseph Callen>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/


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
    [RunInstaller(true)]
    public class vshieldsnapin : PSSnapIn
    {

        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }


        public static int ApiVersion(string _InternalPortGroupMofId, RestClient _Client)
        {
            try
            {
                //https://<vsm-ip>/api/versions/edge/dvportgroup-63

                StringBuilder requestResource = new StringBuilder();
                var request = new RestRequest();
                SetCertificatePolicy();

                requestResource.AppendFormat("api/versions/edge/{0}", _InternalPortGroupMofId);

                request.Resource = requestResource.ToString();
                var rr_ver = _Client.Execute(request);

                if (rr_ver.StatusCode != HttpStatusCode.OK)
                {
                    //WriteWarning(rr_fwrule.ErrorMessage);
                    //WriteWarning(rr_fwrule.StatusDescription);
                    //WriteWarning(rr_fwrule.Content);

                    //we are going to assume <- yeah, that the API version is 1 if Http returns not OK
                    return 1;
                }
                else
                {
                    return 2;
                }

                //WriteWarning("PowerShell Formatting File Not Implemented Yet");
                //WriteObject(rr_fwrule.Data);
            }
            catch (Exception e) { return -1; }

            //we should never get here
            return 0;
        }



        public override string Description
        {
            get { return "This Windows PowerShell snap-in contains Windows PowerShell \r\n cmdlets for managing vSphere vShield."; }
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