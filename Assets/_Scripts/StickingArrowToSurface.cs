using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private KeyControllers keyControllersrRight;    //RIGHT HAND ONLY

    private int changePerspectiveCounter;
    private bool changePerspectiveCounterTrigger;
    //private bool notFlying = false;

    private int modeSelected;

    //TARGET OBJECT 
    TargetObject target;

    //PLAYING AUDIOS
    public AudioClip[] clips;

    private List<AudioSource> audioSources;
    private int currentIndexPlaying = 0;

    private int currentIndexAdding = 0;


    private void Start() 
    { 
        windNavigatingSound.Play();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
        keyControllers = GameObject.FindObjectOfType<KeyControllers>();

        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = rightHand.GetComponent<KeyControllers>();

        target = GameObject.FindObjectOfType<TargetObject>();
        //keyControllerRightHand = GameObject.FindObjectOfType<KeyControllerSupport>();

        audioSources = new List<AudioSource>();
    }

    private void Update() 
    {
        //    if (notFlying)
        //
        //        windNavigatingSound.Stop();
        //    }   
        modeSelected = controller.getModeSelected();
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("ACERTOU");
        keyControllers.enableButtonX();
        modeSelected = controller.getModeSelected();
        //Debug.Log(modeSelected);
        //Debug.Log(collision.gameObject.tag);
        //Debug.Log(collision.gameObject.tag == "Untagged");
        string collidedWith = collision.gameObject.tag;
        if (collidedWith == "WallFirstLayer" || collidedWith == "WallSecondLayer" || collidedWith == "WallThirdLayer" || collidedWith == "WallForthLayer" || collidedWith == "WallFifthLayer")
        {
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Hit_A_Wall");
                spotterNoPoints.GetComponent<AudioSource>().Play();
            }

            controller.addPoints(0);
        }else if (collidedWith == "Floor" )
        {
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Hit_Floor");
                spotterNoPoints.GetComponent<AudioSource>().Play();
            }

            controller.addPoints(0);
        }
        else if (collidedWith == "Ceiling")
        {
            if (controller.getParameterSpotterTalking())
            {
                GameObject spotterNoPoints = GameObject.Find("Hit_Ceiling");
                spotterNoPoints.GetComponent<AudioSource>().Play();

                GameObject spotterAimLower = GameObject.Find("Aim_Lower");
                spotterAimLower.GetComponent<AudioSource>().Play();
            }

            controller.addPoints(0);
        }
        else if (collidedWith == "TargetFirstRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("5_Points");
                    addAudio(spotter.GetComponent<AudioSource>());

                }else
                {
                    GameObject spotter = GameObject.Find("Hit_Center");
                    addAudio(spotter.GetComponent<AudioSource>());
                }

                if (controller.getTargetChangesAtFivePoints())
                {
                    GameObject spotter = GameObject.Find("Time_To_Change_TargetPos");
                    addAudio(spotter.GetComponent<AudioSource>());

                    target.changeTargetPos();      //Por mais a frente para ser so dps dos audios

                }
            }
            controller.addPoints(5);
        }
        else if (collidedWith == "TargetSecondRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("4_Points");
                    spotter.GetComponent<AudioSource>().Play();
                }
            }

            controller.addPoints(4);
        }
        else if (collidedWith == "TargetThirdRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("3_Points");
                    spotter.GetComponent<AudioSource>().Play();
                }
            }

            controller.addPoints(3);
        }
        else if (collidedWith == "TargetForthRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("2_Points");
                    spotter.GetComponent<AudioSource>().Play();
                }
            }

            controller.addPoints(2);
        }
        else if (collidedWith == "TargetFifthRegion")
        {
            if (controller.getParameterSpotterTalking())
            {
                if (controller.getParameterSpotterPoints())
                {
                    GameObject spotter = GameObject.Find("1_Point");
                    spotter.GetComponent<AudioSource>().Play();
                }
            }

            controller.addPoints(1);
        }


        windNavigatingSound.Stop();

        PlayAllSounds();        //play all the sounds registered

        //REPLAY PORPUSE
        actionReplayArrow = GameObject.FindObjectOfType<ActionReplayArrow>();
       
        actionReplayArrow.alreadyHitTrigger();

        //keyControllers.enableButtonX();

        
        if(keyControllersrRight != null)
        {
            //Debug.Log("A ENABLED");
            keyControllersrRight.enableButtonA();
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

    public void activatePerspectiveTrigger()
    {
        changePerspectiveCounterTrigger = true;
    }
    // Get references to the AudioSources on other objects
    //audioSources = new List<AudioSource>();
    //audioSources.Add(GameObject.Find("AudioSource1").GetComponent<AudioSource>());
    //audioSources.Add(GameObject.Find("AudioSource2").GetComponent<AudioSource>());
    // Add more AudioSources to the list as needed...

    // Play all sounds
    //StartCoroutine(PlayAllSounds());
    IEnumerator PlayAllSounds()
    {
        foreach (AudioClip clip in clips)
        {
            // Play the current sound on the next available AudioSource
            audioSources[currentIndexPlaying % audioSources.Count].clip = clip;
            audioSources[currentIndexPlaying % audioSources.Count].Play();
            currentIndexPlaying++;

            // Wait for the clip to finish playing before playing the next one
            while (audioSources[currentIndexPlaying % audioSources.Count].isPlaying)
            {
                yield return null;
            }
        }
    }

    public void addAudio(AudioSource audio)
    {
        audioSources.Add(audio);
    }
}
