using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MemoryBox : MonoBehaviour
{
    public MemoryObjects objectHided;
    public bool hasElement;
    public UnityEvent OnSelect;
    public UnityEvent<bool> OnAnimationEnds;
    public UnityEvent OnNoMoreObjects;
    public IValidator validator;
    public ToyFactory toyFactory;

    [SerializeField]
    private bool interactable;
    private Button _button => GetComponent<Button>();

    private IAnimatedObject _animatedObject;

    public MemoryBox Clone()
    {
        return Clone(transform);
    }

    public MemoryBox Clone(Transform transform)
    {
        return Clone(transform.position, transform.rotation, null);
    }

    public MemoryBox Clone(Vector3 position, Quaternion rotation, Transform parent)
    {
        MemoryBox box = Instantiate(this, position, rotation, parent);
        box.transform.position = parent.TransformPoint(position);
        return box;
    }

    public void SetAnimatedObject(IAnimatedObject animObject)
    {
        _animatedObject = animObject;
    }

    public void HideObject(MemoryObjects memoryObject)
    {
        hasElement = true;
        objectHided = memoryObject;
    }

    public void Select()
    {
        if (!interactable)
            return;

        OnSelect?.Invoke();
        _animatedObject.Animate(() => {
            bool result = validator.Validate(this, OnNoMoreObjects);
            OnAnimationEnds?.Invoke(result);
        });
    }
     
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void BlockButton()
    {
        interactable = false;
    }

    public void EnableButton()
    {
        if (!hasElement || _button.interactable)
        {
            _button.interactable = true;
            interactable = true;
        }
    }
}
