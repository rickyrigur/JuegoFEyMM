using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cross : MonoBehaviour, IAnimatedObject
{
    public float animationTime;
    public float stayDuration;

    private IAnimatedObject.onAnimationEnd _onAnimationEnd;
    private Image _image => GetComponent<Image>();
    private Animator _animator => GetComponent<Animator>();

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

    private void Start()
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

    public void OnCrossAnimateEnd()
    {
        if (_onAnimationEnd != null)
        {
            _onAnimationEnd();
        }
    }

    public void Animate(IAnimatedObject.onAnimationEnd callback = null)
    {
        transform.localScale = Vector3.zero;
        _image.enabled = true;
        _onAnimationEnd = callback;
        _animator.SetTrigger("Animate");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
