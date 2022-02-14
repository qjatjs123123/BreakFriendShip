using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class R6_TrapMove : MonoBehaviourPun
{
    Vector3 pos; //현재위치

    [SerializeField]
    private float delta = 2.0f; // 이동가능한 최대값
    [SerializeField]
    private float speed = 3.0f; // 이동속도
    [SerializeField]
    private bool isXmove = false;

    [SerializeField]
    private bool Rightmove = false;

    public bool turnon = false;
    public bool switchon = false;
    public PhotonView PV;

    private float runningTime = 0f;
    private float yPos = 0f;
    private float xPos = 0f;
    GameObject[] players;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        //if (!turnon)
        //    return;
        
        

        if (!turnon)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (IsAllReady(players))
            {
                PV.RPC("Synswitchon", RpcTarget.AllViaServer);
            }
        }
        if (!switchon)
            return;
        if (isXmove) 
        {
            if(Rightmove)
                moveX();
            if (!Rightmove)
                RmoveX();

        }
        else moveY();
    }

    [PunRPC]
    void Synswitchon()
    {
        switchon = true;
        turnon = true;
    }

    public bool IsAllReady(GameObject[] players)
    {
        for (int i = 0; i < 2; i++)
            if (!players[i].transform.GetComponent<PlayerScript>().isReady)
                return false;

        return true;
    }

    private void moveX() {
        Vector3 vectorPos = pos;
        runningTime += Time.deltaTime * speed;
        vectorPos.x += Mathf.Sin(runningTime) * delta;
        transform.position = vectorPos;
    }
    private void RmoveX()
    {
        Vector3 vectorPos = pos;
        runningTime += Time.deltaTime * speed;
        vectorPos.x -= Mathf.Sin(runningTime) * delta;
        transform.position = vectorPos;

    }
    private void moveY() {
        Vector3 vectorPos = pos;
        runningTime += Time.deltaTime * speed;
        vectorPos.y += Mathf.Sin(runningTime) * delta;
        transform.position = vectorPos;
    }
}
