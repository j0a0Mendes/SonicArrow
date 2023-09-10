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

    private BowStringController bowStringController;

    //PARAMETER MANAGEMENT 
    private ParameterManager parameterManager;

    private SessionManager sessionManager;

    [SerializeField]
    public int shotWithoutHit = 0;

    public bool endPhaseFlag;

    private AudioSource endPhaseSound;

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

        bowStringController = GameObject.FindObjectOfType<BowStringController>();

        parameterManager = GameObject.FindObjectOfType<ParameterManager>();

        sessionManager = GameObject.FindObjectOfType<SessionManager>();

        endPhaseSound = GameObject.Find("EndPhase").GetComponent<AudioSource>();

    }

    public void updateArrowTargetCoords()
    {
        // Get the x and y coordinates of the arrow
        float arrowX = transform.position.z;
        float arrowY = transform.position.y;

        // Create a string in the format "(x, y)"
        string arrowCoords = string.Format("({0}, {1})", arrowX.ToString("F2"), arrowY.ToString("F2"));

        parameterManager.arrowCoords(arrowCoords);


        // Get the x and y coordinates of the target
        float targetX = target.transform.position.z;
        float targetY = target.transform.position.y;
 

        // Create a string in the format "(x, y)"
        string targetCoords = string.Format("({0}, {1})", targetX.ToString("F2"), targetY.ToString("F2"));

        parameterManager.targetCoords(targetCoords);
    }

    private void OnCollisionEnter(Collision collision)
    {
        sessionManager.addShotNumber();

        target.activateCanMove();
        controller.setIsTalking(true);

        updateArrowTargetCoords();

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
        if (collidedWith == "FrontWall")
        {
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Hit_A_Wall");
                audioList.Add(spotterNoPoints.GetComponent<AudioSource>());
            }

            controller.addPoints(0);

            parameterManager.updatePoints("TargetWall Hit");
            controller.addShotWithoutHit();
            //Debug.Log("TARGET WALL HIT");

        }else if (collidedWith == "BackWall")
        {
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Wall_Paralel");
                audioList.Add(spotterNoPoints.GetComponent<AudioSource>());
            }

            controller.addPoints(0);

            parameterManager.updatePoints("BackWall Hit");
            controller.addShotWithoutHit();
            //Debug.Log("BACK WALL HIT");
        }
        else if (collidedWith == "LeftWall")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (parameterManager.getFirstCondition() | parameterManager.getThirdCondition())
                {
                    GameObject spotterNoPoints = GameObject.Find("Wall_Direita");
                    audioList.Add(spotterNoPoints.GetComponent<AudioSource>());
                }
                else
                {
                    GameObject spotterNoPoints = GameObject.Find("Wall_Esquerda");
                    audioList.Add(spotterNoPoints.GetComponent<AudioSource>());
                }
                
            }

            controller.addPoints(0);

            parameterManager.updatePoints("LeftWall Hit");
            controller.addShotWithoutHit();
            //Debug.Log("LEFT WALL HIT");
        }
        else if (collidedWith == "RightWall")
        {
            if (parameterManager.getFirstCondition() | parameterManager.getThirdCondition())
            {
                GameObject spotterNoPoints = GameObject.Find("Wall_Esquerda");
                audioList.Add(spotterNoPoints.GetComponent<AudioSource>());
            }
            else
            {
                GameObject spotterNoPoints = GameObject.Find("Wall_Direita");
                audioList.Add(spotterNoPoints.GetComponent<AudioSource>());
            }

            controller.addPoints(0);

            parameterManager.updatePoints("RightWall Hit");
            controller.addShotWithoutHit();
            //Debug.Log("RIGHT WALL HIT");
        }
        else if (collidedWith == "Floor")
        {
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Hit_Floor");
                audioList.Add(spotterNoPoints.GetComponent<AudioSource>());
            }

            parameterManager.updatePoints("Floor Hit");
            controller.addShotWithoutHit();
            //Debug.Log("FLOOR HIT");
            controller.addPoints(0);
        }
        else if (collidedWith == "Ceiling")
        {
            controller.addShotWithoutHit();
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Hit_Ceiling");
                audioList.Add(spotterNoPoints.GetComponent<AudioSource>());
            }

            parameterManager.updatePoints("Ceiling Hit");
            //Debug.Log("CEILING HIT");
            controller.addPoints(0);
        }
        else if (collidedWith == "TargetFirstRegion")
        {
            controller.resetShotWithoutHit();
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    if(controller.getTargetChangesAtFivePoints())
                    {
                        GameObject spotter = GameObject.Find("5_Points_Change");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else
                    {
                        GameObject spotter = GameObject.Find("5_Points");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    

                }
                else
                {
                    GameObject spotter = GameObject.Find("Hit_Center");
                    audioList.Add(spotter.GetComponent<AudioSource>());
                }


                if (controller.getTargetChangesAtFivePoints())
                {
                    FivePointsFlag = true;
                }
            }

            
            parameterManager.updatePoints("5 Points");
            //Debug.Log("5 POINTS!!!");
            controller.addPoints(5);
            sessionManager.addPoints(5);
        }
        else if (collidedWith == "TargetSecondRegion")
        {
            controller.resetShotWithoutHit();
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    if (controller.getTargetChangesAtFivePoints())
                    {
                        GameObject spotter = GameObject.Find("4_Points_Change");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else
                    {
                        GameObject spotter = GameObject.Find("4_Points");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
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

                if (controller.getTargetChangesAtFivePoints())
                {
                    FivePointsFlag = true;
                }
            }

            parameterManager.updatePoints("4 Points");
            //Debug.Log("4 POINTS");
            controller.addPoints(4);
            sessionManager.addPoints(4);
        }
        else if (collidedWith == "TargetThirdRegion")
        {
            controller.resetShotWithoutHit();
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    if (controller.getTargetChangesAtFivePoints())
                    {
                        GameObject spotter = GameObject.Find("3_Points_Change");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else
                    {
                        GameObject spotter = GameObject.Find("3_Points");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
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

                if (controller.getTargetChangesAtFivePoints())
                {
                    FivePointsFlag = true;
                }
            }

            parameterManager.updatePoints("3 Points");
            //Debug.Log("3 POINTS");
            controller.addPoints(3);
            sessionManager.addPoints(3);
        }
        else if (collidedWith == "TargetForthRegion")
        {
            controller.resetShotWithoutHit();
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    if (controller.getTargetChangesAtFivePoints())
                    {
                        GameObject spotter = GameObject.Find("2_Points_Change");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else
                    {
                        GameObject spotter = GameObject.Find("2_Points");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
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

                if (controller.getTargetChangesAtFivePoints())
                {
                    FivePointsFlag = true;
                }
            }

            parameterManager.updatePoints("2 Points");
            //Debug.Log("2 POINTS");
            controller.addPoints(2);
            sessionManager.addPoints(2);
        }
        else if (collidedWith == "TargetFifthRegion")
        {
            controller.resetShotWithoutHit();
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    if (controller.getTargetChangesAtFivePoints())
                    {
                        Debug.Log("change");
                        GameObject spotter = GameObject.Find("1_Point_Change");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
                    else
                    {
                        GameObject spotter = GameObject.Find("1_Point");
                        audioList.Add(spotter.GetComponent<AudioSource>());
                    }
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

                if (controller.getTargetChangesAtFivePoints())
                {
                    FivePointsFlag = true;
                }
            }

            parameterManager.updatePoints("1 Point");
            //Debug.Log("1 POINT");
            controller.addPoints(1);
            sessionManager.addPoints(1);
        }

        if (controller.getChangeTarget())
        {
            GameObject spotter = GameObject.Find("Timeout");
            audioList.Add(spotter.GetComponent<AudioSource>());
            target.relocateTarget();
        }

        //LOG CREATION
        parameterManager.makeLog();

        windNavigatingSound.Stop();

        if (endPhaseFlag)
        {
            endPhaseFlag = false;
            audioList.Add(endPhaseSound);
        }

        if (!isPlaying)
        {
            StartCoroutine(PlayAllAudioSources());
        }

        

        //REPLAY PORPUSE
        //actionReplayArrow = GameObject.FindObjectOfType<ActionReplayArrow>();

        //actionReplayArrow.alreadyHitTrigger();

        //keyControllers.enableButtonX();


        if (keyControllersRight != null)
        {
            //Debug.Log("A ENABLED");
            keyControllersRight.enableButtonA();
        }

        //keyControllers.enableButtonA();

        //CHANGE PERSPECTIVEEE (REMOVE TO GET ORIGINAL VERSION)
        /**if (modeSelected == 0)
        {
            changePerspectiveCounterTrigger = true;
        }*/

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

        //MAYBE?---------------------------------
        keyControllersRight.activateCanPlayTargetSound();
        keyControllersRight.readyToShootTrue();
        keyControllersRight.reloadCrossbow();
        //MAYBE?---------------------------------
        //Destroy(gameObject);

        

    }

    private void FixedUpdate()
    {
        modeSelected = controller.getModeSelected();

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
        //Debug.Log("Audios Start");

        // Create a copy of the audioList
        List<AudioSource> audioListCopy = new List<AudioSource>(audioList);

        foreach (AudioSource audioSource in audioListCopy)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        isPlaying = false;

        audioList.Clear();
        
    }

    public void activateEndPhaseFlag()
    {
        endPhaseFlag = true;
    }
}
