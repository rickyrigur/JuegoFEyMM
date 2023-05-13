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

    public UnityEvent OnDistractorAnimationEnds; 
    public UnityEvent OnDistractorAnimationStarts; 

    public void Animate(float duration)
    {
        transform.localScale = Vector3.zero;
        _image.enabled = true;
        OnDistractorAnimationStarts?.Invoke();
        StartCoroutine(AnimateDistractor(Vector3.one));
        StartCoroutine(AnimateDistractor(Vector3.zero, duration));
    }

    IEnumerator AnimateDistractor(Vector3 finalScale, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        Vector3 initialScale = transform.localScale;

        for (float time = 0; time < animationTime; time += Time.deltaTime)
        {
            float progress = Mathf.PingPong(time, animationTime) / animationTime;
            transform.localScale = Vector3.Lerp(initialScale, finalScale, progress);
            yield return null;
        }
        transform.localScale = finalScale;

        if (finalScale == Vector3.zero)
        {
            OnDistractorAnimationEnds?.Invoke();
        }
    }
}
