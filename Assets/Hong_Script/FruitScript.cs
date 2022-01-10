using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class FruitScript : MonoBehaviourPunCallbacks
{
    public GameObject fruit;

    void Start()
    {
        
    }
  
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            PhotonView collision_PV = collision.transform.GetComponent<PlayerScript>().PV;
            /*players오브젝트에 모든플레이어 저장*/
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i < players.Length; i++)
            {
                /*players오브젝트중 포톤뷰가 충돌된 플레이어포톤뷰 같은것 찾기*/
                if (players[i].transform.GetComponent<PlayerScript>().PV == collision_PV.IsMine)
                {
                    fruit.transform.SetParent(players[i].transform,false);
                    fruit.transform.localPosition = new Vector2(0.7f, -0.1f);
                    return;
                }
            }
        }

    }
    public void appleSound()
    {
        
    }
}
