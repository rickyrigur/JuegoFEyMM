using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ensayos : MonoBehaviour
{
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

    

    void Start()
    {
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

    }

    
    
    private void Niveles(int nivel)
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
                    // 72 Perro Amarillo - 57 Pelota Naranja Chica -  Perro Naranja chico -  Pelota Amarilla 
                Ensayos6Cartas(76, 53, 77, 52, 72, 57, 73, 56); //modificar cartas de muestra
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
        Pos1 = new Vector3(-40, -20, 0);
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


}
