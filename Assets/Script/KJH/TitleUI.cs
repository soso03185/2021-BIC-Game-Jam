using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public GameObject _optionUI;

    public void Play()
    {
        Invoke("GameStart", 1f);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void GameStart()
    {
        SceneManager.LoadScene(1);
    }
}
