using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LaserScript : MonoBehaviourPunCallbacks
{
    private LineRenderer line;
    public GameObject Defense;
    public GameObject Laser;
    RaycastHit2D hit;

    float LaserLength;
    public int hits;
    bool LaserPoint = false;
    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + new Vector3(-5, 0, 0));

    }

    // Update is called once per frame
    void Update()
    {
        LaserLength = Vector3.Distance(GameObject.Find("Defense").transform.position, transform.position);

        firstLaser();
        if (hit.collider != null)
        {
            LaserPoints();
        }
        else
        {
            line.SetPosition(1, transform.position + new Vector3(-5, 0, 0));
        }
    }
    public void firstLaser()
    {
        Debug.DrawRay(transform.position, new Vector3(-1, 0, 0) * 5f, new Color(0, 1, 0));
        hit = Physics2D.Raycast(transform.position, new Vector3(-1, 0, 0), 5f, 1 << LayerMask.NameToLayer("Defense"));
    }

    public void LaserPoints()
    {
        line.SetPosition(1, hit.point);
    }
}
