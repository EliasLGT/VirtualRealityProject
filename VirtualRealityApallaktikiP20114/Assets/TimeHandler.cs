using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;
 
    [SerializeField] private Gradient graddientNightToSunrise;
    [SerializeField] private Gradient graddientSunriseToDay;
    [SerializeField] private Gradient graddientDayToSunset;
    [SerializeField] private Gradient graddientSunsetToNight;

    //[SerializeField] private TextMeshProUGUI Clock;
 
    [SerializeField] private Light globalLight;
    private int minutes;
    public int Minutes{ get { return minutes; } set { minutes = value; OnMinutesChange(minutes); }}
    private int hours;
    public int Hours{ get { return hours; } set { hours = value; OnHoursChange(hours); }}

    private float tempSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox.SetTexture("_Texture1", skyboxNight);
        RenderSettings.skybox.SetTexture("_Texture2", skyboxSunrise);
        RenderSettings.skybox.SetFloat("_Blend", 0);
        RenderSettings.ambientLight = new Color((float)0.2156, (float)0.5607, (float)0.8313);
        globalLight.color = new Color((float)0.2156, (float)0.5607, (float)0.8313); //(float) (55/255), (float) (143/255), (float) (212/255));
        //RenderSettings.subtractiveShadowColor = new Color((float)0.2156, (float)0.5607, (float)0.8313);
        //RenderSettings.fogColor = globalLight.color;
    }

    // Update is called once per frame
    void Update()
    {
        tempSecond += Time.deltaTime*10; //(float) 0.1; 
        if(tempSecond>=1){
            Minutes += 1; //minutes += 1;
            tempSecond = 0;
        }
        //minutes += 0.1;
    }

    private void OnMinutesChange(int value){
        
        switch(hours)
        {
            case > 6 and <= 8:
            //globalLight.transform.Rotate(Vector3.right, 1f/6f, Space.World);
            globalLight.transform.rotation *= Quaternion.Euler(1f/6f, 0, 0); //torque * Time.deltaTime);
            break;
            case > 8 and <= 18:
            //globalLight.transform.Rotate(Vector3.right, 11f/60f, Space.World);
            globalLight.transform.rotation *= Quaternion.Euler(95f/600f, 0, 0);
            break;
            case > 18 and <= 22:
            //globalLight.transform.Rotate(Vector3.right, 5f/24f, Space.World);
            globalLight.transform.rotation *= Quaternion.Euler(65f/240f, 0, 0);
            break;
            case > 22 or <= 6:
            //globalLight.transform.Rotate(Vector3.right, 0.375f, Space.World);
            globalLight.transform.rotation *= Quaternion.Euler(0.375f, 0, 0);
            break;
        }
        
        //globalLight.transform.Rotate(Vector3.up, (1f / (1440f / 4f)) * 360f, Space.World);
        if(value >= 60){//mallon >=
            Hours++;
            minutes = 0;
        }
        if(Hours >= 24){// mallon >=
            Hours = 0;
        }

        // if(Clock != null){
        //     Clock.text = hours.ToString() + ":" + minutes.ToString();
        // }
    }
    private void OnHoursChange(int value){
        if(value == 6){
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 10f));
            StartCoroutine(LerpLight(graddientNightToSunrise, 10f, 0));
        }
        else if(value == 8){
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 10f));
            StartCoroutine(LerpLight(graddientSunriseToDay, 10f, -1));
        }
        else if(value == 18){
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 10f));
            StartCoroutine(LerpLight(graddientDayToSunset, 10f, 1));
        }
        else if(value == 22){
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 10f));
            StartCoroutine(LerpLight(graddientSunsetToNight, 10f, 0));
        }
    }

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0);
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Blend", i / time);
            yield return null;
        }
        RenderSettings.skybox.SetTexture("_Texture1", b);
    }
    private IEnumerator LerpLight(Gradient lightGradient, float time, int f)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);
            RenderSettings.ambientLight = lightGradient.Evaluate(i / time);
            //RenderSettings.subtractiveShadowColor = lightGradient.Evaluate(i / time);
            RenderSettings.fogColor = globalLight.color;
            // switch(f){
            //     case -1:
            //         RenderSettings.fogDensity += (float) 0.000025;
            //         break;
            //     case 0:
            //         break;
            //     case 1:
            //         RenderSettings.fogDensity -= (float) 0.000025;
            //         break;
            //     default:
            //         break;
            // }
            yield return null;
        }
    }
}
