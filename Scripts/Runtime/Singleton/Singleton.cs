using UnityEngine;
using Object = UnityEngine.Object;

namespace MP_Patterns
{
    public class Singleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new Object();
        private static bool _isQuit;

        public static T Instance
        {
            get
            {
                if (_isQuit)
                {
                    return _instance;
                }

                lock (_lock)
                {
                    if (_instance != null)
                        return _instance;

                    _instance = FindObjectOfType<T>();

                    if (FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogError("[Singleton] Singleton more than 1...! It's wrong!");
                        return _instance;
                    }

                    if (_instance != null)
                        return _instance;

                    var newObj = new GameObject("[Singleton]" + typeof(T).Name);
                    _instance = newObj.AddComponent<T>();

                    DontDestroyOnLoad(_instance);
                    Debug.Log("[singleton] " + _instance + " created");

                    return _instance;
                }
            }
        }

        public void OnDestroy()
        {
            // _isQuit = true;
            _instance = null;   
        }
    }
    // public abstract class NewSingleton<T> : MonoBehaviour where T : MonoBehaviour
    // {
    //     private static readonly Lazy<T> _lazyInstance = new Lazy<T>(CreateSingleton);
    //
    //     public static T Instance => _lazyInstance.Value;
    //
    //     private static T CreateSingleton()
    //     {
    //         var newObject = new GameObject($"{typeof(T).Name} (singleton)");
    //         var instance = newObject.AddComponent<T>();
    //         DontDestroyOnLoad(newObject);
    //
    //         return instance;
    //     }
    // }
}