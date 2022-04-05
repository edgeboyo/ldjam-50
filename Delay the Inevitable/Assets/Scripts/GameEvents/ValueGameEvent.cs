using System;
using UnityEngine;

namespace GameEvents
{
    public abstract class ValueGameEvent : GameEvent
    {
        public abstract void Clear();
    }
	
    public abstract class ValueGameEvent<T> : ValueGameEvent
    {
        [SerializeField] 
        protected T m_value;
		
        public T value => m_value;

        public virtual bool HasValue => m_value != null;

        public virtual void SetValue(T newValue)
        {
            m_value = newValue;
        }
		
        public virtual void Raise(T newValue)
        {
            SetValue(newValue);
            Raise();
        }

        public override void RaiseEmpty()
        {
            SetValue(default);
			
            Raise();
        }
        
        public override void Clear()
        {
            m_value = default;
        }
		
        public void RegisterListener(object listener, Action<T> action)
        {
            RegisterListener(listener, () => action(value));
        }
    }
}