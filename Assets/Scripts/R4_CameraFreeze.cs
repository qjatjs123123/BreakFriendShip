using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R4_CameraFreeze : MonoBehaviour
{
    [SerializeField]    
    private GameObject vcam;
    [SerializeField]
    private GameObject vcamFreeze;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           // Debug.Log("R4_camTriigerOn");
            vcam.transform.position = new Vector3(vcamFreeze.transform.position.x, vcamFreeze.transform.position.y, vcamFreeze.transform.position.z);
        }
    }
}
