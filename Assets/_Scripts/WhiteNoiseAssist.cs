using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteNoiseAssist : MonoBehaviour
{
    public GameObject whiteNoiseSpotter;
    private WhiteNoiseSpotter whiteNoiseScript;
    private float inputNumber;
    // Start is called before the first frame update
    void Start()
    {
        whiteNoiseScript = whiteNoiseSpotter.GetComponent<WhiteNoiseSpotter>();

    }

    // Update is called once per frame
    void Update()
    {
        inputNumber = transform.position.y;

        if(transform.position.y < -2.5f)
        {
            inputNumber = -2.5f;
        }else if(transform.position.y > 14.5f)
        {
            inputNumber = 14.5f;
        }

        whiteNoiseScript.changePitch(getWhiteNoisePitch());
    }

    public float getWhiteNoisePitch()
    {
        // Clamp the input number between -2.5 and 14.5
        float clampedInputNumber = Mathf.Clamp(inputNumber, -2.5f, 14.5f);

        // Calculate the proportional number between 0.5 and 2.0
        //float proportionalNumber = Mathf.InverseLerp(-2.5f, 14.5f, clampedInputNumber) * 1.5f + 0.5f;
        float proportionalNumber = Mathf.InverseLerp(-2.5f, 14.5f, clampedInputNumber) * 2.0f;


        return proportionalNumber;
    }
}
