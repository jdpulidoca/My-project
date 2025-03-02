using UnityEngine;
using TMPro;

public class MapaManager : MonoBehaviour
{
    public GameObject nodoPrefab;
    public GameObject conexionPrefab;
    public Transform contenedorNodos;
    public Transform contenedorLineas;

    private NodoVisual[] nodos;
    private int totalNodos = 30; // Puedes ajustar

    void Start()
    {
        nodos = new NodoVisual[totalNodos];
        CrearNodosTelaraña();
        CrearConexionesTelaraña();
    }

    void CrearNodosTelaraña()
    {
        Vector2 centro = new Vector2(0, 0);

        int[] nodosPorAnillo = { 1, 6, 12, 11 }; // 4 anillos, ajusta a gusto
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

                CrearNodoVisual(index, posicion, anillo);
                index++;
            }
        }
    }

    void CrearNodoVisual(int id, Vector2 posicion, int anillo)
    {
        GameObject nodo = Instantiate(nodoPrefab, contenedorNodos);
        nodo.GetComponent<RectTransform>().anchoredPosition = posicion;

        TMP_Text texto = nodo.GetComponentInChildren<TMP_Text>();
        if (texto != null)
            texto.text = id.ToString();

        nodo.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SeleccionarNivel(id));

        nodos[id] = new NodoVisual(id, posicion, nodo, anillo);
    }

    void CrearConexionesTelaraña()
    {
        for (int i = 0; i < nodos.Length; i++)
        {
            NodoVisual nodoActual = nodos[i];

            // Conectar con vecinos en el mismo anillo
            foreach (NodoVisual otro in nodos)
            {
                if (otro.id == nodoActual.id) continue;

                if (otro.anillo == nodoActual.anillo)
                {
                    ConectarSiSonVecinos(nodoActual, otro);
                }

                // Conectar con nodos de anillos adyacentes (radiales)
                if (Mathf.Abs(otro.anillo - nodoActual.anillo) == 1)
                {
                    ConectarSiSonRadialmenteCercanos(nodoActual, otro);
                }
            }
        }
    }

    void ConectarSiSonVecinos(NodoVisual a, NodoVisual b)
    {
        float distancia = Vector2.Distance(a.posicion, b.posicion);
        if (distancia < 150f) // Ajusta según tamaño de malla
        {
            CrearConexionVisual(a.nodo, b.nodo);
            a.AgregarConexion(b.id);
            b.AgregarConexion(a.id);
        }
    }

    void ConectarSiSonRadialmenteCercanos(NodoVisual a, NodoVisual b)
    {
        float distancia = Vector2.Distance(a.posicion, b.posicion);
        if (distancia < 160f) // Ajusta para nodos entre anillos
        {
            CrearConexionVisual(a.nodo, b.nodo);
            a.AgregarConexion(b.id);
            b.AgregarConexion(a.id);
        }
    }

    void CrearConexionVisual(GameObject nodoA, GameObject nodoB)
    {
        GameObject conexion = Instantiate(conexionPrefab, contenedorLineas);
        LineRenderer line = conexion.GetComponent<LineRenderer>();

        Vector3[] puntos = new Vector3[2];
        puntos[0] = nodoA.GetComponent<RectTransform>().position;
        puntos[1] = nodoB.GetComponent<RectTransform>().position;

        line.SetPositions(puntos);
    }

    void SeleccionarNivel(int nivel)
    {
        Debug.Log("Seleccionaste nivel " + nivel);
        PlayerPrefs.SetInt("NivelSeleccionado", nivel);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Juego");
    }
}




