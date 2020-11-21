using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraCtrl : MonoBehaviour
{
    public Transform player;
    float yaw = 0f, pitch = 0f;
    public float distanceToTarget = 4f;
    public float minPitch = -50f;
    public float maxPitch = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate() // se apeleaza dupa ce update s-a apelat pe toate obiectele
    {
        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y");

        pitch -= dy;
        yaw += dx;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        transform.position = player.position - transform.forward * distanceToTarget;
    }
}
