using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private Vector2 movment;
    private Rigidbody cubeRb;
    private float speedTranslation = 10;
    private float speedRotation = 4;
    // Start is called before the first frame update
    void Start()
    {
        cubeRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movment = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        cubeRb.velocity = transform.forward * movment.y * speedTranslation;
        cubeRb.angularVelocity = new Vector3(0, 1, 0) * movment.x * speedRotation;


    }
}
