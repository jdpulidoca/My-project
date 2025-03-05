using System;

public class MiHashSet
{
    private MiLista<int>[] buckets;

    public MiHashSet(int capacidad = 32)
    {
        buckets = new MiLista<int>[capacidad];
        for (int i = 0; i < capacidad; i++)
        {
            buckets[i] = new MiLista<int>();
        }
    }

    private int Hash(int valor)
    {
        return Math.Abs(valor.GetHashCode() % buckets.Length);  // Asegura índice positivo
    }

    public void Agregar(int valor)
    {
        int indice = Hash(valor);
        if (!Contiene(valor))
        {
            buckets[indice].Agregar(valor);
        }
    }

    public bool Contiene(int valor)
    {
        int indice = Hash(valor);
        var lista = buckets[indice];
        for (int i = 0; i < lista.Conteo; i++)
        {
            if (lista.Obtener(i) == valor) return true;
        }
        return false;
    }

    public int Conteo()
    {
        int total = 0;
        for (int i = 0; i < buckets.Length; i++)
        {
            total += buckets[i].Conteo;
        }
        return total;
    }
}



