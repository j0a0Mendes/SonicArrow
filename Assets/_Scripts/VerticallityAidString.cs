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

    public GameObject wallBeep;
    public GameObject targetBeep;

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
        //Debug.Log(other.gameObject.name);
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

        //if (other.gameObject.name == "BackWall" || other.gameObject.name == "RightWall" || other.gameObject.name == "LeftWall" || other.gameObject.name == "Floor" || other.gameObject.name == "Ceiling")
        //{
        //   otherWallsBeep = false;
        //}
    }

  

    private void Update() {
        if (parameterManager.getHapticOnTargetHover())
        {
            if (flag)
            {
                if (keyControllersrLeft != null)
                {
                    keyControllersrLeft.SendHaptics(true, 0.4f, 0.1f);
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
            
            if (beepFlag)
            {
                wallBeep.GetComponent<AimingWall>().DeactivateLoop();
                targetBeep.GetComponent<AimingTarget>().ActivateLoop();
            }
            else
            {
                if (otherWallsBeep)
                {
                    targetBeep.GetComponent<AimingTarget>().DeactivateLoop();
                    wallBeep.GetComponent<AimingWall>().DeactivateLoop();
                }
                else
                {
                    targetBeep.GetComponent<AimingTarget>().DeactivateLoop();
                    wallBeep.GetComponent<AimingWall>().ActivateLoop();
                }
                
                
            }
        }


    }

    public void sendPointsToTarget(Transform point1, Transform point2)
    {
        targetObject.receiveTwoPoints(point1, point2);
    }
}
