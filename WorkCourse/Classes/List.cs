using System;

namespace WorkCourse.EventList
{
    public class ItemAddedEventArgs<T> : System.EventArgs
    {
        T m_AddedItem;

        public ItemAddedEventArgs(T Item)
        {
            m_AddedItem = Item;
        }

        public T AddedItem
        {
            get { return m_AddedItem; }
        }
    }

    public class List<T> : System.Collections.Generic.List<T>
    {
        public delegate void EventHandler<Tsender, TEventArgs>(Tsender sender, TEventArgs EventArgs) where TEventArgs : ItemAddedEventArgs<T>;

        public event EventHandler<ItemAddedEventArgs<T>> ItemAdded;

        protected virtual void OnItemAdded(ItemAddedEventArgs<T> e)
        {
            ItemAdded?.Invoke(this, e);
        }

        public new void Add(T Item)
        {
            base.Add(Item);

            OnItemAdded(new ItemAddedEventArgs<T>(Item));
        }
    }
}
