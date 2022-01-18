using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class init_round6 : MonoBehaviour
{
    public Transform[] tr;
    public Image youdied;
    public Image someonedied;
    public Image[] IsFruit;
    public Sprite apple_silute;
    public Transform applespawn;
    public Transform ghostspawn;
    public GameObject apple_siluet_obj;
    public GameObject apple;
    public GameObject ghost;
    public GameObject wall1;
    public GameObject wall2;


    public void init_round()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            int actnum = players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr - 1;
            players[i].transform.GetComponent<PlayerScript>().isDie = false;
            players[i].transform.position = new Vector3(tr[actnum].position.x, tr[actnum].position.y, tr[actnum].position.z);
            IsFruit[i].sprite = apple_silute;
        }
        youdied.gameObject.SetActive(false);
        someonedied.gameObject.SetActive(false);
        apple.transform.parent = null;
        apple.transform.position = new Vector3(applespawn.position.x, applespawn.position.y, applespawn.position.z);       
        ghost.transform.position = new Vector3(ghostspawn.position.x, ghostspawn.position.y, ghostspawn.position.z);
        wall1.SetActive(true);
        wall2.SetActive(true);
        apple_siluet_obj.transform.GetComponent<SpriteRenderer>().sprite = apple_silute;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.GetComponent<PlayerScript>().SR.flipX = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
