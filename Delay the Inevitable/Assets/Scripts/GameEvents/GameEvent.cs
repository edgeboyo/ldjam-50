using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvents/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        [TextArea]
        [SerializeField] private string description;
		
        protected readonly Dictionary<object, List<Action>> listenerActions = new Dictionary<object, List<Action>>();

        public List<object> Listeners => listenerActions.Keys.ToList();

        public void Raise()
        {
            foreach (var listener in listenerActions.ToList())
            {
                foreach (var action in listener.Value)
                {
                    action?.Invoke();
                }
            }
        }

        public virtual void RaiseEmpty()
        {
            Raise();
        }
		
        public void RegisterListener(object listener, Action action)
        {
            if (listenerActions.ContainsKey(listener))
            {
                listenerActions[listener].Add(action);
            }
            else
            {
                listenerActions.Add(listener, new List<Action>() {action});
            }
        }

        public virtual void UnregisterListener(object listener)
        {
            listenerActions.Remove(listener);
        }
        
        private void ClearListeners()
        {
            listenerActions.Clear();
        }
    }
}