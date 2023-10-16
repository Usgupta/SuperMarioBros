using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuLoading : MonoBehaviour
{
    public GameObject highScoreText;
    public IntVariable gameScore;

    public UnityEvent gameRestart;
    // Start is called before the first frame update
    void Start()
    {
        SetHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLoadScene()
    {
        SceneManager.LoadSceneAsync("LoadingScreen",LoadSceneMode.Single);
        Debug.Log("ivoking restart");
        gameRestart.Invoke();
        // GameManager.instance.GameRestart();
    }

    public void SetHighScore()
    {
        highScoreText.GetComponent<TextMeshProUGUI>().text = "TOP -" + gameScore.previousHighestValue.ToString("D6");
    }

    public void ResetHighScore()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

        gameScore.ResetHighestValue();
        SetHighScore();
    }

    
}
