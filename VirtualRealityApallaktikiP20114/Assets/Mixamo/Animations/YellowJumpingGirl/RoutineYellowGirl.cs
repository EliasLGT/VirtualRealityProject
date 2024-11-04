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
    // public int indexForBed;
    public float minDistance = 0;
    // public Transform[] bedRoute;
    // public bool initialCodeForBedExecuted = false;
    public TMPro.TextMeshProUGUI time;
    public int hour;
    public Rigidbody doorRigidbody;
    public bool jumping = false, haveYawned = false, check = false;
    public int dorout = 0, yan = 0, yan2 = 0, dowork = 0, rom = 0, rom2 = 0, rom3 = 0;
    // Start is called before the first frame update
    // void Start()
    // {
    //     updateHour();
    //     //hour = Int32.Parse(time.text.Split(':')[0]);
    //     PathPoints = new Transform[PATH.transform.childCount];
    //     for (int i = 0; i < PathPoints.Length; i++)
    //     {
    //         PathPoints[i] = PATH.transform.GetChild(i);
    //     }
    //     // bedRoute = new Transform[4];
    //     // for (int i = 0; i < 4; i++)
    //     // {
    //     //     bedRoute[i] = PathPoints[i];
    //     // }
    //     // Array.Reverse(bedRoute);
    // }

    void Start()
    {
        updateHour();
        PathPoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }
        Walk();
    }

    void Update(){
        unblockDoorIfOk();
        updateHour();

        if(hour >= 5 && hour <= 18){
            if(Vector3.Distance(transform.position, PathPoints[3].position) > minDistance){
                goToWorkoutSpot();
            }else
            {
                workout();
            }
            // if(Vector3.Distance(transform.position, PathPoints[1].position) <= minDistance && !haveYawned){
            //     yawn();
            // }
        }else{
            goToBed();
        }
    }

    private void goToWorkoutSpot(){
        if(!haveYawned && Vector3.Distance(transform.position, PathPoints[1].position) <= minDistance){
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Walking")){
                Stop();
            }
            // if(animator.GetBool("Stop")){
            //     animator.ResetTrigger("Stop");
            // }
            yawn();
        }else
        {
            if(Vector3.Distance(transform.position, PathPoints[index].position) <= minDistance){
                if(index >= 0 && index < PathPoints.Length - 1){
                    index += 1;
                }
            }

            agent.SetDestination(PathPoints[index].position);
        }
    }

    private void yawn(){
        if(check && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.GetBool("stopYawning")){
            // if(animator.GetBool("Stop")){
            //     animator.ResetTrigger("Stop");
            // }
            haveYawned = true;
            animator.ResetTrigger("Stop");
            Walk();
        }else{
            // if(animator.GetBool("startYawning")){
            //     animator.SetTrigger("startYawning");
            // }
            // if(animator.GetBool("stopYawning")){
            //     animator.SetTrigger("stopYawning");
            // }
            if(!check){
                animator.SetTrigger("startYawning");
                animator.SetTrigger("stopYawning");
                check = true;
            }
        }
        // if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
        //     animator.SetTrigger("startYawning");
        //     check = true;
        // }else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.GetBool("startYawning"))
        // {
        //     if(animator.GetBool("Stop")){
        //         animator.ResetTrigger("Stop");
        //     }
        //     haveYawned = true;
        // }
    }

    private void workout(){
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping Jacks")){
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Waving")){
                agent.isStopped = true;
                Stop();
                startJumping();
            }
            // agent.isStopped = true;
            // Stop();
            // startJumping();
        }else{
            if(animator.GetBool("Stop") && animator.GetBool("startJumping")){
                animator.ResetTrigger("Stop");
                animator.ResetTrigger("startJumping");
            }
        }
        // if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping Jacks")){
        //     agent.isStopped = true;
        //     Stop();
        //     startJumping();
        //     Debug.Log("notJumping");
        // }else
        // {
        //     Debug.Log("jumping");
        // }
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     unblockDoorIfOk();
    //     updateHour();

    //     if(Vector3.Distance(transform.position, PathPoints[0].position) <= minDistance){
    //         haveYawned = false;
    //     }

    //     if(hour >= 6 && hour <= 19){
    //         doRoutine();
    //     }else{
    //         if(jumping){
    //             stopJumping();
    //         }else
    //         {
    //             if(!animator.GetBool("idle")){
    //                 // if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Walking") && animator.GetBool("Walking")){
    //                 //     Walk();
    //                 // }
    //                 Walk();
    //                 goToBed();
    //             }
    //         }
    //     }
    // }

    // private void stopJumping(){
    //     jumping = false;
    //     animator.SetTrigger("idle");
    //     animator.SetTrigger("stopJumping");
    // }

    // void doRoutine(){
    //     dorout += 1;
    //     if((Vector3.Distance(transform.position, PathPoints[1].position) <= minDistance) && !haveYawned){
    //         yan += 1;
    //         yawn();
    //     }
    //     else if(Vector3.Distance(transform.position, PathPoints[PathPoints.Length - 1].position) <= minDistance){
    //         dowork += 1;
    //         animator.ResetTrigger("Walk");
    //         doWorkout();
    //     }else
    //     {
    //         rom += 1;
    //         if(!animator.GetBool("stopYawning")){
    //             rom2 += 1;
    //             if(Vector3.Distance(transform.position, PathPoints[index].position) <= minDistance){
    //                 rom3 += 1;
    //                 if(index >= 0 && index < PathPoints.Length - 1){
    //                     index += 1;
    //                     if(index == 1){
    //                         animator.ResetTrigger("Walk");
    //                     }
    //                     //Walk();
    //                 }
    //             }
    //             if(haveYawned){
    //                 agent.isStopped = false;
    //                 Walk();
    //             }
    //             agent.SetDestination(PathPoints[index].position);
    //         }
    //     }
    // }

    // private void yawn(){
    //     if(!animator.GetBool("stopYawning")){
    //         yan2 += 1;
    //         haveYawned = true;
    //         agent.isStopped = true;
    //         animator.SetTrigger("Stop");
    //         animator.SetTrigger("startYawning");
    //         animator.SetTrigger("stopYawning");
    //     }
    //     // if(!yawning){
    //     //     yawning = true;
    //     //     animator.SetTrigger("startYawning");
    //     //     animator.SetTrigger("stopYawning");
    //     // }
    // }

    // private void doWorkout(){
    //     if(!jumping){
    //         jumping = true;
    //         Stop();
    //         startJumping();
    //     }
    // }

    // private void OnTriggerEnter(Collider other){
    //     if (other.CompareTag("Player")){
    //         if(!jumping){
    //             agent.isStopped = true;
    //             Wave();
    //         }else
    //         {
    //             Stop();
    //             Wave();
    //         }
    //     }
    // }

    // private void OnTriggerExit(Collider other){
    //     if (other.CompareTag("Player")){
    //         if(jumping){
    //             Stop();
    //             startJumping();
    //         }
    //         else
    //         {
    //             if(hour >= 6 && hour <= 19){
    //                 Stop();
    //                 Walk();
    //                 agent.isStopped = false;
    //                 agent.SetDestination(PathPoints[index].position);
    //             }
    //             else
    //             {
    //                 Stop();
    //                 Walk();
    //                 agent.isStopped = false;
    //                 agent.SetDestination(PathPoints[0].position);
    //             }
    //         }
    //     }
    // }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            if(haveYawned){
                if(!agent.isStopped){
                    agent.isStopped = true;
                }
                Wave();
                // if(hour >= 6 && hour <= 18){
                //     if(Vector3.Distance(transform.position, PathPoints[3].position) <= minDistance){
                //         if(!agent.isStopped){
                //             agent.isStopped = true;
                //         }
                //         animator.SetTrigger("idle");
                //         stopJumping();
                //     }else
                //     {
                //         if(agent.isStopped){
                //             agent.isStopped = false;
                //         }
                //         Stop();
                //         Walk();
                //     }
                // }else
                // {
                //     if(agent.isStopped){
                //         agent.isStopped = false;
                //     }
                //     Stop();
                //     Walk();
                // }
            }
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            if(haveYawned){
                // if(agent.isStopped){
                //     agent.isStopped = false;
                // }
                if(hour >= 6 && hour <= 18){
                    if(Vector3.Distance(transform.position, PathPoints[3].position) <= minDistance){
                        if(!agent.isStopped){
                            agent.isStopped = true;
                        }
                        Stop();
                        startJumping();
                    }else
                    {
                        if(agent.isStopped){
                            agent.isStopped = false;
                        }
                        Stop();
                        Walk();
                    }
                }else
                {
                    if(agent.isStopped){
                        agent.isStopped = false;
                    }
                    Stop();
                    Walk();
                }
            }
        }
    }

    private void startJumping(){
        animator.SetTrigger("startJumping");
    }

    private void stopJumping(){
        animator.SetTrigger("stopJumping");
    }

    private void Walk(){
        animator.SetTrigger("Walk");
    }

    private void Stop(){
        animator.SetTrigger("Stop");
    }

    private void Wave(){
        animator.SetTrigger("Wave");
    }
    void goToBed(){
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping Jacks")){
            stopJumping();
            animator.SetTrigger("idle");
        }else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
            if(agent.isStopped){
                agent.isStopped = false;
            }
            Walk();
            if(Vector3.Distance(transform.position, PathPoints[0].position) > minDistance){
                agent.SetDestination(PathPoints[0].position);
            }
        }else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Walking")){
            if(Vector3.Distance(transform.position, PathPoints[0].position) > minDistance){
                agent.SetDestination(PathPoints[0].position);
            }else
            {
                index = 0;
                check = false;
                haveYawned = false;
            }
            if(animator.GetBool("Walk")){
                animator.ResetTrigger("Walk");
            }
            if(animator.GetBool("stopJumping")){
                animator.ResetTrigger("stopJumping");
            }
            if(animator.GetBool("startJumping")){
                animator.ResetTrigger("startJumping");
            }
            if(animator.GetBool("Stop")){
                animator.ResetTrigger("Stop");
            }
            // else
            // {
            //     if(animator.GetBool("Walk")){
            //         animator.ResetTrigger("Walk");
            //     }
            //     if(animator.GetBool("stopJumping")){
            //         animator.ResetTrigger("stopJumping");
            //     }
            // }
        }
    }
    private void updateHour(){
        String timee = time.text;
        hour = Int32.Parse(timee.Split(':')[0]);
    }
    private void unblockDoorIfOk(){
        if(Vector3.Distance(transform.position, PathPoints[0].position) <= minDistance){
            doorRigidbody.isKinematic = true;
        }else
        {
            doorRigidbody.isKinematic = false;
        }
    }
}
