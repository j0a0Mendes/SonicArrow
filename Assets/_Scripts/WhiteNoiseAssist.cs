using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteNoiseAssist : MonoBehaviour
{
    public GameObject whiteNoiseSpotter;
    private WhiteNoiseSpotter whiteNoiseScript;
    private float inputNumber;

    private float pitchValue;
    // Start is called before the first frame update
    void Start()
    {
        whiteNoiseScript = whiteNoiseSpotter.GetComponent<WhiteNoiseSpotter>();

    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        inputNumber = transform.position.y;

        //Debug.Log(inputNumber);

        if(transform.position.y < -15.4f)
        {
            inputNumber = -15.4f;
        }else if(transform.position.y > 25.4f)
        {
            inputNumber = 25.4f;
        }

        whiteNoiseScript.changePitch(getWhiteNoisePitch());
    }*/

    void FixedUpdate()
    {
        inputNumber = transform.position.y;

        //Debug.Log(inputNumber);

        if(transform.position.y < -15.4f)
        {
            inputNumber = -15.4f;
        }else if(transform.position.y > 25.4f)
        {
            inputNumber = 25.4f;
        }

        whiteNoiseScript.changePitch(getWhiteNoisePitch());
    }

    public float getWhiteNoisePitch()
    {
        // Clamp the input number between -15.4 and 25.4
        float clampedInputNumber = Mathf.Clamp(inputNumber, -15.4f, 25.4f);

        float proportionalNumber = Mathf.InverseLerp(-15.4f, 25.4f, clampedInputNumber) * 3.0f;

        //Debug.Log(proportionalNumber);
        pitchValue = proportionalNumber;
        
        return proportionalNumber;
    }

    public float getTargetPitch(float inputNumb)
    {
        // Clamp the input number between -15.4 and 25.4
        float clampedInputNumber = Mathf.Clamp(inputNumb, -15.4f, 25.4f);

        float pitchValue = Mathf.InverseLerp(-15.4f, 25.4f, clampedInputNumber) * 3.0f;

        return pitchValue;
    }

    public float getAimPitch()
    {
        return pitchValue;
    }
}
