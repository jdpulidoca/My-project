using UnityEngine;
public class EstadisticasNivel
{
    public int intentos;
    public float mejorTiempo;

    public EstadisticasNivel(int intentos = 0, float mejorTiempo = 0)
    {
        this.intentos = intentos;
        this.mejorTiempo = mejorTiempo;
    }
}

public class HashLevel
{
    private class Entrada
    {
        public int clave;
        public EstadisticasNivel valor;
        public bool ocupada;

        public Entrada()
        {
            ocupada = false;
        }
    }

    private Entrada[] tabla;

    public HashLevel(int tamaño)
    {
        tabla = new Entrada[tamaño];
        for (int i = 0; i < tamaño; i++)
            tabla[i] = new Entrada();
    }

    private int Hash(int clave)
    {
        return clave % tabla.Length;
    }

    public void Insertar(int clave, EstadisticasNivel valor)
    {
        int indice = Hash(clave);
        while (tabla[indice].ocupada)
        {
            indice = (indice + 1) % tabla.Length;
        }
        tabla[indice].clave = clave;
        tabla[indice].valor = valor;
        tabla[indice].ocupada = true;
    }

    public EstadisticasNivel Obtener(int clave)
    {
        int indice = Hash(clave);
        int intentos = tabla.Length;
        while (tabla[indice].ocupada && intentos > 0)
        {
            if (tabla[indice].clave == clave)
                return tabla[indice].valor;
            indice = (indice + 1) % tabla.Length;
            intentos--;
        }
        return new EstadisticasNivel(); 
    }
}
