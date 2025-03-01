using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class GeneradorCartas : MonoBehaviour
{
    public GameObject cartaPrefab; // Prefab de la carta
    public Transform contenedorCartas; // Contenedor donde se generarán las cartas
    public Sprite[] imagenesCartas; // Imágenes de las cartas
    public Button botonVolver;

    private int nivel;
    private int totalCartas;
    private const int columnas = 9; // Máximo de columnas
    private const int filas = 5; // Máximo de filas
    private const int maxCartas = columnas * filas; // Máximo de 45 cartas
  

    private void Start()
    {
        nivel = PlayerPrefs.GetInt("NivelSeleccionado", 1); // Recupera el nivel
        totalCartas = nivel * 2; // 2 cartas por nivel

        // Ajustar si el número de cartas excede la cuadrícula
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
        // Verifica si ya no hay cartas y cambia automáticamente de escena
        if (contenedorCartas.childCount == 0)
        {
            Debug.Log("No quedan cartas, volviendo al menú...");
            VolverAlMenu();
        }
    }
    private void GenerarCartas()
    {
        LimpiarCartas(); // Eliminar cartas anteriores antes de generar nuevas

        ListaSprite cartasSeleccionadas = new ListaSprite();
        // Crear pares de cartas
        for (int i = 0; i < totalCartas / 2; i++)
        {
            Sprite carta = imagenesCartas[i % imagenesCartas.Length];
            cartasSeleccionadas.Add(carta);
            cartasSeleccionadas.Add(carta);
        }

        // Mezclar las cartas
        cartasSeleccionadas = MezclarCartas(cartasSeleccionadas);

        // Definir punto de inicio para centrar las cartas
        Vector3 posicionInicial = contenedorCartas.position;
        float espacioX = 1.1f; // Espaciado horizontal
        float espacioY = 1.2f; // Espaciado vertical

        int contador = 0;

        for (int fila = 0; fila < filas; fila++)
        {
            for (int columna = 0; columna < columnas; columna++)
            {
                if (contador >= totalCartas) return; // No generar más de las necesarias

                Vector3 posicion = posicionInicial + new Vector3(columna * espacioX, -fila * espacioY, 0);
                GameObject nuevaCarta = Instantiate(cartaPrefab, posicion, Quaternion.identity, contenedorCartas);

                // Asignar la imagen a la carta
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


