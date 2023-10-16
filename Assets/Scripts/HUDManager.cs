using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
       new Vector3(-556.49f, 459, 0),
         new Vector3(102, -20, 0)
        };
    private Vector3[] restartButtonPosition = {
        new Vector3(866, 454, 0),
        new Vector3(-68, -234, 0.0f)
    };

    private Vector3 highScoreTextPosition = new Vector3(51, -123, 0.0f);

    

    public GameObject scoreText;
    public Transform restartButton;

    public GameObject gameOverPanel;
    public GameObject highScoreText;
    public IntVariable gameScore;
    public GameObject BackToMain;
    // public Canvas canvas;

    void Awake()
    {
        // GameManager.instance.gameStart.AddListener(GameStart);
        // GameManager.instance.gameRestart.AddListener(GameStart);
        // GameManager.instance.gameOver.AddListener(GameOver);
        // GameManager.instance.scoreChange.AddListener(SetScore);

    }
    
    // Start is called before the first frame update
    
    
    void Start()
    {
        // SceneManager.activeSceneChanged += SetCamera;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {   
        // hide gameover panel
        gameOverPanel.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[0];
        Debug.Log("hello start hud");
        // Debug.Log(scoreText.GetComponent<TextMeshProUGUI>().text);
        restartButton.localPosition = restartButtonPosition[0];
        highScoreText.GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
        highScoreText.GetComponent<TextMeshProUGUI>().text = "Top -" + gameScore.previousHighestValue.ToString("D6");
        BackToMain.SetActive(false);
        Time.timeScale = 1.0f;
        
    }

    public void SetScore(int score)
    {   
        // Debug.Log("i am updating score HUD");
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + gameScore.Value.ToString();
    }


    public void GameOver()
    {   Debug.Log("game over hud is called");
        gameOverPanel.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.localPosition = restartButtonPosition[1];
        highScoreText.GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
        highScoreText.GetComponent<TextMeshProUGUI>().text = "Top -" + gameScore.previousHighestValue.ToString("D6");
        highScoreText.GetComponent<TextMeshProUGUI>().transform.localPosition = highScoreTextPosition;
        BackToMain.SetActive(true);

        Time.timeScale = 0.0f;
    }

    
}
