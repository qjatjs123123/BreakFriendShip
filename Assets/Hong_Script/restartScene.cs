using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class restartScene : MonoBehaviour
{
    public GameObject[] roundimg;
    private void Awake()
    {
        string curscene = "round" + R_NetWorkManager.round;
        roundimg[R_NetWorkManager.round-1].SetActive(true);
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
