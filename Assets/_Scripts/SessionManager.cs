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
    public bool resetScene;

    [SerializeField]
    public float timer = 0f;

    private bool isRunning;

    


    void Start()
    {
        isTraining = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRunning)
        {
            timer += Time.fixedDeltaTime;
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
}