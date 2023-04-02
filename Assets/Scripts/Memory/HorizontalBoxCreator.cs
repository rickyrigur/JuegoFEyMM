using System.Collections.Generic;
using UnityEngine;

public class HorizontalBoxCreator : BaseBoxCreator
{
    public override void CreateBoxes(int amount, MemoryBox box)
    {
        float scale = box.transform.localScale.x;
        float x = 0;
        float space = 2;
        float y = screenHeight / 2;
        for (int i = 0; i < amount; i++)
        {
            var newBox = box.Clone(new Vector3(x + (scale + space) * i, y, 0), Quaternion.identity);
            _boxes.Add(newBox);
        }
    }
}
