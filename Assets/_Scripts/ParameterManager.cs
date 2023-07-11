using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParameterManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public int userID = 0;

    private string pointsLabel;

    //Change of perspective

    private bool previousChangeOfPerspective;
    private bool previousChangeOfPerspectiveInstant;
    private bool previousChangeOfPerspectiveOnReplay;


    [SerializeField]
    public bool changeOfPerspective;

    [SerializeField]
    public bool changeOfPerspectiveInstant;          //UNIQUE



    [SerializeField]
    public bool changeOfPerspectiveOnReplay;        //UNIQUE

    public string separador1;

    //------------------------------------------------
    //Hearing the target sound
    private bool previousTargetSound;
    private bool previousTargetSoundUserPos;
    private bool previousTargetSoundCrossbowAim;

    [SerializeField]
    private bool targetSound;

    [SerializeField]
    private bool targetSoundUserPos;                  //UNIQUE

    [SerializeField]
    private bool targetSoundCrossbowAim;             //UNIQUE

    public string separador2;

    //------------------------------------------------
    //Spotter Aid
    private bool previousSpotterTalking;
    private bool previousSpotterQuadrantAid;
    private bool previousSpotterPointsAid;
    private bool previousSpotterDirectionAid;

    [SerializeField]
    public bool spotterTalking;

    [SerializeField]
    public bool spotterPointsAid;

    [SerializeField]
    public bool spotterQuadrantAid;

    //[SerializeField]
    private bool spotterDirectionAid;

    [SerializeField]
    public bool spotterBeepAid;

    public string separador3;

    //------------------------------------------------
    //Other sound aids

    [SerializeField]
    public bool whiteNoiseVerticalAid;

    //------------------------------------------------
    //Other haptic aids

    [SerializeField]
    public bool hapticOnTargetHover;

    public string separador4;

    //------------------------------------------------
    //Target Movement
    private bool previousTargetStill;
    private bool previousTargetMoving;
    private bool previousTargetChangesAtFivePoints;

    [SerializeField]
    public bool targetStill;

    [SerializeField]
    public bool targetChangesAtFivePoints;

    [SerializeField]
    public bool targetMoving;

    [SerializeField]
    public string separador5;

    [SerializeField]
    public bool activateConditions;

    [SerializeField]
    public bool firstCondition;

    [SerializeField]
    public bool secondCondition;

    [SerializeField]
    public bool thirdCondition;

    private bool previousActivateConditions;
    private bool previousFirstCondition;
    private bool previousSecondCondition;
    private bool previousThirdCondition;

    //AudioListener position
    private AudioListener listenerTarget;

    private AudioListener listenerUser;

    [SerializeField]
    public GameObject centerTarget;

    private LogManager logManager;

    private void Start()
    {
        listenerUser = Camera.main.GetComponent<AudioListener>(); // Get the AudioListener component
        listenerTarget = centerTarget.GetComponent<AudioListener>();

        logManager = GameObject.FindObjectOfType<LogManager>().GetComponent<LogManager>();

        //listener = FindObjectOfType<AudioListener>();

        //PROTOTYPE REASONS, FORCE LOAD CONDITION
        firstCondition = true;
        secondCondition = false;
        thirdCondition = false;

        if (activateConditions)
        {
            if (firstCondition)
            {
                GameObject intro = GameObject.Find("Hi_Im_Tom_1_Condition");
                intro.GetComponent<AudioSource>().Play();
            }
            else if (secondCondition)
            {
                GameObject intro = GameObject.Find("Hi_Im_Tom_2_Condition");
                intro.GetComponent<AudioSource>().Play();
            }
            else if (thirdCondition)
            {
                GameObject intro = GameObject.Find("Hi_Im_Tom_3_Condition");
                intro.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GameObject intro = GameObject.Find("Hi_Im_Tom");
            intro.GetComponent<AudioSource>().Play();
        }

        clearParameters();
        secondCondition = false;
        previousSecondCondition = false;
        thirdCondition = false;
        previousThirdCondition = false;

        //before shot
        targetSound = true;
        targetSoundUserPos = true;
        targetSoundCrossbowAim = false;
        spotterBeepAid = false;
        targetMoving = false;
        targetChangesAtFivePoints = false;
        targetStill = true;
        whiteNoiseVerticalAid = true;

        //after shot
        changeOfPerspective = true;
        changeOfPerspectiveInstant = true;
        spotterTalking = true;
        spotterPointsAid = true;
        spotterQuadrantAid = false;

    }

    private void OnValidate()
    {
        if (activateConditions != previousActivateConditions)
        {
            previousActivateConditions = activateConditions;

            if (!activateConditions)
            {
                firstCondition = false;
                previousFirstCondition = false;
                secondCondition = false;
                previousSecondCondition = false;
                thirdCondition = false;
                previousThirdCondition = false;
            }
        }

        if (activateConditions)
        {
            if (firstCondition != previousFirstCondition)
            {
                previousFirstCondition = firstCondition;

                if (firstCondition)
                {
                    Debug.Log("First Condition");
                    clearParameters();
                    secondCondition = false;
                    previousSecondCondition = false;
                    thirdCondition = false;
                    previousThirdCondition = false;

                    //before shot
                    targetSound = true;
                    targetSoundUserPos = true;

                    //after shot
                    changeOfPerspective = false;
                    changeOfPerspectiveInstant = false;
                    spotterTalking = true;
                    spotterPointsAid = true;
                    spotterQuadrantAid = false;
                }
                else
                {
                    secondCondition = true;

                    thirdCondition = false;
                    previousThirdCondition = false;
                }
            }

            if (secondCondition != previousSecondCondition)
            {
                previousSecondCondition = secondCondition;

                if (secondCondition)
                {
                    Debug.Log("Second Condition");
                    clearParameters();
                    firstCondition = false;
                    previousFirstCondition = false;
                    thirdCondition = false;
                    previousThirdCondition = false;


                    //before shot
                    targetSound = true;
                    targetSoundCrossbowAim = true;

                    //after shot
                    changeOfPerspective = false;
                    changeOfPerspectiveInstant = false;
                    spotterTalking = true;
                    spotterPointsAid = true;
                    spotterQuadrantAid = false;
                }
                else
                {
                    firstCondition = false;
                    previousFirstCondition = false;
                    thirdCondition = true;

                }

            }

            if (thirdCondition != previousThirdCondition)
            {
                previousThirdCondition = thirdCondition;

                if (thirdCondition)
                {
                    Debug.Log("Third Condition");
                    clearParameters();
                    firstCondition = false;
                    previousFirstCondition = false;
                    secondCondition = false;
                    previousSecondCondition = false;

                    //before shot
                    spotterBeepAid = true;
                    targetSound = true;
                    targetSoundUserPos = true;

                    //after shot
                    spotterTalking = true;
                    spotterPointsAid = true;
                    spotterQuadrantAid = false;
                }
                else
                {
                    firstCondition = true;

                    secondCondition = false;
                    previousSecondCondition = false;
                }
            }
        }
        else
        {
            firstCondition = false;
            previousFirstCondition = false;
            secondCondition = false;
            previousSecondCondition = false;
            thirdCondition = false;
            previousThirdCondition = false;
        }


        //Change of perspective constraints 
        if (changeOfPerspective != previousChangeOfPerspective)
        {
            previousChangeOfPerspective = changeOfPerspective;

            if (changeOfPerspective)
            {

            }
            else
            {
                changeOfPerspectiveInstant = false;
                previousChangeOfPerspectiveInstant = false;
                changeOfPerspectiveOnReplay = false;
                previousChangeOfPerspectiveOnReplay = false;
            }
        }

        if (changeOfPerspective)
        {
            if (changeOfPerspectiveInstant != previousChangeOfPerspectiveInstant)
            {
                previousChangeOfPerspectiveInstant = changeOfPerspectiveInstant;

                if (changeOfPerspectiveInstant)
                {
                    changeOfPerspectiveOnReplay = false;
                    previousChangeOfPerspectiveOnReplay = false;
                }
                else
                {
                    changeOfPerspectiveOnReplay = true;
                    previousChangeOfPerspectiveOnReplay = true;
                }
            }

            if (changeOfPerspectiveOnReplay != previousChangeOfPerspectiveOnReplay)
            {
                previousChangeOfPerspectiveOnReplay = changeOfPerspectiveOnReplay;

                if (changeOfPerspectiveOnReplay)
                {
                    changeOfPerspectiveInstant = false;
                    previousChangeOfPerspectiveInstant = false;
                }
                else
                {
                    changeOfPerspectiveInstant = true;
                    previousChangeOfPerspectiveInstant = true;
                }
            }
        }
        else
        {
            changeOfPerspectiveInstant = false;
            previousChangeOfPerspectiveInstant = false;
            changeOfPerspectiveOnReplay = false;
            previousChangeOfPerspectiveOnReplay = false;
        }

        //Audio constraints--------------------------------------

        if (targetSound != previousTargetSound)
        {
            previousTargetSound = targetSound;

            if (targetSound)
            {
            }
            else
            {
                targetSoundUserPos = false;
                previousTargetSoundUserPos = false;
                targetSoundCrossbowAim = false;
                previousTargetSoundCrossbowAim = false;
            }
        }


        if (targetSound)
        {
            if (targetSoundUserPos != previousTargetSoundUserPos)
            {
                previousTargetSoundUserPos = targetSoundUserPos;

                if (targetSoundUserPos)
                {
                    targetSoundCrossbowAim = false;
                    previousTargetSoundCrossbowAim = false;
                }
                else
                {
                    targetSoundCrossbowAim = true;
                    previousTargetSoundCrossbowAim = true;
                }

            }

            if (targetSoundCrossbowAim != previousTargetSoundCrossbowAim)
            {
                previousTargetSoundCrossbowAim = targetSoundCrossbowAim;

                if (targetSoundCrossbowAim)
                {
                    targetSoundUserPos = false;
                    previousTargetSoundUserPos = false;
                }
                else
                {
                    targetSoundUserPos = true;
                    previousTargetSoundUserPos = true;
                }
            }
        }
        else
        {
            targetSoundUserPos = false;
            previousTargetSoundUserPos = false;
            targetSoundCrossbowAim = false;
            previousTargetSoundCrossbowAim = false;
        }

        //Spotter constraints--------------------------------------


        if (spotterTalking != previousSpotterTalking)
        {
            previousSpotterTalking = spotterTalking;
            if (spotterTalking)
            {
                //spotterPointsAid = true;
                //spotterDirectionAid = false;
                //spotterQuadrantAid = false;
            }
            else
            {
                spotterPointsAid = false;
                spotterDirectionAid = false;
                spotterQuadrantAid = false;
            }
        }

        if (spotterTalking == true && spotterPointsAid == false && spotterDirectionAid == false && spotterQuadrantAid == false)
        {
            spotterPointsAid = true;
            previousSpotterPointsAid = true;
        }

        //Target Movement constraints-------------------------------
        if (targetStill != previousTargetStill)
        {
            previousTargetStill = targetStill;
            if (targetStill)
            {
                //targetChangesAtFivePoints = false;
                //previousTargetChangesAtFivePoints = false;
                targetMoving = false;
                previousTargetMoving = false;
            }
            else
            {
                //targetChangesAtFivePoints = true;
                //previousTargetChangesAtFivePoints = true;
                targetMoving = true;
                previousTargetMoving = true;
            }
        }

        /*if (targetChangesAtFivePoints != previousTargetChangesAtFivePoints)
        {
            previousTargetChangesAtFivePoints = targetChangesAtFivePoints;
            if (targetChangesAtFivePoints)
            {
                targetStill = false;
                previousTargetStill = false;
                targetMoving = false;
                previousTargetMoving = false;
            }
            else
            {
                targetStill = true;
                previousTargetStill = true;
                targetMoving = false;
                previousTargetMoving = false;
            }
        }*/

        if (targetMoving != previousTargetMoving)
        {
            previousTargetMoving = targetMoving;
            if (targetMoving)
            {
                targetStill = false;
                previousTargetStill = false;
                //targetChangesAtFivePoints = false;
                //previousTargetChangesAtFivePoints = false;
            }
            else
            {
                targetStill = true;
                previousTargetStill = true;
                //targetChangesAtFivePoints = false;
                //previousTargetChangesAtFivePoints = false;
            }
        }
    }

    private bool conditionChanged;
    // Update is called once per frame
    void Update()
    {
        if (conditionChanged)
        {
            if (firstCondition)
            {
                Debug.Log("First Condition");
                clearParameters();
                secondCondition = false;
                previousSecondCondition = false;
                thirdCondition = false;
                previousThirdCondition = false;

                //before shot
                targetSound = true;
                targetSoundUserPos = true;
                targetSoundCrossbowAim = false;
                spotterBeepAid = false;
                targetStill = true;
                targetMoving = false;
                targetChangesAtFivePoints = true;
                whiteNoiseVerticalAid = true;

                //after shot
                changeOfPerspective = false;
                changeOfPerspectiveInstant = false;
                spotterTalking = true;
                spotterPointsAid = true;
                spotterQuadrantAid = false;
            }
            else if (secondCondition)
            {
                Debug.Log("Second Condition");
                clearParameters();
                firstCondition = false;
                previousFirstCondition = false;
                thirdCondition = false;
                previousThirdCondition = false;


                //before shot
                targetSound = true;
                targetSoundUserPos = false;
                targetSoundCrossbowAim = true;
                targetStill = true;
                targetMoving = false;
                targetChangesAtFivePoints = true;
                whiteNoiseVerticalAid = true;


                //after shot
                changeOfPerspective = false;
                changeOfPerspectiveInstant = false;
                spotterTalking = true;
                spotterPointsAid = true;
                spotterQuadrantAid = false;

            }
            else if (thirdCondition)
            {
                Debug.Log("Third Condition");
                clearParameters();
                firstCondition = false;
                previousFirstCondition = false;
                secondCondition = false;
                previousSecondCondition = false;

                //before shot
                targetSound = true;
                targetSoundUserPos = true;
                targetSoundCrossbowAim = false;
                whiteNoiseVerticalAid = true;

                targetStill = true;
                targetMoving = false;
                targetChangesAtFivePoints = true;
                spotterBeepAid = true;


                //after shot
                changeOfPerspective = false;
                changeOfPerspectiveInstant = false;
                spotterTalking = true;
                spotterPointsAid = true;
                spotterQuadrantAid = false;
            }
        }

        //AUDIOLISTENER
        if (targetSoundCrossbowAim)
        {
            listenerTarget.enabled = true;      //AIMPOS
            listenerUser.enabled = false;

        }
        else if (targetSoundUserPos)
        {
            listenerTarget.enabled = false;     //USERPOS
            listenerUser.enabled = true;
        }

        conditionChanged = false;
    }


    public void clearParameters()
    {
        changeOfPerspective = false;
        targetSound = false;
        spotterTalking = false;
        spotterBeepAid = false;
        //hapticOnTargetHover = false;
        //targetStill = true;
    }

    //Getters
    //Perspective 

    public int getUserId()
    {
        return userID;
    }

    public bool getChangeOfPerspective()
    {
        return changeOfPerspective;
    }

    public bool getChangeOfPerspectiveInstant()
    {
        return changeOfPerspectiveInstant;
    }

    public bool getChangeOfPerspectiveOnReplay()
    {
        return changeOfPerspectiveOnReplay;
    }

    //--------------------------------------
    //Hearing the target sound
    public bool getTargetSound()
    {
        return targetSound;
    }
    public bool getTargetSoundUserPos()
    {
        return targetSoundUserPos;
    }                 //UNIQUE
    public bool getTargetSoundCrossbowAim()
    {
        return targetSoundCrossbowAim;
    }

    //--------------------------------------
    //Spotter aid
    public bool getSpotterTalking()
    {
        return spotterTalking;
    }
    public bool getSpotterPointsAid()
    {
        return spotterPointsAid;
    }
    public bool getSpotterQuadrantAid()
    {
        return spotterQuadrantAid;
    }
    public bool getSpotterDirectionAid()
    {
        return spotterDirectionAid;
    }

    public bool getSpotterBeepAid()
    {
        return spotterBeepAid;
    }

    //--------------------------------------
    //Other sound aids
    public bool getWhiteNoiseVerticalAid()
    {
        return whiteNoiseVerticalAid;
    }

    //--------------------------------------
    //Other haptic aids
    public bool getHapticOnTargetHover()
    {
        return hapticOnTargetHover;
    }
    //--------------------------------------
    //Target movement

    public bool getTargetStill()
    {
        return targetStill;
    }
    public bool getTargetMoving()
    {
        return targetMoving;
    }
    public bool getTargetChangesAtFivePoints()
    {
        return targetChangesAtFivePoints;
    }

    public bool getFirstCondition()
    {
        return firstCondition;
    }

    public bool getSecondCondition()
    {
        return secondCondition;
    }

    public bool getThirdCondition()
    {
        return thirdCondition;
    }

    //--------------------------------------
    //--------------------------------------

    //SETTERS
    //Perspective
    public void setChangeOfPerspective(bool val)
    {
        changeOfPerspective = val;
    }

    public void setChangeOfPerspectiveInstant(bool val)
    {
        changeOfPerspectiveInstant = val;
    }

    public void setChangeOfPerspectiveOnReplay(bool val)
    {
        changeOfPerspectiveOnReplay = val;
    }

    //--------------------------------------
    //Hearing the target sound
    public void setTargetSound(bool val)
    {
        targetSound = val;
    }
    public void setTargetSoundUserPos(bool val)
    {
        targetSoundUserPos = val;
    }
    public void setTargetSoundCrossbowAim(bool val)
    {
        targetSoundCrossbowAim = val;
    }

    //--------------------------------------
    //Spotter aid
    public void setSpotterTalking(bool val)
    {
        spotterTalking = val;
    }
    public void setSpotterPointsAid(bool val)
    {
        spotterPointsAid = val;
    }
    public void setSpotterQuadrantAid(bool val)
    {
        spotterQuadrantAid = val;
    }
    public void setSpotterDirectionAid(bool val)
    {
        spotterDirectionAid = val;
    }


    //--------------------------------------
    //Other sound aids
    public void setWhiteNoiseVerticalAid(bool val)
    {
        whiteNoiseVerticalAid = val;
    }

    //--------------------------------------
    //Other haptic aids
    public void setHapticOnTargetHover(bool val)
    {
        hapticOnTargetHover = val;
    }

    //--------------------------------------
    //Target movement

    public void setTargetStill(bool val)
    {
        targetStill = val;
    }
    public void setTargetMoving(bool val)
    {
        targetMoving = val;
    }
    public void setTargetChangesAtFivePoints(bool val)
    {
        targetChangesAtFivePoints = val;
    }

    //--------------------------------------

    public void selectNextCondition()
    {
        if (activateConditions)
        {
            if (firstCondition)
            {
                firstCondition = false;
                secondCondition = true;
            }
            else if (secondCondition)
            {
                secondCondition = false;
                thirdCondition = true;
            }
            else if (thirdCondition)
            {
                thirdCondition = false;
                firstCondition = true;
            }
        }

        if (firstCondition)
        {
            GameObject intro = GameObject.Find("Hi_Im_Tom_1_Condition");
            intro.GetComponent<AudioSource>().Play();
        }
        else if (secondCondition)
        {
            GameObject intro = GameObject.Find("Hi_Im_Tom_2_Condition");
            intro.GetComponent<AudioSource>().Play();
        }
        else if (thirdCondition)
        {
            GameObject intro = GameObject.Find("Hi_Im_Tom_3_Condition");
            intro.GetComponent<AudioSource>().Play();
        }

        conditionChanged = true;
    }

    public void updatePoints(string pointsMade)
    {
        pointsLabel = pointsMade;
    }

    public void makeLog()
    {
        string targetMovement = "Target Still and Changes On Hit";
        string condition = "condition 0";

        if (firstCondition)
        {
            condition = "Condition 1";
        }
        else if (secondCondition)
        {
            condition = "Condition 2";
        }
        else if (thirdCondition)
        {
            condition = "Condition 3";
        }

        if (targetMoving)
        {
            if (targetChangesAtFivePoints)
            {
                targetMovement = "Target Moving and Changes On Hit";
            }
            else
            {
                targetMovement = "Target Moving";
            }
        }
        else if (targetStill)
        {
            if (targetChangesAtFivePoints)
            {
                targetMovement = "Target Still and Changes On Hit";
            }
            else
            {
                targetMovement = "Target Still";
            }
        }

        logManager.Log("User " + userID.ToString(), condition, targetMovement, pointsLabel);

    }
}
