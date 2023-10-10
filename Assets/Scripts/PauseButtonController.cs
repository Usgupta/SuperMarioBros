using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;

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
            }    
        }
        else if (image.sprite == playIcon)
        {
            if(Time.timeScale==0.0f)
            {
                image.sprite = pauseIcon;
                Time.timeScale = 1.0f;
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