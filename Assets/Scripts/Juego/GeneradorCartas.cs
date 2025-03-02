using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class GeneradorCartas : MonoBehaviour
{
    public GameObject cartaPrefab; 
    public Transform contenedorCartas; 
    public Sprite[] imagenesCartas; 
    public Button botonVolver;

    private int nivel;
    private int totalCartas;
    private const int columnas = 9; 
    private const int filas = 5; 
    private const int maxCartas = columnas * filas; 
  

    private void Start()
    {
        nivel = PlayerPrefs.GetInt("NivelSeleccionado", 1); 
        nivel=nivel+1;
        totalCartas = nivel * 2; 

        if (totalCartas > maxCartas)
        {
            Debug.LogWarning("El número de cartas excede la cuadrícula. Ajustando al máximo permitido.");
            totalCartas = maxCartas;
        }

        GenerarCartas();
        if (botonVolver != null)
        {
            botonVolver.gameObject.SetActive(true);
            botonVolver.onClick.AddListener(VolverAlMenu);
        }
       }

    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú...");
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {

        if (contenedorCartas.childCount == 0)
        {
            Debug.Log("No quedan cartas, volviendo al menú...");
            VolverAlMenu();
        }
    }
    private void GenerarCartas()
    {
        LimpiarCartas(); 

        ListaSprite cartasSeleccionadas = new ListaSprite();
       
        for (int i = 0; i < totalCartas / 2; i++)
        {
            Sprite carta = imagenesCartas[i % imagenesCartas.Length];
            cartasSeleccionadas.Add(carta);
            cartasSeleccionadas.Add(carta);
        }

       
        cartasSeleccionadas = MezclarCartas(cartasSeleccionadas);

        
        Vector3 posicionInicial = contenedorCartas.position;
        float espacioX = 1.1f; 
        float espacioY = 1.2f; 

        int contador = 0;

        for (int fila = 0; fila < filas; fila++)
        {
            for (int columna = 0; columna < columnas; columna++)
            {
                if (contador >= totalCartas) return; 

                Vector3 posicion = posicionInicial + new Vector3(columna * espacioX, -fila * espacioY, 0);
                GameObject nuevaCarta = Instantiate(cartaPrefab, posicion, Quaternion.identity, contenedorCartas);

                
                GiroCarta giroCarta = nuevaCarta.GetComponent<GiroCarta>();
                if (giroCarta != null)
                {
                    giroCarta.cambio = cartasSeleccionadas[contador];
                }

                contador++;
            }
        }
    }

    private ListaSprite MezclarCartas(ListaSprite lista)
    {
        for (int i = 0; i < lista.Count(); i++)
        {
            int randomIndex = Random.Range(0, lista.Count());
            Sprite temp = lista[i];
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
        return lista;
    }
 
  
    private void LimpiarCartas()
    {
        foreach (Transform child in contenedorCartas)
        {
            Destroy(child.gameObject);
        }
    }
}


