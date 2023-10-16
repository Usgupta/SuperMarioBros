using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    void Awake()
    {
        // GameManager.instance.gameRestart.AddListener(GameRestart);
        Debug.Log("box controller awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameRestart()
    {
        QuestionBox[] prefabInstances = GameObject.FindObjectsOfType<QuestionBox>();
        Debug.Log("Game resart q box invoked");

        // Iterate over all of the prefab instances and set their entry animation state.
        foreach (QuestionBox prefabInstance in prefabInstances)
        {
            if (prefabInstance.GetComponent<Animator>() != null)
            {
                prefabInstance.GameRestart();
                // prefabInstance.QuestionBoxCoinAnimator.Play("coin-spawn");
                Debug.Log("game restart box  check");
                // Debug.Log(prefabInstance.QuestionBoxCoinAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                // Debug.Log("restarting arudio check done");
                }
        }
    }

    


}
