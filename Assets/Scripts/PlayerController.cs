using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameObject golfBall;

    public AudioSource audioReverb;
    public AudioSource audioWalking;

    public GameObject chippy;

    Door exitDoor;

    Rigidbody rb;

    Camera walkingCamera;
    Camera puttingCamera;

    public Image chargeMeter;

    SceneManagement sceneManage;

    bool bPutting;
    bool bCountingUp;

    int speed = 2;
    int hitCount = 0;

    float shotPower = 50f;
    float maxPower = 750f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        walkingCamera = transform.Find("Walking Camera").GetComponent<Camera>();
        puttingCamera = transform.Find("Putting Camera").GetComponent<Camera>();

        exitDoor = GameObject.Find("Door").GetComponent<Door>();

        sceneManage = GameObject.Find("Scene").GetComponent<SceneManagement>();

        puttingCamera.enabled = false;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bPutting)
        {
            Move();
            Look();
            Interact();
        }
        else
        {
            Aim();
            Putt();
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (x == 0 && z == 0)
        {
            rb.velocity = Vector3.zero;
            audioWalking.enabled = false;
        }
        else
        {
            rb.velocity = (transform.right * x + transform.forward * z) * speed;
            audioWalking.enabled = true;
        }
    }

    void Look()
    {
        float pitch = Input.GetAxisRaw("Mouse X");
        float yaw = Input.GetAxisRaw("Mouse Y");

        if (pitch != 0)
        {
            transform.eulerAngles = transform.eulerAngles - new Vector3(0, pitch * -1, 0);
        }

        if (yaw != 0)
        {
            walkingCamera.transform.eulerAngles = walkingCamera.transform.eulerAngles - new Vector3(yaw, 0, 0);
        }
    }

    void Interact()
    {
        Ray ray = walkingCamera.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 2);

        if (Physics.Raycast(ray, out hit, 2))
        {
            if(hit.transform.tag == "Ball" && Input.GetKeyDown(KeyCode.E))
            {
                transform.parent = hit.transform;
                golfBall = hit.transform.gameObject;
                golfBall.GetComponent<GolfBall>().arrow.SetActive(true);
                transform.position = hit.transform.position - (hit.transform.right * 0.5f);

                bPutting = true;

                puttingCamera.enabled = true;

                transform.LookAt(hit.transform.position);
                walkingCamera.transform.LookAt(hit.transform.position);
            }
            else if(hit.transform.tag == "Door" && Input.GetKeyDown(KeyCode.E))
            {
                var b = exitDoor.Open();

                if (b)
                {
                    print("Open");
                }
                else
                {
                    print("Locked");
                }
            }
        }
    }

    void Aim()
    {
        if (Input.GetKey(KeyCode.A))
        {
            golfBall.transform.Rotate(Vector3.up * -60 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            golfBall.transform.Rotate(Vector3.up * 60 * Time.deltaTime);
        }
    }

    void Putt()
    {
        if (Input.GetMouseButton(0))
        {
            if (bCountingUp)
            {
                shotPower += 500 * Time.deltaTime;

                chargeMeter.fillAmount = shotPower / maxPower;

                if (shotPower >= maxPower)
                {
                    bCountingUp = false;
                }
            }
            else
            {
                shotPower -= 500 * Time.deltaTime;

                chargeMeter.fillAmount = shotPower / maxPower;

                if (shotPower <= 50f)
                {
                    bCountingUp = true;
                }
            }
            
        }

        else if (Input.GetMouseButtonUp(0))
        {
            transform.parent = null;
            golfBall.GetComponent<GolfBall>().Hit(shotPower);
            golfBall.GetComponent<GolfBall>().arrow.SetActive(false);
            bPutting = false;

            puttingCamera.enabled = false;

            shotPower = 50f;

            hitCount += 1;

            if (hitCount > sceneManage.par)
            {
                audioReverb.Play();
                GameObject.Find("Lights").SetActive(false);

                Instantiate(chippy, GameObject.Find("Chippy Spawn").transform);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
