using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDie : MonoBehaviour
{
    Animator animator;
    AnimatorStateInfo stateInfo;
    public AudioSource audio1;
    public GameObject ghost;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // initializam animatorul atasat playerului
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        audioDie();
    }
    private void audioDie()
    { 
        if (animator.GetBool("Die"))
        {
            audio1.Play();
            ghost.SetActive(true);
        }
    }
}
