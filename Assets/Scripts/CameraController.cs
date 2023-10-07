using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; //Mario's Tranform
    public Transform endLimit; //Game Object that denotes end of the map
    private float offset; //Initial x-offset between camera and Mario
    private float startX; //smallest x-coordinate of the camera
    private float endX; //largest x-coordinate of the camera
    private float viewportHalfWidth;
    // Start is called before the first frame update



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);
        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;
        // endLimit = GameObject.FindGameObjectWithTag("Limit").transform;
        // SceneManager.activeSceneChanged += SetSceneEndLimit;
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x + offset;
        Debug.Log("new position is " + desiredX.ToString());
        Debug.Log("start is "+ startX.ToString());                
        Debug.Log("end is "+ endX.ToString());


        if (desiredX > startX && desiredX < endX)

            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        
    }

    // public void SetSceneEndLimit(Scene current, Scene next)
    // {
    //     if (next.name == "World-1-2")
    //     {
    //         endLimit = GameObject.FindGameObjectWithTag("Limit").transform;
    //         this.Update();
    //     }
    // }
}
