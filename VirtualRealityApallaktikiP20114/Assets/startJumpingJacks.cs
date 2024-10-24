using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startJumpingJacks : MonoBehaviour
{
    public GameObject messageDisplay; //can be text or 3d object
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        //filter only player
        if (other.tag == "Player"){
            messageDisplay.gameObject.SetActive(true);
        }
    }
}
