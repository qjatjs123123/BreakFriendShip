using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class BulletScript : MonoBehaviourPunCallbacks
{
    public GameObject bullet;
    public GameObject BulletFire;
    public PhotonView PV;
    public bool BulletScriptTriiger = false;
    //-85.54383

   // public AudioSource mysfx;
    //public AudioClip hitboxsfx;
    [SerializeField]
    private bool isRight = true;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (Player.transform.childCount > 0)
        if(BulletScriptTriiger)
        {
            moveBullet();
        }
    }
    
    
    void moveBullet()
    {
        if(isRight)
            bullet.transform.Translate(Vector3.right * 5 * Time.deltaTime);
        else
            bullet.transform.Translate(Vector3.left * 5 * Time.deltaTime);
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Player" || collision.tag == "Box")
        {
            bullet.SetActive(false);
            PV.RPC("moveBulletRPC", RpcTarget.AllViaServer);
            //if (collision.tag == "Box")
            //    hitSound();
                   

        }
    }
    [PunRPC]
    void moveBulletRPC()
    {
        bullet.SetActive(true);
        bullet.transform.position = new Vector3(BulletFire.transform.position.x, BulletFire.transform.position.y, BulletFire.transform.position.z);
    }

    //public void hitSound()
    //{
    //    mysfx.PlayOneShot(hitboxsfx);
    //}

}


