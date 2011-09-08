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

namespace vshield
{
    [RunInstaller(true)]
    public class vshieldsnapin : PSSnapIn
    {
        public override string Description
        {
            get { return "This Windows PowerShell snap-in contains Windows PowerShell \r\n cmdlets for managing vSphere vShield."; }
        }
        public override string Name
        {
            get { return "vShield"; }
        }
        public override string Vendor
        {
            get { return "Vendor"; }
        }
    }
}