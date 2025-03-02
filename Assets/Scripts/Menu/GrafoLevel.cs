using UnityEngine;

public class GrafoLevel
{
    private MiLista<NodoNivel> nodos;

    public GrafoLevel()
    {
        nodos = new MiLista<NodoNivel>();
    }

    public void AgregarNodo(int id)
    {
        nodos.Agregar(new NodoNivel(id));
    }

    public void ConectarNodos(int origen, int destino)
    {
        var nodo = ObtenerNodo(origen);
        if (nodo != null)
        {
            nodo.Conectar(destino);
        }
    }

    public NodoNivel ObtenerNodo(int id)
    {
        for (int i = 0; i < nodos.Conteo; i++)
        {
            if (nodos.Obtener(i).idNivel == id) return nodos.Obtener(i);
        }
        return null;
    }

    public MiLista<NodoNivel> ObtenerNodos()
    {
        return nodos;
    }
}
