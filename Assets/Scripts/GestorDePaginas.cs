using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[System.Serializable]
public struct PaginaAnimal
{
    public string nomeDaImagemNoLivro; // Tem de ser igual ao Name no LivroImagens
    public GameObject prefabDoAnimal;  // O cubo azul (Prefab) do animal
}

public class GestorDePaginas : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    public List<PaginaAnimal> paginas;

    private Dictionary<string, GameObject> animaisCriados = new Dictionary<string, GameObject>();

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
        {
            AtualizarAnimal(imagem);
        }

        foreach (var imagem in eventArgs.updated)
        {
            AtualizarAnimal(imagem);
        }

        foreach (var par in eventArgs.removed)
        {
            string nome = par.Value.referenceImage.name;
            if (animaisCriados.ContainsKey(nome))
            {
                animaisCriados[nome].SetActive(false);
            }
        }
    }

    void AtualizarAnimal(ARTrackedImage imagem)
    {
        string nome = imagem.referenceImage.name;

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

        if (animaisCriados.ContainsKey(nome))
        {
            animaisCriados[nome].SetActive(imagem.trackingState == TrackingState.Tracking);
        }
    }
}