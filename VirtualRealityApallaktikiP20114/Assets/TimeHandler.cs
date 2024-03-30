using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    private int minutes;
    public int Minutes{ get { return minutes; } set { minutes = value; OnMinutesChange(value); }}
    private int hours;
    public int Hours{ get { return hours; } set { hours = value; OnHoursChange(value); }}

    private float tempSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempSecond += Time.deltaTime;
        if(tempSecond>=1){
            minutes += 1;
            tempSecond = 0;
        }
    }

    private void OnMinutesChange(int value){
        if(value > 60){//mallon >=
            Hours++;
            minutes = 0;
        }
        if(Hours > 24){// mallon >=
            Hours = 0;
        }
    }
    private void OnHoursChange(int value){
        if(value == 6){}
        else if(value == 8){}
        else if(value == 18){}
        else if(value == 22){}
    }
}
