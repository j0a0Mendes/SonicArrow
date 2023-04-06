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

    private bool alreadyHit = false;

    private bool audioPlaying;

    [SerializeField]
    private AudioSource windNavigatingSound;

    private bool playedHitSound;

    KeyControllers keyControllers;

    private int replayEndTwoSeconds;

    private ChangePerspectiveController controller;

    [SerializeField]
    private GameObject stickingArrow;

    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();

    void Start()
    {
        triggerIsReplayMode = false;
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
    }

    private bool changePerspectiveInASec = false;
    private int countChangePerspective = 0;

    // Update is called once per frame
    void Update()
    {

        
        if (triggerIsReplayMode)
        {
            if (!audioPlaying)
            {
                windNavigatingSound.Play();
                audioPlaying = true;
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
            }
        }
    }

    private void FixedUpdate()
    {

        if (playedHitSound)
        {
            replayEndTwoSeconds += 1;
        }

        if (isInReplayMode == false)
        {
            currentReplayIndex = 0;

            actionReplayRecords.Add(new ActionReplayRecord { position = transform.position, rotation = transform.rotation, alreadyHit = alreadyHit });    
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
                keyControllers = GameObject.FindObjectOfType<KeyControllers>();
                keyControllers.enableButtonA();
                Debug.Log("END OF REPLAY");
                
                replayEndTwoSeconds = 0;
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

        if (actionReplayRecord.alreadyHit)
        {
            if (audioPlaying)
            {
                audioPlaying = false;
                windNavigatingSound.Stop();
                if (!playedHitSound)
                {
                    GameObject arrow = Instantiate(stickingArrow);
                    arrow.transform.position = transform.position;
                    arrow.transform.forward = transform.forward;

                    Debug.Log("HIT SOUND");
                    playedHitSound = true;

                    controller.triggerChangePerspectiveInASec();
                }
            }    
        }
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

    public void alreadyHitTrigger()
    {
        alreadyHit = true;
    }
}
