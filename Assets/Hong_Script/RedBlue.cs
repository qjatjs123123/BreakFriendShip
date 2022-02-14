using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RedBlue : MonoBehaviourPun
{
    public Image red;
    public Image blue;
    public Image blue_white;
    public Image red_white;
    public PhotonView PV;

    public bool IsBlue;
    bool Isturnon;
    // Start is called before the first frame update
    void Start()
    {
        IsBlue = true;
        Isturnon = true;
        //StartCoroutine("blinkColor");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (IsAllReady(players) && Isturnon && PhotonNetwork.IsMasterClient)
        {
            PV.RPC("startTine", RpcTarget.AllViaServer);
            Isturnon = false;
        }

    }

    public bool IsAllReady(GameObject[] players)
    {
        for (int i = 0; i < 2; i++)
            if (!players[i].transform.GetComponent<PlayerScript>().isReady)
                return false;

        return true;
    }

    [PunRPC]
    void startTine()
    {
        StartCoroutine("blinkColor");
    }


    IEnumerator blinkColor()
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 4; i++)
        {
            if (IsBlue)
            {
                blue.gameObject.SetActive(false);
                blue_white.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.3f);


                blue.gameObject.SetActive(true);
                blue_white.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.3f);
            }

            else
            {
                red.gameObject.SetActive(false);
                red_white.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.3f);


                red.gameObject.SetActive(true);
                red_white.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.3f);
            }
        }

        //±ôºý °Å¸² ³¡³ª°í ½ÅÈ£µî »ö±ò Ã¼ÀÎÁö
        if (IsBlue)
        {
            red.gameObject.SetActive(true);
            red_white.gameObject.SetActive(false);

            blue.gameObject.SetActive(false);
            blue_white.gameObject.SetActive(true);
            IsBlue = false;
        }
        else
        {
            blue.gameObject.SetActive(true);
            blue_white.gameObject.SetActive(false);

            red.gameObject.SetActive(false);
            red_white.gameObject.SetActive(true);
            IsBlue = true;
        }

        if (PhotonNetwork.IsMasterClient)       
            PV.RPC("startTine", RpcTarget.AllViaServer);
    }
}
