using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerData : MonoBehaviour
{
    [SerializeField] InputField idInput;
    [SerializeField] InputField passwordInput;
    [SerializeField] InputField nicknameInput;
    [SerializeField] string url;
    public static PlayerData s_playerdata;
    private data User_data;
    

   
    private void Awake()
    {
        StartCoroutine(CheckVersion());

        if (s_playerdata==null)
        {
            User_data = new data();
            s_playerdata = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public data Get_playerdata()
    {
        return User_data;
    }
    #if UNITY_EDITOR_WIN
    public void Update()
    {
        //데이터 확인용
        if(Input.GetKeyDown(KeyCode.A))
        {
            print("ID = " + User_data.ID);
            print("NN = " + User_data.nickname);
            print("KC = " + User_data._killscore);
            print("DC = " + User_data._deathscore);
        }
    }
    #endif
    public void Login() => StartCoroutine(AccountCo("login"));

    public void Register() => StartCoroutine(AccountCo("register"));
    public void KillCheck() => StartCoroutine(CheckPlayerinfo("killcount", User_data.ID));
    public void DeathCheck() => StartCoroutine(CheckPlayerinfo("deathcount", User_data.ID));
    public void NicknameCheck() => StartCoroutine(CheckPlayerinfo("nicknamecheck", User_data.ID));

   

    IEnumerator Updatedata(string command, string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("command", command);
        form.AddField("id", id);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
    }

    IEnumerator CheckVersion()
    {
        WWWForm form = new WWWForm();
        form.AddField("command", "versioncheck");
        form.AddField("id", Application.version);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        //다른방법 모색해야됨
        GameObject contorller = GameObject.Find("UIcontroll");
        if(www.downloadHandler.text == "[ T_VERSION ]")
        {
            Debug.Log("True version");   
        }
        else
        {
            contorller.GetComponent<UIcontroll>().UIevents[4].Invoke();
        }
    }
    IEnumerator CheckPlayerinfo(string command, string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("command", command);
        form.AddField("id", id);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        print(www.downloadHandler.text);
        if(command == "killcount")
        {
            User_data._killscore = int.Parse(www.downloadHandler.text);
        }
        else if (command == "deathcount")
        {
            User_data._deathscore = int.Parse(www.downloadHandler.text);
        }
        else if (command == "nicknamecheck")
        {
            User_data.nickname = www.downloadHandler.text;
        }
    }

    // login or regiter
    IEnumerator AccountCo(string command)
    {
        WWWForm form = new WWWForm();
        form.AddField("command", command);
        form.AddField("id", idInput.text);
        form.AddField("password", passwordInput.text);
        form.AddField("nickname", nicknameInput.text);
        GameObject contorller = GameObject.Find("UIcontroll");
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        if(command=="login"&&www.downloadHandler.text=="[  OK  ]")
        {
            s_playerdata.User_data = new data();
            s_playerdata.User_data.ID = idInput.text;
            NicknameCheck();
            KillCheck();
            DeathCheck();
            contorller.GetComponent<UIcontroll>().UIevents[0].Invoke();
        }
        else if (command=="register" &&www.downloadHandler.text=="[  OK  ]")
        {
            
            contorller.GetComponent<UIcontroll>().UIevents[2].Invoke();
        }
        else if (command == "register"&& www.downloadHandler.text== "[ FAIL ]")
        {
          
            contorller.GetComponent<UIcontroll>().UIevents[3].Invoke();
        }
        else
        {
            //통상 로그인 실패
            contorller.GetComponent<UIcontroll>().UIevents[1].Invoke();
        }
        
    }

    public string Get_playernickname()
    {
     if(User_data != null)
     {
            return User_data.nickname;
     }
     else
     {
            return null;
     }
    }
    public string Get_playerID()
    {
        if (User_data != null)
        {
            return User_data.ID;
        }
        else
        {
            return null;
        }
    }

    public int Get_killscore()
    {
        if (User_data != null)
        {
            return User_data._killscore;
        }
        else
        {
            return 0;
        }
    }

    public int Get_deathscore()
    {
        if (User_data != null)
        {
            return User_data._deathscore;
        }
        else
        {
            return 0;
        }
    }
}


public class data
{
 
    public string ID;
    public string nickname;
    int total_killscore;
    int total_deathscore;   
    
    public int _deathscore
    {
        get {return total_deathscore; }
        set {total_deathscore=value; }
    }
    public int _killscore
    {
        get { return total_killscore; }
        set { total_killscore = value; }
    }

}
