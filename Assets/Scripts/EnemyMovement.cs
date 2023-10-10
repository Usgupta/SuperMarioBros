using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new Vector3(0.0f,0.0f,0.0f);

    public Animator EnemyAnimator;
    
    public AudioSource EnemyAudio;

    private bool enemyDestroyed = false;
    private Transform enemyParentTransform;


    void Start()
    {
        enemyBody = gameObject.GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = gameObject.transform.localPosition.x;
        ComputeVelocity();
        EnemyAnimator.SetBool("goombaAlive",true);

    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba
            Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    public void GameRestart()
    {      //reset Goomba
        if(!gameObject.active)
        {
            gameObject.SetActive(true);
        }
        // if(enemyDestroyed)
        //     CreateEnemy();
        // Debug.Log(enemyDestroyed.ToString());
        // Debug.Log("value before");
        // Debug.Log(gameObject.transform.localPosition);
        // Debug.Log(gameObject.transform.position);
        // gameObject.transform.parent = enemyParentTransform;
        gameObject.transform.localPosition = new Vector3(originalX,0.0f,0.0f);
        moveRight = -1;
        originalX = gameObject.transform.localPosition.x;
        ComputeVelocity();
        EnemyAnimator.SetBool("goombaAlive",true);


    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Mario"){
            if(collider.attachedRigidbody.velocity.y <-0.5f)
            {
                Debug.Log("stomp from other script");
                EnemyAnimator.SetBool("goombaAlive",false);
                GameManager.instance.IncreaseScore(1);
                EnemyAudio.PlayOneShot(EnemyAudio.clip);
            }
        }

        
        // else if(collider.gameObject.layer == 6)
        // {
        //     Debug.Log("trying to move right");
            
        //     moveRight *= -1;
        //     ComputeVelocity();
        //     Movegoomba();
        // }
        // Debug.Log(collider.gameObject.layer.ToString());

    }

    public void DestroyEnemy()
    {   
        gameObject.SetActive(false);
        // enemyParentTransform = gameObject.transform.parent;
        // // Destroy(this.gameObject);
        // this.enemyDestroyed = true;
        // Debug.Log("destroyed");
        // Debug.Log(enemyDestroyed.ToString());
    }

    private void CreateEnemy()
    {
        gameObject.SetActive(true);
        this.enemyDestroyed = false;
        Instantiate(gameObject,enemyParentTransform, false);
        Debug.Log("created");
    } 
}