using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePoint : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject spotterOnePoint;

    void Start()
    {
        spotterOnePoint = GameObject.Find("1_Point");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //spotterOnePoint.GetComponent<AudioSource>().Play();
    }
}
