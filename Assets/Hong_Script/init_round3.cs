using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class init_round3 : MonoBehaviourPun
{
    public float LimitTime;
    public Text[] ClockText;
    public Transform[] tr;
    public TimerLimit TL;
    public Image youdied;
    public Image someonedied;
    public GameObject apple;
    public GameObject applespawn;
    public Image[] IsFruit;
    public Sprite apple_silute;
    public GameObject apple_siluet_obj;
    public GameObject player;
    // Start is called before the first frame update

    public void init_round()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            int actnum = player.transform.GetComponent<test>().get_player_index(players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr);
            players[i].transform.GetComponent<PlayerScript>().isDie = false;
            players[i].transform.position = new Vector3(tr[actnum].position.x, tr[actnum].position.y, tr[actnum].position.z);
            IsFruit[i].sprite = apple_silute;
        }

        TL.LimitTime = 45.0f;
        TL.turnon = false;
        ClockText[0].text = "0" + ((int)(Mathf.Round(LimitTime) / 60)).ToString(); //½Ã
        ClockText[1].text = (Mathf.Round(LimitTime) % 60).ToString(); // ºÐ
        youdied.gameObject.SetActive(false);
        someonedied.gameObject.SetActive(false);
        applespawn.transform.GetComponent<Apple_random_Spawn>().apple_respawn();
        apple.transform.parent = null;
        apple.SetActive(true);
        apple_siluet_obj.transform.GetComponent<SpriteRenderer>().sprite = apple_silute;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
