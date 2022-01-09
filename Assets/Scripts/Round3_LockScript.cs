using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Round3_LockScript : MonoBehaviour
{
    public Sprite spr;
    public GameObject fruit;
    public GameObject Round3BreakTile;
    public GameObject TextEffect;

//    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //IF (count == 3) { TextEffect.GetComponent<Typingeffect>().Stop_Func(); }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fruit")
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            int Round = player[0].GetComponent<PlayerScript>().Round;

            gameObject.GetComponent<SpriteRenderer>().sprite = spr;
            fruit.gameObject.SetActive(false);
            Round3BreakTile.gameObject.SetActive(false);
            GameObject.Find("TextEffect").GetComponent<Typingeffect>().Stop_Func();

            
            

        }
    }
}
