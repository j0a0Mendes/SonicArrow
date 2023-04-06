using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterManager : MonoBehaviour
{
    // Start is called before the first frame update

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

    //------------------------------------------------
    //Hearing the target sound
    private bool previousTargetSound;
    private bool previousTargetSoundUserPos;
    private bool previousTargetSoundCrossbowAim;

    [SerializeField]
    public bool targetSound;

    [SerializeField]
    public bool targetSoundUserPos;                  //UNIQUE

    [SerializeField]
    public bool targetSoundCrossbowAim;             //UNIQUE

    //------------------------------------------------
    //Spotter Aid
    private bool previousSpotterTalking;
    private bool previousSpotterQuadrantAid;
    private bool previousSpotterPointsAid;
    private bool previousSpotterDirectionAid;

    [SerializeField]
    public bool spotterTalking = true; 

    [SerializeField]
    public bool spotterPointsAid = true;

    [SerializeField]
    public bool spotterQuadrantAid = false;

    [SerializeField]
    public bool spotterDirectionAid = false;

    //------------------------------------------------
    //Other sound aids

    [SerializeField]
    public bool whiteNoiseVerticalAid = false;

    //------------------------------------------------

    void Start()
    {
        changeOfPerspective = true;
        previousChangeOfPerspective = true;
        changeOfPerspectiveInstant = true;
        previousChangeOfPerspectiveInstant = true;

        targetSound = true;
        previousTargetSound = true;
        targetSoundUserPos = true;
        previousTargetSoundUserPos = true;
    }

    private void OnValidate()
    {
        //Change of perspective constraints 
        if(changeOfPerspective != previousChangeOfPerspective)
        {
            previousChangeOfPerspective = changeOfPerspective;

            if(changeOfPerspective)
            {
                changeOfPerspectiveInstant = true;
                previousChangeOfPerspectiveInstant = true;
                changeOfPerspectiveOnReplay = false;
                previousChangeOfPerspectiveOnReplay = false;
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
                targetSoundUserPos = true;
                previousTargetSoundUserPos = true;
                targetSoundCrossbowAim = false;
                previousTargetSoundCrossbowAim = false;
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
                spotterPointsAid = true;
                previousSpotterPointsAid = true;
                spotterDirectionAid = false;
                previousSpotterDirectionAid = false;
                spotterQuadrantAid = false;
                previousSpotterQuadrantAid = false;
            }
            else
            {
                spotterPointsAid = false;
                previousSpotterPointsAid = false;
                spotterDirectionAid = false;
                previousSpotterDirectionAid = false;
                spotterQuadrantAid = false;
                previousSpotterQuadrantAid = false;
            }
        }
 
        if (spotterTalking)
        {
            if (spotterPointsAid != previousSpotterPointsAid)
            {
                previousSpotterPointsAid = spotterPointsAid;

                if (spotterPointsAid)
                {
                    spotterDirectionAid = false;
                    previousSpotterDirectionAid = false;
                    spotterQuadrantAid = false;
                    previousSpotterQuadrantAid = false;
                }
                else
                {
                    spotterDirectionAid = true;
                    previousSpotterDirectionAid = true;
                    spotterQuadrantAid = false;
                    previousSpotterQuadrantAid = false;
                }

            }

            if (spotterDirectionAid != previousSpotterDirectionAid)
            {
                previousSpotterDirectionAid = spotterDirectionAid;

                if (spotterDirectionAid)
                {
                    spotterPointsAid = false;
                    previousSpotterPointsAid = false;
                    spotterQuadrantAid = false;
                    previousSpotterQuadrantAid = false;
                }
                else
                {
                    spotterPointsAid = false;
                    previousSpotterPointsAid = false;
                    spotterQuadrantAid = true;
                    previousSpotterQuadrantAid = true;
                }
            }

            if (spotterQuadrantAid != previousSpotterQuadrantAid)
            {
                previousSpotterQuadrantAid = spotterQuadrantAid;

                if (spotterQuadrantAid)
                {
                    spotterPointsAid = false;
                    previousSpotterPointsAid = false;
                    spotterDirectionAid = false;
                    previousSpotterDirectionAid = false;
                }
                else
                {
                    spotterPointsAid = true;
                    previousSpotterPointsAid = true;
                    spotterDirectionAid = false;
                    previousSpotterDirectionAid = false;
                }
            }


        }
        else
        {
            spotterPointsAid = false;
            previousSpotterPointsAid = false;
            spotterDirectionAid = false;
            previousSpotterDirectionAid = false;
            spotterQuadrantAid = false;
            previousSpotterQuadrantAid = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //--------CONSTRAINTS---------
        //Change of perspective method
        

        //Audio Parameters
        if (targetSound)
        {
            if (targetSoundUserPos)
            {
                targetSoundCrossbowAim = false;
            }
            
            if(targetSoundCrossbowAim)
            {
                targetSoundUserPos = false;
            }
        }
        else
        {
            targetSoundCrossbowAim = false;
            targetSoundUserPos = false;
        }

        //Spotter talking parameters
        if (spotterTalking)
        {
            if (spotterPointsAid)
            {
                spotterQuadrantAid = false;
                spotterDirectionAid = false;
            }
            else if(spotterDirectionAid)
            {
                spotterPointsAid = false;
                spotterQuadrantAid = false;
            }
            else
            {
                spotterQuadrantAid = true;
                spotterDirectionAid = false;
                spotterPointsAid = false;
            }
        }
        else
        {
            spotterQuadrantAid = false;
            spotterDirectionAid = false;
            spotterPointsAid = false;
        }
        //-----END-OF-CONSTRAINTS------

    }

    //Getters
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

    //Setters

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
}
