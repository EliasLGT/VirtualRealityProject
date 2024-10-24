using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatGuy : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            Point();
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            stopPointing();
        }
    }

    private void Point(){
        animator.SetTrigger("Point");
    }

    private void stopPointing(){
        animator.SetTrigger("stopPointing");
    }
}
