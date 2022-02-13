using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Ghost_R7 : MonoBehaviourPunCallbacks
{

    GameObject[] players;
    public PhotonView PV;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
         players = GameObject.FindGameObjectsWithTag("Player");
        //if (players.Length != 2)
        //    return;

        if (IsAllRun() && IsAllLeft())           
            PV.RPC("Ghost_Move", RpcTarget.All, 1);
        

        else if (IsAllRun() && IsAllRight())
            PV.RPC("Ghost_Move", RpcTarget.All, 2);


        else if (IsAllJump())
            PV.RPC("Ghost_Move", RpcTarget.All, 3);

        else if (IsAllUnder())
            PV.RPC("Ghost_Move", RpcTarget.All, 4);
    }

    [PunRPC]
    void Ghost_Move(int index)
    {
        if (index == 1)
        {
            transform.Translate(Vector3.left * 3f * Time.deltaTime);
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }

        else if(index == 2)
        {
            transform.Translate(Vector3.right * 3f * Time.deltaTime);
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(index == 3)
            transform.Translate(Vector3.up * 3f * Time.deltaTime);
        else
            transform.Translate(Vector3.down * 3f * Time.deltaTime);
    }
    

    public bool IsAllRun()
    {
        if (players.Length == 1)
        {
            for (int i = 0; i < players.Length; i++)
            {
                // isRun false가 하나라도 있으면 false 리턴
                if (!players[i].transform.GetComponent<PlayerScript>().isRun)
                    return false;
            }
            return true;
        }
        return false;
    }

    public bool IsAllLeft()
    {      
        if (players.Length == 1)
        {
            for (int i = 0; i < players.Length; i++)
            {
                // 왼쪽 키를 누를 경우 True 반환 오른쪽 키를 누르는 경우 False 반환
                if (!players[i].transform.GetComponent<PlayerScript>().SR.flipX)
                    return false;
            }
            return true;
        }
        return false;
    }

    public bool IsAllRight()
    {
        if (players.Length == 1)
        {
            for (int i = 0; i < players.Length; i++)
            {
                // 왼쪽 키를 누를 경우 True 반환 오른쪽 키를 누르는 경우 False 반환
                if (players[i].transform.GetComponent<PlayerScript>().SR.flipX)
                    return false;
            }
            return true;
        }
        return false;
    }

    public bool IsAllJump()
    {
        if (players.Length == 1)
        {
            for (int i = 0; i < players.Length; i++)
            {
                // 왼쪽 키를 누를 경우 True 반환 오른쪽 키를 누르는 경우 False 반환
                if (players[i].transform.GetComponent<PlayerScript>().isGround)
                    return false;
            }
            return true;
        }
        return false;
    }

    public bool IsAllUnder()
    {
        if (players.Length == 1)
        {
            for (int i = 0; i < players.Length; i++)
            {
                // 왼쪽 키를 누를 경우 True 반환 오른쪽 키를 누르는 경우 False 반환
                if (!players[i].transform.GetComponent<PlayerScript>().isUnder)
                    return false;
            }
            return true;
        }
        return false;
    }

}
