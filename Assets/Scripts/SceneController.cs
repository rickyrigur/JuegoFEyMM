using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Android;

public class SceneController : MonoBehaviour
{
    public Text titulo;
    public InputField codigoTest;

    public void Start()
    {
        Permission.RequestUserPermissions(new string[] { Permission.Microphone, Permission.Camera});
    }

    public void LoadScene(int sceneNumber)
    {
        if(codigoTest.text != "")
        {
            SceneManager.LoadScene(sceneNumber);
        }
        else
        {
            titulo.text = "INGRESE NOMBRE DE LA PERSONA A REALIZAR EL TEST";
            titulo.color = Color.red;
        }
    }

    public void ExitGame()
    {
        Debug.Log("CALLED EXIT");
        Application.Quit();
    }
}
