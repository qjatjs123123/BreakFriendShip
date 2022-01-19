using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ElevatorScript : MonoBehaviourPun
{
    public bool[] players_Ison = { false, false, false, false };
    public Transform target;
    public bool turnon = false;
    public PhotonView PV;
    public GameObject player;
    public Image youdied;
    public Image someonedied;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnon)
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
        if (youdied.gameObject.activeSelf == true || someonedied.gameObject.activeSelf == true)
            return;

        int index = collision.transform.GetComponent<PlayerScript>().PV.OwnerActorNr;
        int actnum = player.transform.GetComponent<round5_test>().get_player_index(index);
        players_Ison[actnum] = true;
        collision.transform.SetParent(transform, true);

        if (AllOnElevator() && !collision.transform.GetComponent<PlayerScript>().isDie)
            PV.RPC("moveElevator", RpcTarget.AllViaServer);

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        int index = collision.transform.GetComponent<PlayerScript>().PV.OwnerActorNr;
        int actnum = player.transform.GetComponent<round5_test>().get_player_index(index);
        collision.transform.SetParent(null, true);
        players_Ison[actnum] = false;
        turnon = false;
    }


}
