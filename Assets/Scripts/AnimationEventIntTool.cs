using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorIntTool : MonoBehaviour
{
    public int parameter;
    public UnityEvent<int> useInt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerIntEvent()
    {
        Debug.Log("Triggeringggg");
        useInt.Invoke(parameter);
    }
}
