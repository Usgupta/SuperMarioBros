using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
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

        // Iterate over all of the prefab instances and set their entry animation state.
        foreach (QuestionBox prefabInstance in prefabInstances)
        {
            if (prefabInstance.GetComponent<Animator>() != null)
            {
                prefabInstance.GameRestart();
                // prefabInstance.QuestionBoxCoinAnimator.Play("coin-spawn");
                // Debug.Log("restarting arudio check");
                // Debug.Log(prefabInstance.QuestionBoxCoinAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                // Debug.Log("restarting arudio check done");
                }
        }
    }


}
