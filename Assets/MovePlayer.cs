using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float PlayerSpeed = 2f; //declaram variabila care apare in inspector
    public bool doNormalize = false;
    public float rotationSpeed = 4f;
    public float jumpPower = 4f; // puterea sariturii
    public float groundedTreshold = 0.1f;
    public float minY = -20f;
    public Transform cameraTransform;
    Rigidbody rigidbody;
    Vector3 moveDir;
    Animator animator;
    CapsuleCollider capsule;
    Vector3 initialPos;

    Transform enemy;
    public Transform enemyContainer;
    List<Transform> enemies;
    AnimatorStateInfo stateInfo;
    // Start is called before the first frame update
    void Start()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        rigidbody = GetComponent<Rigidbody>(); // initializam corpul rigid atasat playerului
        capsule = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>(); // initializam animatorul atasat playerului
        initialPos = transform.position;
        enemies = new List<Transform>();
        for (int i = 0; i < enemyContainer.childCount; i++)
            enemies.Add(enemyContainer.GetChild(i));
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementDirection();
        
        UpdateAnimatorParameters();

        ApplyRootMotion();

        ApplyRootRotation();
        // ApplyRootRotation2();

        HandleMidair();

        HandleAttack();

        ApplySpeed();
        Respawn();
    }

    private void Respawn()
    {
       
        if (stateInfo.IsName("Die"))
            transform.position = initialPos;
           
    }

    private void HandleAttack()
    {
       if (stateInfo.IsName("Grounded"))
        {
            if (enemy != null)
            {
                float dist = Vector3.Distance(enemy.position, transform.position);
                float guardWeight = 1f - (Mathf.Clamp(dist, 2f, 4f) - 2f) / 2; //daca e mic de 2f ramane 2, daca e mai mare de 4f ramane 4;
                animator.SetLayerWeight(1, guardWeight);
            }
            else
                animator.SetLayerWeight(1, 0f);
        }
        else
        {
            animator.SetLayerWeight(1, 0f);
        }
        
             
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
        
    }
    private void HandleMidair()
    {
        bool midair = true;
        for (float xOffset = -1; xOffset <= 1f; xOffset += 1f)
        {
            for (float zOffset = -1; zOffset <= 1f; zOffset += 1f)
            {

                Vector3 offset = new Vector3(xOffset, 0, zOffset).normalized * capsule.radius;
                Ray ray = new Ray(transform.position + Vector3.up * groundedTreshold + offset, Vector3.down);
                if (Physics.Raycast(ray, 2 * groundedTreshold))
                {
                    midair = false;
                    break;
                }
            }
        }

        if (midair)
        { animator.SetBool("Midair", true);
        }
        else
        { 
            animator.SetBool("Midair", false);
            if (Input.GetButtonDown("Jump"))
            {
                Vector3 jumpForce = (Vector3.up + moveDir) * jumpPower;
                rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            }
        }
       
        if (transform.position.y < minY)
        {
            transform.position = initialPos;
        }
    }
    private void UpdateAnimatorParameters()
    {
        Vector3 characterSpaceDir = transform.InverseTransformDirection(moveDir);
        animator.SetFloat("Forward", characterSpaceDir.z, 0.1f, Time.deltaTime);
        animator.SetFloat("Right", characterSpaceDir.x, 0.1f, Time.deltaTime);

    }
    private void ApplyRootRotation()
    {
        
        if(animator.GetBool("Midair") || stateInfo.IsTag("attack") || stateInfo.IsTag("Die"))
             return;
        
        
        Vector3 F = transform.forward;
        Vector3 D = GetclosestEnemyDirection();
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
    private Vector3 GetclosestEnemyDirection()
    {
        Vector3 D = moveDir;
        float minDist = float.MaxValue;
        int closestEnemyIndex = -1;

        for (int i = 0; i < enemies.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, enemies[i].position);
            if (dist < 2f && dist < minDist)
            {
                minDist = dist;
                closestEnemyIndex = i;

            }

        }

        if (closestEnemyIndex != -1)
        {
            enemy = enemies[closestEnemyIndex];
            D = enemy.position - transform.position;
            D.y = 0f;
            D = D.normalized;
        }
        return D;

    }
    private void ApplyRootRotation2()
    {
        Quaternion lookAtDir = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtDir, Time.deltaTime * rotationSpeed);
    }

    private void ApplyRootMotion()
    {
       
       // var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //if (animator.GetBool("Midair") || stateInfo.IsTag("attack"))
          //      return;


        ApplySpeed();

        if (animator.GetBool("Midair") )

        {
            animator.applyRootMotion = false;
            return;
        }
        else
        {
            animator.applyRootMotion = true;
        }

        float velY = rigidbody.velocity.y;

        Vector3 fidelityDir = animator.deltaPosition.magnitude * moveDir.normalized;
        rigidbody.velocity += fidelityDir  / Time.deltaTime;


        rigidbody.velocity = new Vector3(rigidbody.velocity.x, velY, rigidbody.velocity.z); // se pastreaza componenta verticala;



    }

    private void ApplySpeed()
    {
        //if (animator.GetBool("Running"))
        //{
          //  animator.applyRootMotion = false;
            //return;
        //}
        //else
        //{
          //  animator.applyRootMotion = true;
        //}

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetTrigger("Running");
            Vector3 offset = moveDir * Time.deltaTime * PlayerSpeed * 2f;
            transform.position += offset; // recalculeaza pozitia la fiecare frame;
        }
        else
        {

            Vector3 offset = moveDir * Time.deltaTime * PlayerSpeed;
            transform.position += offset; // recalculeaza pozitia la fiecare frame;

        }
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
