using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorHandler : MonoBehaviour
{
    public Animator animator;
    public bool enter = false;
    public bool exit = false;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("dorKey")){// || other.CompareTag("Player")){
            enter = true;
            exit = false;
            //animator.ResetTrigger("Close");
            animator.SetTrigger("Open");
            //animator.ResetTrigger("Close");
            if(animator.GetBool("Close")){
                animator.ResetTrigger("Close");
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("dorKey")){// || other.CompareTag("Player")){
            enter = false;
            exit = true;
            //animator.ResetTrigger("Open");
            animator.SetTrigger("Close");
            //animator.ResetTrigger("Open");
            if(animator.GetBool("Open")){
                animator.ResetTrigger("Open");
            }
        }
    }
}
