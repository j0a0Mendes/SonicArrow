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
    public float previousSpeed = 0;

    //Parameter Movement
    private ParameterManager parameterManager;

    [SerializeField]
    public GameObject targetSoundObj;

    [SerializeField]
    public GameObject ballPointer;
    
    [SerializeField]
    public GameObject audioListenerBall;

    private bool canMove;

    private int flagMovement = 1;

    private BowStringController bowStringController;

    [SerializeField]
    public bool relocateTargetVar;

    // Start is called before the first frame update
    void Start()
    {
        bowStringController = GameObject.FindObjectOfType<BowStringController>();
        canMove = true;
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
    void FixedUpdate()
    {
        targetPitch(audioListenerBall.transform.position.y);

        //Target Movement
        if (parameterManager.getTargetMoving() && canMove == true)
        {
            if(wallSystemPos == 1 ||wallSystemPos == 3){
                transform.Translate(Vector3.forward * flagMovement * speed * Time.deltaTime); // move object forward in the z-axis
                if(transform.position.z >  17.5f){
                    InvertSpeed();
                }

                if(transform.position.z < -28.3f){
                    InvertSpeed();
                }
            }else{
                transform.Translate(transform.right * flagMovement * speed * Time.deltaTime);
                if(transform.position.x > -30.5f){
                    InvertSpeed();
                }

                if(transform.position.x < -50.82f){
                    InvertSpeed();
                }
            }
        }

        if (relocateTargetVar)
        {
            relocateTargetVar = false;
            relocateTarget();
        }
    }

    public void activateCanMove(){
        canMove = true;
    }

    public void deactivateCanMove(){
        canMove = false;
    }

    public void InvertSpeed()
    {
        flagMovement = flagMovement * -1;
    }

    public void StopMovement(){
        previousSpeed = speed;
        speed = 0;
    }

    
    public void RestartMovement(){
        speed = previousSpeed;
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
        /*getAngle(wallSystemPos).SetActive(false);
        List<int> numbers = new List<int>();
        numbers.Add(1);
        numbers.Add(2);
        numbers.Add(3);
        numbers.Add(4);
        numbers.Remove(wallSystemPos);

        int selectedNumber = numbers[new System.Random().Next(numbers.Count)];
        wallSystemPos = selectedNumber;*/
        getAngle(1).SetActive(true);
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
            float randomZ = GetRandomNumber(-27.5f, 16.5f);

            targetY = randomY;
            targetZ = randomZ;

            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            transform.position = new Vector3(targetX, targetY, targetZ);
        }
        else if (wallSystemPos == 2)
        {
            targetZ = 49.26f;

            float randomY = GetRandomNumber(-12f, 21f);
            float randomX = GetRandomNumber(-52.5f, -31.7f);

            targetY = randomY;
            targetX = randomX;

            transform.rotation = Quaternion.Euler(0f, -90f, 0f);

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
            targetZ = -60f;

            float randomY = GetRandomNumber(-12f, 21f);
            float randomX = GetRandomNumber(-50f, -31.7f);

            targetY = randomY;
            targetX = randomX;

            transform.rotation = Quaternion.Euler(0f, 90f, 0f);

            transform.position = new Vector3(targetX, targetY, targetZ);
        }

        bowStringController.prepareCrossBow();
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

    void targetPitch(float aimY)
    {
        targetY = transform.position.y;
        float targetPitch = 0.0f;

        if (aimY == targetY)
        {
            targetPitch = 1;
        }
        else if (aimY > targetY)
        {
            if (aimY >= 21)
            {
                targetPitch = 2f;
            }
            else
            {
                float proportion = (aimY - targetY) / (21 - targetY);
                targetPitch = 1 + proportion;
            }
        }
        else
        {
            if (aimY <= -12)
            {
                targetPitch = 0.4f;
            }
            else
            {
                float proportion = (aimY - targetY) / (-12 - targetY);
                if((1 - proportion) >= 0.4)
                {
                    targetPitch = 1 - proportion;
                }
                else
                {
                    targetPitch = 0.4f;
                }
                

            }
        }
       
        if(targetPitch < 0.1f)
        {
            targetPitch= 0.1f;
        }
        targetSoundObj.GetComponent<AudioSource>().pitch = targetPitch;
        audioListenerBall.GetComponent<AudioSource>().pitch = targetPitch;

        audioListenerBall.GetComponent<AudioSource>().pitch = targetPitch;
    }
    
}
