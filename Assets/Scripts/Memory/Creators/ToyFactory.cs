using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyFactory
{
    private Dictionary<MemoryObjects, Toy> _toys;
    private Dictionary<MemoryObjects, Toy> _buildedToys;
    private Transform _hiddenParent;
    private Transform _shownParent;
    private Cross _cross;
    private List<Cross> _crosses;

    public ToyFactory(List<Toy> toys, Cross cross, Transform hiddenParent, Transform shownParent)
    {
        _toys = new Dictionary<MemoryObjects, Toy>();
        _buildedToys = new Dictionary<MemoryObjects, Toy>();
        _cross = cross;
        _crosses = new List<Cross>();
        _hiddenParent = hiddenParent;
        _shownParent = shownParent;

        foreach(Toy currentToy in toys)
        {
            if (_toys.ContainsKey(currentToy.type))
                Debug.LogWarning("DUPLICATED TOY TYPE: " + currentToy.type + " FROM: " + currentToy.name);
            else
                _toys.Add(currentToy.type, currentToy);
        }
    }

    public Toy CreateToy(MemoryObjects type, Vector3 position)
    {
        Vector2 widthAndHeight = GameVars.BoxWidthAndHeight;
        Toy temporalToy;
        if (_toys.ContainsKey(type))
        {
            if (!_buildedToys.ContainsKey(type))
            {
                temporalToy = _toys[type].Clone(position, widthAndHeight, _hiddenParent, _shownParent);
                _buildedToys.Add(type, temporalToy);
            }
            else
            {
                Debug.LogWarning("DUPLICATED TOY: " + type.ToString());
                temporalToy = _toys[type].Clone(position, widthAndHeight, _hiddenParent, _shownParent);
            }
        }
        else
        {
            Debug.LogWarning("UNDEFINED TOY: " + type.ToString());
            temporalToy = _toys[0].Clone(position, widthAndHeight, _hiddenParent, _shownParent);
        }
        return temporalToy;
    }

    public Toy GetToy(MemoryObjects type)
    {
        if (_buildedToys.ContainsKey(type))
            return _buildedToys[type];
        else
        {
            Debug.LogWarning("TOY NOT BUILDED: " + type.ToString());
            return _buildedToys[0];
        }
    }

    public Cross CreateCross(Vector3 position)
    {
        Vector2 widthAndHeight = GameVars.BoxWidthAndHeight;
        Cross temporalCross = _cross.Clone(position, widthAndHeight, _shownParent);
        _crosses.Add(temporalCross);
        return temporalCross;

    }

    public List<Toy> GetAllToys()
    {
        return new List<Toy>(_buildedToys.Values);
    }

    public void AnimateAllToys()
    {
        List<Toy> toys = GetAllToys();
        foreach (Toy currentToy in toys)
        {
            currentToy.AnimateHide();
        }
    }

    public void ClearToys()
    {
        foreach (MemoryObjects type in _buildedToys.Keys)
        {
            _buildedToys[type].Destroy();
        }

        _buildedToys.Clear();
    }

    public void ClearCrosses()
    {
        for (int i = _crosses.Count - 1; i >= 0; i --)
        {
            _crosses[i].Destroy();
        }
        _crosses.Clear();
    }
}
