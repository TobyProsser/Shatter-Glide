using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DarknessController : MonoBehaviour
{
    [Header("Day/Night Change Objects")]
    public Camera camera;
    public Light2D mainLight;

    [Header("Day/Night Settings")]
    public Color dayColor;
    public float dayLightness;
    public Color nightColor;
    public float nightLightness;

    //Time that lightness lasts after lighting torch
    public float lightTime;

    bool changingToDay;
    void Start()
    {
        StartCoroutine(ChangeTime());
    }

    IEnumerator ChangeTime()
    {
        float time = 0;
        while (true)
        {
            if (!changingToDay)
            {
                time += Time.deltaTime / lightTime;
                Color curColor = Color.Lerp(dayColor, nightColor, time);
                float curLightness = Mathf.Lerp(dayLightness, nightLightness, time);

                camera.backgroundColor = curColor;
                mainLight.intensity = curLightness;
            }
            else time = 0;
            
            yield return null;
        }
    }

    public void SetDay() { StartCoroutine(ChangeToDay()); }

    IEnumerator ChangeToDay()
    {
        changingToDay = true;

        print("Change To Day");
        float time = 0;
        while (true)
        {
            time += Time.deltaTime / 2;

            //Change from current values back to day
            Color curColor = Color.Lerp(camera.backgroundColor, dayColor, time);
            float curLightness = Mathf.Lerp(mainLight.intensity, dayLightness, time);

            camera.backgroundColor = curColor;
            mainLight.intensity = curLightness;

            //once timer is done, break
            if (time >= 2) break;

            yield return null;
        }

        changingToDay = false;
    }
}
