using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider sliderTimer;
    bool isFinish;

    void Start()
    {
    
    }

    void Update()
    {
        timer();
    }

    void timer()
    {
        if (sliderTimer.value > sliderTimer.minValue)
        {
            sliderTimer.value -= Time.deltaTime;
          }
        else
        {
            if (isFinish == false)
            {
            Debug.Log("Time Out!!!");

            isFinish = true;
            }
        }
    }
}
