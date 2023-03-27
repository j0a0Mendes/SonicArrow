using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControllerSupport : MonoBehaviour
{
    public KeyControllers keyControllersRight;
    void Start()
    {
        keyControllersRight = GameObject.FindObjectOfType<KeyControllers>();
    }

    public void enableButtonA()
    {
        keyControllersRight.enableButtonA();
    }

}
