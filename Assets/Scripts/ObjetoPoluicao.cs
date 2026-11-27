using UnityEngine;
using UnityEngine.EventSystems;

public class ObjetoPoluicao : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [Header("Configuração deste Lixo")]
    public string ferramentaNecessaria; // Escrever: Pinca, Esponja, Tesoura, Escova ou Aspirador
    public int passosParaLimpar = 3;    // Quantos toques/esfregadelas precisa para sair

    private int progressoAtual = 0;
    private ControladorLimpeza controlador;

    void Start()
    {
        controlador = FindFirstObjectByType<ControladorLimpeza>();
    }

    // Funciona ao tocar no lixo (Pinça, Tesoura, Aspirador)
    public void OnPointerClick(PointerEventData eventData)
    {
        TentarLimpar();
    }

    // Funciona ao esfregar/arrastar o dedo por cima do lixo (Esponja, Escova, Tesoura)
    public void OnDrag(PointerEventData eventData)
    {
        TentarLimpar();
    }

    void TentarLimpar()
    {
        if (controlador == null) return;

        // Verifica se a ferramenta selecionada na barra de baixo é a certa
        if (controlador.ferramentaSelecionada == ferramentaNecessaria)
        {
            progressoAtual++;
            // Diminui ligeiramente o tamanho ou transparência para dar feedback visual
            transform.localScale *= 0.9f;

            if (progressoAtual >= passosParaLimpar)
            {
                controlador.RegistarObjetoLimpo();
                gameObject.SetActive(false);
            }
        }
        else
        {
            // Se escolher a ferramenta errada, avisa o controlador
            controlador.FerramentaErrada();
        }
    }
}