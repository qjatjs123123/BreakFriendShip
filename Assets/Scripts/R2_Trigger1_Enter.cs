using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class R2_Trigger1_Enter : MonoBehaviour
{
    public BulletScript bullet;
    static string[] player_floorIn = { "0", "0", "0", "0" };
    string[] player_name = new string[4];
    public PhotonView PV;
    public Tilemap BreakTile;

    void Update()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            player_name[i] = player[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
        }
    }
    // 트리거 엔터에 들어왔을 때 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {             
        if (collision.tag == "Player")
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            string[] name_and_isIn = new string[2]; // 이름, 플레이어가 층에 있는지 없는지 저장 변수
            string LocalPlayer = collision.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text; // 현재 자기자신 플레이어 닉네임

            //만약 자기 자신이면 게임 플레이 하고있는 플레이어 인덱스 위치 찾고 그 인덱스 위치에 0저장
            if (LocalPlayer == PhotonNetwork.LocalPlayer.NickName)
            {
                for (int i = 0; i < player.Length; i++)
                {
                    if (player_name[i] == LocalPlayer)
                    {
                        player_floorIn[i] = "1"; // 1은 그 층에 있고 0은 그 층에 없음
                        name_and_isIn[0] = LocalPlayer;
                        name_and_isIn[1] = player_floorIn[i];
                        PV.RPC("ElevatorInRPC", RpcTarget.All, name_and_isIn);
                        break;
                    }
                }
            }


            int k = 0;
            for (k = 0; k < player.Length; k++)
            {
                if (player_floorIn[k] == "0")
                {
                    break;
                }
            }

            if (k == player.Length)
            {
                Debug.Log("if문 실행");
                bullet.BulletScriptTriiger = true;
                BreakTile.gameObject.SetActive(true);

                PV.RPC("BreakTileRPC", RpcTarget.All);
                for (k = 0; k < 4; k++)
                {
                    player_floorIn[k] = "0";
                }
            }
        }
    }

    // 트리거 엔터에 나왔을 때 실행
    private void OnTriggerExit2D(Collider2D collision)
    {


        //if (collision.name == "Round2_Apple1" || collision.name == "Round2_Apple2" || collision.name == "Round2_Apple3" || collision.name == "Box1" || collision.name == "Box2" || collision.name == "Box3" || collision.name == "Square" || collision.name == "Bullet") { } //예외처리 사과
        if (collision.tag == "Player")
        {
            Debug.Log(collision.name);
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            string[] name_and_isIn = new string[2]; // 이름, 플레이어가 층에 있는지 없는지 저장 변수
            string LocalPlayer = collision.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text; // 현재 자기자신 플레이어 닉네임
                                                                                                                             //만약 자기 자신이면 게임 플레이 하고있는 플레이어 인덱스 위치 찾고 그 인덱스 위치에 0저장
            if (LocalPlayer == PhotonNetwork.LocalPlayer.NickName)
            {
                for (int i = 0; i < player.Length; i++)
                {
                    if (player_name[i] == LocalPlayer)
                    {
                        player_floorIn[i] = "0"; // 1은 그 층에 있고 0은 그 층에 없음
                        name_and_isIn[0] = LocalPlayer;
                        name_and_isIn[1] = player_floorIn[i];
                        PV.RPC("ElevatorOutRPC", RpcTarget.All, name_and_isIn);
                        break;
                    }
                }
            }
        }
    }
    [PunRPC]
    void BreakTileRPC()
    {
        bullet.BulletScriptTriiger = true;
        BreakTile.gameObject.SetActive(true);
        for (int k = 0; k < 4; k++)
        {
            player_floorIn[k] = "0";
        }
    }


    [PunRPC]
    void ElevatorOutRPC(string[] name_and_elevator)
    {

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            Debug.Log(player_name[i]);
            if (player_name[i] == name_and_elevator[0])
            {
                player_floorIn[i] = "0";
                break;
            }
        }
    }

    [PunRPC]
    void ElevatorInRPC(string[] name_and_elevator)
    {

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            Debug.Log(player_name[i]);
            if (player_name[i] == name_and_elevator[0])
            {
                player_floorIn[i] = "1";
                break;
            }
        }
    }
}
