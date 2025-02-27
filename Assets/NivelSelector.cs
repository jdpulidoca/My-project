using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NivelSelector : MonoBehaviour
{
    public TMPro.TMP_InputField inputNivel; // Campo de entrada para el nivel

    public void Jugar()
    {
        int nivel;
        if (int.TryParse(inputNivel.text, out nivel))
        {
            nivel = Mathf.Clamp(nivel, 1, 22); // Limita el nivel entre 1 y 22
            PlayerPrefs.SetInt("NivelSeleccionado", nivel); // Guarda el nivel
            SceneManager.LoadScene("Juego"); // Carga la escena del juego
        }
        else
        {
            Debug.LogWarning("Ingresa un nivel válido entre 1 y 22.");
        }
    }
}
