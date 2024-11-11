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
    //public Transform initialPosition;
    public Vector3 Position;
    public int index = 1;
    public float minDistance = 1;
    public TMPro.TextMeshProUGUI time;
    public int hour;
    public Animator wheelAnimator, wheelAnimator2, wheelAnimator3, wheelAnimator4;
    public bool haveMadeCircle = false, iAmYellowCar;
    public GameObject otherCar;
    public Transform startingPointOfNonYellowCar;
    public AudioSource honk;
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
        // Position = new Vector3(511.55f, 0.74f, -352.73f);
        // UnityEngine.Debug.Log(transform.position);
        // UnityEngine.Debug.Log(transform.localPosition);
        // UnityEngine.Debug.Log(PathPoints[0].position);
        // UnityEngine.Debug.Log(PathPoints[0].localPosition);
        if (otherCar.Equals(GameObject.Find("Car 2")))
        {
            iAmYellowCar = false;
            UnityEngine.Debug.Log("I am not Yellow car");
        }else{
            iAmYellowCar = true;
            UnityEngine.Debug.Log("I am Yellow car");
        }
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
        if(Vector3.Distance(transform.position, PathPoints[index].position) <= minDistance || (!iAmYellowCar && Vector3.Distance(transform.position, startingPointOfNonYellowCar.position) <= minDistance && index == 0)){
            if(index >= 0 && index < PathPoints.Length){// - 1){
                if (index == 19 && haveMadeCircle)//(hour < 6 || hour > 19))
                {
                    index = 37;
                    agent.speed = 9;
                    //agent.autoBraking = true;
                    // if(hour > 21){
                    //     agent.speed = 9;
                    // }
                }else if (index == 36)
                {
                    index = 13;
                }else if (index == 46){
                    //agent.isStopped = true;
                    agent.enabled = false;
                    index = 0;
                    haveMadeCircle = false;
                    if(iAmYellowCar){
                        transform.position = PathPoints[0].position;
                    }else
                    {
                        transform.position = startingPointOfNonYellowCar.position;
                    }
                }//else if (index == 50)
                // {
                //     index = 0;
                //     haveMadeCircle = false;
                //     agent.autoBraking = true;
                //     agent.speed = 15;
                // }
                else
                {
                    index += 1;
                    if (index == 10)
                    {
                        agent.speed = 4f;
                        agent.autoBraking = false;
                    }else if (index == 20)
                    {
                        haveMadeCircle = true;
                    }else if (index == 47)
                    {
                        agent.speed = 60;
                        agent.autoBraking = true;
                    }else if (index == 1 && hour < 16)
                    {
                        //agent.isStopped = false;
                        agent.enabled = true;
                        agent.autoBraking = false;
                        agent.speed = 9;
                        if(hour >= 3){
                            agent.speed = 12;
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

        if(agent.enabled){
            agent.SetDestination(PathPoints[index].position);
        }else
        {
            if (index == 1 && hour < 16)
            {
                agent.enabled = true;
                agent.autoBraking = false;
                agent.speed = 9;
                if(hour >= 3){
                    agent.speed = 12;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Car")){//if(other.CompareTag("Player") || other.CompareTag("Interactable") || other.CompareTag("Car")){
            agent.isStopped = true;
            wheelAnimator.speed = 0f;
            wheelAnimator2.speed = 0f;
            wheelAnimator3.speed = 0f;
            wheelAnimator4.speed = 0f;
            honk.enabled = true;
        }
    }

    // private void OnCollisionEnter(Collision collision){
    //     if(collision.gameObject.tag == "entryBorder")
    // }

    private void OnTriggerStay(Collider other){
        if(other.CompareTag("Player") || other.CompareTag("Interactable")){// || other.CompareTag("Car")){
            agent.isStopped = true;
            wheelAnimator.speed = 0f;
            wheelAnimator2.speed = 0f;
            wheelAnimator3.speed = 0f;
            wheelAnimator4.speed = 0f;
            honk.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player") || other.CompareTag("Interactable") || other.CompareTag("Car")){
            agent.isStopped = false;
            wheelAnimator.speed = 1f;
            wheelAnimator2.speed = 1f;
            wheelAnimator3.speed = 1f;
            wheelAnimator4.speed = 1f;
            honk.enabled = false;
        }
    }

    private void updateHour(){
        String timee = time.text;
        hour = Int32.Parse(timee.Split(':')[0]);
    }
}
