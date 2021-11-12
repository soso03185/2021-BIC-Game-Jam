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
   //     SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
