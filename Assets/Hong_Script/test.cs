using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class test : MonoBehaviourPunCallbacks
{
    public Transform SpawnPosition_P1;
    // Start is called before the first frame update
    void Start()
    {
        
        Spawn();
    }
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }
    //캐릭터 스폰 함수
    public void Spawn()
    {
        Debug.Log("Spawn함수");
        if (SelectChaPanel.char_num == 1)
        {
            GameObject Player = PhotonNetwork.Instantiate("MaskDude", SpawnPosition_P1.position, SpawnPosition_P1.rotation);
            


        }
        else if (SelectChaPanel.char_num == 2)
        {
            GameObject Player = PhotonNetwork.Instantiate("NinjaFrog", new Vector3(0, 0, 0), Quaternion.identity);
            


        }
        else if (SelectChaPanel.char_num == 3)
        {
            GameObject Player = PhotonNetwork.Instantiate("PinkMan", new Vector3(0, 0, 0), Quaternion.identity) ;
            

        }
        else if (SelectChaPanel.char_num == 4)
        {

            GameObject Player = PhotonNetwork.Instantiate("VitualGuy", new Vector3(0, 0, 0), Quaternion.identity) ;
           

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
