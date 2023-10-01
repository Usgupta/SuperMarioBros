using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public MarioActions marioActions;
    public UnityEvent jump;
    public UnityEvent jumpHold;
    public UnityEvent<int> moveCheck;
    // Start is called before the first frame update
    void Start()
    {
        marioActions = new MarioActions();
        marioActions.gameplay.Enable();
        marioActions.gameplay.jump.performed += onJumpAction;
        marioActions.gameplay.jumphold.performed += onJumpHoldAction;
        marioActions.gameplay.move.started += onMoveAction;
        marioActions.gameplay.move.canceled += onMoveAction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onJumpHoldAction(InputAction.CallbackContext context){
        if(context.started)
            {
                // Debug.Log("Jumphold was started");
            }
        else if(context.performed)
        {   
            jumpHold.Invoke();
            // Debug.Log("Jumphold was pefermoed");
        }    
            
        // else if(context.canceled)
            // Debug.Log("Jumphold was cancelled");
    }

    public void onJumpAction(InputAction.CallbackContext context){
        if(context.started)
        {
            // Debug.Log("Jump was started");
        }
        else if(context.performed)
        {
            jump.Invoke();
            Debug.Log("Jump was pefermoed");
        }
            
        // else if(context.canceled)
        //     Debug.Log("Jump was cancelled");
    }

    public void onMoveAction(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // Debug.Log("move was started");
            int faceRight = context.ReadValue<float>() >0? 1:-1;
            moveCheck.Invoke(faceRight);
            // Debug.Log($"move amount {fa}");
        }
        if(context.canceled){
            // Debug.Log("move stopped");
            moveCheck.Invoke(0);

        }
    }

    public void OnClickAction(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // Debug.Log("mouse click started");
        }    
        else if(context.performed)
        {
            Debug.Log("mouse click performed");
        }

        // else if( context.canceled)
        //     Debug.Log("mouse click cancelled");

    }

    public void OnPointAction(InputAction.CallbackContext context)
    {
       if(context.performed)
        {
            
            Vector2 point = context.ReadValue<Vector2>();
            // Debug.Log($"mouse pos {point}");
        }


    }
}
