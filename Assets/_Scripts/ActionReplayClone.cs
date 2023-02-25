using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplayClone : MonoBehaviour
{
    private int currentReplayIndex;

    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();

    private bool replayActivated;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (replayActivated)
        {

        }
    }

    public void FixedUpdate()
    {
        if (replayActivated)
        {
            int nextIndex = currentReplayIndex + 1;

            if (nextIndex < actionReplayRecords.Count)
            {
                SetTransform(nextIndex);
            }
            else
            {
                replayActivated= false;
            }
        }
    }


    public void setReplayActions(List<ActionReplayRecord> actionList)
    {
        actionReplayRecords = actionList;
    }

    public void SetTransform(int index)
    {
        currentReplayIndex = index;

        ActionReplayRecord actionReplayRecord = actionReplayRecords[index];

        transform.position = actionReplayRecord.position;
        transform.rotation = actionReplayRecord.rotation;
    }

    public void ActivateReplay()
    {
        replayActivated = true;
    }

    public void CleanReplay()
    {
        currentReplayIndex = 0;
        actionReplayRecords = new List<ActionReplayRecord>();
    }

}
