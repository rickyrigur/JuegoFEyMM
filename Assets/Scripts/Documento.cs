using UnityEngine;
using System.IO;

public class Documento : MonoBehaviour
{
    private string _documentPath;
    public string fileName = "";
    public bool _test;

    void Awake()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/Documento_Logs/");
        _documentPath = Application.persistentDataPath + "/Documento_Logs/" + fileName + ".txt";
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
        string title = _test ? "Resultados ensayo \n \n" : fileName + "\n \n";
        File.WriteAllText(_documentPath, title);
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
