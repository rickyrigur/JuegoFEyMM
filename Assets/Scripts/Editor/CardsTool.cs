using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CardsTool : EditorWindow 
{
    List<GameObject> prefabs = new List<GameObject>();
    Object _focusedObject;
    GameObject _GO;
    private string _SearchName;
    private string nName;
    private string nPath;
    public List<Object> assetList = new List<Object>();

    [MenuItem("CustomTools/Cards")] //la ubicacion (la "ruta") de la opcion de menu que abre la ventana
    public static void OpenWindow() //el método al que accede el menú tiene que ser "static"
    {
        //consigue una instancia de una ventana del tipo especificado y la abre.
        //GetWindow(typeof(FirstWindow)).Show();

        //Si necesito guardar la referencia, GetWindow me devuelve una EditorWindow, asi que hay que castear.
        var myWindow = GetWindow<CardsTool>();
        myWindow.wantsMouseMove = true; //necesario para que podamos detectar el mouse
        myWindow.Show();
    }

    //todas las opciones que usamos aca tambien pueden usarse en CustomInspectors
    private void OnGUI() //aca adentro vamos a hacer todo
    {
        if (_focusedObject != null)
        {
            //AssetDatabase.Contains nos dice si el objeto es un prefab o no
            if (AssetDatabase.Contains(_focusedObject))
                DrawPrefabSettings();
            else
                EditorGUILayout.HelpBox("The selected object is not a prefab", MessageType.Error);
        }
        else
        {
            EditorGUILayout.HelpBox("Seleccionar un prefab", MessageType.Info);
            PrefabFinder();
        }
    }
    private void PrefabFinder()
    {
        EditorGUILayout.LabelField("Busqueda de Assets:");
        var aux = _SearchName;
        _SearchName = EditorGUILayout.TextField(aux);
        if (aux != _SearchName)
        {
            assetList.Clear();
            //AssetDatabase.FindAssets me retorna todos los paths de los assets que coinciden con el parámetro, en formato GUID
            string[] paths = AssetDatabase.FindAssets(_SearchName);

            for (int i = 0; i < paths.Length; i++)
            {
                //Convierto el GUID al formato "normal"
                paths[i] = AssetDatabase.GUIDToAssetPath(paths[i]);

                //cargo el asset en memoria
                var loaded = AssetDatabase.LoadAssetAtPath(paths[i], typeof(Object));

                assetList.Add(loaded);
            }
        }

        for (int i = 0; i < assetList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(assetList[i].ToString());
            if (GUILayout.Button("Seleccionar"))
                _focusedObject = assetList[i];
            EditorGUILayout.EndHorizontal();
        }
    }

    private void DrawPrefabSettings()
    {
        EditorGUILayout.LabelField("Informacion general");
        //con AssetDatabase.GetAssetPath conseguimos la ruta donde esta guardado el asset (relativo a la carpeta del proyecto, o sea, empieza en la carpeta "Assets")
        var currentPath = AssetDatabase.GetAssetPath(_focusedObject);
        EditorGUILayout.LabelField(currentPath);
        //para escenas pueden usar el AssetDatabase.GetAssetOrScenePath()

        for (int i = 0; i < 2; i++)
            EditorGUILayout.Space();

        if (GUILayout.Button("Update Prefab"))
        {
            UpdatePrefab();
            UpdateDatabase();
        }
    }


    public void UpdatePrefab()
    {
        _GO = _focusedObject as GameObject;
        Image image = _GO.AddComponent<Image>();
        image.sprite = _GO.GetComponent<SpriteRenderer>().sprite;
    }

    public void UpdateDatabase()
    {
        //para que aparezcan los archivos nuevos creados o los cambios hechos:

        //aplica los cambios hechos a los assets en memoria
        AssetDatabase.SaveAssets();

        //recarga la database (y actualiza el panel "project" del editor)
        AssetDatabase.Refresh();
    }
}
