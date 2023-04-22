using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo nivel de memoria", menuName = "Scriptables/Niveles/Nuevo nivel de memoria", order = 81)]

public class MemoryLevelSO : ScriptableObject
{
    public int level;
    public List<MemoryTestSO> tests;
    public List<MemoryObjects> objects;

    public void OnEnable()
    {
        foreach (MemoryTestSO currentTest in tests)
        {
            currentTest.level = level;
            currentTest.objects = objects;
        }
    }
}
