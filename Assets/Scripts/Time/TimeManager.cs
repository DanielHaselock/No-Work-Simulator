using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float TimeLeft;
    private bool TimerOn = true;

    public Text TimerTxt;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                TimerOn = false;
            }

        }
        
    }

    void UpdateTimer(float Currenttime)
    {
        Currenttime += 1;
        float Minutes = Mathf.FloorToInt(Currenttime / 60);
        float Seconds = Mathf.FloorToInt(Currenttime % 60);
        TimerTxt.text = string.Format("{0:00} : {1:00}", Minutes, Seconds);

    }
}
