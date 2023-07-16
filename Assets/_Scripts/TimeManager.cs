using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    public bool isRunning;

    [SerializeField]
    public bool resetScene;


    [SerializeField]
    public float counter = 0f;

    [SerializeField]
    public bool resetTimer;

    public void FixedUpdate()
    {
        if (isRunning)
        {
            counter += Time.fixedDeltaTime;
        }

        if (resetScene)
        {
            resetScene= false;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        if (resetTimer)
        {
            resetTimer = false;
            counter = 0f;
        }
    }

    private string FormatTime(float counter)
    {
        int minutes = Mathf.FloorToInt(counter / 60f);
        int seconds = Mathf.FloorToInt(counter % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void ResetTimer()
    {
        counter = 0f;
    }

    public string GetCurrentTimeAndReset()
    {
        int minutes = Mathf.FloorToInt(counter / 60f);
        int seconds = Mathf.FloorToInt(counter % 60f);
        counter = 0f;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}