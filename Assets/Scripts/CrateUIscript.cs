using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateUIscript : MonoBehaviour
{
    public GameObject crate1;
    public GameObject crate2;
    public GameObject crate3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if (ScoreScript.scoreValue < 30)
        //{
        //    crate1.SetActive(false);
       //     crate2.SetActive(false);
      //      crate3.SetActive(false);
      //  }
        if (ScoreScript.scoreValue == 10)
        {
            crate1.SetActive(true);
        }
        if (ScoreScript.scoreValue == 20)
        {
            crate2.SetActive(true);
        }
        if (ScoreScript.scoreValue == 30)
        {
            crate3.SetActive(true);
        }
    }
}
