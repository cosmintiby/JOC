using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float PlayerSpeed = 2f; //declaram variabila care apare in inspectorda
    public bool doNormalize = false;
    public float rotationSpeed = 4f;
    public Transform cameraTransform;
    Rigidbody rigidbody;
    Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); // ia valoarea corpului rigid atasat playerului

    }

    // Update is called once per frame
    void Update()
    {
        GetMovementDirection();

        ApplyRootMotion();

        ApplyRootRotation();
       // ApplyRootRotation2();
    }


    private void ApplyRootRotation()
    {
        Vector3 F = transform.forward;
        Vector3 D = moveDir;
        Vector3 FminusD = F - D;
        Vector3 FplusD = F + D;

        if (FminusD.magnitude > 0.0001f && FplusD.magnitude > 0.001f)
        {
            float u = Mathf.Acos(Vector3.Dot(F, D));//alfam unghiul in radiani
            u *= Mathf.Rad2Deg;//il transformam in grade
            Vector3 axis = Vector3.Cross(F, D);// calculam axa y

            transform.rotation = Quaternion.AngleAxis(u * Time.deltaTime * rotationSpeed, axis) * transform.rotation;
        }

        if (FplusD.magnitude < 0.0001f)
        {
            transform.rotation = Quaternion.AngleAxis(Time.deltaTime * rotationSpeed, Vector3.up) * transform.rotation;
        }
    }
    private void ApplyRootRotation2()
    {
        Quaternion lookAtDir = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtDir, Time.deltaTime * rotationSpeed);
    }

    private void ApplyRootMotion()
    {
        //Vector3 offset = dir * Time.deltaTime * PlayerSpeed;
        //transform.position += offset; // recalculeaza pozitia la fiecare frame;
        float velY = rigidbody.velocity.y;
        rigidbody.velocity = moveDir * PlayerSpeed;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, velY, rigidbody.velocity.z); // se pastreaza componenta verticala;
    }

    private void GetMovementDirection()
    {
        float h = Input.GetAxis("Horizontal"); //pentru tastele A si D; 
        float v = Input.GetAxis("Vertical"); //pentru tastele W si S;

        moveDir = h * cameraTransform.right + v * cameraTransform.forward; // directia relativ la camera
        moveDir.y = 0f;  //componenta y=0 >> playerul se deplaseaza doar in plan orizontal;
        if (doNormalize)
            moveDir = moveDir.normalized;
        
    }
}
