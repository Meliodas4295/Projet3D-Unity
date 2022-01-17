using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private  Transform playerBody;

    [SerializeField]
    private GameObject lightingBold;

    private float speed =  12f;

    private int id;
    [SerializeField]
    private Transform groundCheck;
    private float groundDistance = 0.4f;

    private float speedRotation = 1f;
    private float gravity = -9.81f;
    [SerializeField]
    private LayerMask groundMask;

    private SpawnManager spawnManager;
    private GameObject weapon;

    private Image pass;
    private Image spell;
    private Image lightingBall;
    private Image hide;
    private Image background;

    Vector3 velocity;
    bool isGrounded;

    private float timer = 0;
    private float timerMalus = 0;
    private float timerDissimulation = 0;
    private Vector3 lastPosition = new Vector3(0, 0, 0);
    private bool isMoving = false;
    private bool hasKey = false;
    [SerializeField]
    private bool isTouching = false;
    private bool dissimulationPowerUp = false;
    private float timeBeforeNewDissimulation = 5f;
    private bool isGameOver = false;
    [SerializeField]
    private bool isWinner = false;
    // Start is called before the first frame update
    public Image GetLightingBall()
    {
        return lightingBall;
    }
    public void SetLigthingBall(Image _lightingBall)
    {
        lightingBall = _lightingBall;
    }
    public Image GetSpell()
    {
        return spell;
    }
    public void SetSpell(Image _spell)
    {
        spell = _spell;
    }
    public bool GetIsGameOver()
    {
        return isGameOver;
    }
    public void SetIsGameOver(bool _isGameOver)
    {
        isGameOver = _isGameOver;
    }
    public bool GetIsWinner()
    {
        return isWinner;
    }
    public void SetIsWinner(bool _isWinner)
    {
        isWinner = _isWinner;
    }
    public bool GetHasKey()
    {
        return hasKey;
    }
    public void SetHasKey(bool _hasKey)
    {
        hasKey = _hasKey;
    }
    public int GetId()
    {
        return id;
    }
    public void SetId(int _id)
    {
        id = _id;
    }
    public bool GetIsTouching()
    {
        return this.isTouching;
    }
    public void SetIsTouching(bool _isTouching)
    {
        isTouching = _isTouching;
    }
    void Start()
    {
        lightingBold.SetActive(false);
        /*if(id == 0)
        {
            pass = GameObject.Find("PassPlayer1").GetComponent<Image>();
            spell = GameObject.Find("SpellPlayer1").GetComponent<Image>();
            hide = GameObject.Find("HidePlayer1").GetComponent<Image>();
            lightingBall = GameObject.Find("LightBallPlayer1").GetComponent<Image>();
            background = GameObject.Find("BackgroundPlayer1").GetComponent<Image>();
        }
        else
        {
            pass = GameObject.Find("PassPlayer2").GetComponent<Image>();
            spell = GameObject.Find("SpellPlayer2").GetComponent<Image>();
            hide = GameObject.Find("HidePlayer2").GetComponent<Image>();
            lightingBall = GameObject.Find("LightBallPlayer2").GetComponent<Image>();
            background = GameObject.Find("BackgroundPlayer2").GetComponent<Image>();
        }*/
        //pass.color = new Color(pass.color.r, pass.color.g, pass.color.b, 0.25f);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DissimulationPower();
        MovingDetection();
        timer += Time.deltaTime;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
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
            TimeOfMalusEffect();
        }

        controller.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
    }

    private void TimeOfMalusEffect()
    {
        timerMalus += Time.deltaTime;
        if (timerMalus > 10f)
        {
            timerMalus = 0;
            isTouching = false;
            lightingBold.SetActive(false);
            /*pass.color = Color.white;
            pass.color = new Color(pass.color.r, pass.color.g, pass.color.b, 0.25f); ;
            hide.color = Color.white;
            lightingBall.color = Color.white;
            background.color = Color.white;
            spell.color = Color.white;*/
        }
    }

    private void MovingDetection()
    {
        if (lastPosition != gameObject.transform.position)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = gameObject.transform.position;
    }

    private void DissimulationPower()
    {
        timeBeforeNewDissimulation += Time.deltaTime;
        if (timeBeforeNewDissimulation > 5f)
        {
            //hide.color = new Color(hide.color.r, hide.color.g, hide.color.b, 1f);
            if (Input.GetKeyDown(InputTouch()))
            {
                //hide.color = new Color(hide.color.r, hide.color.g, hide.color.b, 0.25f);
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            isTouching = true;
            weapon = collision.gameObject;
            lightingBold.SetActive(true);
            hasKey = false;
          /*  pass.color = new Color(pass.color.r, 0, 0, 0.25f);
            hide.color = Color.red;
            lightingBall.color = Color.red;
            background.color = Color.red;
            spell.color = Color.red;*/
            

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
        GroundCollision(hit);
        WalkSonarEffect(hit);
        KeyCollision(hit);
    }

    private void WalkSonarEffect(ControllerColliderHit hit)
    {
        if (timer > 0.75f && isMoving && !dissimulationPowerUp && !hit.gameObject.CompareTag("Ball"))
        {
            hit.gameObject.GetComponent<SimpleSonarShader_Object>().StartSonarRing(hit.point, 2f);
            timer = 0;
        }
    }

    private void KeyCollision(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            //pass.color = new Color(pass.color.r, pass.color.g, pass.color.b, 1f);
            Destroy(hit.gameObject);
            spawnManager.hasKeyPresent = false;
        }
    }

    private static void GroundCollision(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Ground"))
        {
            hit.gameObject.GetComponent<SimpleSonarShader_Object>().StartSonarRing(hit.point, 2f);
        }
    }

    public KeyCode InputTouch()
    {
        if(id == 0)
        {
            return KeyCode.K;
        }
        else
        {
            return KeyCode.C;
        }
    }
}
