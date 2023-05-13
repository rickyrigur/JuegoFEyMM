using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private const string NAME = "PlayerName";

    public void SetName(string name)
    {
        PlayerPrefs.SetString(NAME, name);
    }

    public void StartLevel()
    {

    }

    public void DeleteData()
    {

    }
}
