using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    Rigidbody rb;

    public GameObject arrow;

    public bool bScored;

    public AudioSource winSound;
    public AudioSource puttSound;

    Door exitDoor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrow = transform.Find("Arrow").gameObject;

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
        winSound.Play();
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
