using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverRespawn : MonoBehaviour
{
    Vector3 initialPos;
    Animator animator;
    AnimatorStateInfo stateInfo;
    public GameObject menuContainer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // initializam animatorul atasat playerului
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Respawn();
    }
    private void Respawn()
    {

        if (stateInfo.IsTag("die"))
        {
            transform.position = initialPos;
            menuContainer.SetActive(true);
        }
        else
            menuContainer.SetActive(false);
    }

}