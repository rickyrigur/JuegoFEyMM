using UnityEngine;
using UnityEngine.Events;

public class MemoryBox : MonoBehaviour
{
    public MemoryObjects objectHided;
    public bool hasElement;
    public UnityEvent<bool> OnSelect;

    public MemoryBox Clone()
    {
        return Clone(transform);
    }

    public MemoryBox Clone(Transform transform)
    {
        return Clone(transform.position, transform.rotation);
    }

    public MemoryBox Clone(Vector3 position, Quaternion rotation)
    {
        return Instantiate(this, position, rotation);
    }

    public void HideObject(MemoryObjects memoryObject)
    {
        hasElement = true;
        objectHided = memoryObject;
    }

    public bool Select(MemoryObjects element)
    {
        bool rightAnswer = hasElement && objectHided == element;
        OnSelect?.Invoke(rightAnswer);
        return rightAnswer;
    }
}
