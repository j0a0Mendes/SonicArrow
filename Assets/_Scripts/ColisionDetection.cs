using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    [SerializeField]
    public GameObject target;

    private TargetObject targetScript;

    // Start is called before the first frame update
    void Start()
    {
        targetScript = target.GetComponent<TargetObject>();
    }

    private void Update()
    {
        //Debug.Log("HII");
    }

    private void OnCollisionEnter(Collision collision)
    {
        string collidedWith = collision.gameObject.tag;
        Debug.Log(collidedWith);
        if (collidedWith == "LeftWall" || collidedWith == "RightWall")
        {
            Debug.Log("Hit");
            targetScript.InvertSpeed(); 
        }
    }
}
