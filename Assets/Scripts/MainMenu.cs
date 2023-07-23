using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Hole 1");
    }

    public void Practice()
    {
        SceneManager.LoadScene("Practice");
    }

    public void Main()
    {
        SceneManager.LoadScene("Entrance");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
