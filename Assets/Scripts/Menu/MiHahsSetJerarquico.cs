using System;
using UnityEngine;

public class MiHashSetJerarquico
{
    private class Entrada
    {
        public int clave;
        public string estado;

        public Entrada(int clave, string estado)
        {
            this.clave = clave;
            this.estado = estado;
        }
    }

    private MiLista<Entrada>[] buckets;
    private int capacidad = 32;

    public MiHashSetJerarquico()
    {
        buckets = new MiLista<Entrada>[capacidad];
        for (int i = 0; i < capacidad; i++)
        {
            buckets[i] = new MiLista<Entrada>();
        }

        Agregar(0, "disponible");
        for (int i = 1; i < capacidad; i++)
        {
            Agregar(i, "bloqueado");
        }
    }

    private int Hash(int clave)
    {
        return Math.Abs(clave) % capacidad;
    }

    public void Agregar(int clave, string estado)
    {
        int indice = Hash(clave);
        MiLista<Entrada> lista = buckets[indice];

        for (int i = 0; i < lista.Conteo; i++)
        {
            if (lista.Obtener(i).clave == clave)
            {
                lista.Obtener(i).estado = estado;
                return;
            }
        }

        lista.Agregar(new Entrada(clave, estado));
    }

    public void CambiarEstado(int clave, string nuevoEstado)
    {
        Agregar(clave, nuevoEstado);
    }

    public string ObtenerEstado(int clave)
    {
        int indice = Hash(clave);
        MiLista<Entrada> lista = buckets[indice];

        for (int i = 0; i < lista.Conteo; i++)
        {
            if (lista.Obtener(i).clave == clave)
            {
                return lista.Obtener(i).estado;
            }
        }
        return "bloqueado";
    }

    public MiLista<int> ObtenerPorEstado(string estadoBuscado)
    {
        MiLista<int> resultado = new MiLista<int>();

        for (int i = 0; i < capacidad; i++)
        {
            MiLista<Entrada> lista = buckets[i];

            for (int j = 0; j < lista.Conteo; j++)
            {
                if (lista.Obtener(j).estado == estadoBuscado)
                {
                    resultado.Agregar(lista.Obtener(j).clave);
                }
            }
        }

        return resultado;
    }

    public void MostrarEstado()
    {
        for (int i = 0; i < capacidad; i++)
        {
            MiLista<Entrada> lista = buckets[i];

            for (int j = 0; j < lista.Conteo; j++)
            {
                var e = lista.Obtener(j);
                Debug.Log($"Nivel {e.clave} - Estado: {e.estado}");
            }
        }
    }
}


