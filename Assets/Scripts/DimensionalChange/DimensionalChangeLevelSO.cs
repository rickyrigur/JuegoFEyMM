using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo nivel de cambio dimensional", menuName = "Scriptables/Niveles/Nuevo nivel de cambio dimensional", order = 80)]
public class DimensionalChangeLevelSO : ScriptableObject
{
    public List<CardSO> cards;
    public CardSO cardM1;
    public CardSO cardM2;
    public int level;
    public List<DimensionalChangeTestSO> fixedTests;
    public List<DimensionalChangeTestSO> randomTest;
}
