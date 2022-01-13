using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alll_init : MonoBehaviour
{
    public Transform[] SpawnPosition;
    public Transform GhostSpawn;
    public Transform AppleSpawn;

    public GameObject ghost;
    public GameObject[] players;
    public GameObject apple;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void init_round6()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int index = 0;

        for (int i = 0; i < players.Length; i++)
        {
            index = players[i].transform.GetComponent<PlayerScript>().PV.OwnerActorNr-1;
            players[i].transform.position = new Vector3(SpawnPosition[index].position.x, SpawnPosition[index].position.y, SpawnPosition[index].position.z);               
        }

        ghost.transform.position = new Vector3(GhostSpawn.position.x, GhostSpawn.position.y, GhostSpawn.position.z);
        apple.transform.position = new Vector3(AppleSpawn.position.x, AppleSpawn.position.y, AppleSpawn.position.z);
        apple.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
