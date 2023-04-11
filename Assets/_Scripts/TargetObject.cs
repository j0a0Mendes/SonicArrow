using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class TargetObject : MonoBehaviour
{
    [SerializeField]
    public float targetX;

    [SerializeField]
    public float targetY;

    [SerializeField]
    public float targetZ;

    [SerializeField]
    public GameObject firstAngle;

    [SerializeField]
    public GameObject secondAngle;

    [SerializeField]
    public GameObject thirdAngle;

    [SerializeField]
    public GameObject forthAngle;

    private int wallSystemPos;     //Wall that the target starts 

    // Start is called before the first frame update
    void Start()
    {
        wallSystemPos = 1;
    }

    private void Awake()
    {
        targetX = 10.45f;
        //targetY = 2.0f;
        //targetZ = -5.46f;

        

        float randomY = GetRandomNumber(0.5f, 8.0f);
        float randomZ = GetRandomNumber(-16.7f, 5.6f);
        
        targetY= randomY;
        targetZ = randomZ;

        //transform.position = new Vector3(targetX, targetY, targetZ);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(targetX, targetY, targetZ);
    }

    public static float GetRandomNumber(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public (float, float, float) getTargetPos()
    {
        return (targetX, targetY, targetZ); 
    }

    public void changeTargetPos()
    {
        getAngle(wallSystemPos).SetActive(false);
        List<int> numbers = new List<int>();
        numbers.Add(1);
        numbers.Add(2);
        numbers.Add(3);
        numbers.Add(4);
        numbers.Remove(wallSystemPos);

        int selectedNumber = numbers[new System.Random().Next(numbers.Count)];
        wallSystemPos = selectedNumber;
        getAngle(wallSystemPos).SetActive(true);
        relocateTarget();

    }

    public GameObject getAngle(int wallAngle)
    {
        if(wallAngle == 1)
        {
            return firstAngle;
        }else if(wallAngle == 2)
        {
            return secondAngle;
        }else if(wallAngle == 3)
        {
            return thirdAngle;
        }else if(wallAngle == 4)
        {
            return forthAngle;
        }

        return firstAngle;

    }

    public void relocateTarget()
    {
        if (wallSystemPos == 1)
        {
            targetX = 10.45f;

            float randomY = GetRandomNumber(0.5f, 8.0f);
            float randomZ = GetRandomNumber(-16.7f, 5.6f);

            targetY = randomY;
            targetZ = randomZ;

            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (wallSystemPos == 2)
        {
            targetZ = 45.6f;
            
            float randomY = GetRandomNumber(0.5f, 8.0f);
            float randomX = GetRandomNumber(-52.5f, -31.7f);
            
            targetY = randomY;
            targetX = randomX;

            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if (wallSystemPos == 3)
        {
            targetX = -95.2f;

            float randomY = GetRandomNumber(0.5f, 8.0f);
            float randomZ = GetRandomNumber(-16.7f, 5.6f);

            targetY = randomY;
            targetZ = randomZ;


            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (wallSystemPos == 4)
        {
            targetZ = -56.6f;

            float randomY = GetRandomNumber(0.5f, 8.0f);
            float randomX = GetRandomNumber(-52.5f, -31.7f);

            targetY = randomY;
            targetX = randomX;

            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
    }
}
