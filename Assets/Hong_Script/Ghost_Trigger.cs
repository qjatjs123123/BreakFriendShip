using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Trigger : MonoBehaviour
{
    public GameObject target;
    public GameObject wall;
    Vector3 pos; //현재위치

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "ghost")
        {
            Debug.Log("실행");
            wall.transform.position = Vector3.MoveTowards(wall.transform.position, target.transform.position, 10 * Time.deltaTime);
        }
    }


}
