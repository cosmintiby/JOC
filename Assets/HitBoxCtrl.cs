using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxCtrl : MonoBehaviour
{
    public string opponentHurtBox;
    public int damage = 5;
    
    
    private void OnTriggerEnter(Collider other)
    {
            Animator animator = other.transform.parent.GetComponentInParent<Animator>();
      
        if (other.gameObject.layer == LayerMask.NameToLayer(opponentHurtBox))
        {
            //animator.Play("TakeHitR");
            animator.SetInteger("TakenDamage", damage);

            if (GameObject.FindGameObjectWithTag("HitBoxL"))
                animator.SetTrigger("TakenHitRight");
            

            if (GameObject.FindGameObjectWithTag("HitBoxR"))
                animator.SetTrigger("TakenHitLeft");
            



           // if (animator.GetBool("TakenHitLeft"))
            //    animator.Play("TakeHitL");

          // if (animator.GetBool("TakenHitRight"))
            //    animator.Play("TakeHitR");

        }

    }
}
 
