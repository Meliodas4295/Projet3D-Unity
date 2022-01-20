using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Transform playerBody;
    private Animator animator;

    [SerializeField]
    private GameObject lightingBold;

    private float speed = 8f;
    private Vector3 moveDirection;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;


    private int id;
    [SerializeField]
    private Transform groundCheck;
    private float groundDistance = 0.4f;

    private float speedRotation = 0.7f;
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

    private float jumpHeight = 1f;

    Vector3 velocity;
    bool isGrounded;

    private float timer = 0;
    private float timerDissimulation = 0;
    private Vector3 lastPosition = new Vector3(0, 0, 0);
    private bool isMoving = false;
    [SerializeField]
    private bool hasKey = false;
    [SerializeField]
    private bool isTouching = false;
    private bool isTouchingAnimator = false;
    private bool dissimulationPowerUp = false;
    private float timeBeforeNewDissimulation = 5f;
    private bool isGameOver = false;
    [SerializeField]
    private bool isWinner = false;

    private GameObject timerBeginning;
    private bool countDown = false;
    private float timerBeforeBeginningOfPlay = 3f;
    private TextMeshProUGUI countPlayer0;
    private TextMeshProUGUI countPlayer1;

    private float timerMalus = 10f;
    private bool countDownMalus = false;
    private GameObject malus;
    private GameObject malusPlayer0;
    private GameObject malusPlayer1;

    private Color passColor;

    private bool malusEffectStart = true;

    private AudioSource audioSource;
    private AudioSource keyAudioSource;
 
    [SerializeField]
    private AudioClip stunSound;
    [SerializeField]
    private AudioClip keyCollectSound;

    private AudioSource dissimulationAudioSource;
    [SerializeField]
    private AudioClip dissimulationSound;

    public Animator GetAnimator()
    {
        return animator;
    }
    public bool GetCountDown()
    {
        return countDown;
    }
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
        InstantiateUIPlayer();
        pass.color = new Color(pass.color.r, pass.color.g, pass.color.b, 0.25f);
        passColor = new Color(pass.color.r, pass.color.g, pass.color.b);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        InstatiateTimerBeginning();
        malus = GameObject.Find("TimerMalus");
        malusPlayer0 = malus.transform.Find("MalusPlayer0").gameObject;
        malusPlayer1 = malus.transform.Find("MalusPlayer1").gameObject;
        StartCoroutine(timerForTheBeginning());
        Debug.Log(malusPlayer0);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        keyAudioSource = GetComponent<AudioSource>();
        dissimulationAudioSource = GetComponent<AudioSource>();
        //timerBeforeBeginningOfPlay += 1;
    }

    private void InstatiateTimerBeginning()
    {
        timerBeginning = GameObject.Find("TimerBeginning");
        countPlayer0 = GameObject.Find("CountPlayer0").GetComponent<TextMeshProUGUI>();
        countPlayer1 = GameObject.Find("CountPlayer1").GetComponent<TextMeshProUGUI>();
    }

    private void InstantiateUIPlayer()
    {
        pass = GameObject.Find("PassPlayer" + id).GetComponent<Image>();
        spell = GameObject.Find("SpellPlayer" + id).GetComponent<Image>();
        hide = GameObject.Find("HidePlayer" + id).GetComponent<Image>();
        lightingBall = GameObject.Find("LightBallPlayer" + id).GetComponent<Image>();
        background = GameObject.Find("BackgroundPlayer" + id).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && !isWinner && countDown)
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

            moveDirection = new Vector3(0, 0, z);
            moveDirection = transform.TransformDirection(moveDirection);
            if (!isTouching)
            {
                controller.Move(move * speed * Time.deltaTime);
                playerBody.Rotate(rotation * speedRotation);
            }
            else
            {
                Stun();
                TimeOfMalusEffect();
            }
            if (id == 0)
            {
                if (Input.GetKeyDown(KeyCode.Keypad0) && isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
            Move();
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Attack());
            }*/
            controller.Move(velocity * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
        }
    }

    private void Move()
    {
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isStunned", false);


            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;

            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }*/

            if (isTouchingAnimator)
            {
                Stun();
                isTouchingAnimator = false;
            }

        }
        else
        {
            animator.SetBool("isJumping", true);
        }
    }

    private void TimeOfMalusEffect()
    {
        if (malusEffectStart)
        {
            StartCoroutine(timerMalusCo());
            malusEffectStart = false;
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
            hide.color = new Color(hide.color.r, hide.color.g, hide.color.b, 1f);
            if (Input.GetKeyDown(InputTouch()))
            {
                hide.color = new Color(hide.color.r, hide.color.g, hide.color.b, 0.25f);
                dissimulationPowerUp = true;
                dissimulationAudioSource.PlayOneShot(dissimulationSound, 0.5f);
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
            if (collision.gameObject.GetComponent<WeaponManager>().idPlayer != id)
            {
                audioSource.PlayOneShot(stunSound, 0.5f);
                if (id == 0)
                {
                    isTouchingAnimator = true;
                    malusPlayer0.SetActive(true);
                }
                else
                {
                    isTouchingAnimator = true;
                    malusPlayer1.SetActive(true);
                }
                isTouching = true;
                weapon = collision.gameObject;
                lightingBold.SetActive(true);
                hasKey = false;
                pass.color = new Color(pass.color.r, 0, 0, 0.25f);
                hide.color = Color.red;
                lightingBall.color = Color.red;
                background.color = Color.red;
                spell.color = Color.red;
            }


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Portal0" || other.name == "Portal1")
        {
            if (hasKey)
            {
                isWinner = true;
            }

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
            pass.color = new Color(pass.color.r, pass.color.g, pass.color.b, 1f);
            Destroy(hit.gameObject);
            spawnManager.hasKeyPresent = false;
            keyAudioSource.PlayOneShot(keyCollectSound, 0.5f);
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
        if (id == 0)
        {
            return KeyCode.Keypad3;
        }
        else
        {
            return KeyCode.R;
        }
    }

    IEnumerator timerForTheBeginning()
    {
        while (timerBeforeBeginningOfPlay > 0)
        {
            timerBeforeBeginningOfPlay--;
            yield return new WaitForSeconds(1f);
            if (id == 0)
            {
                countPlayer0.text = timerBeforeBeginningOfPlay.ToString();

            }
            else
            {
                countPlayer1.text = timerBeforeBeginningOfPlay.ToString();
            }
        }
        countDown = true;
        timerBeginning.SetActive(false);
    }
    IEnumerator timerMalusCo()
    {
        while (timerMalus > 0)
        {
            timerMalus--;
            yield return new WaitForSeconds(1f);
            if (malusPlayer0.activeSelf)
            {
                malusPlayer0.GetComponent<TextMeshProUGUI>().text = timerMalus.ToString();
            }
            else
            {
                malusPlayer1.GetComponent<TextMeshProUGUI>().text = timerMalus.ToString();
            }

        }
        timerMalus = 10f;
        isTouching = false;
        //lightingBold.SetActive(false);
        if (id == 0)
        {
            malusPlayer0.SetActive(false);
            malusPlayer0.GetComponent<TextMeshProUGUI>().text = "10";

        }
        else
        {
            malusPlayer1.SetActive(false);
            malusPlayer1.GetComponent<TextMeshProUGUI>().text = "10";
        }
        malusEffectStart = true;
        pass.color = passColor;
        pass.color = new Color(pass.color.r, pass.color.g, pass.color.b, 0.25f); ;
        hide.color = Color.white;
        lightingBall.color = Color.white;
        background.color = Color.white;
        spell.color = Color.white;
    }

    private void Idle()
    {
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

    }

    private IEnumerator Attack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("AttackLayer"), 1);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(2);

        animator.SetLayerWeight(animator.GetLayerIndex("AttackLayer"), 0);
    }

    private void Stun()
    {
        moveSpeed = 0;
        animator.SetBool("isStunned", true);
    }
}

