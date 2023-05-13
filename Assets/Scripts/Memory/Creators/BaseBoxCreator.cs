using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBoxCreator : IBoxCreator
{
    protected List<MemoryBox> _boxes = new List<MemoryBox>();

    protected float screenWidth;
    protected float screenHeight;

    public BaseBoxCreator()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    public abstract void CreateBoxes(int amount, MemoryBox box, Transform parent);

    public abstract float GetScaleDifference(Vector3 originalScale, int amount);

    public void HideElements(List<MemoryObjects> objects, ToyFactory toyFactory)
    {
        var tempList = new List<MemoryBox>();
        tempList.AddRange(_boxes);

        foreach (var currentObject in objects)
        {
            if (tempList.Count <= 0)
            {
                break;
            }
            var index = Random.Range(0, tempList.Count);
            MemoryBox box = tempList[index];
            box.HideObject(currentObject);
            box.SetAnimatedObject(toyFactory.CreateToy(currentObject, box.transform.position));
            tempList.RemoveAt(index);
        }

        foreach (var currentBox in tempList)
        {
            currentBox.SetAnimatedObject(toyFactory.CreateCross(currentBox.transform.position));
        }
    }

    public void SetValidator(IValidator validator)
    {
        foreach (MemoryBox currentBox in _boxes)
        {
            currentBox.validator = validator;
        }
    }

    public void CleanBoxes()
    {
        foreach (MemoryBox currentBox in _boxes)
        {
            currentBox.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void DestroyBoxes()
    {
        for (int i = _boxes.Count - 1; i >= 0; i--)
        {
            _boxes[i].Destroy();
        }

        _boxes.Clear();
    }

    protected void SetWidthAndHeight(Vector2 widthAndHeight)
    {
        GameVars.BoxWidthAndHeight = widthAndHeight;
    }
}
