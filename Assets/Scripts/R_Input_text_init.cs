using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Input_text_init : MonoBehaviour
{
    public InputField Rogin_EmailInput, Rogin_PasswordInput,NickName_input;

    public void init_text()
    {
        Rogin_EmailInput.text = "";
        Rogin_PasswordInput.text = "";
        NickName_input.text = "";
    }
}
