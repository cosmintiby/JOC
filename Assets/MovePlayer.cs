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
    public GameObject menuContainer;
    public Transform cameraTransform;
    public Transform sword;
    public Transform backSword;
    Vector3 initialPos;
    Rigidbody rigidbody;
    Vector3 moveDir;
    Animator animator;
    AnimatorStateInfo stateInfo;
    CapsuleCollider capsule;

    Transform enemy;
    public Transform enemyContainer;
    List<Transform> enemies;

    Transform rightHand;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // initializam animatorul atasat playerului
        initialPos = transform.position;
        rigidbody = GetComponent<Rigidbody>(); // initializam corpul rigid atasat playerului
        capsule = GetComponent<CapsuleCollider>();
        enemies = new List<Transform>();
        for (int i = 0; i < enemyContainer.childCount; i++)
            enemies.Add(enemyContainer.GetChild(i));
        rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        GetMovementDirection();
        
        UpdateAnimatorParameters();

        ApplyRootMotion();

        ApplyRootRotation();
        // ApplyRootRotation2();

        HandleMidair();

        HandleAttack();

        HandleSwordBehaviour();


        //ApplySpeed();
       
    }

    private void HandleSwordBehaviour()
    {
        if(Input.GetButton("Fire2"))
        {
            animator.SetBool("aiming", true);
            animator.SetLayerWeight(1, 1f);
            sword.gameObject.SetActive(true);
            sword.position = rightHand.position;
            sword.rotation = rightHand.rotation;
            backSword.gameObject.SetActive(false);
        }
        else
        {
            animator.SetBool("aiming", false);
            sword.gameObject.SetActive(false);
            backSword.gameObject.SetActive(true);
        } 
    }
    private void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetLayerWeight(1, 0f);
            animator.SetTrigger("Attack");
        }
      
       if (stateInfo.IsTag("grounded"))
        {
            if (enemy != null)
            {
               float dist = Vector3.Distance(enemy.position, transform.position);
                float guardWeight = .2f - (Mathf.Clamp(dist, 2f, 12f) - 2f) / 2; //daca e mic de 2f ramane 2, daca e mai mare de 4f ramane 4;
                animator.SetLayerWeight(1, Mathf.Pow(guardWeight, .1f));
            }
            else
                animator.SetLayerWeight(1, 0f);
        }
        else
        {
            animator.SetLayerWeight(1, 0f);
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
                Vector3 JumpForce = (Vector3.up + moveDir) * jumpPower;
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
       if (Input.GetKey(KeyCode.LeftShift))
            characterSpaceDir *= .5f;
        animator.SetFloat("Forward", characterSpaceDir.z, 0.1f, Time.deltaTime);
        animator.SetFloat("Right", characterSpaceDir.x, 0.1f, Time.deltaTime);

    }
    private void ApplyRootRotation()
    {
        //stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsTag("attack") || stateInfo.IsTag("die"))
          return;
        
        
        Vector3 F = transform.forward;
        Vector3 D = GetclosestEnemyDirection();
        Vector3 FminusD = F - D;
        Vector3 FplusD = F + D;

        if (animator.GetBool("aiming")) // orienteaza pers cu fata in directia de privire a camerei daca tinteste
        {
            D = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        }

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

        if (closestEnemyIndex != -1)        //schimba targetul pe inamic pe cel mai aproape de jucator
        {
            if(!stateInfo.IsTag("attack"))
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
       
       //stateInfo = animator.GetCurrentAnimatorStateInfo(0);
       if (stateInfo.IsTag("attack") || stateInfo.IsTag("die"))
            return;


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
     
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Running", true);
            Vector3 offset = moveDir * Time.deltaTime * PlayerSpeed * 2f;
            transform.position += offset; // recalculeaza pozitia la fiecare frame;
        }
        else
        {
            animator.SetBool("Running", false);
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
