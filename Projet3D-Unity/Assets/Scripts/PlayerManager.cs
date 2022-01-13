using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterController controller;

    public  Transform playerBody;

    public float speed =  12f;

    public int id;

    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public float speedRotation = 9f;
    public float gravity = -9.81f;
    public LayerMask groundMask;

    private SpawnManager spawnManager;

    Vector3 velocity;
    bool isGrounded;

    private float timer = 0;
    private float timerDissimulation = 0;
    private Vector3 lastPosition = new Vector3(0, 0, 0);
    private bool isMoving = false;
    public bool hasKey = false;
    private bool isTouching = false;
    private bool dissimulationPowerUp = false;
    private float timeBeforeNewDissimulation = 5f;
    private bool isGameOver = false;
    public bool isWinner = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeNewDissimulation += Time.deltaTime;
        Debug.Log(timeBeforeNewDissimulation);
        if (timeBeforeNewDissimulation > 5f)
        {
            if (Input.GetKeyDown(InputTouch()))
            {
                dissimulationPowerUp = true;
            }
        }
        if (dissimulationPowerUp && timerDissimulation < 10)
        {
            timerDissimulation += Time.deltaTime;
            timeBeforeNewDissimulation = 0;
        }
        else
        {
            dissimulationPowerUp = false;
            timerDissimulation = 0;
        }
        if (lastPosition != gameObject.transform.position)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = gameObject.transform.position;
        timer += Time.deltaTime;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal" + id);
        float z = Input.GetAxis("Vertical" + id);

        Vector3 move = transform.forward * z;
        Vector3 rotation = new Vector3(0, 1, 0) * x;
        if (!isTouching)
        {
            controller.Move(move * speed * Time.deltaTime);
            playerBody.Rotate(rotation * speedRotation);
        }
        else
        {
            StartCoroutine(Malus());
        }

        controller.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            isTouching = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Portal0" || other.name == "Portal1")
        {
            isWinner = true;

        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Ground"))
        {
            hit.gameObject.GetComponent<SimpleSonarShader_Object>().StartSonarRing(hit.point, 2f);
        }
        if (timer > 0.75f && isMoving && !dissimulationPowerUp)
        {
            hit.gameObject.GetComponent<SimpleSonarShader_Object>().StartSonarRing(hit.point, 2f);
            timer = 0;
        }
        if (hit.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(hit.gameObject);
            spawnManager.hasKeyPresent = false;
        }
    }
    IEnumerator Malus()
    {
        yield return new WaitForSeconds(10f);
        isTouching = false;
    }

    public KeyCode InputTouch()
    {
        if(id == 0)
        {
            return KeyCode.H;
        }
        else
        {
            return KeyCode.J;
        }
    }
}
