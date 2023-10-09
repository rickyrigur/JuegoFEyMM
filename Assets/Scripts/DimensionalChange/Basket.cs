
using UnityEngine;

public class Basket : MonoBehaviour
{
    private Carta _sampleCard;
    private TestCriteria _testCriteria;

    private bool _animating;
    private float _currentAnimationTime;
    private float _duration;
    private Vector3 _initialScale;
    private Vector3 _endScale;

    private void Update()
    {
        if (_animating)
        {
            if (_currentAnimationTime >= _duration)
            {
                _animating = false;
                transform.localScale = _initialScale;
            }
            else
            {
                _currentAnimationTime += Time.deltaTime;
                float progress = Mathf.PingPong(_currentAnimationTime, 1f);
                transform.localScale = Vector3.Lerp(_initialScale, _endScale, progress);
            }
        }
    }

    public void LoadSampleCard(Carta carta)
    {
        _sampleCard = carta;
    }

    public void LoadCriteria(TestCriteria criterio)
    {
        _testCriteria = criterio;
    }

    public bool Validate(Carta card)
    {
        switch (_testCriteria)
        {
            case TestCriteria.FORMA:
                return _sampleCard.forma == card.forma;

            case TestCriteria.COLOR:
                return _sampleCard.color == card.color;

            case TestCriteria.TAMANO:
                return _sampleCard.tamano == card.tamano;

            case TestCriteria.COLOR_OPUESTO: //Color opuesto
                return _sampleCard.color != card.color;

            case TestCriteria.TAMANO_OPUESTO: //Tamano opuesto
                return _sampleCard.tamano != card.tamano;

            default:
                Debug.LogWarning("CRITERIO NO VALIDO: " + _testCriteria);
                return false;
        }
    }

    public void AnimateScale(Vector3 scale, float duration)
    {
        if (_animating)
            return;

        _animating = true;
        _currentAnimationTime = 0;
        _duration = duration;
        _initialScale = transform.localScale;
        _endScale = scale;
    }

}
