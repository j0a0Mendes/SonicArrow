using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ActionReplayArrow : MonoBehaviour
{
    private int currentReplayIndex;
    private bool isInReplayMode = false;
    private bool triggerIsReplayMode;
    public string state;


    private bool audioPlaying;


    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();
    // Start is called before the first frame update
    void Start()
    {
        triggerIsReplayMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (triggerIsReplayMode)
        {
            if (!audioPlaying)
            {
                audioPlaying = true;
                //ArrowParent.windNavigatingSound.Play();
            }

            triggerIsReplayMode = false;


            isInReplayMode = !isInReplayMode;

            if (isInReplayMode)
            {
                SetTransform(0);
                
            }
            else
            {
                SetTransform(actionReplayRecords.Count -1);
                //CleanReplay();  
            }
        }
    }

    private void FixedUpdate()
    {
        if (isInReplayMode == false)
        {
            actionReplayRecords.Add(new ActionReplayRecord { position = transform.position, rotation = transform.rotation });
        }
        else
        {
            
            int nextIndex = currentReplayIndex + 1;
            
            if (nextIndex < actionReplayRecords.Count)
            {
                SetTransform(nextIndex);
            }
        }
    }

    public void triggerReplayMode()
    {
        triggerIsReplayMode = true;
    }

    private void SetTransform(int index)
    {
        currentReplayIndex = index;

        ActionReplayRecord actionReplayRecord = actionReplayRecords[index];

        transform.position = actionReplayRecord.position;
        transform.rotation = actionReplayRecord.rotation;
    }

    public void CleanReplay()
    {
        currentReplayIndex = 0;
        actionReplayRecords = new List<ActionReplayRecord>();
    }

    public string getState()
    {
        return state;
    }

    public void changeState(string stateGiven)
    {
        state = stateGiven;
    }
}
