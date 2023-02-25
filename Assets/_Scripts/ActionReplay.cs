using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{
    private int currentReplayIndex;
    private bool isInReplayMode = false;
    private bool triggerIsReplayMode;
    private ActionReplayClone actionReplayClone;


    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();
    // Start is called before the first frame update
    void Start()
    {
        triggerIsReplayMode = false;
        actionReplayClone = GameObject.FindObjectOfType<ActionReplayClone>();
    }

    // Update is called once per frame
    void Update()
    {
        actionReplayClone.setReplayActions(actionReplayRecords);

        if (triggerIsReplayMode)
        {

            triggerIsReplayMode = false;


            isInReplayMode = !isInReplayMode;

            if (isInReplayMode )
            {
                //SetTransform(0);
                actionReplayClone.SetTransform(0);
            }
            else
            {
                //SetTransform(actionReplayRecords.Count -1);

                actionReplayClone.SetTransform(actionReplayRecords.Count - 1);

                CleanReplay();
                actionReplayClone.CleanReplay();
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
            actionReplayClone.ActivateReplay();
            //int nextIndex = currentReplayIndex + 1;
            //
            //if (nextIndex < actionReplayRecords.Count)
            //{
            //    actionReplayClone.SetTransform(nextIndex);
            //}
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
}
