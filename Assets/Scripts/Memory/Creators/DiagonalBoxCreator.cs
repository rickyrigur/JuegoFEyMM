using UnityEngine;

public class DiagonalBoxCreator : BaseBoxCreator
{
    private static float horizontalSpace = 5;
    private static float verticalSpace = 5;
    private static float verticalOffset = 20;

    public override void CreateBoxes(int amount, MemoryBox box, Transform parent)
    {
        RectTransform rect = box.gameObject.GetComponent<RectTransform>();
        float scaleDifference = GetScaleDifference(new Vector3(rect.rect.width, rect.rect.height, 0), amount);
        Vector2 scale = new Vector2(rect.rect.width * scaleDifference, rect.rect.height * scaleDifference);
        SetWidthAndHeight(scale);
        float x = -screenWidth / 2;
        float y = (scale.y * amount + verticalSpace * (amount - 1)) / 2 - verticalOffset;
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

    public override float GetScaleDifference(Vector3 originalScale, int amount)
    {
        float desiredScale = (screenWidth - horizontalSpace * (amount - 1)) / amount;
        return desiredScale / originalScale.x;
    }
}
