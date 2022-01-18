using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Ghost_Trigger : MonoBehaviour
{
    public GameObject target;
    public GameObject wall;
    public GameObject[] saw;
    public PhotonView PV;
    Vector3 pos; //현재위치

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "ghost")
        {
            Debug.Log("실행");
            //wall.transform.position = Vector3.MoveTowards(wall.transform.position, target.transform.position, 10 * Time.deltaTime);
            wall.SetActive(false);
            PV.RPC("Synturnon", RpcTarget.AllViaServer);
        }
    }
    
    [PunRPC]
    void Synturnon()
    {
        for(int i = 0; i < saw.Length; i++)
            saw[i].transform.GetComponent<R6_TrapMove>().turnon = true;
    }

}
