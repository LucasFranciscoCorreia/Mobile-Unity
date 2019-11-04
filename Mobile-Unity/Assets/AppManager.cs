using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{

    //public TextMeshProUGUI login;
    //public TextMeshProUGUI password;
    public TMP_InputField login;
    public TMP_InputField password;
    public TextMeshProUGUI result;
    public UserList users;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://greenhouse-backend.herokuapp.com/users");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Erro");
        }
        else
        {
            string json = @"{""users"":" +System.Text.Encoding.UTF8.GetString(www.downloadHandler.data) + "}";
            Debug.Log(json);
            users = JsonUtility.FromJson<UserList>(json);
        }
        
    }

    public void Login()
    {
        
        foreach (User user in users.users) {
            if(user.email == login.text && user.password == password.text)
            {
                result.text = "Login Realizado com sucesso";
                SceneManager.LoadScene("App");
                return;
            }
        }
        result.text = "Login ou senha invalida";
    }
}

