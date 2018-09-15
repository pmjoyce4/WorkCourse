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
        [XmlAttribute]
        public string fileName;
        [XmlAttribute]
        public string isUsed;
        [XmlAttribute]
        public int parent;
        [XmlAttribute]
        public string name;
        [XmlIgnore]
        public clsCourse course;
    }
}
