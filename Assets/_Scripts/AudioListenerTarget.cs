using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerTarget : MonoBehaviour
{

    [SerializeField]
    private GameObject targetAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetAudio != null)
        {
            transform.position = targetAudio.transform.position;
        }
    }
}
