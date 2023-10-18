using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffStateController : StateController
{
    public PowerupType currentPowerupType = PowerupType.Default;
    
    public BuffState shouldBeNextState = BuffState.NoStateMario;
    private SpriteRenderer spriteRenderer;
    public GameConstants gameConstants;


    public override void Start()
    {   Debug.Log("mario state controller started");
        base.Start();
        GameRestart(); // clear powerup in the beginning, go to start state
    }

    // this should be added to the GameRestart EventListener as callback
    public void GameRestart()
    {
        // clear powerup
        currentPowerupType = PowerupType.Default;
        // set the start state
        TransitionToState(startState);
    }

    public void SetPowerup(PowerupType i)
    {
        currentPowerupType = i;
        Debug.Log("powerup is now "+ currentPowerupType.ToString());
        
    }
    
    public void SetRendererToFlicker()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkSpriteRenderer());
    }
    private IEnumerator BlinkSpriteRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        while (string.Equals(currentState.name, "InvincibleMario", StringComparison.OrdinalIgnoreCase))
        {
            // Toggle the visibility of the sprite renderer
            Debug.Log("i am supposed to blink "+ spriteRenderer.enabled.ToString());
            spriteRenderer.enabled = !spriteRenderer.enabled;
            Debug.Log("i am supposed to blink after "+ spriteRenderer.enabled.ToString());


            // Wait for the specified blink interval
            yield return new WaitForSeconds(gameConstants.flickerInterval);
        }

        spriteRenderer.enabled = true;
    }

    public void GoombaFlipKill()
    {
        Debug.Log("calling fire action from mario state controlelr");
        this.currentState.DoEventTriggeredActions(this,ActionType.Attack);
    }

}