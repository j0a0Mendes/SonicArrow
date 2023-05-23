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

    [SerializeField]
    public GameObject targetFirstRegion;

    [SerializeField]
    public GameObject targetSecondRegion;

    [SerializeField]
    public GameObject targetThirdRegion;

    [SerializeField]
    public GameObject targetForthRegion;

    [SerializeField]
    public GameObject targetFifthRegion;

    private MeshCollider first;
    private MeshCollider second;
    private MeshCollider third;
    private MeshCollider forth;
    private MeshCollider fifth;

    private int wallSystemPos;     //Wall that the target starts 

    [SerializeField]
    public Transform endpoint1, endpoint2;

    //Target Movement

    public float speed = 5f; // speed of movement

    //Parameter Movement
    private ParameterManager parameterManager;

    [SerializeField]
    public GameObject targetSoundObj;

    [SerializeField]
    public GameObject ballPointer;


    // Start is called before the first frame update
    void Start()
    {
        wallSystemPos = 1;
        parameterManager = GameObject.FindObjectOfType<ParameterManager>();
    }

    private void Awake()
    {
        targetX = 12.27f;
      

        float randomY = GetRandomNumber(-12f, 21f);
        float randomZ = GetRandomNumber(-16.7f, 5.6f);

        targetY = randomY;
        targetZ = randomZ;

        first = targetFirstRegion.GetComponent<MeshCollider>();
        second = targetSecondRegion.GetComponent<MeshCollider>();
        third = targetThirdRegion.GetComponent<MeshCollider>();
        forth = targetForthRegion.GetComponent<MeshCollider>();
        fifth = targetFifthRegion.GetComponent<MeshCollider>();

        transform.position = new Vector3(targetX, targetY, targetZ);
    }

    // Update is called once per frame
    void Update()
    {
        targetPitch(ballPointer.transform.position.y);

        //Target Movement
        if (parameterManager.getTargetMoving())
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // move object forward in the z-axis
        }
    }

    public void InvertSpeed()
    {
        if (speed == 5)
        {
            speed = -5;
        }
        else
        {
            speed = 5;
        }
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
        if (wallAngle == 1)
        {
            return firstAngle;
        }
        else if (wallAngle == 2)
        {
            return secondAngle;
        }
        else if (wallAngle == 3)
        {
            return thirdAngle;
        }
        else if (wallAngle == 4)
        {
            return forthAngle;
        }

        return firstAngle;

    }

    public void relocateTarget()
    {
        if (wallSystemPos == 1)
        {
            targetX = 12.27f;

            float randomY = GetRandomNumber(-12f, 21f);
            float randomZ = GetRandomNumber(-16.7f, 5.6f);

            targetY = randomY;
            targetZ = randomZ;

            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            transform.position = new Vector3(targetX, targetY, targetZ);
        }
        else if (wallSystemPos == 2)
        {
            targetZ = 45.5f;

            float randomY = GetRandomNumber(-12f, 21f);
            float randomX = GetRandomNumber(-52.5f, -31.7f);

            targetY = randomY;
            targetX = randomX;

            transform.rotation = Quaternion.Euler(0f, 90f, 0f);

            transform.position = new Vector3(targetX, targetY, targetZ);
        }
        else if (wallSystemPos == 3)
        {
            targetX = -97.2f;

            float randomY = GetRandomNumber(-12f, 21f);
            float randomZ = GetRandomNumber(-16.7f, 5.6f);

            targetY = randomY;
            targetZ = randomZ;


            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            transform.position = new Vector3(targetX, targetY, targetZ);
        }
        else if (wallSystemPos == 4)
        {
            targetZ = -56f;

            float randomY = GetRandomNumber(-12f, 21f);
            float randomX = GetRandomNumber(-52.5f, -31.7f);

            targetY = randomY;
            targetX = randomX;

            transform.rotation = Quaternion.Euler(0f, -90f, 0f);

            transform.position = new Vector3(targetX, targetY, targetZ);
        }
    }

    public int getWallSystem()
    {
        return wallSystemPos;
    }

    public int getHitQuadrant(float coord1, float coord2)
    {
        if (wallSystemPos == 1)
        {
            if (coord1 > transform.position.y)
            {
                if (coord2 > transform.position.z)
                {
                    return 4;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (coord2 > transform.position.z)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            }
        }
        else if (wallSystemPos == 2)
        {
            if (coord1 > transform.position.y)
            {
                if (coord2 > transform.position.x)
                {
                    return 1;
                }
                else
                {
                    return 4;
                }
            }
            else
            {
                if (coord2 > transform.position.x)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }
        else if (wallSystemPos == 3)
        {
            if (coord1 > transform.position.y)
            {
                if (coord2 > transform.position.z)
                {
                    return 1;
                }
                else
                {
                    return 4;
                }
            }
            else
            {
                if (coord2 > transform.position.z)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }
        else
        {
            if (coord1 > transform.position.y)
            {
                if (coord2 > transform.position.x)
                {
                    return 4;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (coord2 > transform.position.x)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            }
        }
    }

    public void receiveTwoPoints(Transform point1, Transform point2)
    {
        endpoint1 = point1;
        endpoint2 = point2;
    }

    public void updateMeshes()
    {
        first = targetFirstRegion.GetComponent<MeshCollider>();
        second = targetSecondRegion.GetComponent<MeshCollider>();
        third = targetThirdRegion.GetComponent<MeshCollider>();
        forth = targetForthRegion.GetComponent<MeshCollider>();
        fifth = targetFifthRegion.GetComponent<MeshCollider>();
    }

    
    public void targetPitch(float aimY)
    {
        float targetPitchVal = 0.0f;
        targetY = transform.position.y;

        if (targetY == aimY)
        {
            targetPitchVal = 1;
        }
        else if (aimY > targetY)
        {
            if (aimY >= 21)
            {
                targetPitchVal = 3;
            }
            else
            {
                targetPitchVal = (aimY - targetY) / (21 - targetY) * 2 + 1;
            }
        }
        else // b < a
        {
            if (aimY <= -12)
            {
                targetPitchVal = 0;
            }
            else
            {
                targetPitchVal = (aimY - targetY) / (-12 - targetY) * -1;
            }
        }

        targetSoundObj.GetComponent<AudioSource>().pitch = targetPitchVal;
    }
    
}
