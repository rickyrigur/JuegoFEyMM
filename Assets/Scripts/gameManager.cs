using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gameManager : MonoBehaviour
{
    decimal tiempoJuego;
    decimal tiempoInactivo;
    private int contador;

    //[HideInInspector]
    public int correctos;
    //[HideInInspector]
    public int incorrectos;
    private string respuestaCorrecta;
    private bool finTutorial = false;

    public Text textoCorrectos;
    public Text textoIncorrectos;
    public Text textoOso;
    public Text textoNivel;
    public Text textoSubNivel;
    public Text textoCriterio;
    public Text textoCarta;
    public Text textoSostiene;
    private bool sostiene;

    public GameObject[] cartas;
    public GameObject canasta1;
    public GameObject canasta2;
    public Transform carta;
    public GameObject audioManager;
    AudioManager script;
    public GameObject opciones;

    //[HideInInspector]
    public int nivel = 0;
    public int niv = 0;

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

    [HideInInspector]
    public List<List<string>> listaNiveles;
    private List<List<string>> listaSubNiveles;
    
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
        //numNiveles = new[] { 0, 1, 2, 2, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 8, 8 };

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
        script = audioManager.GetComponent<AudioManager>();

        Pos1 = new Vector3(-0, -13, 0);

        PosCanasta1 = new Vector3(-26, -2f, 0);
        PosCanasta2 = new Vector3(26, -2f, 0);

        Niveles();// listaNiveles[nivel]);
    }

    private void MostrarInforme()
    {
        textoNivel.text = "Nivel: " + nivel.ToString();
        textoSubNivel.text = "Sub Nivel: " + niv.ToString();
        textoCriterio.text = "Criterio: " + listaNiveles[nivel][niv].ToString();
        if(carta != null)
        {
            textoCarta.text = "Carta: " + carta.name;
        }        
        textoSostiene.text = "Sostiene: " + sostiene.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        textoCorrectos.text = "CORRECTO: " + correctos.ToString();
        textoIncorrectos.text = "INCORRECTOS: " + incorrectos.ToString();
        MostrarInforme();

        if (Input.GetMouseButtonDown(0))
        {
            carta = null;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag != "Canasta" && hit.collider.gameObject.tag != "Muestra")
            {
                Debug.Log("Entra cuando aprieto");
                carta = hit.collider.gameObject.GetComponent<Transform>();
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(MousePosition);
            if (carta != null)
            {
                Debug.Log("Entra cuando mantengo");
                sostiene = true;
                carta.transform.position = objPosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            sostiene = false;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null && hit.collider.gameObject.tag != "Canasta" && hit.collider.gameObject.tag != "Muestra" && carta != null)
            {
                Debug.Log("Entra cuando suelto");
                if (hit.collider.OverlapPoint((Vector2)canasta1.transform.position))
                {
                    canasta1.GetComponent<Canasta1>().EstaEnCanasta1(carta.GetComponent<Collider2D>());
                    carta.GetComponent<Collider2D>().enabled = false;
                    carta.transform.localScale = new Vector3(0.3f, 0.3f, 0);
                    TraerSiguienteCarta();
                }
                    
                else if(hit.collider.OverlapPoint((Vector2)canasta2.transform.position))
                {
                    canasta2.GetComponent<Canasta2>().EstaEnCanasta2(carta.GetComponent<Collider2D>());
                    carta.GetComponent<Collider2D>().enabled = false;
                    carta.transform.localScale = new Vector3(0.3f, 0.3f, 0);
                    TraerSiguienteCarta();
                }                    
            }
        }
    }

    public void SiguienteNivel()
    {
        //Debug.Log("nivel: " + nivel + " niv: " + niv);

        if (nivel == 0)
        {
            StartCoroutine(RevisarPrimerNivelCompleto());
        }            

        if (niv == listaNiveles[nivel].Count - 1)
        {
            nivel++;
            niv = 0;
            correctos = 0;
            incorrectos = 0;
        }
        else
            niv++;

        if (finTutorial)
        {
            if (nivel > 3 && niv == 0)
                listaNiveles[nivel].AddRange(RandomizarEnsayos(listaSubNiveles[niv])); //Se agrega de forma random los subniveles al nivel

            Niveles();// listaNiveles[nivel]);
        }
    }

    IEnumerator RevisarPrimerNivelCompleto()
    {
        if (correctos == 4 && incorrectos == 0)
        {
            script.CargarAudio(2); //Audio Ensayo 0 Final
            script.EmpezarAudio();
            yield return new WaitForSeconds(script.TiempoAudio());
        }
        else
        {
            script.CargarAudio(3); //Audio Ensayo 0 Error
            EmpezarAudio();
            yield return new WaitForSeconds(script.TiempoAudio());

            nivel = 0;
            niv = 0;
            correctos = 0;
            incorrectos = 0;
            Niveles();// listaNiveles[0]);
        }
    }

    private void Destruir()
    {
        indiceCartas = new List<int>();

        foreach (GameObject carta in poolGameobjectCartas)
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
    private void Niveles()//List<string> listaNivel)
    {
        switch (nivel) //Ensayos
        {
            case 0: //48 Pelota Azul - 68 Perro Azul
                Debug.Log("Cantidad subNiveles: " + listaNiveles[nivel].Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                Destruir();
                indiceCartas = new List<int> { 48, 68, 48, 68};

                ArmarEnsayo(indiceCartas, 48, 68);
                  
                break;

            case 1: //48 Pelota Azul - 64 Perro Rojo
                Debug.Log("Cantidad subNiveles: " + listaNiveles[nivel].Count);
                Debug.Log("nivel: " + nivel +" - Subnivel: " + niv);

                Destruir();                    
                indiceCartas = new List<int> { 48, 64, 48, 64 };

                ArmarEnsayo(indiceCartas, 48, 64);                      

                break;

            case 2: // 44 Pelota Roja - 68 Perro Azul - 48 Pelota Azul - 64 Perro Rojo
                Debug.Log("Cantidad subNiveles: " + listaNiveles[nivel].Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                Destruir();
                indiceCartas = new List<int> { 44, 68, 48, 64 };

                ArmarEnsayo(indiceCartas, 48, 64);

                break;

            case 3: //4 Auto Rojo - 28 Flor Azul - 8 Auto Azul - 24 Flor Roja
                Debug.Log("Cantidad subNiveles: " + listaNiveles[nivel].Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                Destruir();
                indiceCartas = new List<int> { 4, 28, 4, 28, 4, 28 };


                ArmarEnsayo(indiceCartas, 8, 24);                
                
                break;

            case 4: // 64 Perro Rojo - 48 Pelota Azul - 68 Perro Azul - 44 Pelota Roja
                Debug.Log("Cantidad subNiveles: " + listaNiveles[nivel].Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                Destruir();
                indiceCartas = new List<int> { 64, 48, 64, 48, 64, 48 };


                ArmarEnsayo(indiceCartas, 68, 44);

                break;

            case 5: // 24 Flor Roja - 8 Auto Azul - 28 Flor Azul - 4 Auto Rojo
                Debug.Log("Cantidad subNiveles: " + listaNiveles[nivel].Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                Destruir();
                indiceCartas = new List<int> { 24, 8, 24, 8, 24, 8 };


                ArmarEnsayo(indiceCartas, 28, 4);
                
                break;

            case 6: // 76 Perro Naranja - 53 Pelota Amarilla Chica - 77 Perro Naranja chico - 52 Pelota Amarilla
                    // 72 Perro Amarillo - 57 Pelota Naranja Chica -  Perro Naranja chico -  Pelota Amarilla 
                Debug.Log("Cantidad subNiveles: " + listaNiveles[nivel].Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                Destruir();
                indiceCartas = new List<int> { 76, 53, 77, 52, 72, 57 };


                ArmarEnsayo(indiceCartas, 73, 56);                

                break;

            case 7: // 16 Auto Naranja - 13 Auto Amarillo chico - 36 Flor Naranja - 33 Flor Amarilla chica
                    // 12 Auto Amarillo - 17 Auto Naranja chico - 32 Flor Amarilla - 37 Flor Naranja chica 
                    // 33 Flor Amarilla chica - 37 Flor Naranja chica - 12 Auto Amarillo
                Debug.Log("Cantidad subNiveles: " + listaNiveles[nivel].Count);
                Debug.Log("nivel: " + nivel + " - Subnivel: " + niv);

                Destruir();
                indiceCartas = new List<int> { 16, 13, 36, 33, 12, 17, 32, 37, 33 };

                ArmarEnsayo(indiceCartas, 37, 12);
                
                break;

            case 8: // 76 Perro naranja - 53 Pelota Amarilla chica - 77 Perro Naranja chico - 52 Pelota Amarilla
                    // 57 Pelota Naranja Chica - 73 Perro Amarillo - 57 Pelota Naranja 
                Destruir();
                indiceCartas = new List<int> { 76, 53, 77, 52, 76, 53, 77, 52, 57 };

                ArmarEnsayo(indiceCartas, 73, 56);
                break;

            case 9:
                break;
        }

        //if (niv == listaNivel.Count - 1)
        //{
        //    nivel++;
        //    niv = 0;
        //    correctos = 0;
        //    incorrectos = 0;
        //}
        //else
        //    niv++;
    }

    List<string> RandomizarEnsayos (List<string> subNivel)
    {

        System.Random rnd = new System.Random();
        subNivel = subNivel.OrderBy(item => rnd.Next()).ToList<string>();

        return subNivel;
    }

     private void TraerSiguienteCarta()
    {        
        if (contador < indiceCartas.Count)
            poolGameobjectCartas[contador] = Instantiate(cartas[indiceCartas[contador]], Pos1, transform.rotation);

        contador++;
    }

    private void ArmarEnsayo(List<int> indiceCarta, int cartM1, int cartM2)
    {
        canasta1.GetComponent<Canasta1>().CargarCartaMuestra(cartas[cartM1]);
        canasta2.GetComponent<Canasta2>().CargarCartaMuestra(cartas[cartM2]);
        canasta1.GetComponent<Canasta1>().CargarCriterioComparacion(listaNiveles[nivel][niv].ToString());
        canasta2.GetComponent<Canasta2>().CargarCriterioComparacion(listaNiveles[nivel][niv].ToString());
        
        contador = 0;

        TraerSiguienteCarta();

        cartaMuestra1 = Instantiate(cartas[cartM1], PosCanasta1, transform.rotation);
        cartaMuestra3 = Instantiate(cartas[cartM2], PosCanasta2, transform.rotation);
        cartaMuestra1.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra3.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        cartaMuestra1.tag = "Muestra";
        cartaMuestra3.tag = "Muestra";
        cartaMuestra1.GetComponent<Collider2D>().enabled = false;
        cartaMuestra3.GetComponent<Collider2D>().enabled = false;
    }

    public void InteractuarConOso ()
    {
        Debug.Log(opciones.activeSelf);

        if (opciones.activeSelf == false)
        {
            if (nivel == 0)
                StartCoroutine(playPrimerNivel());
            else
            {
                opciones.SetActive(true);
            }            
        }        
    }

    IEnumerator playPrimerNivel()
    {
        if(!script.EstaReproduciendo())
        {
            textoOso.text = "";
            script.CargarAudio(0);
            script.EmpezarAudio();
            yield return new WaitForSeconds(script.TiempoAudio());
            script.CargarAudio(1);
            script.EmpezarAudio();

            Vector3 escalaInicialCanasta1 = canasta1.transform.localScale;
            Vector3 escalainicialCanasta2 = canasta2.transform.localScale;

            yield return new WaitForSeconds(5f);
            for (float time = 0; time < 1f * 2; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, 1f) / 1f;
                canasta1.transform.localScale = Vector3.Lerp(escalaInicialCanasta1, new Vector3(5.5f, 5.5f, 0), progress);
                yield return null;
            }
            canasta1.transform.localScale = escalaInicialCanasta1;

            yield return new WaitForSeconds(3f);
            for (float time = 0; time < 1f * 2; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, 1f) / 1f;
                canasta2.transform.localScale = Vector3.Lerp(escalainicialCanasta2, new Vector3(5.5f, 5.5f, 0), progress);
                yield return null;
            }
            canasta2.transform.localScale = escalainicialCanasta2;
            finTutorial = true;
        }        
    }

    private void CargarAudiosNiveles()
    {

    }

    public void EmpezarAudio()
    {
        script.EmpezarAudio();
        opciones.SetActive(false);
    }

    public void FinalizarJuego()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void OnMouseDown()
    {
        //offset = cartas.GameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
    }
   
}
