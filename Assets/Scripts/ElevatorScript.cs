using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ElevatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    string[] player_name = new string[4];

    //엘레베이터 올라가있으면 1 그렇지 않으면 0
    static string[] player_Elevator = { "0", "0", "0", "0" };
    public PhotonView PV;
    public Rigidbody2D RB;
    public GameObject round3;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            player_name[i] = player[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        string[] name_and_elevator = new string[2];
        
        if (other.gameObject.name != "Tilemap" && other.gameObject.name != "Square")
        {
            Debug.Log(other.gameObject.name);
            string LocalPlayer = other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
            if (LocalPlayer == PhotonNetwork.LocalPlayer.NickName)
            {               
                for (int i = 0; i < player.Length; i++)
                {
                    if (player_name[i] == LocalPlayer)
                    {
                        player_Elevator[i] = "0";
                        name_and_elevator[0] = LocalPlayer;
                        name_and_elevator[1] = player_Elevator[i];
                        PV.RPC("ElevatorOutRPC", RpcTarget.All, name_and_elevator);
                        break;
                    }
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        
        string[] name_and_elevator = new string[2];
        
        if (other.gameObject.name != "Tilemap" && other.gameObject.name != "Square")
        {
            Debug.Log(other.gameObject.name);
            
            string LocalPlayer = other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
            if (LocalPlayer == PhotonNetwork.LocalPlayer.NickName)
            {
                
                for (int i = 0; i < player.Length; i++)
                {
                    if (player_name[i] == LocalPlayer)
                    {
                        //Debug.Log("dafasdfasd");
                        player_Elevator[i] = "1";
                        name_and_elevator[0] = LocalPlayer;
                        name_and_elevator[1] = player_Elevator[i];
                        PV.RPC("ElevatorInRPC", RpcTarget.All, name_and_elevator);
                        break;
                    }
                }
            }
            Debug.Log(player.Length);
            int j = 0;
            for (j=0; j < player.Length; j++)
            {
                
                if (player_Elevator[j] == "0") break;
            }
            
            if (j == player.Length)
            {
                
                RB.constraints = RigidbodyConstraints2D.None;
                RB.constraints = RigidbodyConstraints2D.FreezePositionX;
                RB.constraints = RigidbodyConstraints2D.FreezeRotation;
                PV.RPC("RB_RPC", RpcTarget.All);
            }
        }
        
        else if(other.gameObject.name == "Tilemap" && round3.activeSelf == true)
        {
            
                GameObject.Find("TextEffect").GetComponent<Typingeffect>().text_start1();
        }
        Debug.Log(other.gameObject.name);
    }
    [PunRPC]
    void RB_RPC()
    {
        RB.constraints = RigidbodyConstraints2D.None;
        RB.constraints = RigidbodyConstraints2D.FreezePositionX;
        RB.constraints = RigidbodyConstraints2D.FreezePositionY;
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
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
                player_Elevator[i] = "0";
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
                player_Elevator[i] = "1";             
                break;
            }
        }
    }
}
