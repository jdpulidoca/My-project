using System;

public class MiLista<T>
{
    private T[] elementos;
    private int conteo;

    public MiLista(int capacidadInicial = 10)
    {
        elementos = new T[capacidadInicial];
        conteo = 0;
    }

    public void Agregar(T item)
    {
        if (conteo >= elementos.Length)
        {
            Array.Resize(ref elementos, elementos.Length * 2);
        }
        elementos[conteo++] = item;
    }

    public T Obtener(int indice)
    {
        if (indice < 0 || indice >= conteo) throw new IndexOutOfRangeException();
        return elementos[indice];
    }

    public int Conteo => conteo;
}





