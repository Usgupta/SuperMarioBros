using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup
    // public UnityEvent gameRestart;

    void Awake()
    {
        // GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("some collision");
        if (other.gameObject.tag == "Player" && !powerup.hasSpawned)
        {
            // show disabled sprite
            this.GetComponent<Animator>().SetBool("isBoxStatic",true);
            // spawn the powerup
            powerupAnimator.SetTrigger("spawned");

        }
    }

    // used by animator
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void GameRestart()
    {
        this.GetComponent<Animator>().SetBool("isBoxStatic",false);

    }



}