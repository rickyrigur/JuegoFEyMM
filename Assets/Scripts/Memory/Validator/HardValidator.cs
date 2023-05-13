using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HardValidator : IValidator
{
    private List<MemoryObjects> _objects = new List<MemoryObjects>();

    public HardValidator(List<MemoryObjects> objects)
    {
        _objects = objects;
    }

    public bool Validate(MemoryBox box, UnityEvent onNoMoreLevels)
    {
        MemoryObjects objectToVerify = _objects[0];
        bool result = false;
        if (box.hasElement && box.objectHided == objectToVerify)
        {
            result = true;
            _objects.RemoveAt(0);
        }

        if (_objects.Count <= 0)
        {
            onNoMoreLevels?.Invoke();
        }

        return result;
    }
}
