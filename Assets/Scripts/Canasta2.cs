using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canasta2 : MonoBehaviour
{
    public GameObject GameManager;
    gameManager script;
    int nivel;
    private Colores color;
    private Formas forma;
    private Tamanos tamano;
    private Cantidad cantidad;
    private GameObject cartaMuestra;
    private string criteriosComparacion;

    // Start is called before the first frame update
    void Start()
    {
        script = GameManager.GetComponent<gameManager>();
        Debug.Log(script.nivel);
    }
    public void CargarCartaMuestra(GameObject carta)
    {
        cartaMuestra = carta;
    }

    public void CargarCriterioComparacion(string criterio)
    {
        criteriosComparacion = criterio;
    }

    public void EstaEnCanasta2(Collider2D collision)
    {
        color = collision.gameObject.GetComponent<Carta>().color;
        forma = collision.gameObject.GetComponent<Carta>().forma;
        tamano = collision.gameObject.GetComponent<Carta>().tamano;
        cantidad = collision.gameObject.GetComponent<Carta>().cantidad;

        Debug.Log("Criterio comparacion: " + criteriosComparacion);
        Debug.Log("Carta muestra:" + cartaMuestra.GetComponent<Carta>().forma);
        Debug.Log("Carta en canasta: " + forma);

        switch (criteriosComparacion)
        {
            case "F": //Forma
                if (cartaMuestra.GetComponent<Carta>().forma == forma)
                    script.correctos++;
                else
                    script.incorrectos++;

                break;
            case "C": //Color
                if (cartaMuestra.GetComponent<Carta>().color == color)
                    script.correctos++;
                else
                    script.incorrectos++;

                break;
            case "T": //Tamano
                if (cartaMuestra.GetComponent<Carta>().tamano == tamano)
                    script.correctos++;
                else
                    script.incorrectos++;

                break;
            case "CO": //Color opuesto
                if (cartaMuestra.GetComponent<Carta>().color != color)
                    script.correctos++;
                else
                    script.incorrectos++;

                break;
            case "TO": //Tamano opuesto
                if (cartaMuestra.GetComponent<Carta>().tamano != tamano)
                    script.correctos++;
                else
                    script.incorrectos++;

                break;
        }
        //Debug.Log("Correcto: " + script.correctos);
        //Debug.Log("Incorrecto: " + script.incorrectos);
    }

    

}
