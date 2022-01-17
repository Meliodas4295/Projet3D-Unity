using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleManager : MonoBehaviour
{
    private Rigidbody capsuleRb;
    void Start()
    {
        capsuleRb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            capsuleRb.AddForce(Vector3.forward * 5, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        capsuleRb.velocity = Vector3.zero;

    }

}
