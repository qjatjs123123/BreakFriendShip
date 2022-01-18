using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> FoundObjects;
     GameObject player;
    bool IsLeft = true;
    public float shortDis;
    public GameObject ghost;
    public float speed;
    bool turnon = false;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length != 4)
            return;
        if (players.Length == 4 && !turnon)
            playerflipX();
        if (checkAxis() && checkdie())
        {
            GameObject p = shortestPlayer();
            ghost.transform.position = Vector3.MoveTowards(ghost.transform.position, p.transform.position, 0.05f);
        }
    }
    // Update is called once per frame
    void Update()
    {


    }
    public bool checkdie()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.GetComponent<PlayerScript>().isDie)
                return false;
        }
        return true;
    }

    public void playerflipX()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.GetComponent<PlayerScript>().SR.flipX = true;
        }
        turnon = true;
    }

    public GameObject shortestPlayer()
    {
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);

        player = FoundObjects[0];

        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance <= shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                
                player = found;
                shortDis = Distance;
            }
        }
        return player;
    }


    public bool checkAxis()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 4)
        {
            for (int i = 0; i<players.Length;i++)
            {
                // 왼쪽 키를 누를 경우 True 반환 오른쪽 키를 누르는 경우 False 반환
                if (players[i].transform.GetComponent<PlayerScript>().SR.flipX)
                    return false;
            }
            return true;
        }
        return false;
    }
}
