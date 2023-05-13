using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DobleDiagonalBoxCreator : BaseBoxCreator
{
    private static float horizontalSpace = 5;
    private static float verticalSpace = 5;
    private static float verticalOffset = 20;
    private static float spaceBetweenRows = 5;

    public override void CreateBoxes(int amount, MemoryBox box, Transform parent)
    {
        amount /= 2;
        RectTransform rect = box.gameObject.GetComponent<RectTransform>();
        float scaleDifference = GetScaleDifference(new Vector3(rect.rect.width, rect.rect.height, 0), amount);
        Vector2 scale = new Vector2(rect.rect.width * scaleDifference, rect.rect.height * scaleDifference);
        SetWidthAndHeight(scale);
        float y = (scale.y * amount + verticalSpace * (amount - 1)) / 2 - verticalOffset;
        CreateDiagonal(y, amount, scale, box, parent);
        CreateDiagonal(y - scale .y - spaceBetweenRows, amount, scale, box, parent);
    }

    public override float GetScaleDifference(Vector3 originalScale, int amount)
    {
        float desiredScale = (screenWidth - horizontalSpace * (amount - 1)) / amount;
        return desiredScale / originalScale.x;
    }

    private void CreateDiagonal(float initialY, int amount, Vector2 scale, MemoryBox box, Transform parent)
    {
        float y = initialY;
        float x = -screenWidth / 2;
        Vector3 position;
        for (int i = 0; i < amount; i++)
        {
            position = new Vector3(x + (scale.x / 2), y, 0);
            x += scale.x + horizontalSpace;
            y -= scale.y + verticalSpace;
            var newBox = box.Clone(position, Quaternion.identity, parent);
            RectTransform newBoxRect = newBox.gameObject.GetComponent<RectTransform>();
            newBoxRect.sizeDelta = scale;
            _boxes.Add(newBox);
        }
    }
}
