using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GhostTrigger : MonoBehaviourPunCallbacks
{
    public bool isTrigger = false;
    public string names = "";


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("고스트 트리거 충돌");

        if (collision.tag == "Player")
        {
            isTrigger = true;
            names = collision.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
        }

    }
}
