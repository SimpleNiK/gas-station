using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public InputField inf;
    public string password = "";
    public void changeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    public void changePassword() {
        password = inf.text;
    }
    public void changetoProtectedScene()
    {
        if (password == "12345") {
            SceneManager.LoadScene("CreationScene");
        }
        
    }

    public void exit()
    {
        Application.Quit();
    }
}
