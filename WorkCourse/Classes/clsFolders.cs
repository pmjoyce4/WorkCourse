using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    public class clsFolders
    {
        public clsFolders()
        {
            folders = new EventList.List<clsFolder>();
        }

        [XmlElement("saveSlot")]
        public EventList.List<clsFolder> folders;
    }
}
