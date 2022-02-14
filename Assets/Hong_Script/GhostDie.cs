using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GhostDie : MonoBehaviourPun
{
    
    public PhotonView PV;
    public Transform GhostSpawn;
    public Transform Ghost;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ghost")
        {           
            PV.RPC("MoveGhost", RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    void MoveGhost()
    {
        Ghost.position = new Vector3(GhostSpawn.position.x, GhostSpawn.position.y, GhostSpawn.position.z);
    }
}
