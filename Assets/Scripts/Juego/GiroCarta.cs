using UnityEngine;
using System.Collections;

public class GiroCarta : MonoBehaviour
{
    [SerializeField] public Sprite cambio;
    private Sprite original;
    private SpriteRenderer spriteRenderer;
    public bool CambioOn = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        original = spriteRenderer.sprite;
    }

    public void VoltearAtras()
    {
        spriteRenderer.sprite = original;
        CambioOn = false;
    }

    public bool EsIgual(GiroCarta otraCarta)
    {
        return this.cambio == otraCarta.cambio;
    }

    public void DestruirCarta()
    {
        StartCoroutine(DestruirConEfecto());
    }

    private IEnumerator DestruirConEfecto()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnMouseDown()
{
    Logica logica = FindAnyObjectByType<Logica>();

    if (logica.EstaBloqueado()) return; // Bloqueamos interacción si hay proceso activo

    if (logica.carta1 == this) 
    {
        // Si el jugador hace clic en la misma carta, la deselecciona
        VoltearAtras();
        logica.carta1 = null;
        return;
    }

    if (CambioOn) return; // Si la carta ya está volteada, no hacer nada

    spriteRenderer.sprite = cambio;
    CambioOn = true;

    logica.RegistrarCarta(this);
}

}