using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chippy : MonoBehaviour
{
    bool bRunning = false;

    public AudioSource audioRunning;
    public AudioSource audioScare;

    public GameObject light;

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

            transform.LookAt(GameObject.Find("Player").transform.position);

            if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) <= 0.5f)
            {
                bRunning = false;
                audioScare.Play();
                GameObject.Find("Player").transform.LookAt(transform.position);
                GameObject.Find("Player").GetComponent<PlayerController>().bScared = true;
                GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = true;
                transform.Find("Jump Camera").GetComponent<Camera>().enabled = true;
                light.SetActive(true);
                StartCoroutine(ScarePlayer());
            }
        }
    }

    IEnumerator ThreatenPlayer()
    {
        yield return new WaitForSeconds(Random.Range(4, 10));

        bRunning = true;

        audioRunning.Play();
    }

    IEnumerator ScarePlayer()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
