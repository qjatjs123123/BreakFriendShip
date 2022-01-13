using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class restartScene : MonoBehaviour
{
    private void Awake()
    {
        string curscene = "round" + R_NetWorkManager.round;
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(curscene);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
