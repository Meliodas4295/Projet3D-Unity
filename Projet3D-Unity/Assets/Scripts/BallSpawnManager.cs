using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    private Rigidbody ballRb;
    [SerializeField]
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject ballInstantiate = Instantiate(ball, transform.position, player.transform.rotation);
            ballRb = ballInstantiate.GetComponent<Rigidbody>();
            ballRb.AddForce(transform.forward * 20f, ForceMode.Impulse);

        }
    }
}
