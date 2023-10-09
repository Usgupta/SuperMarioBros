using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;

    private int score = 0;

    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        //subsribe to scene manager to set up when scene changes
        SceneManager.activeSceneChanged += SceneSetup;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        // reset score
        score = 0;
        SetScore(score);
        Debug.Log("invoking restart");
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {   
        // Debug.Log("inc score gamemanager");
        score += increment;
        SetScore(score);
    }

    public void SetScore(int score)
    {   
        // Debug.Log("set score game manager");
        scoreChange.Invoke(score);
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(score);
        
    }
}