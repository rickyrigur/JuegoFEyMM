using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    public TextMeshProUGUI titulo;
    public TMP_InputField codigoTest;
    public void CargarEscena(string nombreEscena)
    {
        if (codigoTest.text != "")
            SceneManager.LoadScene(nombreEscena);
        else
        {
            titulo.text = "INGRESE CODIGO DE TEST";
            titulo.color = Color.red;
        }
    }
}
