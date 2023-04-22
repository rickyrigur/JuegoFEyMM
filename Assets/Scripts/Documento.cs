using UnityEngine;
using System.IO;

public class Documento : MonoBehaviour
{
    private string _documentPath;
    public string fileName = "";

    void Awake()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/Documento_Logs/");
        _documentPath = Application.persistentDataPath + "/Documento_Logs/" + "Resultados " + fileName + ".txt";
        CreateDocument();
    }
    private void CreateDocument()
    {
        if (!File.Exists(_documentPath))
        {
            File.Create(_documentPath);
        }        
    }

    private void Start()
    {
        File.WriteAllText(_documentPath, "Resultados ensayo \n \n");
    }

    public void AddToDocument(string data)
    {
        File.AppendAllText(_documentPath ,data + "\n");
        Debug.Log(data);
    }

    public void AddToDocumentWithTimestamp(string data)
    {
        data = data + " en el minuto " + Time.realtimeSinceStartup;
        AddToDocument(data + "\n");
    }

    public void SendDocument()
    {

    }
}
