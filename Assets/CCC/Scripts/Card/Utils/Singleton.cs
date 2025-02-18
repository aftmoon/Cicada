using UnityEngine;

// Utils 命名空间中的这两个单例模式实现为项目提供了一种方便的方式来管理全局唯一的实例。
// Singleton<T> 适用于普通类，而 SingletonBehaviour<T> 适用于 Unity 的组件类。
// 通过这种方式，可以避免重复实例化对象，同时保证全局访问的便利性
namespace Utils
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;


        public static T Reset()
        {
            _instance = new T();
            return _instance;
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();
                return _instance;
            }
        }
    }

    /// <summary>
    /// MonoBehaviour的单例模式
    /// </summary>
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Reset()
        {
            instance = (T)FindObjectOfType(typeof(T));
            return instance;
        }

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = (T)FindObjectOfType(typeof(T));
                if(instance == null)
                    instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                return instance;
            }
        }
    }
}