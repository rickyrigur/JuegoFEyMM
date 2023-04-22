using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo ensayo de memoria", menuName = "Scriptables/Ensayos/Nuevo ensayo de memoria", order = 71)]
public class MemoryTestSO : ScriptableObject
{
    [Tooltip("Numero de prueba")]
    public int testNumber = 1;
    [Tooltip("Cantidad de cajas")]
    public int boxesAmount = 1;
    [Tooltip("Cantidad de objetos")]
    public int objectAmount = 1;
    [Tooltip("Posicion de las cajas")]
    public Positions position;
    [Tooltip("Delay en segundos antes de empezar a jugar")]
    public int delay = 1;

    [HideInInspector]
    public int level;
    [HideInInspector]
    public List<MemoryObjects> objects;
}