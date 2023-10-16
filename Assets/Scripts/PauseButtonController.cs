using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;
    public GameObject gamePausePanel;
    public GameObject backtoMain;
    public AudioSource audioSource;
    public UnityEvent gamePaused;
    public UnityEvent gameResumed;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = pauseIcon;
    }

    void Update()
    {

    }

    public void ButtonClick()
    {

        if(image.sprite == pauseIcon)
        {
            if(Time.timeScale==1.0f)
            {
                image.sprite = playIcon;
                Time.timeScale = 0.0f;
                audioSource.PlayOneShot(audioSource.clip); 
                gamePausePanel.SetActive(true);
                backtoMain.SetActive(true);
            }
            
               
        }
        else if (image.sprite == playIcon)
        {
            if(Time.timeScale==0.0f)
            {
                image.sprite = pauseIcon;
                Time.timeScale = 1.0f;
                audioSource.PlayOneShot(audioSource.clip);  
                gamePausePanel.SetActive(false);  
                backtoMain.SetActive(false);
            }  
            

        }

        // Debug.Log("Pause on clic");
        // isPaused = !isPaused;
        // Time.timeScale = isPaused ? 0.0f:1.0f;
        // if (isPaused)
        // {
        //     image.sprite = playIcon;
        // }
        // if( !isPaused && Time.timeScale==0.0f)
        // {   
        //     Debug.Log("not supposed to do anything");
        //     // image.sprite = pauseIcon;
        // }
        // if(!isPaused)
        //     Debug.Log("is pause is false");
        //     image.sprite = pauseIcon;
        
    }
}