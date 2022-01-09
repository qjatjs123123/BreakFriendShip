using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class BoxScript : MonoBehaviourPunCallbacks
{
    public Animator AN;
    public PhotonView PV;
    public GameObject DisappearTile;
    public GameObject BreakBox1;
    public GameObject AppleKey;
    public Tilemap BreakTile;
    public BulletScript bullet;


    int HitCount = 0;
    private int MaxHitCount = 4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HitCount == 4)
        {
            gameObject.SetActive(false);
            DisappearTile.gameObject.SetActive(false);
            BreakBox1.SetActive(true);
            AppleKey.SetActive(true);
            BreakTile.gameObject.SetActive(false);

            //bullet = GameObject.Find("Bullet").GetComponent<BulletScript>();

            bullet.BulletScriptTriiger = false;
            PV.RPC("hitcountRPC", RpcTarget.All);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            AN.SetBool("hit", true);
            HitCount++;
        }


    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Invoke("Animations", 2f);
    }
    void Animations()
    {
        AN.SetBool("hit", false);
    }
    [PunRPC]
    void hitcountRPC()
    {
        gameObject.SetActive(false);
        DisappearTile.gameObject.SetActive(false);
        BreakBox1.SetActive(true);
        AppleKey.SetActive(true);
        BreakTile.gameObject.SetActive(false);

        //bullet = GameObject.Find("Bullet").GetComponent<BulletScript>();

        bullet.BulletScriptTriiger = false;
    }
}
