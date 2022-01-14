using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class TimerLimit : MonoBehaviour
{
    public float LimitTime;
    public Text[] ClockText;
    bool turnon = false;
    public Image youdied;
    GameObject LocalPlayer = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void restart()
    {
        PhotonNetwork.LoadLevel("LoadingScene");
    }

    public GameObject LocalPlayerObject()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.GetComponent<PlayerScript>().PV.IsMine)
                return players[i];
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClockText[1].text == "0" && !turnon)
        {
            LocalPlayer = LocalPlayerObject();
            PlayerScript PS = LocalPlayer.transform.GetComponent<PlayerScript>();
            PS.isDie = true;

            youdied.gameObject.SetActive(true);
            if (PhotonNetwork.IsMasterClient && !turnon)
            {
                Invoke("restart", 2);
            }
            turnon = true;
        }
        if (ClockText[1].text == "0")
            return;

        else
        {
            LimitTime -= Time.deltaTime;
            ClockText[0].text = "0" + ((int)(Mathf.Round(LimitTime) / 60)).ToString(); //½Ã
            ClockText[1].text = (Mathf.Round(LimitTime) % 60).ToString(); // ºÐ
        }


    }
}
