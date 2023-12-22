using UnityEngine;
using UnityEngine.EventSystems;

public enum Colores { blanco, rojo, amarillo, azul, naranja };
public enum Formas { auto, pelota, flor, perro };
public enum Tamanos { grande, chico };
public enum Cantidad { simple, doble };

public class Carta : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{    
    [SerializeField]
    public Colores color;
    [SerializeField]
    public Formas forma;
    [SerializeField]
    public Tamanos tamano;
    [SerializeField]
    public Cantidad cantidad;

    private bool _draggable = true;
    private RectTransform m_DraggingPlane;
    private gameManager _gameManager => FindObjectOfType<gameManager>();


    private RectTransform _rect => GetComponent<RectTransform>();

    public void DeactivateDrag()
    {
        _draggable = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_draggable)
            return;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_draggable)
        {
            return;
        }

        if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = eventData.pointerEnter.transform as RectTransform;

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            _rect.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_draggable)
            return;

        _gameManager.ValidatePointer(eventData.position, this);
    }
}
