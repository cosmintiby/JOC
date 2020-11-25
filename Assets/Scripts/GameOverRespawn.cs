using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverRespawn : MonoBehaviour
{
    Vector3 initialPos;
    Animator animator;
    AnimatorStateInfo stateInfo;
    public GameObject menuContainer;
    public GameObject hpContainer;
    public AudioSource audio1;
    
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
            
           
            menuContainer.SetActive(true);
            transform.position = initialPos;
            
                if(menuContainer)
                    audio1.Play();
                
            Time.timeScale = 0f;
            animator.Play("Revive");

        }

        if (stateInfo.IsName("Revive"))
        {
            hpContainer.SetActive(false);
            animator.SetInteger("HP", 100);
            animator.SetBool("Alive", true);
        }
        else
            hpContainer.SetActive(true);

            
    }
    
    
}