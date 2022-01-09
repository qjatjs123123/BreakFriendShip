using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class P5Script : MonoBehaviourPunCallbacks, IPunObservable
{
    public Text NickNameText;
    public PhotonView PV;
    public Button banbtn;
    public Button crownbtn;
    public Image crownimg;
    public Text readytxt;

    public bool IsReady;
    bool stream_ready;

    GameObject NetworkManager;
    void Awake()
    {
        // 닉네임 표시
        NetworkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        PhotonNetwork.EnableCloseConnection = true;
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;     
    }

    private void Start()
    {
        Collect_player();
        CheckroomMaster();

    }
    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            IsReady = stream_ready;
    }

    public void ChangeReadyText(Player player)
    {
        if(PV.Owner == player)
        {
            /*Ready버튼이 빨간 색이면 검은색으로 그렇지 않으면 빨간색으로 변경*/
            if (readytxt.text == "<color=#ff0000>" + "Ready" + "</color>")
            {
                PV.RPC("BlackRPC", RpcTarget.AllBufferedViaServer);
                IsReady = false;
            }
            else
            {
                PV.RPC("RedRPC", RpcTarget.AllBufferedViaServer);
                IsReady = true;
            }
        }
    }
    
    public void clickX() => NetworkManager.transform.GetComponent<R_NetWorkManager>().KickPlayer(PV.Owner);

    public void clickCrwon() => NetworkManager.transform.GetComponent<R_NetWorkManager>().GiveMasterPlayer(PV.Owner);


    public void CheckroomMaster()
    {
        banbtn.gameObject.SetActive(false);
        crownimg.gameObject.SetActive(false);
        crownbtn.gameObject.SetActive(false);
        if (PhotonNetwork.IsMasterClient && !PV.IsMine)
        {
            banbtn.gameObject.SetActive(true);
            crownbtn.gameObject.SetActive(true);
        }
        if (PV.Owner.IsMasterClient)  crownimg.gameObject.SetActive(true);
    }

    public void Collect_player()
    {
        GameObject[] Players;
        Players = GameObject.FindGameObjectsWithTag("P5");
        GameObject Left = GameObject.FindWithTag("left");

        for (int i = 0; i < Players.Length; i++)
            Players[i].transform.SetParent(Left.transform,false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            stream.SendNext(IsReady);
        }

        else
        {
                stream_ready = (bool)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void BlackRPC() => readytxt.text = "<color=black>" + "Ready" + "</color>";

    [PunRPC]
    void RedRPC() => readytxt.text = "<color=#ff0000>" + "Ready" + "</color>";



}
