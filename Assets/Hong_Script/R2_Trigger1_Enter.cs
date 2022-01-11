using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class R2_Trigger1_Enter : MonoBehaviourPunCallbacks
{
    public BulletScript bullet;
    public int Floor;

    void Update()
    {

    }
    /*모두 트리거에 들어왔는지 체크*/
    private bool AllInTrigger(int Floor)
    {
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (Floor == 1)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i].transform.GetComponent<PlayerScript>().IsRound2_Trigger)
                    return false;
            }
        }
        else if(Floor == 2)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i].transform.GetComponent<PlayerScript>().IsRound2_Trigger2)
                    return false;
            }
        }
        return true;
    }

    // 트리거 엔터에 들어왔을 때 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            PhotonView collision_PV = collision.transform.GetComponent<PlayerScript>().PV;
            /*충돌한 캐릭터 스크립트의 변수를 변경*/

            /*첫번째 층 트리거일때*/
            if (Floor == 1)
                collision.transform.GetComponent<PlayerScript>().IsRound2_Trigger = true;

            else if (Floor == 2)
                collision.transform.GetComponent<PlayerScript>().IsRound2_Trigger2 = true;

            /*모두 트리거 안에 들어왔으면 samestart실행 마스터클라이언트 기준*/
            if (AllInTrigger(Floor) && PhotonNetwork.IsMasterClient)
                photonView.RPC("samestart", RpcTarget.AllViaServer);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PhotonView collision_PV = collision.transform.GetComponent<PlayerScript>().PV;
            /*충돌한 캐릭터 스크립트의 변수를 변경*/

            /*첫번째 층 트리거일때*/
            if (Floor == 1)
                collision.transform.GetComponent<PlayerScript>().IsRound2_Trigger = false;

            else if (Floor == 2)
                collision.transform.GetComponent<PlayerScript>().IsRound2_Trigger2 = false;

            /*모두 트리거 안에 들어왔으면 samestart실행 마스터클라이언트 기준*/
            if (AllInTrigger(Floor) && PhotonNetwork.IsMasterClient)
                photonView.RPC("samestart", RpcTarget.AllViaServer);
        }
    }

    /*똑같이 실행하기 위해서*/
    [PunRPC]
    void samestart()
    {
        Debug.Log("samestart");
        bullet.BulletScriptTriiger = true;
    }


    
}
