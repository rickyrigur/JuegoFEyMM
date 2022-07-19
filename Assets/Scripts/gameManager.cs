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
    private int contador;

    private Vector3 screenPoint;
    private Vector3 offset;
    private List<int> posUtilizadas = new List<int>();
    private bool esRandom;
    private Vector3 Pos1;
    private Vector3 Pos2;
    private Vector3 Pos3;
    private Vector3 Pos4;
    private Vector3 Pos5;
    private Vector3 Pos6;
    private Vector3 Pos7;
    private Vector3 Pos8;
    private Vector3 Pos9;

    private Vector3 PosCanasta1;
    private Vector3 PosCanasta2;

    private GameObject carta1;
    private GameObject carta2;
    private GameObject carta3;
    private GameObject carta4;
    private GameObject carta5;
    private GameObject carta6;
    private GameObject carta7;
    private GameObject carta8;
    private GameObject carta9;

    private GameObject cartaMuestra1;
    private GameObject cartaMuestra3;

    // Start is called before the first frame update
    void Start()
    {
        numNiveles = new[] { 0, 1, 2, 2, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 8, 8 };
        nivel = 0;
        niv = 0;
        contador = 0;
        correctos = 0;
        incorrectos = 0;
        
        Pos1 = new Vector3(-38, -16, 0);
        Pos2 = new Vector3(-22, -16, 0);
        Pos3 = new Vector3(-7, -16, 0);
        Pos4 = new Vector3(8,-16, 0);
        Pos5 = new Vector3(23, -16, 0);
        Pos6 = new Vector3(38, -16, 0);

        PosCanasta1 = new Vector3(-26, -0.5f, 0);
        PosCanasta2 = new Vector3(26, -0.5f, 0);
                
        Niveles();
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
        contador++;
        nivel = numNiveles[contador];

        if (nivel > 1 && nivel == numNiveles[contador - 1])
        {
            niv++;
            esRandom = true;
        }
        if (nivel != numNiveles[contador - 1])
        {
            correctos = 0;
            incorrectos = 0;
            niv = 0;
            esRandom = false;
        }

        Debug.Log("nivel: " + nivel + " niv: " + niv);

        Destruir(nivel);
        Niveles();
    }
    private void Destruir(int nivelGuardado)
    {
        GameObject.Destroy(carta1);
        GameObject.Destroy(carta2);
        GameObject.Destroy(carta3);
        GameObject.Destroy(carta4);
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
    private void Niveles()
    {
        switch (nivel) //Ensayos
        {
            case 0: //48 Pelota Azul - 68 Perro Azul
                Ensayos4Cartas(48, 68, 48, 68, 48, 68); 
                break;

            case 1: //48 Pelota Azul - 64 Perro Rojo
                Ensayos4Cartas(48, 64, 48, 64, 48, 64); 
                break;

            case 2: // 44 Pelota Roja - 68 Perro Azul - 48 Pelota Azul - 64 Perro Rojo
                Ensayos4Cartas(44, 68, 48, 64, 48, 64); 
                break;

            case 3: //4 Auto Rojo - 28 Flor Azul - 8 Auto Azul - 24 Flor Roja
                Ensayos6Cartas(4, 28, 4, 28, 4, 28, 8, 24); 
                break;

            case 4: // 64 Perro Rojo - 48 Pelota Azul - 68 Perro Azul - 44 Pelota Roja
                Ensayos6Cartas(64, 48, 64, 48, 64, 48, 68, 44);
                break;

            case 5: // 24 Flor Roja - 8 Auto Azul - 28 Flor Azul - 4 Auto Rojo
                Ensayos6Cartas(24, 8, 24, 8, 24, 8, 28, 4);
                break;

            case 6: // 76 Perro Naranja - 53 Pelota Amarilla Chica - 77 Perro Naranja chico - 52 Pelota Amarilla
                    // 72 Perro Amarillo - 57 Pelota Naranja Chica - 73 Perro Amarillo chico - 56 Pelota Naranja 
                Ensayos6Cartas(76, 53, 77, 52, 72, 57, 73, 56);
                break;

            case 7: // 16 Auto Naranja - 13 Auto Amarillo chico - 36 Flor Naranja - 33 Flor Amarilla chica
                    // 12 Auto Amarillo - 17 Auto Naranja chico - 32 Flor Amarilla - 37 Flor Naranja chica 
                    // 33 Flor Amarilla chica - 37 Flor Naranja chica - 12 Auto Amarillo
                Ensayos9Cartas(16, 13, 36, 33, 12, 17, 32, 37, 33, 37, 12);
                break;

            case 8:
                break;

            case 9:
                break;
        }
    }

    List<Vector3> PosicionRandom(int CantidadPosiciones)  //REVISAR PAR HACER DESPUES
    {
        List<Vector3> posicionesMax = new List<Vector3> { Pos1, Pos2, Pos3, Pos4, Pos5, Pos6, Pos7, Pos8, Pos9 };

        List<Vector3> posicionesMid = new List<Vector3> { Pos1, Pos2, Pos3, Pos4, Pos5, Pos6 };

        List<Vector3> posicionesLow = new List<Vector3> { Pos1, Pos2, Pos3, Pos4 };

        List<Vector3> randomized = new List<Vector3>();

        if (CantidadPosiciones == 9)
        {
            System.Random rnd = new System.Random();
            randomized = posicionesMax.OrderBy(item => rnd.Next()).ToList<Vector3>();
        }
        else if (CantidadPosiciones == 6)
        {
            System.Random rnd = new System.Random();
            randomized = posicionesMid.OrderBy(item => rnd.Next()).ToList<Vector3>();
        }
        else
        {
            System.Random rnd = new System.Random();
            randomized = posicionesLow.OrderBy(item => rnd.Next()).ToList<Vector3>();
        }

        return randomized;

    }

    List<Vector3> PosicionSinRandom(int CantidadPosiciones)  //REVISAR PAR HACER DESPUES
    {
        List<Vector3> posicionesMax = new List<Vector3> { Pos1, Pos2, Pos3, Pos4, Pos5, Pos6, Pos7, Pos8, Pos9 };

        List<Vector3> posicionesMid = new List<Vector3> { Pos1, Pos2, Pos3, Pos4, Pos5, Pos6 };

        List<Vector3> posicionesLow = new List<Vector3> { Pos1, Pos2, Pos3, Pos4 };

        List<Vector3> returnedList = new List<Vector3> { };

        if (CantidadPosiciones == 9)
            returnedList = posicionesMax;                    
        else if (CantidadPosiciones == 6)
            returnedList = posicionesMid;
        else
            returnedList = posicionesLow;

        return returnedList;
    }

    private void Ensayos4Cartas(int cart1, int cart2, int cart3, int cart4, int cartM1, int cartM2)
    {
        List<Vector3> posiciones;
        if (esRandom)
            posiciones = PosicionRandom(4);
        else
            posiciones = PosicionSinRandom(4);

        carta1 = Instantiate(cartas[cart1], posiciones[0], transform.rotation); 
        carta2 = Instantiate(cartas[cart2], posiciones[1], transform.rotation);
        carta3 = Instantiate(cartas[cart3], posiciones[2], transform.rotation);
        carta4 = Instantiate(cartas[cart4], posiciones[3], transform.rotation);
        cartaMuestra1 = Instantiate(cartas[cartM1], PosCanasta1, transform.rotation);
        cartaMuestra3 = Instantiate(cartas[cartM2], PosCanasta2, transform.rotation);
        cartaMuestra1.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra3.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra1.tag = "Muestra";
        cartaMuestra3.tag = "Muestra";
    }

    private void Ensayos6Cartas(int cart1, int cart2, int cart3, int cart4, int cart5, int cart6, int cartM1, int cartM2)
    {
        List<Vector3> posiciones;
        if (esRandom)
            posiciones = PosicionRandom(6);
        else
            posiciones = PosicionSinRandom(6);

        carta1 = Instantiate(cartas[cart1], posiciones[0], transform.rotation);
        carta2 = Instantiate(cartas[cart2], posiciones[1], transform.rotation);
        carta3 = Instantiate(cartas[cart3], posiciones[2], transform.rotation);
        carta4 = Instantiate(cartas[cart4], posiciones[3], transform.rotation);
        carta5 = Instantiate(cartas[cart5], posiciones[4], transform.rotation);
        carta6 = Instantiate(cartas[cart6], posiciones[5], transform.rotation);
        cartaMuestra1 = Instantiate(cartas[cartM1], PosCanasta1, transform.rotation);
        cartaMuestra3 = Instantiate(cartas[cartM2], PosCanasta2, transform.rotation);
        cartaMuestra1.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra3.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra1.tag = "Muestra";
        cartaMuestra3.tag = "Muestra";
    }

    private void Ensayos9Cartas(int cart1, int cart2, int cart3, int cart4, int cart5, int cart6, int cart7, int cart8, int cart9, int cartM1, int cartM2)
    {
        Pos1 = new Vector3(-40, -20, 0) ;
        Pos2 = new Vector3(-30, -15, 0);
        Pos3 = new Vector3(-20, -20, 0);
        Pos4 = new Vector3(-10, -15, 0);
        Pos5 = new Vector3(0, -20, 0);
        Pos6 = new Vector3(10, -15, 0);
        Pos7 = new Vector3(20, -20, 0);
        Pos8 = new Vector3(30, -15, 0);
        Pos9 = new Vector3(40, -20, 0);
        Vector3 escala = new Vector3(0.35f, 0.35f, 0);

        List<Vector3> posiciones;
        if (esRandom)
            posiciones = PosicionRandom(9);
        else
            posiciones = PosicionSinRandom(9);

        carta1 = Instantiate(cartas[cart1], posiciones[0], transform.rotation);
        carta2 = Instantiate(cartas[cart2], posiciones[1], transform.rotation);
        carta3 = Instantiate(cartas[cart3], posiciones[2], transform.rotation);
        carta4 = Instantiate(cartas[cart4], posiciones[3], transform.rotation);
        carta5 = Instantiate(cartas[cart5], posiciones[4], transform.rotation);
        carta6 = Instantiate(cartas[cart6], posiciones[5], transform.rotation);
        carta7 = Instantiate(cartas[cart7], posiciones[6], transform.rotation);
        carta8 = Instantiate(cartas[cart8], posiciones[7], transform.rotation);
        carta9 = Instantiate(cartas[cart9], posiciones[8], transform.rotation);
        carta1.transform.localScale = escala;
        carta2.transform.localScale = escala;
        carta3.transform.localScale = escala;
        carta4.transform.localScale = escala;
        carta5.transform.localScale = escala;
        carta6.transform.localScale = escala;
        carta7.transform.localScale = escala;
        carta8.transform.localScale = escala;
        carta9.transform.localScale = escala;
        cartaMuestra1 = Instantiate(cartas[cartM1], PosCanasta1, transform.rotation);
        cartaMuestra3 = Instantiate(cartas[cartM2], PosCanasta2, transform.rotation);
        cartaMuestra1.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra3.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra1.tag = "Muestra";
        cartaMuestra3.tag = "Muestra";
    }


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
