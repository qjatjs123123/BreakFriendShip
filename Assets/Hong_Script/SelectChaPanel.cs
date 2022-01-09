using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SelectChaPanel : MonoBehaviour
{
    public Image select_img;
    public Image[] img;
    public static int char_num = 0;
    public int num = 0;
    public Text NickName;
    public Text charactername;
    public InputField NickNameInput;

    GameObject NetworkManager;
    private void Start()
    {
        NetworkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        
        NickName.text = NickNameInput.text;
    }
    public void select_character(int num)
    {
        if (num == 0)
        {
            select_img.transform.GetComponent<Image>().sprite = img[0].GetComponent<Image>().sprite;
            charactername.text = "마스크듀드";
            NetworkManager.transform.GetComponent<R_NetWorkManager>().selectnum = 1;
            
        }
        else if (num == 1)
        {
            select_img.transform.GetComponent<Image>().sprite = img[1].GetComponent<Image>().sprite;
            charactername.text = "닌자거북이";
            NetworkManager.transform.GetComponent<R_NetWorkManager>().selectnum = 2;
        }
        else if (num == 2)
        {
            select_img.transform.GetComponent<Image>().sprite = img[2].GetComponent<Image>().sprite;
            charactername.text = "핑크맨";
            NetworkManager.transform.GetComponent<R_NetWorkManager>().selectnum = 3;
        }
        else if (num == 3)
        {
            select_img.transform.GetComponent<Image>().sprite = img[3].GetComponent<Image>().sprite;
            charactername.text = "버츄얼가이";
            NetworkManager.transform.GetComponent<R_NetWorkManager>().selectnum = 4;
        }
        
    }



    
    // Update is called once per frame
    void Update()
    {
        
    }
}
