﻿/*
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

using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace vshield
{
    public class VShieldXmlSerialzation
    {
        public VShieldXmlSerialzation() { }

        /// <summary>
        /// Help from http://www.dotnetjohn.com/articles.aspx?articleid=173
        ///http://webcache.googleusercontent.com/search?q=cache:sUdcOg-8mNsJ:weblogs.asp.net/rmclaws/archive/2003/07/31/22080.aspx+memorystream+xmltextwriter+%3F&cd=4&hl=en&ct=clnk&gl=us
        ///All I have to say is M$ really screwed this all up...
        /// </summary>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public String SerializeObject(Object pObject)
        {
            try
            {
                String XmlizedString        = null;
                MemoryStream memoryStream   = new MemoryStream();
                XmlSerializer xs            = new XmlSerializer(typeof(VShieldEdgeConfig));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, new System.Text.UTF8Encoding());
                xmlTextWriter.Formatting    = Formatting.Indented;

                xs.Serialize(xmlTextWriter, pObject);

                memoryStream.Position       = 0;
                byte[] memory               = memoryStream.ToArray();
                XmlizedString               = Encoding.UTF8.GetString(memory);

                return XmlizedString;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }
    }
}
