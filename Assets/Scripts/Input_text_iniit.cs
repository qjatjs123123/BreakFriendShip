using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_text_iniit : MonoBehaviour
{
    public InputField Rogin_EmailInput, Rogin_PasswordInput;

    public void init_text()
    {
        Rogin_EmailInput.text = "";
        Rogin_PasswordInput.text = "";
    }
}
