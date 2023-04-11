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
    //Other haptic aids

    [SerializeField]
    public bool hapticOnTargetHover = false;

    //------------------------------------------------
    //Target Movement
    private bool previousTargetStill = true;
    private bool previousTargetMoving = false;
    private bool previousTargetChangesAtFivePoints = false;

    [SerializeField]
    public bool targetStill = true;

    [SerializeField]
    public bool targetChangesAtFivePoints = false;

    [SerializeField]
    public bool targetMoving = false;



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
                spotterDirectionAid = false;
                spotterQuadrantAid = false;
            }
            else
            {
                spotterPointsAid = false;
                spotterDirectionAid = false;
                spotterQuadrantAid = false;
            }
        }

        if(spotterTalking == true && spotterPointsAid == false && spotterDirectionAid== false && spotterQuadrantAid == false)
        {
            spotterPointsAid = true;
            previousSpotterPointsAid = true;
        }

        //Target Movement constraints-------------------------------
        if(targetStill != previousTargetStill)
        {
            previousTargetStill = targetStill;
            if(targetStill)
            {    
                targetChangesAtFivePoints = false;
                previousTargetChangesAtFivePoints = false;
                targetMoving = false;
                previousTargetMoving = false;
            }
            else
            {
                targetChangesAtFivePoints = true;
                previousTargetChangesAtFivePoints = true;
                targetMoving = false;
                previousTargetMoving = false;
            }
        }

        if (targetChangesAtFivePoints != previousTargetChangesAtFivePoints)
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
                targetStill = false;
                previousTargetStill = false;
                targetMoving = true;
                previousTargetMoving = true;
            }
        }

        if (targetMoving != previousTargetMoving)
        {
            previousTargetMoving = targetMoving;
            if (targetMoving)
            {
                targetStill = false;
                previousTargetStill = false;
                targetChangesAtFivePoints = false;
                previousTargetChangesAtFivePoints = false;
            }
            else
            {
                targetStill = true;
                previousTargetStill = true;
                targetChangesAtFivePoints = false;
                previousTargetChangesAtFivePoints = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
 
    //Getters
    //Perspective 
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
}