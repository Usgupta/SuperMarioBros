using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
       new Vector3(-556.49f, 459, 0),
         new Vector3(73, -20, 0)
        };
    private Vector3[] restartButtonPosition = {
        new Vector3(866, 454, 0),
        new Vector3(-68, -143, 0.0f)
    };

    public GameObject scoreText;
    public Transform restartButton;

    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log(scoreText.GetComponent<TextMeshProUGUI>().text);
        restartButton.localPosition = restartButtonPosition[0];
    }

    public void SetScore(int score)
    {   Debug.Log("i am updating score HUD");
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.localPosition = restartButtonPosition[1];
    }
}