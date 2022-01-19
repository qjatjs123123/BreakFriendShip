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

    public Image regameimg;
    public int num;
    GameObject defense;
    int player_index;
    bool turnon = false;
    public int[] arr = { 0, 0, 0, 0 };

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
            Debug.Log(players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr);
            if (get_player_index(players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr) == 0)
            {
                
                leftdefense.transform.parent = players[i].transform;
                leftdefense.transform.localPosition = new Vector2(-0.5f, 0);
            }
            else if (get_player_index(players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr) == 1)
            {
               
                updefense1.transform.parent = players[i].transform;
                updefense1.transform.localPosition = new Vector2(0, 0.5f);
            }
            else if (get_player_index(players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr) == 2)
            {
                
                updefense2.transform.parent = players[i].transform;
                updefense2.transform.localPosition = new Vector2(0, 0.5f);
            }
            else if (get_player_index(players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr) == 3)
            {               
                rightdefense.transform.parent = players[i].transform;
                rightdefense.transform.localPosition = new Vector2(0.5f, 0);
            }
        }


    }

    public int get_player_index(int num)
    {
        int i = 0;
        for (i = 0; i < 4; i++)
        {
            if (arr[i] == num)
                break;
        }
        return i;
    }

    void players_sort()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            arr[i] = players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr;
        }

        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            { //j:비교하는 숫자, i번째의 오른쪽=>i+1
                if (arr[i] > arr[j])
                {
                    int tmp = arr[i];  //변수값 서로 바꿀때는 tmp에 미리 값 1개 옮겨놓기!
                    arr[i] = arr[j];
                    arr[j] = tmp;
                }
            }
        }

    }

    public void restart()
    {
        PhotonNetwork.LoadLevel("LoadingScene");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 4 && !turnon)
        {
            players_sort();
            DefenseSpawn();
            turnon = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsMasterClient)
            regameimg.gameObject.SetActive(true);
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
