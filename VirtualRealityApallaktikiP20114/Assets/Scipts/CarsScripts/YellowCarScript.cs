using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class YellowCarScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject PATH;
    public Transform[] PathPoints;
    public Transform initialPosition;
    public int index = 1;
    public float minDistance = 1;
    public TMPro.TextMeshProUGUI time;
    public int hour;
    public Animator wheelAnimator, wheelAnimator2, wheelAnimator3, wheelAnimator4;
    public bool haveMadeCircle = false;
    //private Transform difference;
    // public Collider entryBorder, exitBorder;
    // Start is called before the first frame update
    void Start()
    {
        updateHour();
        String stri = "SphereForPath";
        PathPoints = new Transform[PATH.transform.childCount];
        PathPoints[0] = PATH.transform.GetChild(0);//Find("SphereForPath");
        for (int i = 1; i < PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.Find(stri + " (" + i + ")");
        }
        foreach (Transform item in PathPoints)
        {
            if (item == null)
            {
                UnityEngine.Debug.Log("Null value at" + item);
            }
        }
        //difference.position = initialPosition.position - PathPoints[PathPoints.Length - 1].position;
    }

    // Update is called once per frame
    void Update()
    {
        updateHour();

        // if(hour >= 6 && hour <= 19){
        //     if(Vector3.Distance(transform.position, PathPoints[3].position) > minDistance){
        //         goToTalkingSpot();
        //     }else
        //     {
        //         talk();
        //     }
        // }else{
        //     goToBed();
        // }
        if(Vector3.Distance(transform.position, PathPoints[index].position) <= minDistance){
            if(index >= 0 && index < PathPoints.Length - 1){
                if (index == 19 && haveMadeCircle)//(hour < 6 || hour > 19))
                {
                    index = 37;
                    agent.speed = 9;
                    // if(hour > 21){
                    //     agent.speed = 9;
                    // }
                }else if (index == 36)
                {
                    index = 13;
                }else if (index == 50)
                {
                    index = 0;
                }
                else
                {
                    index += 1;
                    if (index == 10)
                    {
                        agent.speed = 4.5f;
                    }else if (index == 20)
                    {
                        haveMadeCircle = true;
                    }else if (index == 47)
                    {
                        agent.speed = 40;
                    }else if (index == 1)
                    {
                        if(hour >= 4){
                            agent.speed = 11;
                        }
                    }
                }
            }
            // else if (index == 46)// && hour >= 0 && hour <= 6)
            // {
            //     transform.position = initialPosition.position;//PathPoints[0].position;
            //     haveMadeCircle = false;
            //     // if (transform.position == initialPosition.position)
            //     // {
            //     //     index = 1;
            //     //     if (hour >= 4){
            //     //         agent.speed = 11;
            //     //     }
            //     // }
            //     index = 1;
            //     if (hour >= 4){
            //         agent.speed = 11;
            //     }
            // }
        }

        agent.SetDestination(PathPoints[index].position);
    }

    // private void OnTriggerEnter(Collider other){
    //     if(other.CompareTag("Player") || other.CompareTag("Interactable")){
    //         agent
    //     }
    // }

    // private void OnCollisionEnter(Collision collision){
    //     if(collision.gameObject.tag == "entryBorder")
    // }

    private void OnTriggerStay(Collider other){
        if(other.CompareTag("Player") || other.CompareTag("Interactable") || other.CompareTag("Car")){
            agent.isStopped = true;
            wheelAnimator.speed = 0f;
            wheelAnimator2.speed = 0f;
            wheelAnimator3.speed = 0f;
            wheelAnimator4.speed = 0f;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player") || other.CompareTag("Interactable") || other.CompareTag("Car")){
            agent.isStopped = false;
            wheelAnimator.speed = 1f;
            wheelAnimator2.speed = 1f;
            wheelAnimator3.speed = 1f;
            wheelAnimator4.speed = 1f;
        }
    }

    private void updateHour(){
        String timee = time.text;
        hour = Int32.Parse(timee.Split(':')[0]);
    }
}
