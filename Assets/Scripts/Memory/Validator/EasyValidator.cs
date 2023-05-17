using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EasyValidator : IValidator
{
    private List<MemoryObjects> _objects = new List<MemoryObjects>();
    private TutorialController _tutorialController;

    public EasyValidator(List<MemoryObjects> objects, TutorialController tutorialController)
    {
        _objects = objects;
        _tutorialController = tutorialController;
    }

    public bool Validate(MemoryBox box, UnityEvent onNoMoreObjects)
    {
        bool result = false;
        if (box.hasElement && _objects.Contains(box.objectHided))
        {
            result = true;
            GameVars.CorrectAmount += 1;
            _objects.Remove(box.objectHided);
        }
        else
        {
            GameVars.WrongAmount += 1;
        }

        if (GameVars.PlayingTutorial)
        {
            _tutorialController.Validate(result);
            return result;
        }

        if (_objects.Count <= 0)
        {
            onNoMoreObjects?.Invoke();
        }

        return result;
    }
}
