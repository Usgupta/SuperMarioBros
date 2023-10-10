using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
    public CanvasGroup c;

    void Start()
    {
        // StartCoroutine(Fade());
        // StartCoroutine(FadeUndo());

    }
    IEnumerator Fade()
    {
        for (float alpha = 1f; alpha >= -0.05f; alpha -= 0.05f)
        {
            c.alpha = alpha;
            Debug.Log("fading "+ alpha.ToString());
            yield return new WaitForSecondsRealtime(0.1f);
        }

        // once done, go to next scene
       
    }

    IEnumerator FadeUndo()
    {
        for (float alpha = 0.1f; alpha <= 0.95f; alpha += 0.05f)
        {
            c.alpha = alpha;
            Debug.Log("undo fading "+ alpha.ToString());
            yield return new WaitForSecondsRealtime(0.1f);
        }
        // once done, go to next scene
       
    }


    public void BackToMain()
    {
        // TODO
        Debug.Log("Return to main menu");
        SceneManager.LoadSceneAsync("MainMenu",LoadSceneMode.Single);
    }
}
