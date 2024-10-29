using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoutineYellowGirl : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject PATH;
    public Transform[] PathPoints;
    public int index = 0;
    public int indexForBed;
    public float minDistance = 0;
    public Transform[] bedRoute;
    public bool initialCodeForBedExecuted = false;
    public TMPro.TextMeshProUGUI time;
    public int hour;
    // Start is called before the first frame update
    void Start()
    {
        updateHour();
        //hour = Int32.Parse(time.text.Split(':')[0]);
        PathPoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }
        bedRoute = new Transform[4];
        for (int i = 0; i < 4; i++)
        {
            bedRoute[i] = PathPoints[i];
        }
        Array.Reverse(bedRoute);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void updateHour(){
        String timee = time.text;
        hour = Int32.Parse(timee.Split(':')[0]);
    }
}
