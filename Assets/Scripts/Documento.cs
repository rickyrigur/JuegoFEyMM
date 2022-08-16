using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Documento : MonoBehaviour
{
    public string txtDocumento;
    private gameManager script;

    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<gameManager>();

        //Directory.CreateDirectory(Application.streamingAssetsPath + "/Documento_Logs/");
        //txtDocumento = Application.streamingAssetsPath + "/Documento_Logs/" + "Resultados" + ".txt";

        Directory.CreateDirectory(Application.persistentDataPath + "/Documento_Logs/");

        txtDocumento = Application.persistentDataPath + "/Documento_Logs/" + "Resultados" + ".txt";

        CrearDocumento();
    }

    public void CrearDocumento()
    {
        //Crea el .txt en el directorio en la función Start
        //string txtDocumento = Application.streamingAssetsPath + "/Documento_Logs/" + "Resultados" + ".txt";

        if (!File.Exists(txtDocumento))
        {
            File.WriteAllText(txtDocumento, "Resultados ensayo \n \n");
            File.AppendAllText(txtDocumento, "Sujeto: " + "\n\n");
        }        
    }

    public void LlenarDocumento()
    {
        if (script.niv == 0)
        {
            File.AppendAllText(txtDocumento, "-------------------" + script.nivel + "\n");
            File.AppendAllText(txtDocumento, "Ensayo nº " + script.nivel + "\n\n");
        }            

        File.AppendAllText(txtDocumento, "Sub ensayo nº " + script.niv + "\n");
        File.AppendAllText(txtDocumento, "Criterio: " + script.listaNiveles[script.nivel][script.niv].ToString() + "\n");
        File.AppendAllText(txtDocumento, "Correctos: " + script.correctos + "\n");
        File.AppendAllText(txtDocumento, "Incorrectos: " + script.incorrectos + "\n");

        File.AppendAllText(txtDocumento, "\n\n");

    }
}
