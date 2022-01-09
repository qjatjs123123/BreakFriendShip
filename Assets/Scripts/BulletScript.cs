using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class BulletScript : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public GameObject bullet;
    public GameObject Player;
    public GameObject BulletFire;
    public bool BulletScriptTriiger = false;
    //-85.54383

    public AudioSource mysfx;
    public AudioClip hitboxsfx;
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
        //if (bullet.transform.position.x > -85.544 )
        //{
        //    PV.RPC("moveBulletRPC", RpcTarget.AllBuffered);
        //}
        if(isRight)
            bullet.transform.Translate(Vector3.right * 5 * Time.deltaTime);
        else
            bullet.transform.Translate(Vector3.left * 5 * Time.deltaTime);

    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground" || collision.tag == "Player" || collision.tag == "Box")
        {
            PV.RPC("moveBulletRPC", RpcTarget.AllBuffered);
            if (collision.tag == "Box")
                hitSound();
            //Destroy(bullet);           

        }
        // moveBulletRPC();
        //Instantiate(bullet, BulletFire.transform.position, BulletFire.transform.rotation);

    }



    [PunRPC]
    void moveBulletRPC()
    {
        //bullet.transform.position = new Vector3(-4.5f, -1.5f, 0f);
        bullet.transform.position = new Vector3(BulletFire.transform.position.x, BulletFire.transform.position.y,BulletFire.transform.position.z);
        //Instantiate(bullet, BulletFire.transform.position, BulletFire.transform.rotation);
    }
    public void hitSound()
    {
        mysfx.PlayOneShot(hitboxsfx);
    }

}


