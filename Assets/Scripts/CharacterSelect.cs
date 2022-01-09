using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Image select_img;
    public Image[] img;
    public void select_character(int num)
    {
        if (num == 0)
        {
            select_img.transform.GetComponent<Image>().sprite = img[0].GetComponent<Image>().sprite;
        }
        else if (num == 1)
        {
            select_img.transform.GetComponent<Image>().sprite = img[1].GetComponent<Image>().sprite;
        }
        else if (num == 2)
        {
            select_img.transform.GetComponent<Image>().sprite = img[2].GetComponent<Image>().sprite;
        }
        else if (num == 3)
        {
            select_img.transform.GetComponent<Image>().sprite = img[3].GetComponent<Image>().sprite;
        }
    }
}
