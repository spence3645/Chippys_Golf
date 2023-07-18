using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chippy : MonoBehaviour
{
    bool bRunning = false;

    public AudioSource audioRunning;
    public AudioSource audioScare;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThreatenPlayer()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (bRunning)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, 10 * Time.deltaTime);

            if(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) <= 0.5f)
            {
                bRunning = false;
                GameObject.Find("Player").transform.LookAt(transform.position);
                audioScare.Play();
                StartCoroutine(ScarePlayer());
            }
        }
    }

    IEnumerator ThreatenPlayer()
    {
        yield return new WaitForSeconds(Random.Range(5f, 9f));

        bRunning = true;

        audioRunning.Play();
    }

    IEnumerator ScarePlayer()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
