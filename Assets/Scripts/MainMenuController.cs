using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static string NAME = "PlayerName";
    [SerializeField]
    private InputField _inputField;
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        SetPlayerName();
    }

    public void SetPlayerName()
    {
        if (PlayerPrefs.GetString(NAME, "") != "")
        {
            _inputField.text = PlayerPrefs.GetString(NAME);
        }
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString(NAME, name);
    }

    public void DeleteData()
    {
        Directory.Delete(Application.persistentDataPath + "/Documento_Logs", true);
        Directory.Delete(Application.temporaryCachePath, true);
        Directory.Delete(Application.persistentDataPath + "/Videos", true);
    }
}
