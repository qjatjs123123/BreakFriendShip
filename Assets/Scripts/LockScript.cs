using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class LockScript : MonoBehaviour
{
    public Sprite spr;
    public GameObject fruit;
    public Tilemap Round1BreakTile;
    public AudioSource mysfx;
    public AudioClip locksfx;
    
    [SerializeField]
    private Canvas ClearTime;   
    private Text C_Hour;
    private Text C_Min;
    private Text C_sec;
    private Text Time_Hour;
    private Text Time_Min;
    private Text Time_sec;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fruit")
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            int Round = player[0].GetComponent<PlayerScript>().Round;


            gameObject.GetComponent<SpriteRenderer>().sprite = spr;
            fruit.gameObject.SetActive(false);
            //collision.gameObject.SetActive(false);
            Round1BreakTile.gameObject.SetActive(false);
            lockSound();
            if(Round == 3)
                GameObject.Find("TextEffect").GetComponent<Typingeffect>().Stop_Func();

            if(Round== 6)
            {
                ClearTime.gameObject.SetActive(true);

                C_Hour = GameObject.FindWithTag("C_hour").GetComponent<Text>();
                C_Min = GameObject.FindWithTag("C_min").GetComponent<Text>();
                C_sec = GameObject.FindWithTag("C_sec").GetComponent<Text>();

                Time_Hour = GameObject.FindWithTag("hour").GetComponent<Text>();
                Time_Min = GameObject.FindWithTag("min").GetComponent<Text>();
                Time_sec = GameObject.FindWithTag("sec").GetComponent<Text>();


                GameObject.FindWithTag("timer").GetComponent<PlayTimer>().TimerOn = false;

                //시간,데스카운터 불러오기

                C_Hour.GetComponent<Text>().text = (int.Parse(Time_Hour.GetComponent<Text>().text)).ToString();
                C_Min.GetComponent<Text>().text = (int.Parse(Time_Min.GetComponent<Text>().text)).ToString();
                C_sec.GetComponent<Text>().text = (int.Parse(Time_sec.GetComponent<Text>().text)).ToString();

            }

        }
    }
    public void lockSound()
    {
        mysfx.PlayOneShot(locksfx);
    }
}
