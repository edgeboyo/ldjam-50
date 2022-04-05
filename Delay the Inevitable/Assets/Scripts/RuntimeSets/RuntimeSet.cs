using System;
using System.Collections.Generic;
using System.Linq;

namespace RuntimeSets
{
    public class RuntimeSet<T>
    {
        protected List<T> m_items = new List<T>();
		
        public virtual List<T> Items => m_items;
        
        public event Action<T> ItemAdded;
        public event Action<T> ItemRemoved;

        public virtual void Add(T item)
        {
            if (!Items.Contains(item))
            {
                Items.Add(item);

                ItemAdded?.Invoke(item);
            }
        }
        
        public virtual void Remove(T item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);

                ItemRemoved?.Invoke(item);
            }
        }
        
        public void Clear()
        {
            Items.Clear();
        }

        public List<TDerived> GetOfType<TDerived>() where TDerived : T
        {
            return Items.OfType<TDerived>().ToList();
        }
    }
}