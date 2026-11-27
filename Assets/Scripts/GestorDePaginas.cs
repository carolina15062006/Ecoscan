using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

[System.Serializable]
public struct PaginaAnimal
{
    public string nomeDaImagemNoLivro; // Ex: Golfinho_1 (igual ao LivroImagens)
    public string nomeBonito;          // Ex: Golfinho
    public GameObject prefabDoAnimal;  // O Prefab recortado do animal
}

public class GestorDePaginas : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    public List<PaginaAnimal> paginas;

    [Header("Interface do Ecrã (A tua colega vai dar-te isto)")]
    public TextMeshProUGUI textoInstrucao;
    public GameObject botaoAtivarEcoScan;

    private string animalEscolhidoNoMenu;
    private Dictionary<string, GameObject> animaisCriados = new Dictionary<string, GameObject>();

    void Start()
    {
        // Se vier do Menu, usa o animal escolhido; se testares direto esta cena, usa o 1º da lista
        animalEscolhidoNoMenu = PlayerPrefs.GetString("AnimalAtual", paginas[0].nomeDaImagemNoLivro);

        if (botaoAtivarEcoScan != null)
            botaoAtivarEcoScan.SetActive(false);

        foreach (var item in paginas)
        {
            if (item.nomeDaImagemNoLivro == animalEscolhidoNoMenu && textoInstrucao != null)
            {
                textoInstrucao.text = "Aponta para a página do " + item.nomeBonito;
                break;
            }
        }
    }

    void OnEnable()
    {
        if (imageManager != null)
            imageManager.trackablesChanged.AddListener(OnImagensAtualizadas);
    }

    void OnDisable()
    {
        if (imageManager != null)
            imageManager.trackablesChanged.RemoveListener(OnImagensAtualizadas);
    }

    void OnImagensAtualizadas(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var imagem in eventArgs.added)
            AtualizarAnimal(imagem);

        foreach (var imagem in eventArgs.updated)
            AtualizarAnimal(imagem);
    }

    void AtualizarAnimal(ARTrackedImage imagem)
    {
        string nome = imagem.referenceImage.name;

        if (nome != animalEscolhidoNoMenu) return;

        if (!animaisCriados.ContainsKey(nome))
        {
            foreach (var item in paginas)
            {
                if (item.nomeDaImagemNoLivro == nome && item.prefabDoAnimal != null)
                {
                    GameObject novoAnimal = Instantiate(item.prefabDoAnimal, imagem.transform);
                    novoAnimal.transform.localPosition = Vector3.zero;
                    animaisCriados[nome] = novoAnimal;
                    break;
                }
            }
        }

        bool estaVisivel = (imagem.trackingState == TrackingState.Tracking);

        if (animaisCriados.ContainsKey(nome))
            animaisCriados[nome].SetActive(estaVisivel);

        if (estaVisivel)
        {
            if (botaoAtivarEcoScan != null) botaoAtivarEcoScan.SetActive(true);
            if (textoInstrucao != null) textoInstrucao.text = "Animal encontrado!";
        }
    }

    public void IrParaLimpeza()
    {
        SceneManager.LoadScene("04_Limpeza");
    }
}