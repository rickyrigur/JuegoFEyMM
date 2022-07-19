using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colores { blanco, rojo, amarillo, azul, naranja };
public enum Formas { auto, pelota, flor, perro };
public enum Tamanos { grande, chico };
public enum Cantidad { simple, doble };

public class Carta : MonoBehaviour
{    

    [SerializeField]
    public Colores color;
    [SerializeField]
    public Formas forma;
    [SerializeField]
    public Tamanos tamano;
    [SerializeField]
    public Cantidad cantidad;
        
}
