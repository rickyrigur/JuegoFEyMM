using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IValidator
{
    public bool Validate(MemoryBox box, UnityEvent onNoMoreObjects);
}
