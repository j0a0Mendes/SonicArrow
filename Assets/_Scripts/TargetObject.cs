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

    // Start is called before the first frame update
    void Start()
    {
        wallSystemPos = 1;
    }

    private void Awake()
    {
        //targetX = 10.45f;
        targetX = 13f;
        //targetY = 2.0f;
        //targetZ = -5.46f;



        float randomY = GetRandomNumber(0.5f, 8.0f);
        float randomZ = GetRandomNumber(-16.7f, 5.6f);
        
        targetY= randomY;
        targetZ = randomZ;

        first = targetFirstRegion.GetComponent<MeshCollider>();
        second = targetSecondRegion.GetComponent<MeshCollider>();
        third = targetThirdRegion.GetComponent<MeshCollider>();
        forth = targetForthRegion.GetComponent<MeshCollider>();
        fifth = targetFifthRegion.GetComponent<MeshCollider>();

        //transform.position = new Vector3(targetX, targetY, targetZ);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(targetX, targetY, targetZ);

        /**bool isBetweenPoints = Physics.Linecast(endpoint1.position, endpoint2.position, out RaycastHit hitInfo)
            && hitInfo.collider == first;

        if (isBetweenPoints)
        {
            // The object's mesh collider is between point A and point B
            Debug.Log("Object is between points A and B");
        }
        else
        {
            // The object's mesh collider is not between point A and point B
            //Debug.Log("Object is not between points A and B");
        }**/
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
        }else if(wallSystemPos == 2)
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
        else if(wallSystemPos == 3)
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
}
