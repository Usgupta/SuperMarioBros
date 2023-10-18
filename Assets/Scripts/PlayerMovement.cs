using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IPowerupApplicable
{

    public GameConstants gameConstants;
    public float speed;
    public float maxSpeed;

    public float upSpeed;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public BoolVariable marioFaceRight;

    private Rigidbody2D marioBody;

    // public TextMeshProUGUI scoreText;
    // public GameObject enemies;
    // public JumpOverGoomba jumpOverGoomba;

    // public QuestionBox questionBox;


    // public GameObject GameOverScreen;
    // public GameObject RestartButton;
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioSource marioDeath;
    public float deathImpulse;

    [System.NonSerialized]
    public bool alive = true;
    public Transform gameCamera;

    private bool moving = false;
    private bool jumpedState = false;

    // public GameManager gameManager;
    public UnityEvent gameOver;
    public UnityEvent playerDamaged;

    // Start is called before the first frame update
    void Awake()
    {
        // GameManager.instance.gameRestart.AddListener(RestartGame);
    }

    void Start()
    {
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        upSpeed = gameConstants.upSpeed;
        deathImpulse = gameConstants.deathImpulse;

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        // GameOverScreen.SetActive(false);
        //update animator state
        marioAnimator.SetBool("onGround", onGroundState);
        marioBody.gameObject.layer = 0;
        // SceneManager.activeSceneChanged += SetStartingPosition;

    }

    // Update is called once per frame

    
    void Update()
    {
        // if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && faceRightState)
        // {
        //     faceRightState = false;
        //     marioSprite.flipX = true;
        //     if (marioBody.velocity.x > 0.1f)
        //         marioAnimator.SetTrigger("onSkid");
        // }

        // if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !faceRightState)
        // {
        //     faceRightState = true;
        //     marioSprite.flipX = false;
        //     if (marioBody.velocity.x < -0.1f)
        //         marioAnimator.SetTrigger("onSkid");
        // }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    int collisionLayerMask = ((1 << 3) | (1 << 6) | (1 << 7) | (1 << 9));

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
            onGroundState = true;
        //update animator state

        marioAnimator.SetBool("onGround", onGroundState);

    }

    void FixedUpdate()
    {   
        if (alive && moving){
            Move(faceRightState ==true? 1:-1);
        }
        if (!alive)
        {
            marioBody.gameObject.layer = 3;
        }
        else{
            marioBody.gameObject.layer = 0;
        }
        //     float moveHorizontal = Input.GetAxisRaw("Horizontal");


        //     if (Mathf.Abs(moveHorizontal) > 0)
        //     {

        //         Vector2 movement = new Vector2(moveHorizontal, 0);

        //         if (marioBody.velocity.magnitude < maxSpeed)
        //             marioBody.AddForce(movement * speed);

        //     }
        //     //stop

        //     if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        //     {
        //         //stop
        //         marioBody.velocity = Vector2.zero;
        //     }
        //     // Debug.Log("fix update");
        //     // Debug.Log(onGroundState);
        //     // Debug.Log("call func");
        //     if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
        //     {   
        //         marioBody.velocity.Set(marioBody.velocity.x,0);
        //         marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        //         onGroundState = false;
        //         // Debug.Log(onGroundState);
        //         //update animator state
        //         marioAnimator.SetBool("onGround", onGroundState);
        //     }
        // }

    }

    void FlipMarioSprite(int value)
    {
        if( value == -1 && faceRightState)
        {
            // faceRightState = false;
            updateMarioShouldFaceRight(false);
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");

        } 
        else if (value == 1 && !faceRightState)
        {
            updateMarioShouldFaceRight(true);
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    public void MoveCheck(int value)
    {
        if(value == 0)
            moving = false;
        else 
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);

        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void Jump(){
        if(alive && onGroundState){
            jumpedState = true;
            onGroundState = false;
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    public void Jumphold(){
        if(alive && jumpedState){
            //jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 50, ForceMode2D.Force);
            jumpedState = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with goomba");
            if(marioBody.velocity.y <-0.5f)
                {
                    Debug.Log("stomping");
                }
            else
            {
                
                if (this.gameObject.GetComponent<BuffStateController>().currentPowerupType == PowerupType.StarMan)
                {
                    Debug.Log("not supposed to do anything");
                    StartCoroutine(other.gameObject.GetComponent<EnemyMovement>().FlipGoomba());
                }
                else
                {
                    Debug.Log("right now its this "+this.gameObject.GetComponent<BuffStateController>().currentPowerupType.ToString());
                    playerDamaged.Invoke();
                }
                // MarioDie();
            }
        }

        if(other.isTrigger && other.gameObject.name == "Pit-Limit")
        {
            Debug.Log("fallen into pit");
            playerDamaged.Invoke();
            // MarioDie();
        }
    }

    private void MarioDie()
    {
        // marioAnimator.Play("mario-die");
        // alive = false;
        DamageMario();
    }

    // public void RestartButtonCallback(int input)
    // {
    //     // Debug.Log("Restart");
    //     RestartGame();
    //     Time.timeScale = 1.0f;
    // }

    public void RestartGame()
    {
        //reset position
        marioBody.transform.position = new Vector3(-7.523f, -6.098f, 0.0f);
        //reset face direction
        marioSprite.flipX = false;
        faceRightState = true;
        //reset score
        // scoreText.text = "Score: 0";
        // jumpOverGoomba.score = 0;
   
        //stop mario
        marioBody.velocity = Vector2.zero;

        //remove Game Over Screen
        // scoreText.transform.localPosition = new Vector3(-556.49f, 459, 0);
        // RestartButton.transform.localPosition = new Vector3(866, 454, 0);
        // GameOverScreen.SetActive(false);
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
        gameCamera.transform.localPosition = new Vector3(1.69f, 0, -10);
        marioBody.gameObject.layer = 1;
        // GoToEntryAnimationStateForAllPrefabInstances();
    }

    void GameOverScene()
    {
        Debug.Log("you are dying");
        gameOver.Invoke();
        // GameManager.instance.GameOver();
        // Time.timeScale = 0.0f;
        // GameOverScreen.SetActive(true);
        // RestartButton.transform.localPosition = new Vector3(-68, -143, 0.0f);
        // scoreText.transform.localPosition = new Vector3(37, -20, 0);

    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpluse()
    {
        marioBody.velocity = Vector2.zero;
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
        marioDeath.PlayOneShot(marioDeath.clip);
    }

    // public void GoToEntryAnimationStateForAllPrefabInstances()
    // {
    //     // Get all of the prefab instances in the current scene.
    //     QuestionBox[] prefabInstances = GameObject.FindObjectsOfType<QuestionBox>();

    //     // Iterate over all of the prefab instances and set their entry animation state.
    //     foreach (QuestionBox prefabInstance in prefabInstances)
    //     {
    //         if (prefabInstance.GetComponent<Animator>() != null)
    //         {
    //             prefabInstance.QuestionBoxAnimator.SetBool("isBoxStatic",false);
    //             prefabInstance.boxIsStatic = false;
    //             prefabInstance.QuestionBoxBody.bodyType = RigidbodyType2D.Dynamic;
    //             prefabInstance.QuestionBoxCoinAnimator.StopPlayback();
    //             // prefabInstance.QuestionBoxCoinAnimator.Play("coin-spawn");
    //             // Debug.Log("restarting arudio check");
    //             // Debug.Log(prefabInstance.QuestionBoxCoinAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    //             // Debug.Log("restarting arudio check done");

    //         }
    //     }
    // }

    public void SetStartingPosition(Scene current, Scene next)
    {
        if(next.name == "World-1-2")
            marioBody.transform.position = new Vector3(-11.72f, -6.18f, 0);
    }

    public void DamageMario()
    {
        if (this.gameObject.GetComponent<BuffStateController>().currentPowerupType != PowerupType.StarMan)
            // Debug.Log("dont do anythingggg stopp");
            GetComponent<MarioStateController>().SetPowerup(PowerupType.Damage);
    }

    private void updateMarioShouldFaceRight(bool value)
    {
        faceRightState = value;
        marioFaceRight.SetValue(value);
    }

    public void RequestPowerupEffect(IPowerup i)
    {
        Debug.Log("mario request is called "+ i.ToString());
        i.ApplyPowerup(this);
        // throw new System.NotImplementedException();
    }
}
