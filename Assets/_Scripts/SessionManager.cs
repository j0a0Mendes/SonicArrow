using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public string userID = "0";

    [SerializeField]
    public string label;

    [SerializeField]
    public bool isTimeRunning;

    [SerializeField]
    public bool resetTime;

    [SerializeField]
    public bool relocateTarget;

    [SerializeField]
    public bool resetScene;




    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
