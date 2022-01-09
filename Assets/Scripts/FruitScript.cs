using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class FruitScript : MonoBehaviourPunCallbacks
{
    //int FruitCount = 0;
    //public float speed;
    //public float distance;
    public GameObject player;
    public PhotonView PV;
    public AudioSource mysfx;
    public AudioClip applesfx;
    public bool isTrigger = false;
    private string name = "";

    private bool isSound = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Call_update()
    {
        PV.RPC("Syn_update", RpcTarget.All);
    }

    [PunRPC]
    public void Syn_update()
    {
        int i = 0;
        if (isTrigger)
        {
            for (i = 0; i < player.transform.childCount; i++)
            {
                if (player.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == name) break;
            }
            float x = player.transform.GetChild(i).position.x + 0.7f;
            float y = player.transform.GetChild(i).position.y - 0.1f;
            Vector2 vec = new Vector2(x, y);
            transform.position = Vector2.MoveTowards(transform.position, vec, 0.5f);

        }
        //Debug.Log(name);
    }
    
    void Update()
    {
        int i = 0;
        if (isTrigger)
        {
            for (i = 0; i < player.transform.childCount; i++)
            {
                if (player.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == name)
                {
                    break;
                }
            }

            float x = player.transform.GetChild(i).position.x + 0.7f;
            float y = player.transform.GetChild(i).position.y - 0.1f;
            Vector2 vec = new Vector2(x, y);
            transform.position = Vector2.MoveTowards(transform.position, vec, 0.5f);

        }

        Call_update();
        //transform.position = Vector2.MoveTowards(gameObject.transform.position, PlayerPosition, 0.01f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTrigger = true;
            name =  collision.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
            if (!isSound)
            {
                appleSound();
                isSound = true;
            }

        }

    }
    public void appleSound()
    {
        mysfx.PlayOneShot(applesfx);
    }
}
