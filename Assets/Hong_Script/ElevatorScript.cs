using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ElevatorScript : MonoBehaviourPun
{
    bool[] players_Ison = { false, false, false, false };
    public Transform target;
    bool turnon = false;
    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turnon)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 3 * Time.deltaTime);
    }

    [PunRPC]
    void moveElevator()
    {
        turnon = true;
    }

    private bool AllOnElevator()
    {
        for(int i = 0; i < 4; i++)
        {
            if (!players_Ison[i])
                return false;
        }
        return true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        int actnum = collision.transform.GetComponent<PlayerScript>().PV.OwnerActorNr;
        players_Ison[actnum-1] = true;
        collision.transform.SetParent(transform, true);

        if (AllOnElevator())
            PV.RPC("moveElevator", RpcTarget.AllViaServer);

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        int actnum = collision.transform.GetComponent<PlayerScript>().PV.OwnerActorNr;
        collision.transform.SetParent(null, true);
        players_Ison[actnum-1] = false;
    }


}
