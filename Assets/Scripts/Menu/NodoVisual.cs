using UnityEngine;
public class NodoVisual
{
    public int id;
    public Vector2 posicion;
    public GameObject nodo;
    public int anillo;
    public int[] conexiones; // Máximo 6 conexiones
    public int totalConexiones;

    public NodoVisual(int id, Vector2 posicion, GameObject nodo, int anillo)
    {
        this.id = id;
        this.posicion = posicion;
        this.nodo = nodo;
        this.anillo = anillo;
        conexiones = new int[6];
        totalConexiones = 0;
    }

    public void AgregarConexion(int nodoDestino)
    {
        if (totalConexiones < 6 && !YaConectado(nodoDestino))
        {
            conexiones[totalConexiones] = nodoDestino;
            totalConexiones++;
        }
    }

    private bool YaConectado(int nodoDestino)
    {
        for (int i = 0; i < totalConexiones; i++)
        {
            if (conexiones[i] == nodoDestino)
                return true;
        }
        return false;
    }
}

