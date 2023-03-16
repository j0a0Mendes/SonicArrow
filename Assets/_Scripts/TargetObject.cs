using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        targetX = 10.45f;
        //targetY = 2.0f;
        //targetZ = -5.46f;

        float randomY = GetRandomNumber(0.5f, 8.0f);
        float randomZ = GetRandomNumber(-52.0f, 24.0f);
        
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
}
