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
