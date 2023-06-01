using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTargetTrigger : MonoBehaviour
{
    private TargetObject targetObject;


    // Start is called before the first frame update
    void Start()
    {
        targetObject = GameObject.FindObjectOfType<TargetObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObject.getWallSystem() == 1){
            if(transform.position.z == -15){
                GameObject spotterNoPoints = GameObject.Find("Hit_Floor");
                spotterNoPoints.GetComponent<AudioSource>().Play();
                targetObject.InvertSpeed();
            }

        }else if(targetObject.getWallSystem() == 2){


        }else if(targetObject.getWallSystem() == 3){


        }else{

        }
    }
}
