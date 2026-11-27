using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorConclusao : MonoBehaviour
{
    public void JogarOutraVez()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("01_Abertura");
    }
}