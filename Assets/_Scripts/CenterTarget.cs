using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTarget : MonoBehaviour
{
    [SerializeField]
    public bool flip;
    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation *= Quaternion.Euler(0f, 90f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        /**if(flip)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        //transform.rotation = Quaternion.Euler(0f, -90f, 0f);*/
    }

    public void flipRotation()
    {
        flip = !flip;
    }
}
