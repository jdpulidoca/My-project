using UnityEngine;

public class ConjuntoModos
{
    private string[] modos;
    private int cantidad;

    public ConjuntoModos()
    {
        modos = new string[10];
        cantidad = 0;
    }

    public void Agregar(string modo)
    {
        if (!Contiene(modo) && cantidad < 10)
        {
            modos[cantidad++] = modo;
        }
    }

    public bool Contiene(string modo)
    {
        for (int i = 0; i < cantidad; i++)
        {
            if (modos[i] == modo)
                return true;
        }
        return false;
    }
}





