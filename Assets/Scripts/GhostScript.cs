using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GhostScript : MonoBehaviourPunCallbacks
{
    //int FruitCount = 0;
    public float speed;
    public float distance;
    public GameObject PlayerSpawn;
    public GameObject PlayerSpawn2;
    public GameObject player;
    public PhotonView PV;
    public Rigidbody2D RB;
    public GameObject GhostTest;


    public string names = "";
    void Start()
    {
    }
    void Update()
    {
        names = GameObject.Find("R6GhostTrigger").GetComponent<GhostTrigger>().names;

    }

    public void GhostMove()
    {
        if (names != "")
        {
            int i = 0;

            for (i = 0; i < player.transform.childCount; i++)
            {
                if (player.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == names) break;
            }

            float x = player.transform.GetChild(i).position.x;
            float y = player.transform.GetChild(i).position.y;
            Vector2 vec = new Vector2(x, y);
            transform.position = Vector2.MoveTowards(transform.position, vec, 0.015f);

            PV.RPC("Syn_update_move", RpcTarget.All);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int index = 0;
            Debug.Log("dieArea ¡¯¿‘!");
            for (index = 0; index < player.transform.childCount; index++)
            {
                if (player.transform.GetChild(index).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == PhotonNetwork.LocalPlayer.NickName)
                {
                    break;
                }
            }

            player.transform.GetChild(index).transform.position = new Vector3(PlayerSpawn.transform.position.x, PlayerSpawn.transform.position.y, PlayerSpawn.transform.position.z);
            GhostTest.transform.position = new Vector3(PlayerSpawn2.transform.position.x, PlayerSpawn2.transform.position.y, PlayerSpawn2.transform.position.z);

        }

    }

    [PunRPC]
    public void Syn_update_move()
    {
        if (names != null)
        {
            int i = 0;

            for (i = 0; i < player.transform.childCount; i++)
            {
                if (player.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == names) break;
            }

            float x = player.transform.GetChild(i).position.x;
            float y = player.transform.GetChild(i).position.y;
            Vector2 vec = new Vector2(x, y);
            transform.position = Vector2.MoveTowards(transform.position, vec, 0.015f);

        }
        //Debug.Log(name);
    }


}
