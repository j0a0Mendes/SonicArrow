using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool flag;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "TargetFirstRegion" || other.gameObject.name == "TargetSecondRegion" || other.gameObject.name == "TargetThirdRegion" || other.gameObject.name == "TargetForthRegion" || other.gameObject.name == "TargetFifthRegion" || other.gameObject.name == "Target")
        {
            //Debug.Log("TOUCHING TARGET");
            if (parameterManager.getHapticOnTargetHover())
            {
                flag = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TargetFirstRegion" || other.gameObject.name == "TargetSecondRegion" || other.gameObject.name == "TargetThirdRegion" || other.gameObject.name == "TargetForthRegion" || other.gameObject.name == "TargetFifthRegion" || other.gameObject.name == "Target")
        {
            if (parameterManager.getHapticOnTargetHover())
            {
                flag = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("String collided " + collision.gameObject.name);
    }

    private void Update() {
        //sendPointsToTarget(endpoint_1,endpoint_2);
        if (flag)
        {
            if (keyControllersrLeft != null)
            {
                Debug.Log("YAAAA");
                //keyControllersrLeft.activateVibrate();
                keyControllersrLeft.SendHaptics(true, 0.4f, 0.1f);
                //LeftHand.GetComponent<KeyControllers>().SendHaptics(keyControllersrLeft);
            }
            else
            {
                keyControllersrLeft.SendHaptics(true, 1, 0f);
                keyControllersrLeft.deactivateVibrate();
            }
        }


    }

    public void sendPointsToTarget(Transform point1, Transform point2)
    {
        targetObject.receiveTwoPoints(point1, point2);
    }
}
