using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviourPun
{
    public Transform[] SpawnPosition;
    public Image youdied;
    public Image someonedied;
    public PhotonView PV;
    
    public string curscene;
    bool turnon = false;
    GameObject LocalPlayer = null;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    private void Update()
    { 

    }
 
    void restart()
    {

        PhotonNetwork.LoadLevel("LoadingScene");
        // SceneManager.LoadScene(curscene);
    }
    public Transform SelectSpwanPosition()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            /*인덱스에 맞는 스폰포지션 리턴*/
            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
                return SpawnPosition[i];
        }
        return null;
    }

    /*Instantiate로 생성된 player중 localplayer 오브젝트 반환하는 함수*/
    public GameObject LocalPlayerObject()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        for(int i =0; i< players.Length; i++)
        {
            if (players[i].transform.GetComponent<PlayerScript>().PV.IsMine)
                return players[i];         
        }
        return null;
    }

    [PunRPC]
    void hitplayer(int index)
    {
        LocalPlayer = LocalPlayerObject();
        PlayerScript PS = LocalPlayer.transform.GetComponent<PlayerScript>();
        PS.isDie = true;

        /*전역변수 플레이어 액터넘버에 맞는 죽은 횟수 더하기*/
        R_NetWorkManager.player_die[index - 1] += 1;
        if (index == PS.PV.OwnerActorNr)  
            youdied.gameObject.SetActive(true);
        
        else
            someonedied.gameObject.SetActive(true);

        if (PhotonNetwork.IsMasterClient && !turnon)
        {
            turnon = true;
            Invoke("restart", 2);
        }
    }

    //DieArea 진입시
    void OnTriggerEnter2D(Collider2D collision)
    {
        /*DieArea 충돌된 태그가 플레이어면*/
        if (collision.tag == "Player")
        {
            int actornum = collision.transform.GetComponent<PlayerScript>().PV.OwnerActorNr;
            PV.RPC("hitplayer", RpcTarget.All, actornum);

        }
    }



}
