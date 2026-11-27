using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorAbertura : MonoBehaviour
{
    public GameObject painelMissaoGaivota;

    public void AbrirPainelMissao()
    {
        if (painelMissaoGaivota != null)
            painelMissaoGaivota.SetActive(true);
    }

    public void AceitarMissao()
    {
        SceneManager.LoadScene("02_Menu");
    }
}