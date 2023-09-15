using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        GameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            onGroundState = true;
    }

    void FixedUpdate()
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
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("Collided with goomba");
            Time.timeScale = 0.0f;
            ShowGameOverScreen();
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
        scoreText.transform.localPosition = new Vector3(-626,459,0);
        RestartButton.transform.localPosition = new Vector3(866,454,0);
        GameOverScreen.SetActive(false);
    }

    void ShowGameOverScreen()
    {
        GameOverScreen.SetActive(true);
        RestartButton.transform.localPosition = new Vector3(-68,-143,0.0f);
        scoreText.transform.localPosition = new Vector3(37,-20,0);
    
    }
}
