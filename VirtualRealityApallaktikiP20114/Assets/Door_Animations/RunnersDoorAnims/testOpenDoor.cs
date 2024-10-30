using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testOpenDoor : MonoBehaviour
{
    public Animator animator;
    public bool inside = false;
    public bool outside = true;
    
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("dorKey") || other.CompareTag("Player")){
            inside = true;
            outside = false;
            animator.SetTrigger("Open");
        }
    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("dorKey") || other.CompareTag("Player")){
            inside = false;
            outside = true;
            animator.SetTrigger("Close");
        }
    }
}
