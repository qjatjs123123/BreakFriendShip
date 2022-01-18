using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class TimerLimit : MonoBehaviourPunCallbacks
{
    public float LimitTime;
    public Text[] ClockText;
    public bool turnon = false;
    public Image youdied;
    bool synon = false;
    public PhotonView PV;
    public test test;

    
    GameObject LocalPlayer = null;
    // Start is called before the first frame update
    void Start()
    {
        // 시작할 떄 시간 동기화
        PV.RPC("synonfunc", RpcTarget.AllViaServer);
    }
    void restart()
    {
        //PhotonNetwork.LoadLevel("LoadingScene");
        PV.RPC("respawn", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void respawn()
    {
        GameObject.FindGameObjectWithTag("init").transform.GetComponent<init_round3>().init_round();
    }

    /*시간 동기화 맞추기 위해서*/
    [PunRPC]
    void synonfunc()
    {
        synon = true;
    }

    public GameObject LocalPlayerObject()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.GetComponent<PlayerScript>().PV.IsMine)
                return players[i];
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        /*동기화 되기전이면 빠져나간다*/
        if (!synon)
            return;

        /*동기화 되고 시간이 0이고 turnon스위치가 false라면 실행, turnon 쓰는 이유는
         한번만 실행되기 위해서*/
        if (ClockText[1].text == "0" && !turnon)
        {
            //죽었을 때 사용하는 코드
            LocalPlayer = LocalPlayerObject();
            PlayerScript PS = LocalPlayer.transform.GetComponent<PlayerScript>();
            PS.isDie = true;
            
            youdied.gameObject.SetActive(true);

            if (PhotonNetwork.IsMasterClient && !turnon)
            {             
                Invoke("restart", 2);
            }


            turnon = true;
        }

        /*만약 시간이 0초라면 빠져나감, 아래문 실행되게 하지 않기위해서, */
        if (ClockText[1].text == "0")
            return;

        /*시간이 0초 아니라면 분, 초 표시*/
        else
        {
            LimitTime -= Time.deltaTime;
            ClockText[0].text = "0" + ((int)(Mathf.Round(LimitTime) / 60)).ToString(); //시
            ClockText[1].text = (Mathf.Round(LimitTime) % 60).ToString(); // 분
        }


    }
}
