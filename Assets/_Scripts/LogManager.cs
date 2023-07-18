using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogManager : MonoBehaviour
{
    public string logFileName = "";

    private StreamWriter writer;
    private static bool isFileOpen = false;

    private TimeManager timeManager;

    

    private void Awake()
    {
        if (!isFileOpen)
        {
            string logFilePath = Path.Combine(Application.persistentDataPath, logFileName);
            bool fileExists = File.Exists(logFilePath);

            writer = new StreamWriter(logFilePath, true);

            if (!fileExists)
            {
                writer.WriteLine("Log file created.");
                writer.Flush();
            }

            isFileOpen = true;
        }

        timeManager = GameObject.FindObjectOfType<TimeManager>();
    }

    private void OnDestroy()
    {
        if (writer != null)
        {
            writer.Close();
            writer.Dispose();
            isFileOpen = false;
        }
    }

    public void Log(string userID, string conditionNumber, string targetMovement, string points, string targetCoords, string arrowCoords)
    {
        string bars = " || ";
        string targetLabel = " TargetPos: ";
        string arrowLabel = " ArrowPos: ";

        string timeTook = timeManager.GetCurrentTimeAndReset();
        string logMessage = $" {bars} {timeTook} {bars} {userID} {bars} {conditionNumber} {bars} {targetMovement} {bars} {points} {bars} {targetLabel} {targetCoords} {bars} {arrowLabel} {arrowCoords}";
        Debug.Log(logMessage);
        writer.WriteLine(logMessage);
        writer.Flush();
    }
}