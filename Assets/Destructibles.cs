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
    int i;
   
    private void OnTriggerEnter(Collider other)
    {
    
      
            
          if (other.gameObject.layer == LayerMask.NameToLayer(box))
          {
            Instantiate(destroyedversion, transform.position, transform.rotation);
            Destroy(gameObject);
            audio.Play();
            Instantiate(particle, transform.position, transform.rotation);
            i += 1; 
             if (i >= 3)
                {
                    youWin.SetActive(true);
                    Time.timeScale = 1f;
                }
                //else
               // {
               //     youWin.SetActive(false);
               // }
            
          }
        
    }
   
}
