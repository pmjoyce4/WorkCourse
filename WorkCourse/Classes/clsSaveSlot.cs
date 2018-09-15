using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    public class clsSaveSlot
    {
        public clsSaveSlot() { slot = new List<clsSlot>(); }
        [XmlElement("slot")]
        public List<clsSlot> slot;
    }
}
