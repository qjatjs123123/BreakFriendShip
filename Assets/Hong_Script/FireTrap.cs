using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FireTrap : MonoBehaviourPun
{
    public GameObject FireOn;
    GameObject[] players;
    public PhotonView PV;

    public bool turnon = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (!turnon)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (IsAllReady(players))
            {
                if (PhotonNetwork.IsMasterClient)
                    PV.RPC("Synswitchon", RpcTarget.AllViaServer);
            }
        }
        
    }

    [PunRPC]
    void Synswitchon()
    {
        StartCoroutine("FireTrapOn");
        turnon = true;
    }


    public bool IsAllReady(GameObject[] players)
    {
        for (int i = 0; i < 2; i++)
            if (!players[i].transform.GetComponent<PlayerScript>().isReady)
                return false;

        return true;
    }




    // Update is called once per frame
    
    IEnumerator FireTrapOn()
    {
        if (FireOn.activeSelf == true)
        {
            yield return new WaitForSeconds(3f);
            FireOn.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(2.5f);
            FireOn.SetActive(true);
        }
        if (PhotonNetwork.IsMasterClient)
            PV.RPC("Synswitchon", RpcTarget.AllViaServer);
    }
}
