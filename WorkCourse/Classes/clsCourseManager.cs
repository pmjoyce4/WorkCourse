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
            bool tempFlag = true;
            if (saves == null)
                return;
            
            foreach (clsSlot CurrentSlot in saves.slot)
            {
                if (CurrentSlot.IsUsed.ToLower() != "true")
                    continue;
                clsCourse CurrentCourse = new clsCourse();
                CurrentCourse = clsXmlSaveLoad.DeserializeXmlFromFile<clsCourse>(Path + System.IO.Path.DirectorySeparatorChar + CurrentSlot.FileName);
                CurrentSlot.course = CurrentCourse;
                if (tempFlag)
                {
                    CurrentSlot.course.IsDisplayed = true;
                    //tempFlag = false;
                }
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
