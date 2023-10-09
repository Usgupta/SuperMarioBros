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
    }

    void Update()
    {

    }

    public void ButtonClick()
    {
        Debug.Log("Pause on clic");
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        isPaused = !isPaused;
        if (isPaused)
        {
            image.sprite = pauseIcon;
        }
        else
            image.sprite = playIcon;
    }
}