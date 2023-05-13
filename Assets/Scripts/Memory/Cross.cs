using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cross : MonoBehaviour, IAnimatedObject
{
    public float animationTime;
    public float stayDuration;
    private Image _image => GetComponent<Image>();

    public Cross Clone(Vector3 position, Vector2 widthAndHeight, Transform parent)
    {
        Cross cross = Instantiate(this, position, Quaternion.identity);
        cross.SetParameters(widthAndHeight, parent);
        return cross;
    }

    private void SetParameters(Vector2 widthAndHeight, Transform parent)
    {
        RectTransform rectT = gameObject.GetComponent<RectTransform>();
        float widthHeightRelation = rectT.rect.width / rectT.rect.height;
        Vector2 newSize = new Vector2();

        if (rectT.rect.width > rectT.rect.height)
        {
            newSize.x = widthAndHeight.x;
            newSize.y = widthAndHeight.x * (1 / widthHeightRelation);
        }
        else
        {
            newSize.y = widthAndHeight.y;
            newSize.x = widthAndHeight.y * widthHeightRelation;
        }
        rectT.sizeDelta = newSize;
        transform.SetParent(parent);
    }

    private void OnEnable()
    {
        _image.enabled = false;
    }

    public void SetSize()
    {
        Vector2 widthAndHeight = GameVars.BoxWidthAndHeight;
        RectTransform rectT = gameObject.GetComponent<RectTransform>();
        float widthHeightRelation = rectT.rect.width / rectT.rect.height;
        Vector2 newSize = new Vector2();

        if (rectT.rect.width > rectT.rect.height)
        {
            newSize.x = widthAndHeight.x;
            newSize.y = widthAndHeight.x * (1 / widthHeightRelation);
        }
        else
        {
            newSize.y = widthAndHeight.y;
            newSize.x = widthAndHeight.y * widthHeightRelation;
        }
        rectT.sizeDelta = newSize;
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    IEnumerator AnimateSize(IAnimatedObject.onAnimationEnd callback)
    {
        for (float time = 0; time < animationTime; time += Time.deltaTime)
        {
            float progress = Mathf.PingPong(time, animationTime) / animationTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);
            yield return null;
        }
        transform.localScale = Vector3.one;

        yield return new WaitForSeconds(stayDuration);

        for (float time = 0; time < animationTime; time += Time.deltaTime)
        {
            float progress = Mathf.PingPong(time, animationTime) / animationTime;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, progress);
            yield return null;
        }
        transform.localScale = Vector3.zero;
        _image.enabled = false;

        if (callback != null)
        {
            yield return new WaitForSeconds(0.5f);
            callback();
        }
    }

    public void Animate(IAnimatedObject.onAnimationEnd callback = null)
    {
        transform.localScale = Vector3.zero;
        _image.enabled = true;
        StartCoroutine(AnimateSize(callback));
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
