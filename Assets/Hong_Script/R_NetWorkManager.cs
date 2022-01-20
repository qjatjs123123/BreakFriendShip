using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class R_NetWorkManager : MonoBehaviourPunCallbacks
{
    [Header("DisconnectPanel")]
    public InputField NickNameInput;
    public GameObject DisconnectPanel;

    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public Text WelcomeText;
    public Image Lobby_Img;
    public Text NicknameText;
    public Text LobbyInfoText;
    public InputField RoomInput;
    public Button[] CellBtn;
    public Button PreviousBtn;
    public Button NextBtn;


    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public Text ListText;
    public Text RoomInfoText;
    public Text[] ChatText;
    public InputField ChatInput;
    public GameObject Left;
    public GameObject Masterban;
    public Text baninfoText;
    public Text MasterbaninfoText;
    public Text MastergiveinfoText;
    public GameObject Clientban;
    public GameObject Mastergive;
    public Button StartBtn;

    [Header("SelectCharacterImagePanel")]
    public GameObject SelectCharacterImagePanel;
    public Image selectImg;

    public PhotonView PV;

    [Header("ETC")]
    public Text StatusText;
    public Image[] img;
    GameObject a = null;
    Player kickplayer = null;
    Player masterplayer = null;
    public GameObject errornotice;
    public GameObject imgnotnotice;
    public GameObject StatusImg;
    public Text StatusTxt;
    public Text RoomTxt;
    public int selectnum = 0;

    public Canvas cv;

    /*전역변수*/
    public static int round = 0;
    public static int[] player_die = { 0, 0, 0, 0 }; 


    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

    void Start()
    {

    }

    void Update()
    {
        // 채팅 Enter시 보내기
        inputField_Enter();
        // 네트워크 상태표시 변경
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        //네트워크상태이미지표시 함수 호출
        statusImg();
        StatusTxt.text = PhotonNetwork.NetworkClientState.ToString(); ;
        //로비 접속수 및 총 접속수 표시 변경
        LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비 / " + PhotonNetwork.CountOfPlayers + "접속";
        //방장만 실행하는 함수
        if(PhotonNetwork.IsMasterClient)
            useStartBtn();

    }
    private void Awake()
    {
        // 플레이어 강퇴하기 위해서 true
        PhotonNetwork.EnableCloseConnection = true;
        PhotonNetwork.AutomaticallySyncScene = true;
        Screen.SetResolution(960, 540, false);
    }

    #region 포톤에 관련된 함수
    // QuickStart버튼을 눌렀을 때 실행
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    // 로비 X버튼을 눌렀을 때 실행
    public void Disconnect() => PhotonNetwork.Disconnect();

    // 플레이어가 방에 나갔을 때 호출되는 콜백 함수
    public override void OnPlayerLeftRoom(Player otherPlayer) => RoomRenewal();

    // Master서버, 상태가 되면 로비에 참가
    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    // 함수 JoinRandomRoom이 성공적으로 수행하지 못했을 경우 실행
    public override void OnJoinRandomFailed(short returnCode, string message) =>  RoomInput.text = "";

    // 방장이 바뀌었을 때 실행하는 콜백 함수
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        CheckRoomMaster();
        showStartBtn();
    }

    // 플레이어가 방에 들어왔을 때 실행하는 콜백함수
    public override void OnPlayerEnteredRoom(Player newPlayer) => RoomRenewal();

    // 함수 createRoom이 성공적으로 수행하지 못했을 경우 실행
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        /*로비에 접속, 에러다이얼로그 활성화, 네트워크상태패널비활성화, 방입력텍스트 공백*/
        PhotonNetwork.JoinLobby();
        errornotice.SetActive(true);
        StatusImg.SetActive(false);
        RoomInput.text = "";
    }

    // 함수 Diconnect가 실행되었을 때 실행하는 콜백함수
    public override void OnDisconnected(DisconnectCause cause)
    {
        /*로비패널, 룸패널 비활성화, 닉네임인풋 공백*/
        if (NickNameInput.text != "")
        {
            LobbyPanel.SetActive(false);
            RoomPanel.SetActive(false);
            NickNameInput.text = "";
        }

    }

    // RoomList가 업데이트 되었을 때 실행하는 콜백함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }

    // 방에 조인되었을 때 실행되는 콜백함수
    public override void OnJoinedRoom()
    {
        if (selectnum == 1) a = PhotonNetwork.Instantiate("P1", Vector3.zero, Quaternion.identity);
        else if (selectnum == 2) a = PhotonNetwork.Instantiate("P2", Vector3.zero, Quaternion.identity);
        else if (selectnum == 3) a = PhotonNetwork.Instantiate("P3", Vector3.zero, Quaternion.identity);
        else if (selectnum == 4) a = PhotonNetwork.Instantiate("P4", Vector3.zero, Quaternion.identity);
        a.transform.localScale = new Vector3(1f, 1f, 1f);
        a.SetActive(true);

        RoomRenewal();
        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);
    }

    //JoinLobby() 콜백함수로 실행
    public override void OnJoinedLobby()
    {
        SelectCharacterImagePanel.SetActive(false);
        Debug.Log("로비참가");
        LobbyPanel.SetActive(true);
        DisconnectPanel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        WelcomeText.text = PhotonNetwork.LocalPlayer.NickName + "님 환영합니다.";
        NicknameText.text = PhotonNetwork.LocalPlayer.NickName + " 님";
        Lobby_Img.transform.GetComponent<Image>().sprite = selectImg.GetComponent<Image>().sprite;
        myList.Clear();
    }
    #endregion

    #region 사용자 정의 함수
    // 방장만 StartBtn 보이게 하는 함수
    void showStartBtn()
    {
        if (PhotonNetwork.IsMasterClient)
            StartBtn.gameObject.SetActive(true);
        else
            StartBtn.gameObject.SetActive(false);
    }

    public bool IsAllReady()
    {
        /*instantiate로 생성된 프리팹이 오브젝트 Left안에 옮기는 시간으로 인해 문제발생, 예외처리 아직 생기지 않았을 때 */
        if (Left.transform.childCount == 0) return false;
        for (int i = 0; i < Left.transform.childCount; i++)
            if (!Left.transform.GetChild(i).transform.GetComponent<P5Script>().IsReady) return false;

        return true;         
    }

    // 전부다 Ready했을 때 버튼 interactable되도록 하는 함수
    void useStartBtn()
    {
        if (PhotonNetwork.IsMasterClient && IsAllReady())
        {
            StartBtn.interactable = true;
        }
        else
            StartBtn.interactable = false;
    }
     

    // 로비 방 페이지 텍스트 리뉴얼
    void MyListRenewal()
    {
        // 최대페이지
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // 이전, 다음버튼
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }

    // 플레이어가 방에 나갔을 때 실행
    public void LeaveRoom()
    {
        /*네트워크상태 텍스트 변경, 네트워크상태 패널 활성화, 룸이름인풋텍스트 공백, 포톤리브룸 실행*/
        RoomTxt.text = "로비 접속중...";
        StatusImg.SetActive(true);
        RoomInput.text = "";
        PhotonNetwork.LeaveRoom();

    }
    //서버의 연결 , Master서버에 연결하면 OnConnectedToMaster 콜백
    public void Connect()
    {
        if (selectnum == 0) imgnotnotice.SetActive(true);
        else PhotonNetwork.ConnectUsingSettings();
    }

    // 방 생성 버튼을 눌렀을 때 실행
    public void CreateRoom()
    {
        if (RoomInput.text != "")
        {
            StatusImg.SetActive(true);
            RoomTxt.text = "방 생성중...";
            if (StatusText.text == "JoinedLobby") PhotonNetwork.CreateRoom(RoomInput.text, new RoomOptions { MaxPlayers = 4 });
            else
            {
                errornotice.SetActive(true);
                return;
            }
        }
    }

    // Disconnect패널에서 다음버튼을 눌렀을 때
    public void OnClickNicknameBtn()
    {
        if (NickNameInput.text != "") SelectCharacterImagePanel.SetActive(true);
        else NickNameInput.GetComponent<Animator>().SetTrigger("On");
    }

    // 로비 패널에서 생성된 방에 접속할 때
    public void MyListClick(int num)
    {
        StatusImg.SetActive(true);
        RoomTxt.text = "방 접속중...";
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        MyListRenewal();
    }

    // 누가 방장인지 확인하는 함수
    public void CheckRoomMaster()
    {
        /*Instantiate로 생성된 프리팹 P5의 스크립트참조하여 checkRoomMaster를 실행*/
        for (int i = 0; i < Left.transform.childCount; i++)
            Left.transform.GetChild(i).transform.GetComponent<P5Script>().CheckroomMaster();
    }

    // 방장 권한 줄지 확인하는 함수
    public void GiveMasterPlayer(Player master)
    {
        MastergiveinfoText.text = master.NickName + "님에게 방장을 주시겠습니까?";
        masterplayer = master;
        Mastergive.SetActive(true);
    }

    // 플레이어를 강퇴할지 확인하는 함수
    public void KickPlayer(Player kick)
    {
        MasterbaninfoText.text = kick.NickName + "을 강퇴하시겠습니까?";
        kickplayer = kick;
        Masterban.SetActive(true);      
    }

    public void ClickStart()
    {
        
        RoomPanel.SetActive(false);
        PhotonNetwork.LoadLevel("round1");       
    }

    // 강퇴확인 다이얼로그 No버튼을 눌렀을 때
    public void clickno() => Masterban.SetActive(false);

    // 강퇴확인 다이얼로그 Yes버튼을 눌렀을 때
    public void clickyes()
    {
        PV.RPC("playerout", RpcTarget.All, kickplayer);
        PhotonNetwork.CloseConnection(kickplayer);
        Masterban.SetActive(false);
        kickplayer = null;
    }

    // 방장주기확인 다이얼로그 Yes버튼을 눌렀을 때
    public void MasterGive_clickyes()
    {
        PhotonNetwork.SetMasterClient(masterplayer);
        Mastergive.SetActive(false);
        masterplayer = null;      
    }

    // 레디버튼을 눌렀을 때
    public void clickready()
    {
        /*Left에 생성된 프리팹의 스크립트에서 ChangeReadyText호출*/
        for (int i = 0; i < Left.transform.childCount; i++)
            Left.transform.GetChild(i).transform.GetComponent<P5Script>().ChangeReadyText(PhotonNetwork.LocalPlayer);
        
    }

    // 강퇴당한 플레이어에게  ClientBan오브젝트 활성화하고 OK버튼을 눌렀을 때 실행
    public void clickok()
    {
        StatusImg.SetActive(true);
        RoomPanel.SetActive(false);
    }
    
    // 방 리뉴얼 함수
    void RoomRenewal()
    {
        CheckRoomMaster();
        showStartBtn();
        List<string> namelist = new List<string>();
        ListText.text = "";

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
            namelist.Add(PhotonNetwork.PlayerList[i].NickName);
        }
        for (int i = PhotonNetwork.PlayerList.Length; i < 4; i++)
            namelist.Add("");
       
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
    }

    // 채팅입력 후 Send버튼 눌렀을 때 함수
    public void Send()
    {
        string msg = PhotonNetwork.NickName + " : " + ChatInput.text;
        PV.RPC("ChatRPC", RpcTarget.AllBuffered, PhotonNetwork.NickName + " : " + ChatInput.text);
        ChatInput.text = "";
    }

    // 채팅입력 후 Enter버튼을 눌렀을 때 함수
    public void inputField_Enter()
    {
        if (Input.GetKeyDown(KeyCode.Return)) Send();
    }

    // 네트워크 상태 패널 표시 함수
    public void statusImg()
    {
        if(StatusText.text== "Joined" || StatusText.text == "JoinedLobby") StatusImg.SetActive(false);    
    }
    #endregion

    #region RPC 함수
    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    void ChatRPC(string msg)
    {
        bool isInput = false;

        for (int i = 0; i < ChatText.Length; i++)
        {

            if (ChatText[i].text == "")
            {
                Debug.Log("chat if문: " + i);
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }

    [PunRPC]
    void playerout(Player kickplayer)
    {
        if(PhotonNetwork.LocalPlayer == kickplayer)
        {
            LobbyPanel.SetActive(true);
            baninfoText.text = "방장님이 " + kickplayer.NickName + "님을 강퇴하였습니다.";
            Clientban.SetActive(true);
            
        }
    }
    #endregion
}
