using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Typingeffect : MonoBehaviour
{

    public Text tx;
    public GameObject PlayerObj;
    public GameObject round3position;
    public GameObject TextEffect;
    public PhotonView PV;
    private string m_text = "¹«±ÃÈ­ ²ÉÀÌ ÇÇ¾ú½À´Ï´Ù";
    // Start is called before the first frame update

    void Update()
    {
        string name = PlayerObj.transform.GetChild(0).name;
        PlayerScript PS = GameObject.Find(name).GetComponent<PlayerScript>();

        if (tx.text == "¹«±ÃÈ­ ²ÉÀÌ ÇÇ¾ú½À´Ï´Ù")
        {
            //Debug.Log("hh");
            bool isRun = PS.isRun;
            bool isGround = PS.isGround;
            if (isRun || !isGround)
            {
                Debug.Log("Á×À½");
                test();
            }
        }
    }
    public void text_start1()
    {        
        InvokeRepeating("text_start", 1.5f,9.6f);
    }
    public void text_start()
    {       
        if (NetworkManager.RoomMaster == PhotonNetwork.LocalPlayer.NickName)
        {
            float[] random_array = new float[m_text.Length+1];
            for (int i = 0; i <= m_text.Length; i++)
            {
                float random = Random.Range(0.4f, 0.8f);
                random_array[i] = random;       
            }
            PV.RPC("Text_Syn_RPC", RpcTarget.All, random_array);
        } 
    }

    public void test()
    {
        PV.RPC("Show_deathImg", RpcTarget.All); 
    }

    public void Stop_Func()
    {
        float[] random_array = new float[m_text.Length + 1];
        for (int i = 0; i <= m_text.Length; i++)
        {
            float random = Random.Range(0.4f, 0.8f);
            random_array[i] = random;

        }
        StopCoroutine(countTime(random_array));
        CancelInvoke("text_start");
        tx.text = "";
        TextEffect.SetActive(false);
    }


    [PunRPC]
    public void Text_Syn_RPC(float[] random)
    {      
        StartCoroutine(countTime(random));
    }

IEnumerator countTime(float[] random)
    {       
        string name = PlayerObj.transform.GetChild(0).name;
        PlayerScript PS = GameObject.Find(name).GetComponent<PlayerScript>();

        for (int i = 0; i<= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);
            if(i== m_text.Length)
            {               
                bool isRun = PS.isRun;
                bool isGround = PS.isGround;
                if (isRun || !isGround)
                {
                    //Debug.Log("Á×À½");
                    test();
                }
            }
            yield return new WaitForSeconds(random[i]);  
        }
    }


    [PunRPC]
    public void Show_deathImg()
    {
        Stop_Func();
        int index = 0;
        for (index = 0; index < PlayerObj.transform.childCount; index++)
        {
            if (PlayerObj.transform.GetChild(index).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == PhotonNetwork.LocalPlayer.NickName)
            {
                break;
            }
        }

        //RespawnÇÔ¼ö
        PlayerObj.transform.GetChild(index).transform.position = new Vector3(round3position.transform.position.x, round3position.transform.position.y, round3position.transform.position.z);
        TextEffect.SetActive(true);
        text_start1();
    }
}
