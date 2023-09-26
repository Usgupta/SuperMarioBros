using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{   
    public AudioSource questionBoxAudio;
    public Animator QuestionBoxAnimator;
    public Animator QuestionBoxCoinAnimator;
    // public AudioClip coinSpawnAudio;
    // Start is called before the first frame update
    private Rigidbody2D QuestionBoxBody;
    private SpriteRenderer questionBoxSprite;

    void Start()
    {

        QuestionBoxBody = GetComponent<Rigidbody2D>();
        questionBoxSprite = GetComponent<SpriteRenderer>();
        
    }

    void OnCollisionEnter2D(Collision2D col){

        Debug.Log("Collided with box");
        Vector3 collisonNormal = col.transform.position - transform.position;
        collisonNormal.Normalize();
        if(collisonNormal.y<0){
            Debug.Log("get coin");
            questionBoxAudio.PlayOneShot(questionBoxAudio.clip);
            QuestionBoxAnimator.SetTrigger("static-mode");
            QuestionBoxCoinAnimator.Play("coin-spawn");
        }
        // Debug.Log(collisonNormal);

        // 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
