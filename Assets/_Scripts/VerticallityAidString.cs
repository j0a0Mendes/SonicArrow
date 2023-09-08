using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(LineRenderer))]


public class VerticallityAidString : MonoBehaviour
{
    [SerializeField]
    private Transform endpoint_1, endpoint_2;

    private LineRenderer lineRenderer;

    private TargetObject targetObject;

    private GameObject LeftHand;
    private KeyControllers keyControllersrLeft;

    //PARAMETER MANAGEMENT 
    private ParameterManager parameterManager;
    private ChangePerspectiveController controller;

    [SerializeField]
    public GameObject ballPrefab;

    [SerializeField]
    public GameObject ballPointer;

    [SerializeField]
    public GameObject redBall;

    public GameObject wallBeep;
    public GameObject targetBeep;

    [SerializeField]
    public GameObject centerTarget;

    //Flags
    private bool flag;
    private bool beepFlag;
    private bool otherWallsBeep;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();

        parameterManager = GameObject.FindObjectOfType<ParameterManager>();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
    }


    public void CreateString(Vector3? midPosition)
    {
        Vector3[] linePoints = new Vector3[midPosition == null ? 2 : 3];
        linePoints[0] = endpoint_1.localPosition;
        if (midPosition != null)
        {
            linePoints[1] = transform.InverseTransformPoint(midPosition.Value);
        }
        linePoints[^1] = endpoint_2.localPosition;

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }

    private void Start()
    {
        CreateString(null);
        targetObject = GameObject.FindObjectOfType<TargetObject>();
    }

   

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TargetSurface" )
        {
            flag = true;
            beepFlag = true;
        }
        else
        {
            if (other.gameObject.name == "BackWall" || other.gameObject.name == "RightWall" || other.gameObject.name == "LeftWall" || other.gameObject.name == "Floor" || other.gameObject.name == "Ceiling")
            {
                otherWallsBeep = true;
            }
            else
            {
                otherWallsBeep = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TargetSurface") { 
            flag = false;
            beepFlag = false;
        }
    }

  
    private int beepCount = 0;
    private int beepFreq = 0;

    private void FixedUpdate() {

        beepCount += 1;

        beepFreq = calculateBeepFreq(centerTarget.transform.position.x, centerTarget.transform.position.y, centerTarget.transform.position.z, redBall.transform.position.x, redBall.transform.position.y, redBall.transform.position.z);
        
        if (parameterManager.getHapticOnTargetHover())
        {
            if (flag)
            {
                if (keyControllersrLeft != null)
                {
                    keyControllersrLeft.SendHaptics(true, 0.2f, 0.1f);
                }
                else
                {
                    keyControllersrLeft.SendHaptics(true, 1, 0f);
                    keyControllersrLeft.deactivateVibrate();
                }
            }
        }

        if (parameterManager.getSpotterBeepAid())
        {
            if(beepCount >= beepFreq){
                wallBeep.GetComponent<AudioSource>().Play();
                beepCount = 0;
            }
        }
        else
        {
            targetBeep.GetComponent<AimingTarget>().DeactivateLoop();
            wallBeep.GetComponent<AimingWall>().DeactivateLoop();
        }


    }

    public void sendPointsToTarget(Transform point1, Transform point2)
    {
        targetObject.receiveTwoPoints(point1, point2);
    }


    public int calculateBeepFreq(float aX, float aY, float aZ, float bX, float bY, float bZ)
    {
        float distance = Vector3.Distance(new Vector3(aX, aY, aZ), new Vector3(bX, bY, bZ));

        if (distance <= 0.5f)
        {
            return 15;
        }
        else if (distance >= 10f)
        {
            return 300000000;
        }
        else
        {
            // Calculate the beep frequency within the range of 20 to 50
            float scaleFactor = 30f / 10f; // This value adjusts the range (50 - 20 = 30)
            int result = 20 + Mathf.RoundToInt(distance * scaleFactor);

            if(distance <= 0.3f)
            {
                result -= 3;
            }
            return result;
        }
    }
}
