using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacol : MonoBehaviour
{
    float initialY;
    public float freq = 1f;
    public float amplitude = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            initialY + Mathf.Sin(Time.time * freq) * amplitude,
            transform.position.z);
        
    }
}