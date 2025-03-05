using UnityEngine;

public class NodoNivel
{
    public int idNivel;
    public MiLista<int> conexiones;

    public NodoNivel(int id)
    {
        idNivel = id;
        conexiones = new MiLista<int>();
    }

    public void Conectar(int idDestino)
    {
        conexiones.Agregar(idDestino);
    }
}
