using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoutineOfRunner : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject PATH;
    private Transform[] PathPoints;
    public int index = 0;
    public float minDistance = 10;
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
        if(Vector3.Distance(transform.position, PathPoints[index].position) < minDistance){
            if(index > 0 && index < PathPoints.Length){
                index += 1;
            }
            else{
                index = 0;
            }
        }

        agent.SetDestination(PathPoints[index].position);
    }
}
