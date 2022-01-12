using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class defense_Script : MonoBehaviourPunCallbacks, IPunObservable
{

    Vector3 curPos;
    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (!PV.IsMine)
            transform.position = curPos;*/
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);

        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();

        }
    }
}
