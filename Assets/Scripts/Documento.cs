using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Documento : MonoBehaviour
{
    public string txtDocumento;

    // Start is called before the first frame update
    void Start()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/Documento_Logs/");

        txtDocumento = Application.streamingAssetsPath + "/Documento_Logs/" + "Resultados" + ".txt";

        CrearDocumento();
    }

    public void CrearDocumento()
    {
        //Crea el .txt en el directorio en la función Start
        //string txtDocumento = Application.streamingAssetsPath + "/Documento_Logs/" + "Resultados" + ".txt";

        if (!File.Exists(txtDocumento))
        {
            File.WriteAllText(txtDocumento, "Resultados ensayo \n \n");
            File.AppendAllText(txtDocumento, "Ensayo prueba " + "\n");
        }        
    }

    public void LlenarDocumento()
    {
        File.AppendAllText(txtDocumento, "Ensayo nº " + GetComponent<gameManager>().nivel + "\n");
        File.AppendAllText(txtDocumento, "Correctos: " + GetComponent<gameManager>().correctos + "\n");
        File.AppendAllText(txtDocumento, "Incorrectos: " + GetComponent<gameManager>().incorrectos + "\n");

        File.AppendAllText(txtDocumento, "\n\n");

    }
}
