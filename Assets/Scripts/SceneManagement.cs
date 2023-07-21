using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    public int par;
    public int holeNum;

    private int finalHole = 3;

    Text parText; 

    // Start is called before the first frame update
    void Start()
    {
        parText = GameObject.Find("Par Text").GetComponent<Text>();

        parText.text = "Par: " + par;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetFinalHole()
    {
        return finalHole;
    }
}
