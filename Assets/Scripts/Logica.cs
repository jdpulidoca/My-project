using UnityEngine;
using System.Collections;

public class Logica : MonoBehaviour
{
    public GiroCarta carta1 = null;
    public GiroCarta carta2 = null;
    private bool bloqueado = false;

    public bool EstaBloqueado()
    {
        return bloqueado;
    }

    public void RegistrarCarta(GiroCarta nuevaCarta)
    {
        if (bloqueado) return; // No permitir selección si está bloqueado

        if (carta1 == null)
        {
            carta1 = nuevaCarta;
        }
        else if (carta2 == null)
        {
            carta2 = nuevaCarta;
            bloqueado = true; // Bloqueamos el juego aquí mismo
            Verificar();
        }
    }

    private void Verificar()
    {
        if (carta1 != null && carta2 != null)
        {
            if (carta1.EsIgual(carta2))
            {
                Debug.Log("¡Son iguales!");
                StartCoroutine(EliminarCartas());
            }
            else
            {
                Debug.Log("No son iguales, girando de nuevo...");
                StartCoroutine(OcultarCartas());
            }
        }
    }

    private IEnumerator OcultarCartas()
    {
        yield return new WaitForSeconds(1f); // Espera antes de voltear

        carta1?.VoltearAtras();
        carta2?.VoltearAtras();

        yield return new WaitForSeconds(0.5f); // Pequeño delay antes de desbloquear

        ResetCartas();
    }

    private IEnumerator EliminarCartas()
    {
        yield return new WaitForSeconds(0.5f); // Espera un momento antes de eliminar

        carta1?.DestruirCarta();
        carta2?.DestruirCarta();

        yield return new WaitForSeconds(1f); // Espera que las cartas sean eliminadas

        ResetCartas();
    }

    private void ResetCartas()
    {
        carta1 = null;
        carta2 = null;
        bloqueado = false; // Desbloqueamos el juego después de la eliminación
    }
}
