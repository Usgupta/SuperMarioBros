using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    // public UnityEvent updateScore;

    private int score = 0;
    public IntVariable gameScore;

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
        Debug.Log("restarting game bro");
        gameScore.SetValue(0);
        SetScore(gameScore.Value);
        Debug.Log("invoking restart");
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {   
        Debug.Log("inc score gamemanager");
        Debug.Log("before was "+ gameScore.Value.ToString());
        gameScore.ApplyChange(1);
        Debug.Log("after was "+ gameScore.Value.ToString());
        SetScore(gameScore.Value);
    }

    public void SetScore(int score)
    {   
        // Debug.Log("set score game manager");
        scoreChange.Invoke(gameScore.Value);
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(gameScore.Value);
        
    }

    public void RequestPowerupEffect()
    {
        
    }

    public void FilterAndCastPowerup()
    {
        
    }

    public void PauseGame()
    {
        
    }

    public void ResumeGame()
    {
        
    }
}