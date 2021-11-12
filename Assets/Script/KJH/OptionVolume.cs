using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionVolume : MonoBehaviour
{
    public String type;
    public Image[] bars;

    private int level;

    private int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            while (level > bars.Length)
            {
                level = 0;
            }

            for (int i = level; i < bars.Length; i++)
            {
                bars[i].enabled = false;
            }

            for (int i = 0; i < level; i++)
            {
                bars[i].enabled = true;
            }

            if (type.Trim().ToLower() == "bgm")
            {
                SoundManager.Instance.SetBGMVolume((float)level / bars.Length);
            }
            else if (type.Trim().ToLower() == "sfx")
            {
                SoundManager.Instance.SetSFXVolume((float)level / (float)bars.Length);
            }
        }
    }

    private void Start()
    {
        Level = bars.Length;
    }

    public void NextLevel()
    {
        Level += 1;
    }
}