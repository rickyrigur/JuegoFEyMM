using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public abstract void CreateBoxes(int amount, MemoryBox box);

    public void HideElements(List<MemoryObjects> objects)
    {
        var tempList = new List<MemoryBox>();
        tempList.AddRange(_boxes);

        foreach (var currentObject in objects)
        {
            if (tempList.Count <= 0)
            {
                Debug.LogWarning("THERE ARE MORE BOXES THAN OBJECTS");
                break;
            }
            var index = Random.Range(0, tempList.Count);
            tempList[index].HideObject(currentObject);
            tempList.RemoveAt(index);
        }
    }
}
