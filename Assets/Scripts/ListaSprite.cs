using UnityEngine;

public class NodoSprite
{
    public Sprite Dato;
    public NodoSprite Siguiente;

    public NodoSprite(Sprite dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class ListaSprite
{
    private NodoSprite cabeza;

    public void Add(Sprite sprite)
    {
        NodoSprite nuevo = new NodoSprite(sprite);
        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            NodoSprite actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }

   public Sprite this[int index]
    {
        get
        {
            NodoSprite actual = cabeza;
            int contador = 0;
            while (actual != null)
            {
                if (contador == index)
                    return actual.Dato;
                actual = actual.Siguiente;
                contador++;
            }
            throw new System.IndexOutOfRangeException("Índice fuera de rango");
        }
        set
        {
            NodoSprite actual = cabeza;
            int contador = 0;
            while (actual != null)
            {
                if (contador == index)
                {
                    actual.Dato = value;
                    return;
                }
                actual = actual.Siguiente;
                contador++;
            }
            throw new System.IndexOutOfRangeException("Índice fuera de rango");
        }
    }

    public int Count()
    {
        int contador = 0;
        NodoSprite actual = cabeza;
        while (actual != null)
        {
            contador++;
            actual = actual.Siguiente;
        }
        return contador;
    }
}

