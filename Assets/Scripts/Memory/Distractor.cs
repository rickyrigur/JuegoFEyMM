using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class Distractor : MonoBehaviour
{
    public float animationTime = 0.5f;
    private Image _image => GetComponent<Image>();
    private Animator _anim => GetComponent<Animator>();
    private float _distractionTime;

    public UnityEvent OnDistractorAnimationEnds; 
    public UnityEvent OnDistractorAnimationStarts;

    public void Animate(float duration)
    {
        _distractionTime = duration;
        transform.localScale = Vector3.zero;
        _image.enabled = true;
        OnDistractorAnimationStarts?.Invoke();
        _anim.SetTrigger("Animate");
    }

    public void OnAnimateInEnds() 
    {
        StartCoroutine(WaitForSeconds());
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(_distractionTime);
        _anim.SetTrigger("Animate");     
    }

    public void onAnimateOutEnds() 
    {
        GameVars.DistractorShowed = true;
        OnDistractorAnimationEnds?.Invoke();
    }
}
