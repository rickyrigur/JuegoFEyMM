
using UnityEngine;

public class Basket : MonoBehaviour
{
    private Carta _sampleCard;
    private TestCriteria _testCriteria;

    private Animator _anim => GetComponent<Animator>();

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

    public void AnimateScale()
    {
        _anim.SetTrigger("Animate");
    }

}
