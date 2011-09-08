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

using System.Xml;
using System.Xml.Serialization;

namespace vshield
{
 
        //[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        [XmlRoot(ElementName="VShieldEdgeConfig")]
        public class VShieldEdgeConfig
        {
            public FirewallConfig FirewallConfig { get; set; }
            public NATConfig NATConfig { get; set; }
        }

        public class FirewallConfig : List<FirewallRule> { }
        public class NATConfig : List<NATRule> { }

        public class NATRule
        {
            public string protocol { get; set; }
            public string icmpType { get; set; }
            public IpInfo internalIpAddress { get; set; }
            public PortInfo internalPort { get; set; }
            public IpInfo externalIpAddress { get; set; }
            public PortInfo externalPort { get; set; }
            public bool log { get; set; }
        }

        public class FirewallRule
        {
            public String protocol { get; set; }
            public IpInfo sourceIpAddress { get; set; }
            public PortInfo sourcePort { get; set; }
            public IpInfo destinationIpAddress { get; set; }
            public PortInfo destinationPort { get; set; }
            public String direction { get; set; }
            public String action { get; set; }
            public Boolean log { get; set; }
            public UInt32 ruleId { get; set; }

        }
        public class IpInfo
        {
            public String ipAddress { get; set; }
            public IpRange IpRange { get; set; }
        }
        public class IpRange
        {
            public String rangeStart { get; set; }
            public String rangeEnd { get; set; }
        }
        public class PortInfo
        {
            public String port { get; set; }
            public PortRange PortRange { get; set; }
        }
        public class PortRange
        {
            public String rangeStart { get; set; }
            public String rangeEnd { get; set; }
        }
}
