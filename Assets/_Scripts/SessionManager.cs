using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SessionManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public string userID = "0";

    [SerializeField]
    public bool isRightHanded;

    [SerializeField]
    public bool relocateTarget;

    [SerializeField]
    public bool isTraining;

    [SerializeField]
    public bool startFirstPhase;

    [SerializeField]
    public bool startSecondPhase;

    [SerializeField]
    public int shotsDone;

    [SerializeField]
    public int pointsMade;

    [SerializeField]
    public int targetsHad;

    [SerializeField]
    public float timer = 0f;

    [SerializeField]
    public bool resetScene;

    private bool isRunning;

    private AudioSource startPhaseSound;

    private AudioSource endPhaseSound;

    private bool playEndPhaseAudioFlag;

    private ChangePerspectiveController controller;

    private TimeManager timeManager;

    private ParameterManager parameterManager;

    private bool flag;

    private LogManager logManager;

    [SerializeField]
    public GameObject bowEvilRight; // Drag the "Bow_Evil" object here in the Unity Inspector

    [SerializeField]
    public GameObject bowEvilLeft;  // Drag the "Bow_Evil_Left" object here in the Unity Inspector

    void Start()
    {
        isTraining = true;
        startPhaseSound = GameObject.Find("StartPhase").GetComponent<AudioSource>();
        endPhaseSound = GameObject.Find("EndPhase").GetComponent<AudioSource>();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
        timeManager = GameObject.FindObjectOfType<TimeManager>();
        parameterManager = GameObject.FindObjectOfType<ParameterManager>();
        logManager = GameObject.FindObjectOfType<LogManager>().GetComponent<LogManager>();
        flag = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (relocateTarget)
        {
            //RELOCATE TARGET
            relocateTarget = false;
        }


        if (isRunning)
        {
            if(timer == 0)
            {
                Debug.Log("----------START OF PHASE----------");
                playEndPhaseAudioFlag = false;
                controller.resetShotWithoutHit();
                startPhaseSound.Play();
                logManager.isTraining(false);
                pointsMade = 0;
                shotsDone = 0;
                targetsHad = 0;

            }

            if(timer > 4 & flag)
            {
                timeManager.ResetTimer();
                timeManager.StartTimer();
                flag = false;
            }

            timer += Time.fixedDeltaTime;

            //TIMEOUT OR SHOTS DONE
            if(timer >= 300 | shotsDone == 15)
            {
                Debug.Log("----------END OF PHASE----------");
                Debug.Log(" PHASE STATS|| Points made: " + pointsMade.ToString() + " || Targets tried: " + targetsHad.ToString());
                playEndPhaseAudioFlag = true;
                logManager.isTraining(true);
                isRunning = false;
                isTraining = true;
                flag = true;
                shotsDone = 0;
                timer = 0;
            }

          
        }
    }

    public void startTimer()
    {
        isRunning = true;
    }

    public void stopTimer()
    {
        isRunning = false;
    }

    private void OnValidate()
    {
        if(startFirstPhase)
        {
            startFirstPhase= false;
            isTraining = false;
            startTimer();
        }

        if (startSecondPhase)
        {
            startSecondPhase = false;
            isTraining = false;
            startTimer();
        }
    }

    public void addShotNumber()
    {
        shotsDone += 1;
    }

    public void addPoints(int points)
    {
        pointsMade += points;
        targetsHad += 1;
    }

    public bool getEndPhaseFlag()
    {
        if (playEndPhaseAudioFlag)
        {
            playEndPhaseAudioFlag = false;
            return true;
        }

        return playEndPhaseAudioFlag;
    }

    public bool getIsRightHanded()
    {
        return isRightHanded;
    }
}