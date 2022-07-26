using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gameManager : MonoBehaviour
{
    decimal tiempoJuego;
    decimal tiempoInactivo;

    //[HideInInspector]
    public int correctos;
    //[HideInInspector]
    public int incorrectos;
    private string respuestaCorrecta;

    public Text textoCorrectos;
    public Text textoIncorrectos;

    public GameObject[] cartas;
    public GameObject canasta1;
    public GameObject canasta2;
    Transform carta;

    //[HideInInspector]
    public int nivel = 0;
    public int niv = 0;
    private int[] numNiveles;
    private int contSubNiveles;

    private List<string> prueba;

    private List<string> nivel1;
    private List<string> nivel2;
    private List<string> nivel3;
    private List<string> nivel4;
    private List<string> nivel5;
    private List<string> nivel6;
    private List<string> nivel7;
    private List<string> nivel8;

    private List<string> subNivel4;
    private List<string> subNivel5;
    private List<string> subNivel6;
    private List<string> subNivel7;
    private List<string> subNivel8;

    private List<List<string>> listaNiveles;
    private List<List<string>> listaSubNiveles;
    

    private Vector3 screenPoint;
    private Vector3 offset;
    private List<int> posUtilizadas = new List<int>();
    private bool esRandom;
    private Vector3 Pos1;

    private Vector3 PosCanasta1;
    private Vector3 PosCanasta2;

    private List<int> indiceCartas;
    private GameObject carta1;
    private GameObject carta2;
    private GameObject carta3;
    private GameObject carta4;
    private GameObject carta5;
    private GameObject carta6;
    private GameObject carta7;
    private GameObject carta8;
    private GameObject carta9;

    private List<GameObject> poolGameobjectCartas;

    private GameObject cartaMuestra1;
    private GameObject cartaMuestra3;

    // Start is called before the first frame update
    void Start()
    {
        //prueba.AddRange(subNivel4);
        numNiveles = new[] { 0, 1, 2, 2, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 8, 8 };

        prueba = new List<string> { "F" };
        nivel1 = new List<string> { "C" };
        nivel2 = new List<string> { "C", "F" };
        nivel3 = new List<string> { "C", "F" };
        nivel4 = new List<string> { "C", "F" };
        nivel5 = new List<string> { "F", "C" };
        nivel6 = new List<string> { "T", "C" };
        nivel7 = new List<string> { "C", "CO" };
        nivel8 = new List<string> { "T", "TO" };

        subNivel4 = new List<string> { "F", "F", "F", "F", "C", "C" };
        subNivel5 = new List<string> { "C", "C", "C", "C", "F", "F" };
        subNivel6 = new List<string> { "T", "T", "T", "T", "F", "F", "C", "C" };
        subNivel7 = new List<string> { "CO", "CO", "CO", "CO", "CO", "CO", "C", "C", "C" };
        subNivel8 = new List<string> { "TO", "TO", "TO", "TO", "TO", "TO", "T", "T", "T" };

        listaNiveles = new List<List<string>> { prueba, nivel1, nivel2, nivel3, nivel4, nivel5, nivel6, nivel7, nivel8 };
        listaSubNiveles = new List<List<string>> { subNivel4, subNivel5, subNivel6, subNivel7, subNivel8 };
        poolGameobjectCartas = new List<GameObject> { carta1, carta2, carta3, carta4, carta5, carta6, carta7, carta8, carta9 };

        nivel = 0;
        niv = 0;
        correctos = 0;
        incorrectos = 0;
        
        Pos1 = new Vector3(-0, -13, 0);

        PosCanasta1 = new Vector3(-26, -0.5f, 0);
        PosCanasta2 = new Vector3(26, -0.5f, 0);
                
        Niveles(listaNiveles[nivel]);
    }

    // Update is called once per frame
    void Update()
    {
        textoCorrectos.text = "CORRECTO: " + correctos.ToString();
        textoIncorrectos.text = "INCORRECTOS: " + incorrectos.ToString();


        if(Input.GetMouseButtonDown(0))
        {
            carta = null;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag != "Canasta" && hit.collider.gameObject.tag != "Muestra")
            {
                carta = hit.collider.gameObject.GetComponent<Transform>();
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(MousePosition);
            if (carta != null)
            {
                carta.transform.position = objPosition;
            }
            
                
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null && hit.collider.gameObject.tag != "Canasta" && hit.collider.gameObject.tag != "Muestra" && carta != null)
            {
                if (hit.collider.OverlapPoint((Vector2)canasta1.transform.position))
                    canasta1.GetComponent<Canasta1>().EstaEnCanasta1(carta.GetComponent<Collider2D>());
                else if(hit.collider.OverlapPoint((Vector2)canasta2.transform.position))
                    canasta2.GetComponent<Canasta2>().EstaEnCanasta2(carta.GetComponent<Collider2D>());
            }

        }
    }

    public void SiguienteNivel()
    {

        //Debug.Log("nivel: " + nivel + " niv: " + niv);

        Destruir();

        if (nivel > 3 && niv == 0)
        {
            listaNiveles[nivel].AddRange(RandomizarEnsayos(listaSubNiveles[niv])); //Se agrega de forma random los subniveles al nivel
        }

        Niveles(listaNiveles[nivel]);



        //contador++;
        //nivel = numNiveles[contador];

        //if (nivel > 1 && nivel == numNiveles[contador - 1])
        //{
        //    niv++;
        //    esRandom = true;
        //}
        //if (nivel != numNiveles[contador - 1])
        //{
        //    correctos = 0;
        //    incorrectos = 0;
        //    niv = 0;
        //    esRandom = false;
        //}

        //Debug.Log("nivel: " + nivel + " niv: " + niv);

        //Destruir();
        //Niveles();
    }

    private void Destruir()
    {
        indiceCartas = new List<int>();

        foreach(GameObject carta in poolGameobjectCartas)
        {
            GameObject.Destroy(carta);
        }

        GameObject.Destroy(cartaMuestra1);
        GameObject.Destroy(cartaMuestra3);
        if (nivel >= 3)
        {
            GameObject.Destroy(carta5);
            GameObject.Destroy(carta6);
        }
        if (nivel >= 7)
        {
            GameObject.Destroy(carta7);
            GameObject.Destroy(carta8);
            GameObject.Destroy(carta9);
        }
        
    }
    private void Niveles(List<string> listaNivel)
    {
        switch (nivel) //Ensayos
        {
            case 0: //48 Pelota Azul - 68 Perro Azul
                Debug.Log("Cantidad subNiveles: " + listaNivel.Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                indiceCartas = new List<int> { 48, 68, 48, 68};
                Ensayos4Cartas(indiceCartas, 48, 68);
                //if (niv == listaNivel.Count - 1)
                //{
                //    nivel++;
                //    niv = 0;
                //}       
                break;

            case 1: //48 Pelota Azul - 64 Perro Rojo
                Debug.Log("Cantidad subNiveles: " + listaNivel.Count);
                Debug.Log("nivel: " + nivel +" - Subnivel: " + niv);

                indiceCartas = new List<int> { 48, 64, 48, 64 };
                
                Ensayos4Cartas(indiceCartas, 48, 64);
                //if (niv == listaNivel.Count - 1)
                //{
                //    nivel++;
                //    niv = 0;
                //}
                break;

            case 2: // 44 Pelota Roja - 68 Perro Azul - 48 Pelota Azul - 64 Perro Rojo
                Debug.Log("Cantidad subNiveles: " + listaNivel.Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                indiceCartas = new List<int> { 44, 68, 48, 64 };
                
                Ensayos4Cartas(indiceCartas, 48, 64);
                //if (niv == listaNivel.Count - 1)
                //{
                //    nivel++;
                //    niv = 0;
                //}
                break;

            case 3: //4 Auto Rojo - 28 Flor Azul - 8 Auto Azul - 24 Flor Roja
                Debug.Log("Cantidad subNiveles: " + listaNivel.Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                indiceCartas = new List<int> { 4, 28, 4, 28, 4, 28 };
                Ensayos4Cartas(indiceCartas, 8, 24);
                //Ensayos6Cartas(indiceCartas, 8, 24);
                //if (niv == listaNivel.Count - 1)
                //{
                //    nivel++;
                //    niv = 0;
                //}
                break;

            case 4: // 64 Perro Rojo - 48 Pelota Azul - 68 Perro Azul - 44 Pelota Roja
                Debug.Log("Cantidad subNiveles: " + listaNivel.Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                indiceCartas = new List<int> { 64, 48, 64, 48, 64, 48 };
                Ensayos4Cartas(indiceCartas, 68, 44);
                //Ensayos6Cartas(indiceCartas, 68, 44);
                //if (niv == listaNivel.Count - 1)
                //{
                //    nivel++;
                //    niv = 0;
                //}
                break;

            case 5: // 24 Flor Roja - 8 Auto Azul - 28 Flor Azul - 4 Auto Rojo
                Debug.Log("Cantidad subNiveles: " + listaNivel.Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                indiceCartas = new List<int> { 24, 8, 24, 8, 24, 8 };
                Ensayos4Cartas(indiceCartas, 28, 4);
                //Ensayos6Cartas(indiceCartas, 28, 4);
                //if (niv == listaNivel.Count - 1)
                //{
                //    nivel++;
                //    niv = 0;
                //}
                break;

            case 6: // 76 Perro Naranja - 53 Pelota Amarilla Chica - 77 Perro Naranja chico - 52 Pelota Amarilla
                    // 72 Perro Amarillo - 57 Pelota Naranja Chica -  Perro Naranja chico -  Pelota Amarilla 
                Debug.Log("Cantidad subNiveles: " + listaNivel.Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                indiceCartas = new List<int> { 76, 53, 77, 52, 72, 57 };
                Ensayos4Cartas(indiceCartas, 73, 56);
                //Ensayos6Cartas(indiceCartas, 73, 56); //modificar cartas de muestra
                //if (niv == listaNivel.Count - 1)
                //{
                //    nivel++;
                //    niv = 0;
                //}
                break;

            case 7: // 16 Auto Naranja - 13 Auto Amarillo chico - 36 Flor Naranja - 33 Flor Amarilla chica
                    // 12 Auto Amarillo - 17 Auto Naranja chico - 32 Flor Amarilla - 37 Flor Naranja chica 
                    // 33 Flor Amarilla chica - 37 Flor Naranja chica - 12 Auto Amarillo
                Debug.Log("Cantidad subNiveles: " + listaNivel.Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                indiceCartas = new List<int> { 16, 13, 36, 33, 12, 17, 32, 37, 33 };
                Ensayos4Cartas(indiceCartas, 37, 12);
                //Ensayos9Cartas(indiceCartas, 37, 12);
                //if (niv == listaNivel.Count - 1)
                //{
                //    nivel++;
                //    niv = 0;
                //}
                break;

            case 8:

                indiceCartas = new List<int> { };
                break;

            case 9:
                break;
        }

        if (niv == listaNivel.Count - 1)
        {
            nivel++;
            niv = 0;
        }
        else
            niv++;
    }

    List<string> RandomizarEnsayos (List<string> subNivel)
    {

        System.Random rnd = new System.Random();
        subNivel = subNivel.OrderBy(item => rnd.Next()).ToList<string>();

        return subNivel;
    }

     

    private void Ensayos4Cartas(List<int> indiceCarta, int cartM1, int cartM2)
    {
        for (int i = 0; i < indiceCarta.Count; i++)
        {
            poolGameobjectCartas[i] = Instantiate(cartas[indiceCarta[i]], Pos1, transform.rotation);
        }
        //carta1 = Instantiate(cartas[cart1], Pos1, transform.rotation); 
        //carta2 = Instantiate(cartas[cart2], Pos1, transform.rotation);
        //carta3 = Instantiate(cartas[cart3], Pos1, transform.rotation);
        //carta4 = Instantiate(cartas[cart4], Pos1, transform.rotation);
        cartaMuestra1 = Instantiate(cartas[cartM1], PosCanasta1, transform.rotation);
        cartaMuestra3 = Instantiate(cartas[cartM2], PosCanasta2, transform.rotation);
        cartaMuestra1.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra3.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra1.tag = "Muestra";
        cartaMuestra3.tag = "Muestra";
    }

    //private void Ensayos6Cartas(List<int> indiceCarta, int cartM1, int cartM2)
    //{

    //    carta1 = Instantiate(cartas[cart1], Pos1, transform.rotation);
    //    carta2 = Instantiate(cartas[cart2], Pos1, transform.rotation);
    //    carta3 = Instantiate(cartas[cart3], Pos1, transform.rotation);
    //    carta4 = Instantiate(cartas[cart4], Pos1, transform.rotation);
    //    carta5 = Instantiate(cartas[cart5], Pos1, transform.rotation);
    //    carta6 = Instantiate(cartas[cart6], Pos1, transform.rotation);
    //    cartaMuestra1 = Instantiate(cartas[cartM1], PosCanasta1, transform.rotation);
    //    cartaMuestra3 = Instantiate(cartas[cartM2], PosCanasta2, transform.rotation);
    //    cartaMuestra1.transform.localScale = new Vector3(0.3f, 0.3f, 0);
    //    cartaMuestra3.transform.localScale = new Vector3(0.3f, 0.3f, 0);
    //    cartaMuestra1.tag = "Muestra";
    //    cartaMuestra3.tag = "Muestra";
    //}

    //private void Ensayos9Cartas(List<int> indiceCarta, int cartM1, int cartM2)
    //{
        

    //    carta1 = Instantiate(cartas[cart1], Pos1, transform.rotation);
    //    carta2 = Instantiate(cartas[cart2], Pos1, transform.rotation);
    //    carta3 = Instantiate(cartas[cart3], Pos1, transform.rotation);
    //    carta4 = Instantiate(cartas[cart4], Pos1, transform.rotation);
    //    carta5 = Instantiate(cartas[cart5], Pos1, transform.rotation);
    //    carta6 = Instantiate(cartas[cart6], Pos1, transform.rotation);
    //    carta7 = Instantiate(cartas[cart7], Pos1, transform.rotation);
    //    carta8 = Instantiate(cartas[cart8], Pos1, transform.rotation);
    //    carta9 = Instantiate(cartas[cart9], Pos1, transform.rotation);
    //    cartaMuestra1 = Instantiate(cartas[cartM1], PosCanasta1, transform.rotation);
    //    cartaMuestra3 = Instantiate(cartas[cartM2], PosCanasta2, transform.rotation);
    //    cartaMuestra1.transform.localScale = new Vector3(0.3f, 0.3f, 0);
    //    cartaMuestra3.transform.localScale = new Vector3(0.3f, 0.3f, 0);
    //    cartaMuestra1.tag = "Muestra";
    //    cartaMuestra3.tag = "Muestra";
    //}


    private void OnMouseDown()
    {
        //offset = cartas.GameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
    }
   
    //private void OnMouseDrag()
    //{
    //    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    //    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    //    transform.position = curPosition;
    //}
}
