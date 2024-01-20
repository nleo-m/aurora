using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Component
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("Controller");
                    _instance = gameObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance == null ) {
            _instance = this as T;
        } else
        {
            Destroy(gameObject);
        }
    }
}
