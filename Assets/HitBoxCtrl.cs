using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxCtrl : MonoBehaviour
{
    public string opponentHurtBox;
    public int damage = 5;

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.layer == LayerMask.NameToLayer(opponentHurtBox))
        {
            // if (GameObject.FindWithTag("HitBoxR"))
            //  other.transform.parent.GetComponentInParent<Animator>().Play("TakeHitL");
            //  if (GameObject.FindWithTag("HitBoxL"))
            Animator animator = other.transform.parent.GetComponentInParent<Animator>();
            animator.Play("TakeHitR");
            animator.SetInteger("TakenDamage", damage);

        }
    }
}
 
