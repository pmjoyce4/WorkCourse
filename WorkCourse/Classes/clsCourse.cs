using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace WorkCourse
{
    [XmlRoot("course")]
    public class clsCourse : IXmlSerializable
    {
        private bool numHeadlandLanesValid;
        private int numHeadlandLanesInt;
        private bool workWidthDoubleValid;
        private double workWidthDouble;
        private string headlandDirectionCWString;
        public clsCourse() { waypoint = new List<clsWaypoint>(); }
        public List<clsWaypoint> waypoint;

        [XmlAttribute]
        public string workWidth
        {
            get
            {
                if (workWidthDoubleValid)
                    return string.Format("{0:0.000000}", workWidthDouble);
                return null;
            }
            set {
                if (value != null)
                {
                    workWidthDouble = Convert.ToDouble(value);
                    workWidthDoubleValid = true;
                }
                else
                {
                    workWidthDoubleValid = false;
                }
            }
        }
        [XmlAttribute]
        public string numHeadlandLanes { get; set; }
        [XmlAttribute]
        public string headlandDirectionCW { get; set; }

        #region XML_Stuff
        // Get the messy XML stuff out of the way. This is only necessary because CoursePlay records waypoints with an
        // index integer the ElementName, e.g. "<waypoint1... <waypoint2... etc. Way to break XML guys.
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            do
            {
                if (reader.Name.Contains("course"))
                {
                    if (reader.HasAttributes)
                    {
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            // Take care of this courses properties via reflection.
                            reader.MoveToNextAttribute();
                            PropertyInfo prop = this.GetType().GetProperty(reader.Name, BindingFlags.Public | BindingFlags.Instance);
                            if (prop != null && prop.CanWrite)
                            {
                                prop.SetValue(this, reader.Value, null);
                            }
                        }
                    }
                }
                else if (reader.Name.Contains("waypoint"))
                {
                    // Remove Index from Element tag then use normal XmlSerialization to convert to waypoint
                    string Xml = reader.ReadOuterXml();
                    string ModXml = Xml.Substring(0, "<waypoint".Length) + Xml.Substring(Xml.IndexOf(" "), Xml.Length - Xml.IndexOf(" "));
                    clsWaypoint NewWaypoint = clsXmlSaveLoad.DeserializeXmlFromString<clsWaypoint>(ModXml);
                    waypoint.Add(NewWaypoint);
                }

                reader.Read();
                
            } while (!reader.EOF);
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                foreach ( XmlAttributeAttribute attrib in prop.GetCustomAttributes(typeof(XmlAttributeAttribute),false))
                {
                    // If we get here at all, our property must be decorated with the XmlAttribute Attribute... Serialize it.
                    if (prop.GetValue(this, null) != null)
                        writer.WriteAttributeString(prop.Name, prop.GetValue(this, null).ToString());
                }
            }
            //TODO: Add course data handler....
            int Index = 1;
            foreach (clsWaypoint CurrentWaypoint in waypoint)
            {
                string ModString = clsXmlSaveLoad.SerializeXmlToString<clsWaypoint>(CurrentWaypoint);
                
                writer.WriteRaw(ModString.Insert(ModString.IndexOf("waypoint") + "waypoint".Length, Index.ToString()));
                Index += 1;
            }
        }
        #endregion
    }
}
