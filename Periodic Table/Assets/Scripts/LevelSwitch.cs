using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelSwitch : MonoBehaviour
{

    public int currentLevel = 0;

    public string[] levelNames = new string[2] { "Quiz Room", "Real Quiz Room" };

    static LevelSwitch s = null;

    // Use this for initialization
    void Start()
    {
        if (s == null)
            s = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
            currentLevel = (currentLevel + 1) % 2;
            SteamVR_LoadLevel.Begin(levelNames[currentLevel]);
    }
}
