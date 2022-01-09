using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayFabManager : MonoBehaviour
{
    public InputField Rogin_EmailInput, Rogin_PasswordInput;
    public InputField EmailInput, PasswordInput, UsernameInput;
    public Text register_text;
    public Text rogin_text;
    public List<PlayerLeaderboardEntry> PlayFabUserList = new List<PlayerLeaderboardEntry>();
    public static string Nickname = "";
    public PlayerLeaderboardEntry MyPlayFabInfo;
    public GameObject SelectCharacterImagePanel;


    public void LoginBtn()
    {
        var request = new LoginWithEmailAddressRequest { Email = Rogin_EmailInput.text, Password = Rogin_PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { GetLeaderboard(result.PlayFabId); SelectCharacterImagePanel.SetActive(true); }, (error) => rogin_text.text = "로그인 실패");
        
    }

        public void RegisterBtn()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text, DisplayName = UsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { register_text.text = "회원가입 성공"; SetStat(); SetData("default"); }, (error) => register_text.text = "회원가입 실패");
    }

    void SetStat()
    {
        var request = new UpdatePlayerStatisticsRequest { Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "IDInfo", Value = 0 } } };
        PlayFabClientAPI.UpdatePlayerStatistics(request, (result) => { }, (error) => print("값 저장실패"));
    }

    void SetData(string curData)
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() { { "Home", curData } },
            Permission = UserDataPermission.Public
        };
        PlayFabClientAPI.UpdateUserData(request, (result) => { }, (error) => print("데이터 저장 실패"));
    }

    void GetLeaderboard(string myID)
    {
        PlayFabUserList.Clear();
        Debug.Log(myID);
        for (int i = 0; i < 10; i++)
        {
            var request = new GetLeaderboardRequest
            {
                StartPosition = i * 100,
                StatisticName = "IDInfo",
                MaxResultsCount = 100,
                ProfileConstraints = new PlayerProfileViewConstraints() { ShowDisplayName = true }
            };
            PlayFabClientAPI.GetLeaderboard(request, (result) =>
            {
                if (result.Leaderboard.Count == 0) return;
                for (int j = 0; j < result.Leaderboard.Count; j++)
                {
                    PlayFabUserList.Add(result.Leaderboard[j]);
                    if (result.Leaderboard[j].PlayFabId == myID) { 
                        
                        MyPlayFabInfo = result.Leaderboard[j];
                        Nickname = MyPlayFabInfo.DisplayName;
                        Debug.Log(Nickname);
                        PhotonNetwork.LocalPlayer.NickName = MyPlayFabInfo.DisplayName;
                    }
                }
            },
            (error) => { });
        }
    }

    void OnLoginSuccess(LoginResult result) => print("로그인 성공");

    

    void OnRegisterSuccess(RegisterPlayFabUserResult result) => register_text.text = "회원가입 성공";

    void OnRegisterFailure(PlayFabError error) => register_text.text = "회원가입 실패";

}
