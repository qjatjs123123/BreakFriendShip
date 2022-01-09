using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerScriptH : MonoBehaviourPun, IPunObservable //오버라이드 해줄 필요 없음 Network와 다르게
{
    public int value , valuePerClick, clickUpgradeCost, clickUpgradeAdd, autoUpgradeCost, autoUpgradeAdd, valuePerSecond;
    Network NM;
    PhotonView PV;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(value);
        }
        else
        {
            value = (int)stream.ReceiveNext();
            Debug.Log(value);
        }
    }

    void Start()
    {
        PV = photonView;
        NM = GameObject.FindWithTag("NetworkManager").GetComponent<Network>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;
        NM.ValueText.text = value.ToString();
    }
}
