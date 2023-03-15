using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickingArrowToSurface : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SphereCollider myCollider;

    [SerializeField]
    private GameObject stickingArrow;

    [SerializeField]
    private AudioSource windNavigatingSound;

    private ChangePerspectiveController controller;
    //[SerializeField]
    //GameObject rightHand;

    //KeyControllers keyControllers;

    private ActionReplayArrow actionReplayArrow;

    private KeyControllers keyControllers;

    private int changePerspectiveCounter;
    private bool changePerspectiveCounterTrigger;
    //private bool notFlying = false;

    private void Start() 
    { 
        windNavigatingSound.Play();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
    }

    //private void Update() 
    //{ 
    //    if (notFlying)
    //
    //        windNavigatingSound.Stop();
    //    }   
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        //Debug.Log(collision.gameObject.tag == "Untagged");
        string collidedWith = collision.gameObject.tag;
        if (collidedWith == "WallFirstLayer" || collidedWith == "WallSecondLayer" || collidedWith == "WallThirdLayer" || collidedWith == "WallForthLayer" || collidedWith == "WallFifthLayer" || collidedWith == "Floor" || collidedWith == "Ceiling")
        {
            GameObject spotterNoPoints = GameObject.Find("No_Points");
            spotterNoPoints.GetComponent<AudioSource>().Play();
        }
        else if (collidedWith == "TargetFirstRegion")
        {
            GameObject spotter = GameObject.Find("5_Points");
            spotter.GetComponent<AudioSource>().Play();
        }
        else if (collidedWith == "TargetSecondRegion")
        {
            GameObject spotter = GameObject.Find("4_Points");
            spotter.GetComponent<AudioSource>().Play();
        }
        else if (collidedWith == "TargetThirdRegion")
        {
            GameObject spotter = GameObject.Find("3_Points");
            spotter.GetComponent<AudioSource>().Play();
        }
        else if (collidedWith == "TargetForthRegion")
        {
            GameObject spotter = GameObject.Find("2_Points");
            spotter.GetComponent<AudioSource>().Play();
        }
        else if (collidedWith == "TargetFifthRegion")
        {
            GameObject spotter = GameObject.Find("1_Points");
            spotter.GetComponent<AudioSource>().Play();
        }


        windNavigatingSound.Stop();

        //REPLAY PORPUSE
        actionReplayArrow = GameObject.FindObjectOfType<ActionReplayArrow>();
       
        actionReplayArrow.alreadyHitTrigger();

        keyControllers = GameObject.FindObjectOfType<KeyControllers>();
        keyControllers.enableButtonX();
        

        //CHANGE PERSPECTIVEEE (REMOVE TO GET ORIGINAL VERSION)
        changePerspectiveCounterTrigger = true;

        rb.isKinematic = true;
        myCollider.isTrigger = true;

        GameObject arrow = Instantiate(stickingArrow);
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;

        if (collision.collider.attachedRigidbody != null)
        {
            arrow.transform.parent = collision.collider.attachedRigidbody.transform;
        }

        collision.collider.GetComponent<IHittable>()?.GetHit();

        //Destroy(gameObject);

    }

    private void FixedUpdate()
    {
        if (changePerspectiveCounterTrigger)
        {
            changePerspectiveCounter += 1;
            if(changePerspectiveCounter == 75)
            {
                controller.enableChange();
                controller.changePerspective();

                changePerspectiveCounter= 0;
                changePerspectiveCounterTrigger = false;
            }
        }
    }
}
