using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoutineChutback : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject PATH;
    public Transform[] PathPoints;
    public int index = 0;
    public float minDistance = 0;
    public TMPro.TextMeshProUGUI time;
    public int hour;
    public Rigidbody doorRigidbody;
    public bool haveYawned = false, check = false;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        unblockDoorIfOk();
        updateHour();

        if(hour >= 5 && hour <= 18){
            if(Vector3.Distance(transform.position, PathPoints[4].position) > minDistance){
                goToYellingSpot();
            }else
            {
                yell();
            }
        }else{
            goToBed();
        }
    }

    private void goToYellingSpot(){
        if(!haveYawned && Vector3.Distance(transform.position, PathPoints[1].position) <= minDistance){
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Walking")){
                Stop();
            }
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
            haveYawned = true;
            animator.ResetTrigger("Stop");
            Walk();
        }else{
            if(!check){
                animator.SetTrigger("startYawning");
                animator.SetTrigger("stopYawning");
                check = true;
            }
        }
    }

    private void yell(){
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Yelling")){
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Waving")){
                agent.isStopped = true;
                Stop();
                startYelling();
            }
        }else{
            if(animator.GetBool("Stop") && animator.GetBool("startYelling")){
                animator.ResetTrigger("Stop");
                animator.ResetTrigger("startYelling");
            }
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            if(haveYawned){
                if(!agent.isStopped){
                    agent.isStopped = true;
                }
                Wave();
            }
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            if(haveYawned){
                if(hour >= 6 && hour <= 18){
                    if(Vector3.Distance(transform.position, PathPoints[4].position) <= minDistance){
                        if(!agent.isStopped){
                            agent.isStopped = true;
                        }
                        Stop();
                        startYelling();
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

    private void startYelling(){
        animator.SetTrigger("startYelling");
    }

    // private void stopYelling(){
    //     animator.SetTrigger("idle");
    // }

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
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Yelling")){
            //stopYelling();
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
            // if(animator.GetBool("stopYelling")){
            //     animator.ResetTrigger("stopYelling");
            // }
            if(animator.GetBool("startYelling")){
                animator.ResetTrigger("startYelling");
            }
            if(animator.GetBool("Stop")){
                animator.ResetTrigger("Stop");
            }
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
