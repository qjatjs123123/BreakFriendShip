using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Ghost_Trigger : MonoBehaviour
{
    [SerializeField]
    public GameObject wall;

    public GameObject[] FireTrap;
    public GameObject[] saw;
    public PhotonView PV;
    Vector3 pos; //현재위치
    int round;
    void start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "ghost")
        {
            Debug.Log("실행");
            //wall.transform.position = Vector3.MoveTowards(wall.transform.position, target.transform.position, 10 * Time.deltaTime);
            if(round == 6)
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
