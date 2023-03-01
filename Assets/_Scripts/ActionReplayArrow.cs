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
    private bool lastFrameRecorded = false;

    private bool audioPlaying;
    private bool endSoundPlayed;

    [SerializeField]
    private AudioSource windNavigatingSound;

    //[SerializeField]
    //private AudioSource arrowHitSound;
    private bool playedHitSound;

    KeyControllers keyControllers;

    private int replayEndTwoSeconds;

    private ChangePerspectiveController controller;


    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();
    // Start is called before the first frame update
    void Start()
    {
        triggerIsReplayMode = false;
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
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

        if (playedHitSound)
        {
            replayEndTwoSeconds += 1;
        }

        if (isInReplayMode == false)
        {
            actionReplayRecords.Add(new ActionReplayRecord { position = transform.position, rotation = transform.rotation, alreadyHit = alreadyHit });    
        }
        else
        {
            int nextIndex = currentReplayIndex + 1;
            
            if (nextIndex < actionReplayRecords.Count && replayEndTwoSeconds < 200)
            {
                SetTransform(nextIndex);
            }
            else
            {
                keyControllers = GameObject.FindObjectOfType<KeyControllers>();
                keyControllers.enableButtonA();
                Debug.Log("END OF REPLAY");
                
                //changePerspective
                controller.enableChange();
                controller.changePerspective();
                
                Destroy(gameObject);
                replayEndTwoSeconds = 0;
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

        if (actionReplayRecord.alreadyHit)
        {
            if (audioPlaying)
            {
                audioPlaying = false;
                windNavigatingSound.Stop();
                if (!playedHitSound)
                {
                    //arrowHitSound.Play();
                    //KeyControllers keyControllers = GameObject.FindObjectOfType<KeyControllers>();
                    //keyControllers.enableButtonA();
                    //keyControllers.enableButtonX(); 

                    Debug.Log("HIT SOUND");
                    playedHitSound = true;
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
