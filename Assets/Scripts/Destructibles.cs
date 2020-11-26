using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour
{
    public GameObject destroyedversion;
    public string box;
    public AudioSource audio;
    public ParticleSystem particle;
    public GameObject youWin;

    private void OnTriggerEnter(Collider other)
    {
    
      
            
          if (other.gameObject.layer == LayerMask.NameToLayer(box))
          {
            ScoreScript.scoreValue += 1;
            Instantiate(destroyedversion, transform.position, transform.rotation);
            Destroy(gameObject);
            audio.Play();
            Instantiate(particle, transform.position, transform.rotation);

          }
        
    }
   
}
