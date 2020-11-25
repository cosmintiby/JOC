using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChrAberration : MonoBehaviour
{
    public Animator animator;
    AnimatorStateInfo stateInfo;
  
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // initializam animatorul atasat playerului
        
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsTag("hit"))
        {
            // UnityEngine.Rendering.Volume   
        }
    }
}
