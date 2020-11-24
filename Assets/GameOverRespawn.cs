using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverRespawn : MonoBehaviour
{
    Vector3 initialPos;
    Animator animator;
    AnimatorStateInfo stateInfo;
    public GameObject menuContainer;
    public AudioSource audio1;
    public AudioSource audio2;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        animator = GetComponent<Animator>(); // initializam animatorul atasat playerului
        
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Respawn();
    }
    private void Respawn()
    {

        if (stateInfo.IsTag("die"))
        {
            audio2.Stop();
            audio1.Play();
            menuContainer.SetActive(true);
            transform.position = initialPos;
        }
        else
            menuContainer.SetActive(false);
            
    }

}