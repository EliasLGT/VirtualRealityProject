using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RoutineOfRunner : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject PATH;
    private Transform[] PathPoints;
    public int index = 0;
    public float minDistance = 0;
    public int[] indeces = {2, 3, 4};
    // Start is called before the first frame update
    void Start()
    {
        PathPoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        roam();
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
}
