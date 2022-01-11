using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Apple_random_Spawn : MonoBehaviourPunCallbacks
{
    public Transform[] SpawnPosition;
    public GameObject apple;
    public GameObject TimerLimitObj;
    public PhotonView PV;
    // Start is called before the first frame update
    private void Awake()
    {
        int random = Random.Range(0, 3);
        Transform tr = SpawnPosition[random].transform;
        apple.transform.position = new Vector3(tr.position.x, tr.position.y, tr.position.z);
        if (PhotonNetwork.IsMasterClient)
            PV.RPC("turnOn", RpcTarget.AllViaServer);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    void turnOn()
    {
        TimerLimitObj.SetActive(true);
    }
}
