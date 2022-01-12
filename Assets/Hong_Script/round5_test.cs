using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class round5_test : MonoBehaviourPunCallbacks
{
    public Transform[] SpawnPosition;
    public Text[] PlayersText;
    public Image outimg;

    public GameObject character;

    public GameObject updefense1;
    public GameObject updefense2;
    public GameObject leftdefense;
    public GameObject rightdefense;
    GameObject defense;
    int player_index;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        DefenseSpawn();
    }
    void Awake()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            PlayersText[i].text = PhotonNetwork.PlayerList[i].NickName;
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    /*지금 현재 플레이어가 몇번째 플레이언지 확인*/
    public Transform SelectSpwanPosition()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            /*인덱스에 맞는 스폰포지션 리턴*/
            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
            {
                player_index = i;
                return SpawnPosition[i];
            }
        }
        return null;
    }

    //캐릭터 스폰 함수
    public void Spawn()
    {
        Debug.Log("Spawn함수");
        if (SelectChaPanel.char_num == 1)
            character = PhotonNetwork.Instantiate("MaskDude1", SelectSpwanPosition().position, SelectSpwanPosition().rotation);
        else if (SelectChaPanel.char_num == 2)
            character = PhotonNetwork.Instantiate("NinjaFrog1", SelectSpwanPosition().position, SelectSpwanPosition().rotation);
        else if (SelectChaPanel.char_num == 3)
            character = PhotonNetwork.Instantiate("PinkMan1", SelectSpwanPosition().position, SelectSpwanPosition().rotation);
        else if (SelectChaPanel.char_num == 4)
            character = PhotonNetwork.Instantiate("VitualGuy1", SelectSpwanPosition().position, SelectSpwanPosition().rotation);
    }


    public void DefenseSpawn()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr == 1)
            {
                
                leftdefense.transform.parent = character.transform;
                leftdefense.transform.localPosition = new Vector2(-0.5f, 0);
            }
            else if (players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr == 2)
            {
               
                updefense1.transform.parent = character.transform;
                updefense1.transform.localPosition = new Vector2(0, 0.5f);
            }
            else if (players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr == 3)
            {
                
                updefense2.transform.parent = character.transform;
                updefense2.transform.localPosition = new Vector2(0, 0.5f);
            }
            else if (players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr ==4)
            {
                
                rightdefense.transform.parent = character.transform;
                rightdefense.transform.localPosition = new Vector2(0.5f, 0);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void click()
    {
        Debug.Log("클릭");
        PhotonNetwork.LoadLevel("Title");
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        /*로비패널, 룸패널 비활성화, 닉네임인풋 공백*/
        PhotonNetwork.LeaveRoom();
        outimg.gameObject.SetActive(true);

    }
}
