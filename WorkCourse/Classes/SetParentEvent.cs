using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    public class SetParentEventArgs<TParent, TId> : EventArgs
    {
        TParent m_Parent;
        TId m_Parent_Id;

        public SetParentEventArgs(TId ParentId)
        {
            m_Parent_Id = ParentId;
        }

        public TParent Parent
        {
            get { return m_Parent; }
            set { m_Parent = value; }
        }

        public TId Id
        {
            get { return m_Parent_Id; }
        }
    }
}
