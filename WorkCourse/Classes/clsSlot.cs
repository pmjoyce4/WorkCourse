using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    public class clsSlot
    {
        [XmlAttribute("fileName")]
        public string FileName;
        [XmlAttribute("isUsed")]
        public string IsUsed;
        [XmlAttribute("parent")]
        public int Parent;
        [XmlAttribute("name")]
        public string Name;
        [XmlIgnore]
        public clsCourse course;
    }
}
