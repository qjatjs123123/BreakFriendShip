using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class test : MonoBehaviourPunCallbacks
{
    public Transform[] SpawnPosition;
    public Text[] PlayersText;
    public Image outimg;
    public Image regameimg;
    public int num;

    // Start is called before the first frame update
    void Start()
    {     
        Spawn();
        
    }
    void Awake()
    {
        R_NetWorkManager.round = num;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            PlayersText[i].text = PhotonNetwork.PlayerList[i].NickName;
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    /*지금 현재 플레이어가 몇번째 플레이언지 확인*/
    public Transform SelectSpwanPosition()
    {
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            /*인덱스에 맞는 스폰포지션 리턴*/
            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
                return SpawnPosition[i];
        }
        return null;
    }

    //캐릭터 스폰 함수
    public void Spawn()
    {
        Debug.Log("Spawn함수");
        if (SelectChaPanel.char_num == 1)
            PhotonNetwork.Instantiate("MaskDude", SelectSpwanPosition().position, SelectSpwanPosition().rotation);
        else if (SelectChaPanel.char_num == 2)
            PhotonNetwork.Instantiate("NinjaFrog", SelectSpwanPosition().position, SelectSpwanPosition().rotation);
        else if (SelectChaPanel.char_num == 3)
            PhotonNetwork.Instantiate("PinkMan", SelectSpwanPosition().position, SelectSpwanPosition().rotation);
        else if (SelectChaPanel.char_num == 4)      
            PhotonNetwork.Instantiate("VitualGuy", SelectSpwanPosition().position, SelectSpwanPosition().rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsMasterClient)
            regameimg.gameObject.SetActive(true);
    }
    public void click()
    {
        PhotonNetwork.LoadLevel("Title");

    }
    public void restart()
    {
        PhotonNetwork.LoadLevel("LoadingScene");
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        /*로비패널, 룸패널 비활성화, 닉네임인풋 공백*/
        PhotonNetwork.Disconnect();
        outimg.gameObject.SetActive(true);

    }
}
