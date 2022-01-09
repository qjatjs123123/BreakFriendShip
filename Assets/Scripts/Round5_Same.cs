using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Round5_Same : MonoBehaviour
{
    public GameObject PlayerObj;
    public PhotonView PV;

    string[] player_name = new string[PhotonNetwork.CountOfPlayers];
    bool[] player_jump = new bool[PhotonNetwork.CountOfPlayers];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

}
