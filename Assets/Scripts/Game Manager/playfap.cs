using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using TMPro;
using UnityEditor.PackageManager;

public class playfap : MonoBehaviour
{
    public TMP_InputField user;
    public TMP_InputField pass;
    public TMP_InputField email;
    public TextMeshProUGUI message;

    public GameObject rowprefab;
    public Transform tbody;

    public int diem;
    public static string PlayerName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void btndn()
    {
        var req = new LoginWithPlayFabRequest
        {
            Username = user.text,
            Password = pass.text,
        };
        PlayFabClientAPI.LoginWithPlayFab(req, dntc, loidn);
    }
    void dntc(LoginResult loginResult)
    {
        message.text = "đăng nhập thành công";
        SceneManager.LoadScene("SampleScene");
        SendPoint(0);
        PlayerName = user.text;
    }
    public void SendPoint(int x)
    {
        var req = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { new StatisticUpdate()
            {
                StatisticName="Point",
                Value=x
            }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(req, updatetc, loidn);
    }
    void loidn(PlayFabError error)
    {
        message.text = error.ErrorMessage;
    }
    public void updatetc(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("updateSuccess");
    }
    public void btndk()
    {
        var req = new RegisterPlayFabUserRequest
        {
            Username = user.text,
            Password = pass.text,
            Email = email.text,
            DisplayName = user.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(req, dktc, loidk);
    }
    void dktc(RegisterPlayFabUserResult result)
    {
        message.text = "đăng kí thành công";
        SceneManager.LoadScene("Login");
    }
    void loidk(PlayFabError error)
    {
        message.text = error.ErrorMessage;
    }

    public void getBD()
    {
        var req = new GetLeaderboardRequest
        {
            StatisticName = "Point", //  "Bangdiem",
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(req, onLeaderboardGet, loibd);
    }
    void loibd(PlayFabError error)
    {
        message.text = error.ErrorMessage;
    }
    void onLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in tbody)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowprefab, tbody);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName.ToString();
            texts[2].text = item.StatValue.ToString();
            Debug.Log(item.Position + " " + item.PlayFabId + item.StatValue);
        }
    }
    public void LoadScreenBD()
    {
        SceneManager.LoadScene("BangDiem");
    }
    public void ChoiLai()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadScreenGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
