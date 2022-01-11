using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerLimit : MonoBehaviour
{
    public float LimitTime;
    public Text[] ClockText;
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LimitTime -= Time.deltaTime;
        ClockText[0].text = "0"+((int)(Mathf.Round(LimitTime)/60)).ToString(); //½Ã
        ClockText[1].text = (Mathf.Round(LimitTime) % 60).ToString(); // ºÐ
    }
}
