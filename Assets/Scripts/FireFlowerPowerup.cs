
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

public class FireFlowerPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    public UnityEvent<IPowerup> powerupCollected;

    void Awake()
    {
        // GameManager.instance.gameRestart.AddListener(this.GameRestart);
    }
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.FireFlower;
        this.rigidBody.bodyType = RigidbodyType2D.Static;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().forceRenderingOff = false;

    }

    void OnCollisionEnter2D(Collision2D col)
    {   Debug.Log("im collisind iwth "+ col.ToString());
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: do something when colliding with Player

            // then destroy powerup (optional)

            // this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(PlayAudio());
            powerupCollected.Invoke(this);
            // ApplyPowerup(this);
            // powerupAudioSource.PlayOneShot(powerupAppliedAudio);
            Debug.Log("Magic fire flower Powerup");
            

        }
        else if(col.gameObject.layer != 3)// else if hitting Pipe, flip travel direction
        {
            Debug.Log("Some other collisoon "+ spawned.ToString() + goRight.ToString());
            // if (spawned)
            // {
            //     goRight = !goRight;
            //     Debug.Log("i wann go rght");
            //     this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().forceRenderingOff = false;
            //     rigidBody.AddForce(Vector2.right * 8 * (goRight ? 1 : -1), ForceMode2D.Impulse);
            //
            // }
        }
    }


    IEnumerator PlayAudio()
    {
        powerupAudioSource.PlayOneShot(powerupAppliedAudio);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().forceRenderingOff = true;
        yield return new WaitForSeconds(powerupAppliedAudio.length);
        this.transform.GetChild(0).gameObject.SetActive(false);
        

    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        Debug.Log("powerup spawned");
        
        
        // StartCoroutine(ChangeBodyType());
    }

    IEnumerator ChangeBodyType()
    {
        // this.rigidBody.bodyType = RigidbodyType2D.Dynamic;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForFixedUpdate();
        // rigidBody.AddForce(Vector2.right * 5 * (goRight ? 1 : -1), ForceMode2D.Impulse); // move to the right
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {   
        // base.ApplyPowerup(i);
        Debug.Log("applying fire flower");
        MarioStateController mario;
        bool result = i.TryGetComponent<MarioStateController>(out mario);
        if (result)
        {
            mario.SetPowerup(PowerupType.FireFlower);
            Debug.Log("fire flower powerup allied");
        }
            
        // TODO: do something with the object

    }

    public void PowerupAppearAudio()
    {
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().forceRenderingOff = false;
        powerupAudioSource.PlayOneShot(powerupAppearAudio);
    }

    void FixedUpdate()
    {
        if(spawned && this.gameObject.active && Mathf.Abs(this.rigidBody.velocity.x) <0.1f)
            rigidBody.AddForce(Vector2.right * 10 * (goRight ? 1 : -1), ForceMode2D.Impulse);

        
    }

    public void GameRestart()
    {   
        Debug.Log("fire flower restart invoked");
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.rigidBody.bodyType = RigidbodyType2D.Static;
        this.transform.GetChild(0).transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
        Debug.Log(this.transform.GetChild(0).transform.localPosition.ToString());
        Debug.Log("before  "+ this.transform.localPosition.ToString());
        this.transform.localPosition = new Vector3(0,0,0);
        Debug.Log("after  "+ this.transform.localPosition.ToString());
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().forceRenderingOff = true;
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("InitialState");
        this.spawned = false;

    }
}