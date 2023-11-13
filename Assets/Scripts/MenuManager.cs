using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string email, subject, text;

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public void OpenEmailApplication()
    {
        string emailURL = "mailto:" + email + "?subject=" + subject + "&body=" + text;
        Application.OpenURL(emailURL);
    }
}
