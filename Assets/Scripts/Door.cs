using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    bool bIsLocked = true;

    SceneManagement sceneManage;

    // Start is called before the first frame update
    void Start()
    {
        sceneManage = GameObject.Find("Scene").GetComponent<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Open()
    {
        if (!bIsLocked)
        {
            if(sceneManage.holeNum == sceneManage.GetFinalHole())
                SceneManager.LoadScene("Finish");
            else
                SceneManager.LoadScene("Hole " + (sceneManage.holeNum + 1));

            return true;
        }
        else
            return false;
    }

    public void Unlock()
    {
        bIsLocked = false;
    }
}
