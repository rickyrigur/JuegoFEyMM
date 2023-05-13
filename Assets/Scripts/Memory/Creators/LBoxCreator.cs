using UnityEngine;

public class LBoxCreator : BaseBoxCreator
{
    private static float horizontalSpace = 5;
    private static float verticalSpace = 5;
    private static float verticalOffset = 50;

    public override void CreateBoxes(int amount, MemoryBox box, Transform parent)
    {
        RectTransform rect = box.gameObject.GetComponent<RectTransform>();
        float scaleDifference = GetScaleDifference(new Vector3(rect.rect.width, rect.rect.height, 0), amount);
        Vector2 scale = new Vector2(rect.rect.width * scaleDifference, rect.rect.height * scaleDifference);
        SetWidthAndHeight(scale);
        float initialY = - screenHeight / 2 + verticalOffset;
        float initialX = - screenWidth / 2;
        float currentX = initialX;
        float currentY = initialY;
        bool isHorizontal = true;
        Vector3 position;
        for (int i = 0; i < amount; i++)
        {
            if (isHorizontal)
            {
                position = new Vector3(currentX + scale.x / 2, initialY + scale.y / 2, 0);
                currentY += scale.y + verticalSpace;
            }
            else
            {
                position = new Vector3(initialX + scale.x / 2, currentY + scale.y / 2, 0);
                currentX += scale.x + horizontalSpace;
            }
            isHorizontal = !isHorizontal;
            var newBox = box.Clone(position, Quaternion.identity, parent);
            RectTransform newBoxRect = newBox.gameObject.GetComponent<RectTransform>();
            newBoxRect.sizeDelta = scale;
            _boxes.Add(newBox);
        }
    }

    public override float GetScaleDifference(Vector3 originalScale, int amount)
    {
        if (amount % 2 == 0)
            amount /= 2;
        else
            amount = (amount + 1) / 2;
        float desiredScale = (screenWidth - horizontalSpace * (amount - 1)) / amount;
        return desiredScale / originalScale.x;
    }
}
