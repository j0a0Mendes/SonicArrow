using System;
using System.IO;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    public string logFileName = "logs.txt";

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

    public void Log(string userID, string conditionNumber, string targetMovement, string points)
    {
        string conditionLabel = "Condition: ";
        string bars = " || ";
        string timeTook = timeManager.GetCurrentTimeAndReset();
        string logMessage = $"{timeTook} {bars} {userID} {bars} {conditionLabel} {conditionNumber} {bars} {targetMovement} {bars} {points}";
        Debug.Log(logMessage);
        writer.WriteLine(logMessage);
        writer.Flush();
    }
}