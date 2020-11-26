using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulScript : MonoBehaviour
{
        public float freq = 2f;
        public float amplitude = 45f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rotatiaZ = Mathf.Sin(Time.time * freq) * amplitude; 
        transform.localRotation = Quaternion.Euler(0, 0, rotatiaZ);
    }
}
