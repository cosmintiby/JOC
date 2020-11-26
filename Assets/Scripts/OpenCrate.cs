using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCrate : MonoBehaviour
{
    
    public string box;
    public AudioSource audio;
    public ParticleSystem particle;
    
    

    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.layer == LayerMask.NameToLayer(box))
        {
            GetComponent<Animator>().SetBool("open", true);
            audio.Play();
            Instantiate(particle, transform.position, transform.rotation);
         
        }
        

    }
}
