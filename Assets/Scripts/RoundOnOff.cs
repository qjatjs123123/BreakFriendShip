using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundOnOff : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject RoundTriggerOn;
    [SerializeField]
    private GameObject RoundTriggerOff;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RoundTriggerOn.SetActive(true);
            RoundTriggerOff.SetActive(false);
        }
        if (RoundTriggerOn.gameObject.name == "Round3")
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < player.Length; i++)
            {
                player[i].GetComponent<PlayerScript>().Round = 3;
            }
        }
        if (RoundTriggerOn.gameObject.name == "Round4")
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < player.Length; i++)
            {
                player[i].GetComponent<PlayerScript>().Round =4;
            }
        }
        if (RoundTriggerOn.gameObject.name == "Round5")
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < player.Length; i++)
            {
                player[i].GetComponent<PlayerScript>().Round = 5;
            }
        }
        if (RoundTriggerOn.gameObject.name == "Round6")
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < player.Length; i++)
            {
                player[i].GetComponent<PlayerScript>().Round = 6;
            }
        }
    }
}
