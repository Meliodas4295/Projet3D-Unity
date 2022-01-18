using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    private Rigidbody ballRb;
    [SerializeField]
    private GameObject player;
    private float timerBeforeNewBall = 1f;
    private Image lightingBall;

    private void Start()
    {
        lightingBall = GetComponentInParent<PlayerManager>().GetLightingBall();
    }
    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInParent<PlayerManager>().GetIsTouching())
        {
            BallInstantiation();
        }
    }

    private void BallInstantiation()
    {
        if (!GetComponentInParent<PlayerManager>().GetIsWinner() && !GetComponentInParent<PlayerManager>().GetIsGameOver())
        {
            timerBeforeNewBall += Time.deltaTime;
            if (timerBeforeNewBall > 1f)
            {
                lightingBall.color = new Color(lightingBall.color.r, lightingBall.color.g, lightingBall.color.b, 1f);
                if (Input.GetKeyDown(InputTouch()))
                {
                    lightingBall.color = new Color(lightingBall.color.r, lightingBall.color.g, lightingBall.color.b, 0.25f);
                    GameObject ballInstantiate = Instantiate(ball, transform.position, player.transform.rotation);
                    ballRb = ballInstantiate.GetComponent<Rigidbody>();
                    ballRb.AddForce(transform.forward * 10f, ForceMode.Impulse);
                    timerBeforeNewBall = 0;
                }
            }
        }
    }

    KeyCode InputTouch()
    {
        if(GetComponentInParent<PlayerManager>().GetId() == 0)
        {
            return KeyCode.Keypad2;
        }
        else
        {
            return KeyCode.E;
        }
    }
}
