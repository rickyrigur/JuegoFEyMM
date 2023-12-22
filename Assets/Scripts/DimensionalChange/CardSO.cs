using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CA_Card", menuName = "Scriptables/Cartas/Nueva Carta", order = 80)]
public class CardSO : ScriptableObject
{
    [SerializeField, Tooltip("Prefab de la carta a usar")]
    private GameObject _prefab;
    [SerializeField, Tooltip("Tipo de carta")]
    private Cards _type;

    public GameObject Prefab { get { return _prefab; } }
    public Cards Type { get { return _type; } }

    public GameObject Clone(Vector3 position, Quaternion rotation, Vector3 scale, Transform parent)
    {
        GameObject clone = Instantiate(Prefab, position, rotation, parent);
        clone.GetComponent<RectTransform>().localPosition = position;
        clone.transform.localScale = scale;
        return clone;
    }
}
