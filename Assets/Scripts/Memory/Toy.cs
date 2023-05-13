using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour, IAnimatedObject
{
    public float animationTime;
    public MemoryObjects type;

    public Vector3 _originalScale;
    public Vector3 _originalPosition;
    public Vector3 _middlePosition;
    private float _height;

    private Transform _hiddenParent;
    private Transform _shownParent;

    public Toy Clone(Vector3 position, Vector2 widthAndHeight, Transform hided, Transform shown)
    {
        Toy toy = Instantiate(this, position, Quaternion.identity);
        toy.SetParameters(hided, shown, widthAndHeight);
        return toy;
    }

    private void SetParameters(Transform hidden, Transform shown, Vector2 widthAndHeight)
    {
        _originalScale = transform.localScale;
        _originalPosition = transform.position;

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
        _height = rectT.rect.height;

        _middlePosition = transform.position + new Vector3(0, _height / 2, 0);
        _hiddenParent = hidden;
        _shownParent = shown;
        transform.SetParent(_shownParent);
    }

    public void AnimateHide(IAnimatedObject.onAnimationEnd callback = null)
    {
        StartCoroutine(Animate(_originalScale, _middlePosition, animationTime / 2, null, 0));
        StartCoroutine(Animate(Vector3.zero, _originalPosition, animationTime / 2, _hiddenParent, animationTime / 2, callback));
    }

    public void AnimateShow(IAnimatedObject.onAnimationEnd callback = null)
    {
        StartCoroutine(Animate(_originalScale, _middlePosition, animationTime / 2, null, 0));
        StartCoroutine(Animate(_originalScale, _originalPosition, animationTime / 2, _shownParent, animationTime / 2, callback));
    }

    IEnumerator Animate(Vector3 finalScale, Vector3 finalPosition, float duration, Transform newParent, float delay, IAnimatedObject.onAnimationEnd callback = null)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);
        
        Vector3 initialScale = transform.localScale;
        Vector3 initialPosition = transform.position;

        if (newParent != null)
            transform.SetParent(newParent);

        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            float progress = Mathf.PingPong(time, duration) / duration;
            transform.localScale = Vector3.Lerp(initialScale, finalScale, progress);
            transform.position = Vector3.Lerp(initialPosition, finalPosition, progress);
            yield return null;
        }
        transform.position = finalPosition;
        transform.localScale = finalScale;

        if (callback != null)
        {
            yield return new WaitForSeconds(0.5f);
            callback();
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Animate(IAnimatedObject.onAnimationEnd callback = null)
    {
        AnimateShow(callback);
    }
}
