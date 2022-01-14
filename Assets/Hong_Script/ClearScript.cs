using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ClearScript : MonoBehaviour
{

    public Text SPYText;
    public Text[] NameText1;


    // Start is called before the first frame update
    private void Awake()
    {
        int index = 0;
        int max = R_NetWorkManager.player_die[0];
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) {
            NameText1[i].text = PhotonNetwork.PlayerList[i].NickName + "´ÔÀÇ Á×Àº È½¼ö : " + "<color=#ff0000>" + (R_NetWorkManager.player_die[i]).ToString() + "</color>";
            if (max <= R_NetWorkManager.player_die[i])
            {
                max = R_NetWorkManager.player_die[i];
                index = i;
            }
            
        }
        SPYText.text = "°¡Àå ¸¹ÀÌ Á×Àº ¹üÀÎÀº " + PhotonNetwork.PlayerList[index].NickName + "´Ô ÀÔ´Ï´Ù.";



    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
