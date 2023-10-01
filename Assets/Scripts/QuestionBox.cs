using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{   
    public AudioSource questionBoxAudio;
    public Animator QuestionBoxAnimator;
    public Animator QuestionBoxCoinAnimator;

    public bool boxIsStatic = false;
    // public AudioClip coinSpawnAudio;
    // Start is called before the first frame update
    public Rigidbody2D QuestionBoxBody;
    private SpriteRenderer questionBoxSprite;

    // public PlayerMovement playerMovement;

    void Start()
    {

        QuestionBoxBody = GetComponent<Rigidbody2D>();
        questionBoxSprite = GetComponent<SpriteRenderer>();
        
    }

    void OnCollisionEnter2D(Collision2D col){

        // Debug.Log("Collided with box");
        Vector3 collisonNormal = col.transform.position - transform.position;
        collisonNormal.Normalize();
        if(collisonNormal.y<0 & (Mathf.Abs(collisonNormal.x) <0.9f) & !boxIsStatic){
            Debug.Log("get coin");
            boxIsStatic = true;
            QuestionBoxAnimator.SetBool("isBoxStatic",boxIsStatic);
            PlayCoinSpawn();
            Debug.Log("played already");
        }
        // Debug.Log("djfd");
        // Debug.Log(collisonNormal);

        // 
    }

    // Update is called once per frame
    void Update()
    {
        // playerMovement.marioAnimator.trig
        // Debug.Log("positon");
        // Debug.Log(transform.localPosition);
    }

    void FixedUpdate(){
        // if(boxIsStatic){
            
        // }
    }

    void PlayCoinSpawn(){
        QuestionBoxCoinAnimator.Play("coin-spawn");
        
        questionBoxAudio.PlayOneShot(questionBoxAudio.clip);
        // Debug.Log("should play now");
        // Debug.Log(QuestionBoxCoinAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        // Debug.Log("arudio check ");
    }

    void MakeBoxStatic(){
        
        if(Mathf.Abs(QuestionBoxBody.velocity.y) <0.1f){
            QuestionBoxBody.bodyType = RigidbodyType2D.Static;

        }
    }

    public void GameRestart()
    {

        QuestionBoxAnimator.SetBool("isBoxStatic",false);
        boxIsStatic = false;
        QuestionBoxBody.bodyType = RigidbodyType2D.Dynamic;
        QuestionBoxCoinAnimator.StopPlayback();
        // foreach (QuestionBox prefabInstance in prefabInstances)
        // {
        //     if (prefabInstance.GetComponent<Animator>() != null)
        //     {
                
        //         // prefabInstance.QuestionBoxCoinAnimator.Play("coin-spawn");
        //         // Debug.Log("restarting arudio check");
        //         // Debug.Log(prefabInstance.QuestionBoxCoinAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //         // Debug.Log("restarting arudio check done");

        //     }
        // }
    }
}
