using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public MarioActions marioActions;
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
            Debug.Log("Jumphold was started");
        else if(context.performed)
            Debug.Log("Jumphold was pefermoed");
        else if(context.canceled)
            Debug.Log("Jumphold was cancelled");
    }

    public void onJumpAction(InputAction.CallbackContext context){
        if(context.started)
            Debug.Log("Jump was started");
        else if(context.performed)
            Debug.Log("Jump was pefermoed");
        else if(context.canceled)
            Debug.Log("Jump was cancelled");
    }

    public void onMoveAction(InputAction.CallbackContext context){
        if(context.started)
        {
            Debug.Log("move was started");
            float move = context.ReadValue<float>();
            Debug.Log($"move amount {move}");
        }
        // if(context.canceled){
        //     Debug.Log("move stopped");
        // }
    }
}
