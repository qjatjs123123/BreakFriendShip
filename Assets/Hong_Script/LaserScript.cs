using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LaserScript : MonoBehaviourPun
{
    private LineRenderer line;
    public GameObject player;
    public GameObject Laser;
    RaycastHit2D hit;
    GameObject LocalPlayer = null;
    public Image youdied;
    public Image someonedied;
    

    public int hits;
    bool turnon = false;

    bool LaserPoint = false;
    PlayerScript PS;
    public string curscene;
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    void restart()
    {

       PhotonNetwork.LoadLevel("LoadingScene");

    }
    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        

    }

    // Update is called once per frame
    void Update()
    {
        


        firstLaser();
        if (hit.collider != null)
        {
            /*turnon 쓴이유 rpc한번 호출하려고*/
            if (hit.collider.tag == "Player" )
            {
                LocalPlayer = player.transform.GetComponent<round5_test>().character;
                PS = LocalPlayer.transform.GetComponent<PlayerScript>();
                PhotonView collision_PV = hit.collider.transform.GetComponent<PlayerScript>().PV;               
                PS.isDie = true;
                R_NetWorkManager.player_die[collision_PV.OwnerActorNr - 1] += 1;
                if (collision_PV.IsMine)
                {
                    youdied.gameObject.SetActive(true);

                }
                else
                    someonedied.gameObject.SetActive(true);
                if (PhotonNetwork.IsMasterClient && !turnon)
                {
                    turnon = true;
                    Invoke("restart",2);
                }
                
                return;
            }
            LaserPoints();
        }
        else
        {

            line.SetPosition(1, transform.position + new Vector3(10, 0, 0));
        }
    }

    public void firstLaser()
    {
        Debug.DrawRay(transform.position, new Vector3(1, 0, 0) * 50f, new Color(1, 1, 0));
        hit = Physics2D.Raycast(transform.position, new Vector3(1, 0, 0), 50f, 1 << LayerMask.NameToLayer("Defense") | 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Player"));
        


    }

    public void LaserPoints()
    {
        line.SetPosition(1, hit.point);
    }


}
