using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsAudio : MonoBehaviour
{
    
    public AudioSource audio;
    Animator animator;
    AnimatorStateInfo stateInfo;

    void Start()
    {
        
        animator = GetComponent<Animator>(); // initializam animatorul atasat playerului
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
    }

    void Update()
    {
        
        Audio();
    }
    private void Audio()
    {



       if (stateInfo.IsName("Grounded"))
        {
            
            audio.Play();
        }
        else
        {

            audio.Stop();
        }

    }
}
