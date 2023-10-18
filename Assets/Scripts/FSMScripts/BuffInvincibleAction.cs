using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/BuffInvincibility")]
public class BuffInvincibleAction : Action
{
    public AudioClip invincibilityStart;
    public override void Act(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        m.gameObject.GetComponent<AudioSource>().PlayOneShot(invincibilityStart);
        m.StartCoroutine(PlayActionAudio(m));
        
        m.SetRendererToFlicker();
    }

    private IEnumerator PlayActionAudio(StateController stateController)
    {
        stateController.gameObject.GetComponent<AudioSource>().PlayOneShot(invincibilityStart);
        yield return new WaitForSecondsRealtime(10);
        stateController.gameObject.GetComponent<AudioSource>().Stop();
    }
}