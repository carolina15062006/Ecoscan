using UnityEngine;

[CreateAssetMenu(fileName = "NovoAnimal", menuName = "EcoScan/Ficha de Animal")]
public class FichaAnimal : ScriptableObject
{
    public string idImagemLivro;      // Ex: Cavalo Marinho_1 (igual ao LivroImagens)
    public string nomeAnimal;         // Ex: Cavalo-marinho
    public Sprite spriteAnimalLimpo;  // Imagem do animal sem poluição
    public Sprite spritePoluicao;     // Imagem da poluição (opcional por agora)

    [Header("Textos do Storyboard")]
    public string tipoPoluicao;       // Ex: Cotonetes e plásticos
    [TextArea] public string[] factosSabiasQue; // Os 3 factos "Sabias que..."
    [TextArea] public string dicaAjudar;        // Texto "Tu também podes ajudar"

    [Header("Ferramentas Necessárias")]
    public bool usaPinca;
    public bool usaEsponja;
    public bool usaTesoura;
    public bool usaEscova;
    public bool usaAspirador;
}