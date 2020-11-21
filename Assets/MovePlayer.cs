using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float PlayerSpeed = 2f; //declaram variabila care apare in inspectorda
    public bool doNormalize = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //pentru tastele A si D 
        float v = Input.GetAxis("Vertical"); //pentru tastele W si S

        Vector3 dir = new Vector3(h, 0, v);
        if (doNormalize)
            dir = dir.normalized;
 
        Vector3 offset = dir * Time.deltaTime * PlayerSpeed;
        transform.position += offset;

    }
}
