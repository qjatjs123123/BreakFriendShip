using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class init_round4 : MonoBehaviour
{

    public Transform[] tr;
    public Image youdied;
    public Image someonedied;
    public Image[] IsFruit;
    public Sprite apple_silute;
    public Transform applespawn;
    public GameObject apple;
    public GameObject Typingeffect;
    public GameObject apple_siluet_obj;

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
        apple.transform.position = new Vector3(applespawn.position.x, applespawn.position.y, applespawn.position.z);
        Typingeffect.transform.GetComponent<Typingeffect>().tx.text = "";
        Typingeffect.SetActive(false);
        Typingeffect.SetActive(true);
        Typingeffect.transform.GetComponent<Typingeffect>().Start();
        Typingeffect.transform.GetComponent<Typingeffect>().Ghost.transform.rotation = Quaternion.Euler(0, 180, 0);
        apple.transform.parent = null;
        apple_siluet_obj.transform.GetComponent<SpriteRenderer>().sprite = apple_silute;
        apple.SetActive(true);
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
