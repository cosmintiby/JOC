﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBhvr : StateMachineBehaviour
{
    public HumanBodyBones bone;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public float minT;
    public float maxT;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetLayerWeight(1, 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float t = stateInfo.normalizedTime;
        if (t > minT && t < maxT)
        {
            animator.GetBoneTransform(bone).GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            animator.GetBoneTransform(bone).GetComponent<SphereCollider>().enabled = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.GetBoneTransform(bone).GetComponent<SphereCollider>().enabled = false; 
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
