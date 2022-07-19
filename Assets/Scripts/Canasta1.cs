using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canasta1 : MonoBehaviour
{
    public GameObject GameManager;
    gameManager script;
    int nivel;
    Vector2 posCanasta;

    private Colores color;
    private Formas forma;
    private Tamanos tamano;
    private Cantidad cantidad;

    // Start is called before the first frame update
    void Start()
    {
        script = GameManager.GetComponent<gameManager>();
    }


    public void EstaEnCanasta1(Collider2D collision)
    {
        color = collision.gameObject.GetComponent<Carta>().color;
        forma = collision.gameObject.GetComponent<Carta>().forma;
        tamano = collision.gameObject.GetComponent<Carta>().tamano;
        cantidad = collision.gameObject.GetComponent<Carta>().cantidad;

        switch (script.nivel)
        {
            case 0: //Forma
                if(forma == Formas.pelota)
                    script.correctos++;
                else
                    script.incorrectos++;

                break;

            case 1: //Color
                if(color == Colores.azul)
                    script.correctos++;
                else
                    script.incorrectos++;

                break;

            case 2:
                if (script.niv == 0) //Color
                {
                    if (color == Colores.azul)
                        script.correctos++;
                    else
                        script.incorrectos++;
                }
                else if (script.niv == 1) //Forma
                {
                    if (forma == Formas.pelota)
                        script.correctos++;
                    else
                        script.incorrectos++;
                }

                break;
            case 3:
                if (script.niv == 0) //Color
                {
                    if (color == Colores.azul)
                        script.correctos++;
                    else
                        script.incorrectos++;
                }
                else if (script.niv == 1) //Forma
                {
                    if (forma == Formas.auto)
                        script.correctos++;
                    else
                        script.incorrectos++;
                }
                break;
            case 4:
                if (script.niv == 0) //Color
                {
                    if (color == Colores.azul)
                        script.correctos++;
                    else
                        script.incorrectos++;
                }
                else if (script.niv == 1) //Forma
                {
                    if (forma == Formas.perro)
                        script.correctos++;
                    else
                        script.incorrectos++;
                }
                else if (script.niv == 2) //???
                {
                    //if (forma == Formas.auto)
                    //    script.correctos++;
                    //else
                    //    script.incorrectos++;
                }
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
        }
        //Debug.Log("Correcto: " + script.correctos);
        //Debug.Log("Incorrecto: " + script.incorrectos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
