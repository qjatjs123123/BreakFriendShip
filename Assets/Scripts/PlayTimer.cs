using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimer : MonoBehaviour
{
    public Text[] ClockText;
    public bool TimerOn = false;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            time += Time.deltaTime;

            ClockText[0].text = ((int)time / 3600).ToString(); //Ω√
            ClockText[1].text = (((int)time / 60 % 60)).ToString(); // ∫–
            ClockText[2].text = (((int)time % 3600) % 60).ToString(); //√ 
        }
    }
}
