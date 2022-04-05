using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static bool TryGetComponentRecursive<T>(this Transform parent, out T found)
        {
            if (parent.TryGetComponent(out T component))
            {
                found = component;
                return true;
            }
            
            foreach (Transform child in parent)
            {
                if (TryGetComponentRecursive(child, out found))
                {
                    return true;
                }
            }

            found = default;
            return false;
        }
        
        public static bool TryGetChildRecursive<T>(this Transform parent, out T found)
        {
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent(out T component))
                {
                    found = component;
                    return true;
                }
                
                if (TryGetChildRecursive(child, out found))
                {
                    return true;
                }
            }

            found = default;
            return false;
        }
    }
}