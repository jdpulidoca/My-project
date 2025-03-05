using UnityEngine;
using TMPro;

public class MapaManager : MonoBehaviour
{
    public GameObject nodoPrefab;
    public GameObject conexionPrefab;
    public Transform contenedorNodos;
    public Transform contenedorLineas;

    private NodoVisual[] nodos;
    private GrafoLevel grafo = new GrafoLevel();
    private int totalNodos = 30;

    void Start()
    {
        AsegurarGameManager();

        nodos = new NodoVisual[totalNodos];
        CrearNodosTelaraña();
        CrearConexionesTelaraña();
        RevisarDesbloqueoDeNiveles();
        ActualizarEstadoVisualDeNodos();
    }

    void AsegurarGameManager()
    {
        if (GameManager.Instance == null)
        {
            GameObject obj = new GameObject("GameManager");
            GameManager gm = obj.AddComponent<GameManager>();
            gm.InicializarManual();
        }
    }

    void CrearNodosTelaraña()
    {
        Vector2 centro = Vector2.zero;
        int[] nodosPorAnillo = { 1, 6, 12, 11 };
        float[] radios = { 0f, 100f, 200f, 300f };

        int index = 0;
        for (int anillo = 0; anillo < nodosPorAnillo.Length; anillo++)
        {
            for (int i = 0; i < nodosPorAnillo[anillo]; i++)
            {
                float angulo = (360f / nodosPorAnillo[anillo]) * i;
                Vector2 posicion = centro + new Vector2(
                    Mathf.Cos(angulo * Mathf.Deg2Rad) * radios[anillo],
                    Mathf.Sin(angulo * Mathf.Deg2Rad) * radios[anillo]
                );

                CrearNodoVisual(index, posicion);
                grafo.AgregarNodo(index);
                index++;
            }
        }
    }

    void CrearNodoVisual(int id, Vector2 posicion)
    {
        GameObject nodo = Instantiate(nodoPrefab, contenedorNodos);
        nodo.GetComponent<RectTransform>().anchoredPosition = posicion;
        TMP_Text texto = nodo.GetComponentInChildren<TMP_Text>();
        if (texto != null) texto.text = id.ToString();

        nodo.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SeleccionarNivel(id));
        nodos[id] = new NodoVisual(id, posicion, nodo, 0);
    }

    void CrearConexionesTelaraña()
    {
        for (int i = 0; i < nodos.Length; i++)
        {
            for (int j = i + 1; j < nodos.Length; j++)
            {
                if (Vector2.Distance(nodos[i].posicion, nodos[j].posicion) < 160f)
                {
                    CrearConexionVisual(nodos[i].nodo, nodos[j].nodo);
                    grafo.ConectarNodos(nodos[i].id, nodos[j].id);
                    grafo.ConectarNodos(nodos[j].id, nodos[i].id);
                    Debug.Log($"Conexión creada entre {nodos[i].id} y {nodos[j].id}");
                }
            }
        }
    }

    void CrearConexionVisual(GameObject nodoA, GameObject nodoB)
    {
        var conexion = Instantiate(conexionPrefab, contenedorLineas);
        var line = conexion.GetComponent<LineRenderer>();
        line.SetPositions(new Vector3[] { nodoA.GetComponent<RectTransform>().position, nodoB.GetComponent<RectTransform>().position });
    }

    void RevisarDesbloqueoDeNiveles()
{
    Debug.Log("Ingreso Revision");
    var estado = GameManager.Instance.estado;
    var bloqueados = estado.ObtenerPorEstado("bloqueado");

    for (int i = 0; i < bloqueados.Conteo; i++)
    {
        int id = bloqueados.Obtener(i);
        var nodo = grafo.ObtenerNodo(id);

        if (nodo == null)
        {
            Debug.LogWarning($"El nodo {id} no existe en el grafo. Se omite.");
            continue;  // Salta al siguiente nodo si el actual no existe
        }

        for (int j = 0; j < nodo.conexiones.Conteo; j++)
        {
            int vecino = nodo.conexiones.Obtener(j);
            Debug.Log($"Se obtuvo: {vecino}");
            if (estado.ObtenerEstado(vecino) == "completo")
            {
                estado.CambiarEstado(id, "disponible");
                Debug.Log($"El {id} esta disponible porque {vecino} está completo");
                break;
            }
            Debug.Log($"Todo bien");
        }
        Debug.Log($"Segundo ciclo off para nodo {id}");
    }
    Debug.Log("Sale Revision");
    GameManager.Instance.estado.MostrarEstado();
}


    void ActualizarEstadoVisualDeNodos()
    {
        var estado = GameManager.Instance.estado;

        for (int i = 0; i < nodos.Length; i++)
        {
            var nodoVisual = nodos[i];
            var img = nodoVisual.nodo.GetComponent<UnityEngine.UI.Image>();
            var btn = nodoVisual.nodo.GetComponent<UnityEngine.UI.Button>();

            switch (estado.ObtenerEstado(nodoVisual.id))
            {
                case "completo":
                    img.color = Color.green;
                    btn.interactable = false;
                    break;
                case "disponible":
                    img.color = Color.white;
                    btn.interactable = true;
                    break;
                default:
                    img.color = Color.gray;
                    btn.interactable = false;
                    break;
            }
        }
    }

    void SeleccionarNivel(int nivel)
    {
        PlayerPrefs.SetInt("NivelSeleccionado", nivel);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Juego");
    }
}

