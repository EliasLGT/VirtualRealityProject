using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class RoutineOfRunner : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject PATH;
    public Transform[] PathPoints;
    public int index = 0;
    public int indexForBed;
    public float minDistance = 1;
    public int[] indeces = {2, 3, 4};
    //public Light globalLight;
    public Transform[] bedRoute1;
    public Transform[] bedRoute2;
    public Transform[] bedRoute;
    public bool initialCodeForBedExecuted = false;
    public TMPro.TextMeshProUGUI time;
    public Collider boxColliderOfDoorBlocker;
    public int hour;
    public bool bedr1 = false;
    public bool bedr2 = false;
    public bool pat = false;
    //public Quaternion[] Vers = new float[3];
    // public Quaternion ver3, ver0;
    // public Vector3 ver1, ver2;
    // Start is called before the first frame update
    public String d, d2;
    void Start()
    {
        // ver0 = globalLight.transform.localRotation;
        // ver1 = globalLight.transform.eulerAngles;
        // ver2 = globalLight.transform.localEulerAngles;
        // //Vers[3] = globalLight.transform.localEulerAngles.x;
        // ver3 = globalLight.transform.rotation;
        d = time.text;
        updateHour();
        //hour = Int32.Parse(time.text.Split(':')[0]);
        PathPoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }
        pat = true;
        bedRoute1 = new Transform[5];
        for (int i = 0; i < 5; i++)
        {
            bedRoute1[i] = PathPoints[i];
        }
        bedr1 = true;
        bedRoute2 = new Transform[6];
        for (int i = 0; i < 5; i++)
        {
            bedRoute2[i] = PathPoints[i+5];
        }
        bedRoute2[5] = PathPoints[0];
        bedr2 = true;
        Array.Reverse(bedRoute1);
    }

    // Update is called once per frame
    void Update()
    {
        unblockDoorIfOk();
        // ver0 = globalLight.transform.localRotation;
        // ver1 = globalLight.transform.eulerAngles;
        // ver2 = globalLight.transform.localEulerAngles;
        // //Vers[3] = globalLight.transform.localEulerAngles.x;
        // ver3 = globalLight.transform.rotation;
        d2 = time.text;
        updateHour();
        //hour = Int32.Parse(time.text.Split(':')[0]);
        if(hour >= 6 && hour <= 21){
            roam();
            initialCodeForBedExecuted = false;
        }else{
            if(initialCodeForBedExecuted){
                Walk();
                goToBed();
            }else
            {
                findRouteForBed();
                initialCodeForBedExecuted = true;
            }
        }
        //roam();
    }

    void findRouteForBed(){
        if(index <= 4){
            bedRoute = bedRoute1;
            indexForBed = 4 - index;
        }else{
            bedRoute = bedRoute2;
            indexForBed = index - 5;
        }
        // float minimum = 0;
        // int indexOfMin = 0;
        // for (int i = 0; i < bedRoute.Length; i++)
        // {
        //     float dis = Vector3.Distance(transform.position, bedRoute[i].position);
        //     if(dis <= minimum){
        //         minimum = dis;
        //         indexOfMin = i;
        //     }
        // }
        // indexForBed = indexOfMin;
        index = 0;
    }
    void goToBed(){
        if(!(Vector3.Distance(transform.position, bedRoute[bedRoute.Length - 1].position) <= minDistance)){
            if(Vector3.Distance(transform.position, bedRoute[indexForBed].position) <= minDistance){
                if(indexForBed >= 0 && indexForBed < bedRoute.Length ){
                    indexForBed += 1;
                }
                // else{
                //     index = 2;
                // }
            }
            agent.SetDestination(bedRoute[indexForBed].position);
        }
    }

    void roam(){
        if(Vector3.Distance(transform.position, PathPoints[index].position) <= minDistance){
            if(index >= 0 && index < PathPoints.Length - 1){
                index += 1;
                if(indeces.Contains(index)){
                    Jog();
                }
                else{
                    Walk();
                }
            }
            else{
                index = 2;
            }
        }
        // if(index == 8){
        //     agent.isStopped = true;
        //     Wave();
        //     Stop();
        //     if(indeces.Contains(index)){
        //         Jog();
        //     }
        //     else{
        //         Walk();
        //     }
        //     agent.isStopped = false;
        //     agent.SetDestination(PathPoints[index].position);
        // }

        agent.SetDestination(PathPoints[index].position);

        
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            agent.isStopped = true;
            Wave();
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            if(hour >= 6 && hour <= 9){
                Stop();
                if(indeces.Contains(index)){
                    Jog();
                }
                else{
                    Walk();
                }
                agent.isStopped = false;
                agent.SetDestination(PathPoints[index].position);
            }
            else
            {
                Stop();
                Walk();
                agent.isStopped = false;
                agent.SetDestination(bedRoute[indexForBed].position);
            }
        }
    }

    private void Walk(){
        animator.SetTrigger("Walk");
    }

    private void Jog(){
        animator.SetTrigger("Jog");
    }

    private void Stop(){
        animator.SetTrigger("Stop");
    }

    private void Wave(){
        animator.SetTrigger("Wave");
    }

    // private void stopWaving(){
    //     animator.SetTrigger("stopWaving");
    // }
    private void updateHour(){
        // if (String.Equals(time.text.Split(':')[0].Substring(0, 0), "0"))
        // {
        //     hour = Int32.Parse(time.text.Split(':')[0].Substring(1,1));
        // }else
        // {
        //     hour = Int32.Parse(time.text.Split(':')[0]);
        // }
        String timee = time.text;
        hour = Int32.Parse(timee.Split(':')[0]);
    }
    private void unblockDoorIfOk(){
        // // if(index == 0 && indexForBed){
        // //     if(!boxColliderOfDoorBlocker.isTrigger){
        // //         boxColliderOfDoorBlocker.isTrigger = true;
        // //     }
        // // }
        // if(Vector3.Distance(transform.position, PathPoints[0].position) <= minDistance + 1){
        //     if(boxColliderOfDoorBlocker.isTrigger){
        //         boxColliderOfDoorBlocker.isTrigger = false;
        //     }
        // }else
        // {
        //     if(!boxColliderOfDoorBlocker.isTrigger){
        //         boxColliderOfDoorBlocker.isTrigger = true;
        //     }
        // }
    }
}
