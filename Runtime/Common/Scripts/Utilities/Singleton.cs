/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using UnityEngine;

/* Reference: https://blogs.unity3d.com/2016/07/26/il2cpp-optimizations-devirtualization/
 * write 'sealed' keyword with each singleton inheriting classes, if they are leaf nodes,
 * so by 'sealed' keyword the overriding function calls become optimized, De-Virtualization
 */

namespace MK.Common.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        // use it for testing
        [SerializeField] private string instanceID;

        private static object _lock = new object();

        protected virtual void Start()
        {
        }

        protected virtual void Awake()
        {
            if (ReferenceEquals(_instance, null) || this.GetInstanceID() == _instance.GetInstanceID())
            {
                // if I am the first instance, make me the Singleton
                // to access Instance, so it won't create new instance of same class. Important point: Don't comment this line
                bool has = ReferenceEquals(Instance, null);

                // If I am the first instance, make me the Singleton
                DontDestroyOnLoad(this);
                instanceID = this.GetInstanceID().ToString();
                Debug.LogWarning("<color=green>[Singleton]NewInstance: " + this.gameObject.name +
                                 " - " + typeof(T) + " - " + has + "   ID: </color>" + instanceID);
            }
            else
            {
                // If a Singleton already exists and you find another reference in scene, destroy it!
                // if (!ReferenceEquals(_instance, null))
                Debug.LogError("<color=red>[Singleton]Destroying: " + this.gameObject.name + "   New InstanceID: " +
                               this.GetInstanceID() + "   ->Old Current: </color>" + _instance.GetInstanceID());
                Destroy(this.gameObject);
            }
        }

        public static bool HasInstance
        {
            get { return (!ReferenceEquals(_instance, null)); }
        }

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (ReferenceEquals(_instance, null))
                    {
                        _instance = (T) FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("<color=red>[Singleton] Something went really wrong " + typeof(T) +
                                           " - there should never be more than 1 singleton!" +
                                           " Reopenning the scene might fix it.</color>");
                            return _instance;
                        }

                        if (ReferenceEquals(_instance, null))
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(Singleton) " + typeof(T).ToString();

                            Debug.LogWarning("[Singleton] An instance of " + typeof(T) +
                                             " is needed in the scene, so '" + singleton.name +
                                             "' was created.");
                        }
                        else
                        {
                            Debug.LogWarning("<color=blue>[Singleton] " + typeof(T) +
                                             " - Using instance already created: </color>" + _instance.gameObject.name);
                        }
                    }

                    return _instance;
                }
            }
        }

        protected virtual void OnApplicationQuit()
        {
            if (!ReferenceEquals(_instance, null))
                _instance = null;
        }

        protected virtual void OnDestroy()
        {
            if (!ReferenceEquals(_instance, null))
                _instance = null;
        }
    }
}