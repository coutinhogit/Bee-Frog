using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Certifique-se de que este namespace está incluído

public class CardSwapper : MonoBehaviour
{
    public RectTransform[] cards; // Array com as cartas
    public bool[] isInformativeCard; // Array para identificar cartas informativas
    private int currentIndex = 0; // Índice da carta atual
    private int maxCard = 0; // Índice da maior carta atingida

    private Vector2 referencePosition; // Posição de referência para as cartas

    private Button btnA;
    private Button btnB;
    private Button btnC;
    private Button btnD;

    private int[] respostaBotao; // Array de respostas será configurado dinamicamente

    TextMeshProUGUI txtFeedback;

    private string[][] textosBotoes;

    private bool canUseArrowKeys = false; // Variável para controlar o uso das setas

    private void Start()
    {
        // Detecta a cena atual e configura a ordem das respostas
        string currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "Fase1Cartas":
                respostaBotao = new int[] { 0, 0, 0, 0, 0, 1, 2, 3 }; // Ordem para Fase1Cartas
                break;
            case "Fase2Cartas":
                respostaBotao = new int[] { 0, 0, 0, 0, 1, 3, 4 }; // Ordem para Fase2Cartas
                break;
            case "Fase3Cartas":
                respostaBotao = new int[] { 0, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 3,2,1}; // Ordem para Fase3Cartas
                break;
            default:
                Debug.LogWarning("Cena não reconhecida. Usando ordem padrão.");
                respostaBotao = new int[] { 0, 0, 0, 0, 1, 2, 3 }; // Ordem padrão
                break;
        }

        
    /*    if (currentScene == "Fase1Cartas")
        {
            respostaBotao = new int[] { 0, 0, 0, 0, 1, 2, 3 }; // Ordem para Fase1Cartas
        }
        else if (currentScene == "Fase2Cartas")
        {
            respostaBotao = new int[] { 0, 0, 0, 0, 1, 3, 4 }; // Ordem para Fase2Cartas
        }
        else
        {
            Debug.LogWarning("Cena não reconhecida. Usando ordem padrão.");
            respostaBotao = new int[] { 0, 0, 0, 0, 1, 2, 3 }; // Ordem padrão
        }
*/



        isInformativeCard = new bool[cards.Length];
        for (int i = 0; i < respostaBotao.Length; i++)
            isInformativeCard[i] = (respostaBotao[i]==0);

     
        btnA = GameObject.Find("btnA").GetComponent<Button>();
        btnB = GameObject.Find("btnB").GetComponent<Button>();
        btnC = GameObject.Find("btnC").GetComponent<Button>();
        btnD = GameObject.Find("btnD").GetComponent<Button>();
        txtFeedback = GameObject.Find("txtFeedback").GetComponent<TextMeshProUGUI>();

        // Inicialmente, esconde o feedback
        txtFeedback.gameObject.SetActive(false);

        // Captura a posição inicial da primeira carta como referência
        if (cards.Length > 0)
        {
            referencePosition = new Vector2(-82, 73); // Define a posição mostrada na imagem
        }

