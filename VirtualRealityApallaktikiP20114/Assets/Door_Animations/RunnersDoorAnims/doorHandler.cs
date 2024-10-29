using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorHandler : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("dorKey")){// || other.CompareTag("Player")){
            //animator.ResetTrigger("Close");
            animator.SetTrigger("Open");
            //animator.ResetTrigger("Close");
            if(animator.GetBool("Close")){
                animator.ResetTrigger("Close");
            }
        }
    }

    // void OnTriggerStay(Collider other){
    //     animator.ResetTrigger("Close");
    //     animator.ResetTrigger("Open");//na to dokimaso
    // }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("dorKey")){// || other.CompareTag("Player")){
            //animator.ResetTrigger("Open");
            animator.SetTrigger("Close");
            //animator.ResetTrigger("Open");
            if(animator.GetBool("Open")){
                animator.ResetTrigger("Open");
            }
        }
    }
}
