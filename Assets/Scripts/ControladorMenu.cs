using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public struct BotaoAnimalMenu
{
    public string idImagemLivro; // Ex: Golfinho_1
    public GameObject iconeVistoVerde;
}

public class ControladorMenu : MonoBehaviour
{
    public TextMeshProUGUI textoProgresso; // Ex: "0/9 salvos"
    public BotaoAnimalMenu[] botoesAnimais;

    void Start()
    {
        int salvos = 0;

        foreach (var item in botoesAnimais)
        {
            bool jaSalvo = PlayerPrefs.GetInt("Salvo_" + item.idImagemLivro, 0) == 1;
            if (item.iconeVistoVerde != null)
                item.iconeVistoVerde.SetActive(jaSalvo);

            if (jaSalvo) salvos++;
        }

        if (textoProgresso != null)
            textoProgresso.text = salvos + "/9 salvos";

        if (salvos >= 9)
            SceneManager.LoadScene("05_Conclusao");
    }

    // Ligar a cada um dos 9 botões, escrevendo o ID exato (ex: Golfinho_1)
    public void EscolherAnimal(string idImagemLivro)
    {
        PlayerPrefs.SetString("AnimalAtual", idImagemLivro);
        PlayerPrefs.Save();
        SceneManager.LoadScene("03_ScanAR");
    }
}