using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviourPunCallbacks
{
    static string nextScene;

    [SerializeField]
    Image progressBar;
    public Text status;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        PhotonNetwork.LoadLevel("LoadingScene");
        
    }
    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess(4f));
        

    }
    public void SceneChange()
    {
        PhotonNetwork.LoadLevel(nextScene);
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator LoadSceneProcess(float time)
    {
        /*비동기 포톤*/

        //yield return new WaitForSeconds(time);

        //PhotonNetwork.LoadLevel(nextScene);

        //while (PhotonNetwork.LevelLoadingProgress < 1)
        //{
        //    status.text = "Loading: %" + (int)(PhotonNetwork.LevelLoadingProgress * 100);

        //    progressBar.fillAmount = PhotonNetwork.LevelLoadingProgress;

        //    yield return new WaitForEndOfFrame();
        //}

        /*씬매니저 비동기*/

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }


    }
    

}
