using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BtnController : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitBtnClick() {
        Application.Quit();
    }

    public void GotoTitle() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
    }
}
