using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    Rigidbody rb;

    public GameObject arrow;

    PlayerController controller;

    public bool bScored;

    public AudioSource winSound;
    public AudioSource puttSound;

    Door exitDoor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrow = transform.Find("Arrow").gameObject;

        controller = GameObject.Find("Player").GetComponent<PlayerController>();

        exitDoor = GameObject.Find("Door").GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Hit(float power)
    {
        rb.AddForce(transform.forward * power);
        puttSound.Play();
    }

    public void Scored()
    {
        exitDoor.Unlock();
        controller.audioHeartbeat.Stop();
        winSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Room")
        {
            controller.Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Hole")
        {
            bScored = true;
            Scored();
        }
    }
}