        // Garante que apenas a primeira carta esteja ativa no início
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].anchoredPosition = referencePosition; // Centraliza todas as cartas na posição desejada
            cards[i].gameObject.SetActive(i == currentIndex); // Ativa apenas a carta atual
        }

        btnA.onClick.AddListener(() => botaoClick(1));
        btnB.onClick.AddListener(() => botaoClick(2));
        btnC.onClick.AddListener(() => botaoClick(3));
        btnD.onClick.AddListener(() => botaoClick(4));

        updateButtons();
    }

    void botaoClick(int id)
    {
        // Verifica se o índice atual está dentro dos limites do array respostaBotao
        if (currentIndex >= respostaBotao.Length && respostaBotao[currentIndex] == 0)
        {
            Debug.LogWarning("Índice fora dos limites do array respostaBotao.");
            return;
        }

        // Verifica se o botão clicado corresponde à resposta correta
        if (id == respostaBotao[currentIndex])
        {
            Debug.Log($"Resposta correta! Índice atual: {currentIndex}");
            maxCard = (maxCard < currentIndex) ? currentIndex : maxCard; // Atualiza o índice máximo alcançado
            ShowFeedback("ACERTOU"); // Exibe o feedback de acerto
            canUseArrowKeys = true; // Libera o uso das setas para avançar
        }
        else
        {
            Debug.Log("Resposta incorreta.");
            ShowFeedback("ERROU"); // Exibe o feedback de erro
        }
    }

    private void ShowFeedback(string message)
    {
        StopAllCoroutines(); // Para qualquer transição anterior
        txtFeedback.text = message;
        txtFeedback.gameObject.SetActive(true); // Ativa o feedback
        StartCoroutine(FeedbackTransition());
    }

    private IEnumerator FeedbackTransition()
    {
        // Transição de opacidade (fade in)
        CanvasGroup canvasGroup = txtFeedback.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = txtFeedback.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;

        // Fade in
        float fadeDuration = 0.5f;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Aguarda 2 segundos
        yield return new WaitForSeconds(2f);

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1f - (t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;

        txtFeedback.gameObject.SetActive(false); // Esconde o feedback
    }

    public int getCurrentIndex()
    {
        return currentIndex;
    }

    private void updateButtons()
    {
        float buttonAlpha = !isInformativeCard[currentIndex] ? 1f : 0f;

        btnA.GetComponent<Image>().color = new Color(1f, 1f, 1f, buttonAlpha);
        btnB.GetComponent<Image>().color = new Color(1f, 1f, 1f, buttonAlpha);
        btnC.GetComponent<Image>().color = new Color(1f, 1f, 1f, buttonAlpha);
        btnD.GetComponent<Image>().color = new Color(1f, 1f, 1f, buttonAlpha);
    }

    // Função para ir para a próxima carta (botão da direita)
    public void ShowNextCard()
    {
        if (currentIndex < cards.Length - 1)
        {
            // Verifica se a carta atual é informativa ou se as setas foram liberadas
            if (isInformativeCard[currentIndex] || canUseArrowKeys || maxCard >= currentIndex)
            {
                Debug.Log($"Avançando para a próxima carta. Índice atual: {currentIndex}");
                cards[currentIndex].gameObject.SetActive(false); // Desativa a carta atual
                currentIndex++; // Avança para a próxima carta
                cards[currentIndex].anchoredPosition = referencePosition; // Move a nova carta para a posição de referência
                cards[currentIndex].gameObject.SetActive(true); // Ativa a nova carta
                canUseArrowKeys = false; // Reseta a permissão para avançar
            }
            else
            {
                // Exibe o feedback caso a seta não esteja liberada
                ShowFeedback("Você ainda não marcou a resposta :(");
            }
        }
        else
        {
            // Se não houver mais cartas, verifica se a última foi acertada
            if (canUseArrowKeys)
            {
                Debug.Log("Última carta concluída.");
                
                // Verifica a cena atual e carrega a próxima cena correspondente
                string currentScene = SceneManager.GetActiveScene().name;
                if (currentScene == "Fase2Cartas")
                {
                    Debug.Log("Carregando Fase3Plataforma.");
                    SceneManager.LoadScene("Fase3Plataforma");
                }
                else if (currentScene == "Fase3Cartas")
                {
                    Debug.Log("Terminou o jogo.");
                    SceneManager.LoadScene("Créditos");   //-- Carregar a fase de créditos
                }
                else
                {
                    Debug.Log("Carregando cena padrão.");
                    SceneManager.LoadScene("Fase2Plataforma"); // Cena padrão
                }
            }
            else
            {
                ShowFeedback("Você ainda não marcou a resposta :(");
            }
        }
        updateButtons();
    }

    // Função para voltar para a carta anterior (botão da esquerda)
    public void ShowPreviousCard()
    {
        if (currentIndex > 0) // Permite voltar sem restrições
        {
            cards[currentIndex].gameObject.SetActive(false); // Desativa a carta atual
            currentIndex--; // Volta para a carta anterior
            cards[currentIndex].anchoredPosition = referencePosition; // Move a carta anterior para a posição de referência
            cards[currentIndex].gameObject.SetActive(true); // Ativa a carta anterior
        }
        updateButtons();
    }
}
