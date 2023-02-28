using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ActionReplayArrow : MonoBehaviour
{
    private int currentReplayIndex;
    private bool isInReplayMode = false;
    private bool triggerIsReplayMode;

    private bool endReplayRecordVar;


    private bool audioPlaying;
    private bool endSoundPlayed;

    [SerializeField]
    private AudioSource windNavigatingSound;


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
                windNavigatingSound.Play();
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
            else
            {
                Destroy(gameObject);
            }
        }

        //ADICIONAR UM PARAMETRO AO RECORD
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

    public void endReplayRecord()
    {
        endReplayRecordVar = true;
    }
}
