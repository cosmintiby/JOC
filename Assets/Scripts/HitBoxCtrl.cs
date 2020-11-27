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
            if (animator.GetInteger("HP") > 0f)
            {

                if (animator.GetFloat("timeSinceTakenHit") > 0.3f)
            {
                
                animator.SetInteger("TakenDamage", damage);

                if (GameObject.FindGameObjectWithTag("HitBoxL"))
                    animator.SetTrigger("TakenHitRight");


                if (GameObject.FindGameObjectWithTag("HitBoxR"))
                    animator.SetTrigger("TakenHitLeft");
            }

            }

        }

    }
}
 
