using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo ensayo de cambio dimensional", menuName = "Scriptables/Ensayos/Nuevo ensayo de cambio dimensional", order = 70)]

public class DimensionalChangeTestSO : ScriptableObject
{
    public TestCriteria criteria;
    public AudioClipSO audioClip;
    public GameEventSO eventOnEnd;
}
