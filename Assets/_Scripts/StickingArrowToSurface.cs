using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
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

    private ActionReplayArrow actionReplayArrow;

    private KeyControllers keyControllers;  //LEFT HAND ONLY

    //[SerializeField]
    private GameObject rightHand;

    private KeyControllers keyControllersRight;    //RIGHT HAND ONLY

    private int changePerspectiveCounter;
    private bool changePerspectiveCounterTrigger;
    //private bool notFlying = false;

    private int modeSelected;

    //TARGET OBJECT 
    TargetObject target;

    //PLAYING AUDIOS
    public List<AudioSource> audioList;

    private int currentSourceIndex = 0;

    private bool isPlaying = false;

    private bool FivePointsFlag = false;


    private void Start()
    {
        windNavigatingSound.Play();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
        keyControllers = GameObject.FindObjectOfType<KeyControllers>();

        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersRight = rightHand.GetComponent<KeyControllers>();

        target = GameObject.FindObjectOfType<TargetObject>();
        //keyControllerRightHand = GameObject.FindObjectOfType<KeyControllerSupport>();

        audioList = new List<AudioSource>();

    }

    private void Update()
    {
        modeSelected = controller.getModeSelected();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (controller.getModeSelected() == 1)
        {
            controller.canShootAgain();
        }
        keyControllers.enableButtonX();
        modeSelected = controller.getModeSelected();
        //Debug.Log(modeSelected);
        //Debug.Log(collision.gameObject.tag);
        //Debug.Log(collision.gameObject.tag == "Untagged");
        string collidedWith = collision.gameObject.tag;
        if (collidedWith == "FrontWall" || collidedWith == "BackWall" || collidedWith == "LeftWall" || collidedWith == "RightWall")
        {
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Hit_A_Wall");
                audioList.Add(spotterNoPoints.GetComponent<AudioSource>());
            }

            controller.addPoints(0);

            Debug.Log("WALL HIT");
        }
        else if (collidedWith == "Floor")
        {
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Hit_Floor");
                audioList.Add(spotterNoPoints.GetComponent<AudioSource>());

                if (controller.getParameterSpotterDirection())
                {
                    GameObject spotterAimLower = GameObject.Find("Aim_Higher");
                    audioList.Add(spotterAimLower.GetComponent<AudioSource>());

                }
            }

            Debug.Log("FLOOR HIT");
            controller.addPoints(0);
        }
        else if (collidedWith == "Ceiling")
        {
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Hit_Ceiling");
                audioList.Add(spotterNoPoints.GetComponent<AudioSource>());

                if (controller.getParameterSpotterDirection())
                {
                    GameObject spotterAimLower = GameObject.Find("Aim_Lower");
                    audioList.Add(spotterAimLower.GetComponent<AudioSource>());
                }
            }

            Debug.Log("CEILING HIT");
            controller.addPoints(0);
        }
        else if (collidedWith == "TargetFirstRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("5_Points");
                    audioList.Add(spotter.GetComponent<AudioSource>());

                }
                else
                {
                    GameObject spotter = GameObject.Find("Hit_Center");
                    audioList.Add(spotter.GetComponent<AudioSource>());
                }


                if (controller.getTargetChangesAtFivePoints())
                {
                    GameObject spotter = GameObject.Find("Time_To_Change_TargetPos");
                    audioList.Add(spotter.GetComponent<AudioSource>());

                    FivePointsFlag = true;
                }
            }

            Debug.Log("5 POINTS!!!");
            controller.addPoints(5);
        }
        else if (collidedWith == "TargetSecondRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("4_Points");
                    audioList.Add(spotter.GetComponent<AudioSource>());
                }

                if (controller.getParameterSpotterQuadrants())
                {
                    int quadrant;

                    if (target.getWallSystem() == 1)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.z);
                    }
                    else if (target.getWallSystem() == 2)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.x);
                    }
                    else if (target.getWallSystem() == 3)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.z);
                    }
                    else if (target.getWallSystem() == 4)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.x);
                    }
                    else
                    {
                        quadrant = 1;
                    }

                    if (quadrant == 1)
                    {
                        GameObject spotter = GameObject.Find("First_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 2)
                    {
                        GameObject spotter = GameObject.Find("Fourth_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 3)
                    {
                        GameObject spotter = GameObject.Find("Third_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 4)
                    {
                        GameObject spotter = GameObject.Find("Second_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                }
            }

            Debug.Log("4 POINTS");
            controller.addPoints(4);
        }
        else if (collidedWith == "TargetThirdRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("3_Points");
                    audioList.Add(spotter.GetComponent<AudioSource>());
                }

                if (controller.getParameterSpotterQuadrants())
                {
                    int quadrant;

                    if (target.getWallSystem() == 1)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.z);
                    }
                    else if (target.getWallSystem() == 2)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.x);
                    }
                    else if (target.getWallSystem() == 3)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.z);
                    }
                    else if (target.getWallSystem() == 4)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.x);
                    }
                    else
                    {
                        quadrant = 1;
                    }

                    if (quadrant == 1)
                    {
                        GameObject spotter = GameObject.Find("First_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 2)
                    {
                        GameObject spotter = GameObject.Find("Fourth_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 3)
                    {
                        GameObject spotter = GameObject.Find("Third_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 4)
                    {
                        GameObject spotter = GameObject.Find("Second_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                }
            }

            Debug.Log("3 POINTS");
            controller.addPoints(3);
        }
        else if (collidedWith == "TargetForthRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("2_Points");
                    audioList.Add(spotter.GetComponent<AudioSource>());
                }

                if (controller.getParameterSpotterQuadrants())
                {
                    int quadrant;

                    if (target.getWallSystem() == 1)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.z);
                    }else if (target.getWallSystem() == 2)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.x);
                    }
                    else if (target.getWallSystem() == 3)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.z);
                    }
                    else if (target.getWallSystem() == 4)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.x);
                    }
                    else
                    {
                        quadrant = 1;
                    }

                    if (quadrant == 1)
                    {
                        GameObject spotter = GameObject.Find("First_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 2)
                    {
                        GameObject spotter = GameObject.Find("Fourth_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 3)
                    {
                        GameObject spotter = GameObject.Find("Third_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 4)
                    {
                        GameObject spotter = GameObject.Find("Second_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                }
            }

            Debug.Log("2 POINTS");
            controller.addPoints(2);
        }
        else if (collidedWith == "TargetFifthRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("1_Point");
                    audioList.Add(spotter.GetComponent<AudioSource>());
                }

                if (controller.getParameterSpotterQuadrants())
                {
                    int quadrant;

                    if (target.getWallSystem() == 1)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.z);
                    }
                    else if (target.getWallSystem() == 2)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.x);
                    }
                    else if (target.getWallSystem() == 3)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.z);
                    }
                    else if (target.getWallSystem() == 4)
                    {
                        quadrant = target.getHitQuadrant(transform.position.y, transform.position.x);
                    }
                    else
                    {
                        quadrant = 1;
                    }

                    if (quadrant == 1)
                    {
                        GameObject spotter = GameObject.Find("First_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 2)
                    {
                        GameObject spotter = GameObject.Find("Fourth_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 3)
                    {
                        GameObject spotter = GameObject.Find("Third_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else if (quadrant == 4)
                    {
                        GameObject spotter = GameObject.Find("Second_Quadrant_Clear");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                }
            }

            Debug.Log("1 POINTS");
            controller.addPoints(1);
        }


        windNavigatingSound.Stop();

        if (!isPlaying)
        {
            StartCoroutine(PlayAllAudioSources());
        }

        

        //REPLAY PORPUSE
        actionReplayArrow = GameObject.FindObjectOfType<ActionReplayArrow>();

        actionReplayArrow.alreadyHitTrigger();

        //keyControllers.enableButtonX();


        if (keyControllersRight != null)
        {
            //Debug.Log("A ENABLED");
            keyControllersRight.enableButtonA();
        }

        //keyControllers.enableButtonA();

        //CHANGE PERSPECTIVEEE (REMOVE TO GET ORIGINAL VERSION)
        if (modeSelected == 0)
        {
            changePerspectiveCounterTrigger = true;
        }

        rb.isKinematic = true;
        myCollider.isTrigger = true;

        GameObject arrow = Instantiate(stickingArrow);
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;

        //Debug.Log("ARROW: " +  transform.position);
        //Debug.Log("TARGET: " + target.transform.position);


        if (collision.collider.attachedRigidbody != null)
        {
            arrow.transform.parent = collision.collider.attachedRigidbody.transform;
        }

        collision.collider.GetComponent<IHittable>()?.GetHit();

        if (FivePointsFlag)
        {
            target.changeTargetPos();      //Por mais a frente para ser so dps dos audios
            FivePointsFlag = false;
        }
        //Destroy(gameObject);

    }

    private void FixedUpdate()
    {
        if (changePerspectiveCounterTrigger)
        {
            changePerspectiveCounter += 1;
            if (changePerspectiveCounter == 75)
            {
                controller.enableChange();
                controller.changePerspective();

                changePerspectiveCounter = 0;
                changePerspectiveCounterTrigger = false;
            }
        }
    }

    public void activatePerspectiveTrigger()
    {
        changePerspectiveCounterTrigger = true;
    }

    private IEnumerator PlayAllAudioSources()
    {
        isPlaying = true;

        // Create a copy of the audioList
        List<AudioSource> audioListCopy = new List<AudioSource>(audioList);

        foreach (AudioSource audioSource in audioListCopy)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        isPlaying = false;

        audioList.Clear();

        keyControllersRight.readyToShootTrue();
        keyControllersRight.reloadCrossbow();
    }

}
