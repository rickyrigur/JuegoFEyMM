using System.Collections.Generic;
using UnityEngine;

public class BoxCreatorFactory
{
    private MemoryBox _box;
    private Transform _container;
    private IBoxCreator _currentCreator;

    public BoxCreatorFactory(MemoryBox boxPrefab, Transform parent)
    {
        _box = boxPrefab;
        _container = parent;
    }

    public void CreateBoxesAndToys(MemoryTestSO test, List<MemoryObjects> objectsToUse, IValidator validator, ToyFactory toyFactory)
    {
        if (_currentCreator != null)
            _currentCreator.DestroyBoxes();

        _currentCreator = GetBoxCreator(test);
        _currentCreator.CreateBoxes(test.boxesAmount, _box, _container);
        _currentCreator.SetValidator(validator);
        _currentCreator.HideElements(objectsToUse, toyFactory);
    }

    private IBoxCreator GetBoxCreator(MemoryTestSO test)
    {
        switch (test.position)
        {
            case Positions.DIAGONAL:
                if (test.boxesAmount > 4)
                    return new DobleDiagonalBoxCreator();
                else
                    return new DiagonalBoxCreator();
            case Positions.HORIZONTAL:
                if (test.boxesAmount > 4)
                    return new DobleHorizontalBoxCreator();
                else
                    return new HorizontalBoxCreator();
            case Positions.L:
                return new LBoxCreator();
            default:
                Debug.LogWarning("WRONG BUILD POSITION: " + test.position);
                return new HorizontalBoxCreator();
        }
    }

    public void CleanBoxes()
    {
        _currentCreator.CleanBoxes();
    }
}
