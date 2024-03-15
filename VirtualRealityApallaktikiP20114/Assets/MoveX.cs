using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveX : MonoBehaviour
{
    public float speed = 3f;
    public float torque = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(speed, 0, 0)* Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, 0, torque * Time.deltaTime);
    }
}
