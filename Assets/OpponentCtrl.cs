using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCtrl : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    public Transform player;
    public float attackDistanceThresh = 1.5f;
    public float runningDistanceThresh = 5f;
    Vector3 destinationOffset;
    float phase;
    AnimatorStateInfo stateInfo;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        StartCoroutine(SeedMoveDirection(1f));
        phase = UnityEngine.Random.Range(0f, Mathf.PI * 5f);
    }

    IEnumerator SeedMoveDirection(float t)
    {
        yield return new WaitForSeconds(t);
        int offsetType = UnityEngine.Random.Range(0, 10);
        switch (offsetType)
        {
            case 0: case 1: case 2:
                destinationOffset = -player.right * 3f;
                break;
            case 3: case 4: case 5:
                destinationOffset = player.right * 3f;
                break;
            case 6: case 7: case 8:
                destinationOffset = player.forward * 3f;
                break;
             case 9:
                destinationOffset = Vector3.zero;
                break;
        }
        float newT = UnityEngine.Random.Range(0.5f, 1.5f);
        yield return StartCoroutine(SeedMoveDirection(newT));
         
    }
    // Update is called once per frame
    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Die"))
            agent.SetDestination(transform.position);
        else
             agent.SetDestination(player.position + destinationOffset);
        
        
        

        Vector3 characterSpaceDir = transform.InverseTransformDirection(agent.velocity);
        animator.SetFloat("Forward", characterSpaceDir.z, 0.1f, Time.deltaTime);
        animator.SetFloat("Right", characterSpaceDir.x, 0.1f, Time.deltaTime);

        attackDistanceThresh = (Mathf.Sin(Time.time + phase) + 1f) * 0.5f + 0.5f;
        if (Vector3.Distance(transform.position, player.position) < attackDistanceThresh) //oponentul ataca la distanta mai mica decat cea declarata public in inspector
            animator.SetTrigger("Attack");

        if (Vector3.Distance(transform.position, player.position) > runningDistanceThresh) //oponentul alearga la distanta mai mare decat cea declarata public in inspector
            animator.SetTrigger("Running");

    }
    private void LateUpdate()
    {
        if (stateInfo.IsTag("die"))
            return;
         
            Vector3 D = (player.position - transform.position);
        D.y = 0f;
        D = D.normalized;
        Quaternion lookAtDir = Quaternion.LookRotation(D);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtDir, Time.deltaTime * 10f);
    }
}
