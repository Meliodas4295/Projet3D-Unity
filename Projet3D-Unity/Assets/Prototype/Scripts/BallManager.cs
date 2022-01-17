using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyBall());

    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
