using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10;
    public float maxSpeed = 20;

    public float upSpeed = 10;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    private Rigidbody2D marioBody;

    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;

    public GameObject GameOverScreen;
    public GameObject RestartButton;
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public float deathImpulse;
    
    [System.NonSerialized]
    public bool alive = true;
    public Transform gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        GameOverScreen.SetActive(false);
        //update animator state
        marioAnimator.SetBool("onGround",onGroundState);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if(marioBody.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if(marioBody.velocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
        marioAnimator.SetFloat("xSpeed",Mathf.Abs(marioBody.velocity.x));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            onGroundState = true;
            //update animator state
            marioAnimator.SetBool("onGround",onGroundState);
            
    }

    void FixedUpdate()
    {
        if (alive) 
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");


            if (Mathf.Abs(moveHorizontal) > 0)
            {

                Vector2 movement = new Vector2(moveHorizontal, 0);

                if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);

            }
            //stop

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                //stop
                marioBody.velocity = Vector2.zero;
            }

            if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                //update animator state
                marioAnimator.SetBool("onGround",onGroundState);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("Collided with goomba");
            marioAnimator.Play("mario-die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }
    }

    public void RestartButtonCallback(int input)
    {
        // Debug.Log("Restart");
        RestartGame();
        Time.timeScale = 1.0f;
    }

    private void RestartGame()
    {
        //reset position
        marioBody.transform.position = new Vector3(-7.523f, -6.098f, 0.0f);
        //reset face direction
        marioSprite.flipX = false;
        //reset score
        scoreText.text = "Score: 0";
        jumpOverGoomba.score = 0;
        //reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }
        //stop mario
        marioBody.velocity = Vector2.zero;

        //remove Game Over Screen
        scoreText.transform.localPosition = new Vector3(-556.49f,459,0);
        RestartButton.transform.localPosition = new Vector3(866,454,0);
        GameOverScreen.SetActive(false);
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
        gameCamera.transform.localPosition = new Vector3(1.69f,0,-10);
    }

    void GameOverScene()
    {
        Time.timeScale = 0.0f;
        GameOverScreen.SetActive(true);
        RestartButton.transform.localPosition = new Vector3(-68,-143,0.0f);
        scoreText.transform.localPosition = new Vector3(37,-20,0);
    
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpluse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }
}
