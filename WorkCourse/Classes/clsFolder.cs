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
        /*
        private clsFolder ParentFolder;
        private bool ParentIsRoot = false;
        */

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("id")]
        public int Id;

        [XmlAttribute("parent")]
        public int ParentId;
        /*
        {
            get { return (ParentIsRoot) ? 0 : ParentFolder.Id; }
            set
            {
                if (value == 0)
                {
                    ParentIsRoot = true;
                }
                else
                {
                    SetParentEventArgs<clsFolder, int> EventArgs = new SetParentEventArgs<clsFolder, int>(value);
                    OnSetParent(EventArgs);
                    if (EventArgs.Parent != null)
                    {
                        this.ParentFolder = EventArgs.Parent;
                    }
                }
            }
        }

        public delegate void EventHandler<Tsender, TEventArgs>(Tsender sender, TEventArgs EventArgs) where TEventArgs : SetParentEventArgs<clsFolder, int>;

        public event EventHandler<SetParentEventArgs<clsFolder, int>> SetParent;

        protected virtual void OnSetParent(SetParentEventArgs<clsFolder, int> e)
        {
            SetParent?.Invoke(this, e);
        }
        */
    }
}
