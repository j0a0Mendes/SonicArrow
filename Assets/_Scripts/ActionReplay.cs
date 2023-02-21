using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{
    private bool isInReplayMode;

    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        actionReplayRecords.Add(new ActionReplayRecord { position = transform.position , rotation = transform.rotation });
    }
}
