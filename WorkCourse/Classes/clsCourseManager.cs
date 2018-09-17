using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    [XmlRoot("courseManager")]
    public class clsCourseManager
    {
        [XmlIgnore]
        public string Path;
        [XmlElement("saveSlot")]
        public clsSaveSlot saves;
        [XmlElement("folders")]
        public clsFolders folders;

        public void LoadCourses()
        {
            if (saves == null)
                return;
            
            foreach (clsSlot CurrentSlot in saves.slot)
            {
                if (CurrentSlot.isUsed.ToLower() != "true")
                    continue;
                clsCourse CurrentCourse = new clsCourse();
                CurrentCourse = clsXmlSaveLoad.DeserializeXmlFromFile<clsCourse>(Path + System.IO.Path.DirectorySeparatorChar + CurrentSlot.fileName);
                CurrentSlot.course = CurrentCourse;
            }
        }

        private void SaveCourses()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
