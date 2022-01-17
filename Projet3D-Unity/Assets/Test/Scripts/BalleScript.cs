using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleScript : MonoBehaviour
{
    private Rigidbody ballRb;
    private float speedAction = 10f;
    // Start is called before the first frame update
    void Start()
    {
        ballRb = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ballRb.AddForce(transform.forward * speedAction, ForceMode.Impulse);
    }
}
