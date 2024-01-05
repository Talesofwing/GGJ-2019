using UnityEngine;


public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {

    #region Cache GameObject & Transform

        private GameObject _cacheGameObject;
        public GameObject CacheGameObject {

            get {

                if (null == _cacheGameObject) {
                    _cacheGameObject = this.gameObject;
                }

                return _cacheGameObject;
            }

        }


        private Transform _cacheTransform;
        public Transform CacheTransform {

            get {

                if (null == _cacheTransform) {
                    _cacheTransform = this.GetComponent <Transform> ();
                }

                return _cacheTransform;
            }

        }

    #endregion

    private static T _instance;
    public static T Instance { get { return GetInstance (); } }

    public static T GetInstance () {
        if (_instance == null) {
            _instance = FindObjectOfType <T> ();
            if (FindObjectsOfType (typeof(T)).Length > 1) {
                Debug.LogError ("場景中有多於一個的單例, Type: " + typeof(T));
                return _instance;
            }
            if (_instance == null) {
                GameObject singleton = new GameObject (typeof(T).ToString ());
                _instance = singleton.AddComponent<T> ();
            } 
        }
        return _instance;
    }

}