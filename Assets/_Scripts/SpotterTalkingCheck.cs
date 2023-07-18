using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpotterTalkingCheck : MonoBehaviour
{
    private ChangePerspectiveController controller;

    private List<AudioSource> allAudioSources;

    private TimeManager timeManager;

    private void Start()
    {
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();

        timeManager = GameObject.FindObjectOfType<TimeManager>();

        allAudioSources = new List<AudioSource>();

        AudioSource a = GameObject.Find("Timeout").GetComponent<AudioSource>();
        allAudioSources.Add(a);

        AudioSource b = GameObject.Find("Hit_A_Wall").GetComponent<AudioSource>();
        allAudioSources.Add(b);

        AudioSource c = GameObject.Find("Wall_Direita").GetComponent<AudioSource>();
        allAudioSources.Add(c);

        AudioSource d = GameObject.Find("Wall_Esquerda").GetComponent<AudioSource>();
        allAudioSources.Add(d);

        AudioSource e = GameObject.Find("Hit_Ceiling").GetComponent<AudioSource>();
        allAudioSources.Add(e);

        AudioSource f = GameObject.Find("Hit_Floor").GetComponent<AudioSource>();
        allAudioSources.Add(f);

        AudioSource g = GameObject.Find("Time_To_Change_TargetPos").GetComponent<AudioSource>();
        allAudioSources.Add(g);

        AudioSource h = GameObject.Find("5_Points").GetComponent<AudioSource>();
        allAudioSources.Add(h);

        AudioSource i = GameObject.Find("5_Points_Change").GetComponent<AudioSource>();
        allAudioSources.Add(i);

        AudioSource j = GameObject.Find("4_Points_Change").GetComponent<AudioSource>();
        allAudioSources.Add(j);

        AudioSource k = GameObject.Find("4_Points").GetComponent<AudioSource>();
        allAudioSources.Add(k);

        AudioSource l = GameObject.Find("3_Points_Change").GetComponent<AudioSource>();
        allAudioSources.Add(l);

        AudioSource m = GameObject.Find("3_Points").GetComponent<AudioSource>();
        allAudioSources.Add(m);

        AudioSource n = GameObject.Find("2_Points").GetComponent<AudioSource>();
        allAudioSources.Add(n);

        AudioSource o = GameObject.Find("2_Points_Change").GetComponent<AudioSource>();
        allAudioSources.Add(o);

        AudioSource p = GameObject.Find("1_Point_Change").GetComponent<AudioSource>();
        allAudioSources.Add(p);

        AudioSource q = GameObject.Find("1_Point").GetComponent<AudioSource>();
        allAudioSources.Add(q);
    }

    public void FixedUpdate()
    {
        if (controller.getIsTalking())
        {
            bool isAnyAudioSourcePlaying = CheckForPlayingAudioSources();
            if (!isAnyAudioSourcePlaying)
            {
                //Debug.Log("Audios End");
                controller.setIsTalking(false);
                timeManager.StartTimer();
            }
        }
    }

    public bool CheckForPlayingAudioSources()
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.isPlaying)
            {
                return true;
            }
        }

        return false;
    }
}
