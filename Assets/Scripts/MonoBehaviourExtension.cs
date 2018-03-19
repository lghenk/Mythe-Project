
using UnityEngine;

namespace ExtensionMethods
{
        public static class MonoBehaviourExtensions
        {
                public static void GetCompNotNull<T>(this MonoBehaviour mono, MonoBehaviour m) where T : MonoBehaviour
                {
                        if (mono != null || m == null) return;
                        mono = m.GetComponent<T>();
                }
        }
}
