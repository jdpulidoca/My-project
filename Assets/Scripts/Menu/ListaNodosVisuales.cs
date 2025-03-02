using UnityEngine;

// Estructura personalizada tipo lista
public class ListaNodosVisuales
{
    private GameObject[] nodos;
    private int contador;

    public ListaNodosVisuales(int capacidadInicial = 10)
    {
        nodos = new GameObject[capacidadInicial];
        contador = 0;
    }

    // Agregar nodo a la lista
    public void Agregar(GameObject nodo)
    {
        if (contador >= nodos.Length)
        {
            ExpandirCapacidad();
        }
        nodos[contador] = nodo;
        contador++;
    }

    // Obtener nodo por índice
    public GameObject Obtener(int indice)
    {
        if (indice < 0 || indice >= contador)
        {
            Debug.LogError("Índice fuera de rango");
            return null;
        }
        return nodos[indice];
    }

    // Cantidad actual de nodos
    public int Tamano()
    {
        return contador;
    }

    // Expansión manual
    private void ExpandirCapacidad()
    {
        int nuevaCapacidad = nodos.Length * 2;
        GameObject[] nuevosNodos = new GameObject[nuevaCapacidad];
        for (int i = 0; i < nodos.Length; i++)
        {
            nuevosNodos[i] = nodos[i];
        }
        nodos = nuevosNodos;
    }
}

