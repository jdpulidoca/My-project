using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MiHashSetJerarquico estado;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InicializarEstado();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InicializarManual()
    {
        if (estado == null)
        {
            InicializarEstado();
        }
    }

    private void InicializarEstado()
    {
        estado = new MiHashSetJerarquico();
        estado.MostrarEstado();
        Debug.Log("Estado inicializado desde cero.");
    }

    public void GuardarEstado()
    {
        // Intencionalmente vacío para este modo volátil
    }
}




