using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBoxObstacol : MonoBehaviour
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
              animator.Play("TakeHitR");
              animator.SetInteger("TakenDamage", damage);
            }
        }
    }
}

                
                

                    
                
