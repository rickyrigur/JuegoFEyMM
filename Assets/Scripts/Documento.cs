using UnityEngine;
using System.IO;

public class Documento : MonoBehaviour
{
    private string _documentPath;
    public string fileName = "";
    public bool _test;
    public URLUploader uploader;
    private bool _sending;

    void Awake()
    {
        string playerName = PlayerPrefs.GetString(MainMenuController.NAME, "");
        Directory.CreateDirectory(Application.persistentDataPath + "/Documentos/");
        _documentPath = Application.persistentDataPath + $"/Documentos/{fileName}_{playerName}.txt";
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
        string playerName = PlayerPrefs.GetString(MainMenuController.NAME, "");
        string title = _test ? $"{fileName} de {playerName} \n \n" : $"{fileName} \n \n";
        File.WriteAllText(_documentPath, title);
    }

    public void AddToDocument(string data)
    {
        if (_sending)
            return;
        File.AppendAllText(_documentPath ,data + "\n");
        Debug.Log(data);
    }

    public void AddToDocumentWithTimestamp(string data)
    {
        if (_sending)
            return;
        data = data + " en el minuto " + Time.realtimeSinceStartup;
        AddToDocument(data + "\n");
    }

    public void SendDocument()
    {
        if (_sending || uploader == null)
            return;
        _sending = true;
        uploader.UploadFile(_documentPath);
    }
}
