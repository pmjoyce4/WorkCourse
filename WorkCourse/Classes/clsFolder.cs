using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    public class clsFolder
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public int id;
        [XmlAttribute]
        public int parent;
    }
}
