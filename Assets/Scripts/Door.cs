using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool bIsLocked = true;
    public bool bIsPractice = false;

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

    public void Open()
    {
        if (!bIsLocked)
        {
            if(sceneManage.holeNum == sceneManage.GetFinalHole())
                SceneManager.LoadScene("Finish");
            else if(sceneManage.holeNum == -1)
                SceneManager.LoadScene("Entrance");
            else
                SceneManager.LoadScene("Hole " + (sceneManage.holeNum + 1));
        }
    }

    public bool GetStatus()
    {
        return bIsLocked;
    }

    public void Unlock()
    {
        bIsLocked = false;
    }
}
