using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateGhost : MonoBehaviour

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
            initialY + (Mathf.Sin(3f * (Time.time * freq)) + Mathf.Pow(freq, 2f)) * amplitude, 
            transform.position.z);
        Destroy(gameObject, 3f);
    }
}
