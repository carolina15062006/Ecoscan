using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControladorLimpeza : MonoBehaviour
{
    [Header("Todas as 9 Fichas de Animais")]
    public List<FichaAnimal> todasAsFichas;

    [Header("Interface do Topo e Animal")]
    public Image imagemAnimalNoEcra;
    public Slider barraProgressoLimpeza;
    public TextMeshProUGUI textoPercentagem;
    public TextMeshProUGUI textoFactoSabiasQue;

    [Header("Botões das 5 Ferramentas (Barra Inferior)")]
    public Button botaoPinca;
    public Button botaoEsponja;
    public Button botaoTesoura;
    public Button botaoEscova;
    public Button botaoAspirador;

    [Header("Painel Final (Animal Salvo!)")]
    public GameObject painelAnimalSalvo;
    public TextMeshProUGUI textoDicaAjudar;

    [HideInInspector]
    public string ferramentaSelecionada = "";

    private FichaAnimal fichaAtual;
    private int totalObjetosPoluicao = 0;
    private int objetosLimpos = 0;

    void Start()
    {
        if (painelAnimalSalvo != null)
            painelAnimalSalvo.SetActive(false);

        string idAnimal = PlayerPrefs.GetString("AnimalAtual", todasAsFichas[0].idImagemLivro);

        foreach (var ficha in todasAsFichas)
        {
            if (ficha.idImagemLivro == idAnimal)
            {
                fichaAtual = ficha;
                break;
            }
        }

        if (fichaAtual != null)
        {
            ConfigurarEcraAnimal();
        }
    }

    void ConfigurarEcraAnimal()
    {
        if (imagemAnimalNoEcra != null && fichaAtual.spriteAnimalLimpo != null)
            imagemAnimalNoEcra.sprite = fichaAtual.spriteAnimalLimpo;

        // Mostra o primeiro facto "Sabias que..."
        if (textoFactoSabiasQue != null && fichaAtual.factosSabiasQue.Length > 0)
            textoFactoSabiasQue.text = fichaAtual.factosSabiasQue[0];

        // Ativa apenas as ferramentas que servem para este animal
        if (botaoPinca != null) botaoPinca.interactable = fichaAtual.usaPinca;
        if (botaoEsponja != null) botaoEsponja.interactable = fichaAtual.usaEsponja;
        if (botaoTesoura != null) botaoTesoura.interactable = fichaAtual.usaTesoura;
        if (botaoEscova != null) botaoEscova.interactable = fichaAtual.usaEscova;
        if (botaoAspirador != null) botaoAspirador.interactable = fichaAtual.usaAspirador;

        // Conta quantos objetos de poluição existem ativos na cena
        ObjetoPoluicao[] lixos = FindObjectsByType<ObjetoPoluicao>(FindObjectsSortMode.None);
        totalObjetosPoluicao = lixos.Length;
        AtualizarBarra();
    }

    // Ligar a cada um dos 5 botões de ferramentas (escrevendo: Pinca, Esponja, Tesoura, Escova ou Aspirador)
    public void SelecionarFerramenta(string nomeFerramenta)
    {
        ferramentaSelecionada = nomeFerramenta;
    }

    public void RegistarObjetoLimpo()
    {
        objetosLimpos++;
        AtualizarBarra();

        // Muda para o facto "Sabias que..." seguinte a cada limpeza
        if (textoFactoSabiasQue != null && fichaAtual.factosSabiasQue.Length > 0)
        {
            int indice = Mathf.Clamp(objetosLimpos, 0, fichaAtual.factosSabiasQue.Length - 1);
            textoFactoSabiasQue.text = fichaAtual.factosSabiasQue[indice];
        }

        if (objetosLimpos >= totalObjetosPoluicao)
        {
            ConcluirSalvamento();
        }
    }

    void AtualizarBarra()
    {
        float percentagem = (totalObjetosPoluicao > 0) ? (float)objetosLimpos / totalObjetosPoluicao : 1f;
        if (barraProgressoLimpeza != null) barraProgressoLimpeza.value = percentagem;
        if (textoPercentagem != null) textoPercentagem.text = Mathf.RoundToInt(percentagem * 100f) + "%";
    }

    public void FerramentaErrada()
    {
        // Aqui pode ser ligada a animação do animal a abanar a cabeça
        Debug.Log("Ferramenta errada para este lixo!");
    }

    void ConcluirSalvamento()
    {
        // Guarda no telemóvel que este animal já está salvo
        PlayerPrefs.SetInt("Salvo_" + fichaAtual.idImagemLivro, 1);
        PlayerPrefs.Save();

        if (textoDicaAjudar != null)
            textoDicaAjudar.text = fichaAtual.dicaAjudar;

        if (painelAnimalSalvo != null)
            painelAnimalSalvo.SetActive(true);
    }

    // Ligar ao botão "Voltar ao mapa"
    public void VoltarAoMapa()
    {
        SceneManager.LoadScene("02_Menu");
    }
}