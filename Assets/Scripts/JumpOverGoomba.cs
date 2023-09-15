using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class JumpOverGoomba : MonoBehaviour
{

    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    private bool onGroundState;

    [System.NonSerialized]
    public int score = 0;

    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //mario jumps
        // Debug.Log(onGroundCheck());
        if (Input.GetKeyDown(KeyCode.Space) && onGroundCheck())
        {
            onGroundState = false;
            countScoreState = true;
        }

        //when mario jumps, it is over Goomba and we have not updated the score
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                scoreText.text = "Score: " + score.ToString();
                // Debug.Log(score);
            }
        }
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            // Debug.Log("on ground");
            return true;
        }
        else
        {
            // Debug.Log("not on ground");
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
}
