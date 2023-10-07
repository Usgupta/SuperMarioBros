using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    // Start is called before the first frame update

    private static T _instance;

    public static T instance {
        get{
            return _instance;
        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Awake()
    {
        Debug.Log("Singleton Awake is called");
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

    }
}
