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
    private Rigidbody2D QuestionBoxBody;
    private SpriteRenderer questionBoxSprite;

    public PlayerMovement playerMovement;

    void Start()
    {

        QuestionBoxBody = GetComponent<Rigidbody2D>();
        questionBoxSprite = GetComponent<SpriteRenderer>();
        
    }

    void OnCollisionEnter2D(Collision2D col){

        Debug.Log("Collided with box");
        Vector3 collisonNormal = col.transform.position - transform.position;
        collisonNormal.Normalize();
        if(collisonNormal.y<0 & (Mathf.Abs(collisonNormal.x) <0.9f) & !boxIsStatic){
            Debug.Log("get coin");
            boxIsStatic = true;
            QuestionBoxAnimator.SetBool("isBoxStatic",boxIsStatic);
            PlayCoinSpawn();
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
        Debug.Log("should play now");
        Debug.Log(QuestionBoxCoinAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Debug.Log("arudio check ");
    }

    void MakeBoxStatic(){
        
        if(Mathf.Abs(QuestionBoxBody.velocity.y) <0.1f){
            QuestionBoxBody.bodyType = RigidbodyType2D.Static;

        }
    }
}
