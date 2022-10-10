using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Android;

public class SceneController : MonoBehaviour
{

    public Text titulo;
    public InputField codigoTest;
    public void CargarEscena()
    {
        if(codigoTest.text != "")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            titulo.text = "INGRESE CODIGO TEST";
            titulo.color = Color.red;
        }
        
    }

    public void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Debug.Log("Microphone permission has been granted.");

        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            Debug.Log("Camera permission has been granted.");
    }
}
