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
    public GameObject player;

    public string curscene;
    bool turnon = false;
    GameObject LocalPlayer = null;
    public GameObject apple;



    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    private void Update()
    { 

    }
 
    void restart()
    {
        PV.RPC("respawn", RpcTarget.AllViaServer);
        //PhotonNetwork.LoadLevel("LoadingScene");
        // SceneManager.LoadScene(curscene);
    }

    /*라운드에 맞는 초기화함수 호출*/
    [PunRPC]
    void respawn()
    {
        if (R_NetWorkManager.round == 1 || R_NetWorkManager.round == 5)
            GameObject.FindGameObjectWithTag("init").transform.GetComponent<init_round1>().init_round();
        else if (R_NetWorkManager.round == 3)
            GameObject.FindGameObjectWithTag("init").transform.GetComponent<init_round3>().init_round();
        else if (R_NetWorkManager.round == 4)
            GameObject.FindGameObjectWithTag("init").transform.GetComponent<init_round4>().init_round();
        else if (R_NetWorkManager.round == 6)
            GameObject.FindGameObjectWithTag("init").transform.GetComponent<init_round6>().init_round();
        Debug.Log("respawn함수");
        turnon = false;
        apple.SetActive(true);
    }

    /*현재 로컬 포지션 반환함수*/
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
        turnon = true;
        /*여러명이 동시에 떨어졌을 때 죽는 화면 한번만 출력되도록 하기 위해서 사용*/
        if (youdied.gameObject.activeSelf == true || someonedied.gameObject.activeSelf == true)
            return;

        /*로컬 캐릭터 isDle함으로써 못움직이게 하기*/
        LocalPlayer = LocalPlayerObject();
        PlayerScript PS = LocalPlayer.transform.GetComponent<PlayerScript>();
        int num = player.transform.GetComponent<test>().get_player_index(index);
        Debug.Log(num);
        PS.isDie = true;
        R_NetWorkManager.player_die[num] += 1;
        /*전역변수 플레이어 액터넘버에 맞는 죽은 횟수 더하기*/
        if (index == PS.PV.OwnerActorNr)
        {
            youdied.gameObject.SetActive(true);        
        }


        else
            someonedied.gameObject.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("restart", 2);
        }

        //if (PhotonNetwork.IsMasterClient && !turnon)
        //{
        //    turnon = true;
        //    Invoke("restart", 2);
        //}
    }


    //DieArea 진입시
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" || collision.tag != "ghost")
            return;
        LocalPlayer = LocalPlayerObject();
        PlayerScript PS = LocalPlayer.transform.GetComponent<PlayerScript>();
        PhotonView collision_pv = collision.transform.GetComponent<PlayerScript>().PV;
        
        /*DieArea 충돌된 태그가 플레이어 중심으로 RPC호출*/
        if (collision.tag == "Player" && collision_pv == PS.PV && !turnon)
        {
            int actornum = collision.transform.GetComponent<PlayerScript>().PV.OwnerActorNr;
            PV.RPC("hitplayer", RpcTarget.All, actornum);          
        }


    }
}
