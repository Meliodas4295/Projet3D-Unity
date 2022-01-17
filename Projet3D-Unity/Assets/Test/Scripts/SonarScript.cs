using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarScript : MonoBehaviour
{
    private Renderer[] ObjectRenderers;
    private float currentRadius;
    // Update is called once per frame
    void Start()
    {
        ObjectRenderers = GetComponentsInChildren<Renderer>();
    }
    void FixedUpdate()
    {
        currentRadius += 0.01f;
        if(currentRadius > 6)
        {
            currentRadius = 0;
        }
        foreach(Renderer renderer in ObjectRenderers)
        {
            renderer.material.SetFloat("_Radius", currentRadius);
        }
    }
}
