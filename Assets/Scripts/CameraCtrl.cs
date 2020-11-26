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

    public Vector3 cameraOffset, aimingCameraOffset;
    public Animator characterAnimator;
    Quaternion initRot;
    Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.rotation;
        Vector3 euler = initRot.eulerAngles;
        pitch = euler.x;
        yaw = euler.y;
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

        Vector3 camOffset = transform.TransformDirection(cameraOffset);

        if (characterAnimator.GetBool("aiming"))
        {
            camOffset = transform.TransformDirection(aimingCameraOffset);
        }
        else 
        {
            
            camOffset = transform.TransformDirection(cameraOffset);
        }
        transform.position = player.position - transform.forward * distanceToTarget + camOffset;
    }
}
