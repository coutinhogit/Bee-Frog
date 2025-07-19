using UnityEngine;
using TMPro;
using System.Collections; // Necessário para usar IEnumerator

public class worldScene : MonoBehaviour
{
    public int faseAtual = 1;
    public string nomeDaFase = "fase1";
    public string nomeDasCartas = "Fase1Cartas";
    public int qtdMaxMoedas = 0;
    private static int qtdMoedas;

    TextMeshProUGUI txtQuantBees;
    TextMeshProUGUI txtFeedBack; // Texto de feedback
    private bool feedbackShown = false; // Para evitar múltiplas execuções da corrotina

    public bool isPaused = false;

    public int getMaxMoedas()
    {
        return qtdMaxMoedas;
    }

    public void setMaxMoedas(int newMaxCoins)
    {
        qtdMaxMoedas = newMaxCoins;
    }

    public int getMoedas()
    {
        return qtdMoedas;
    }

    public void setMoedas(int newCoins)
    {
        qtdMoedas = newCoins;
    }

    public void addCoin(int qtd)
    {
        qtdMoedas += qtd;
    }

    void Start()
    {
        qtdMoedas = 0;
        Time.timeScale = 1;
        // Tenta encontrar o texto de contagem de moedas
        txtQuantBees = GameObject.Find("txtQuantBees")?.GetComponent<TextMeshProUGUI>();
        if (txtQuantBees == null)
        {
            Debug.LogError("O objeto 'txtQuantBees' não foi encontrado na cena.");
        }

        // Tenta encontrar o texto de feedback
        txtFeedBack = GameObject.Find("txtFeedBack")?.GetComponent<TextMeshProUGUI>();
        if (txtFeedBack != null)
        {
            txtFeedBack.gameObject.SetActive(false); // Torna o texto invisível no início
        }
        else
        {
            Debug.LogError("O objeto 'txtFeedBack' não foi encontrado na cena.");
        }
 
        if (pauseCanvas != null)
    {
        pauseCanvas.sortingOrder = 0;
        pauseCanvas.targetDisplay = 1; // Começa "escondido" (Display 2, que normalmente não existe)
    }
        PlayerPrefs.SetString("nomeDaFase",nomeDaFase);
        PlayerPrefs.SetInt("faseAtual", faseAtual);
        PlayerPrefs.Save();
    }

    void Update()
    {
        txtQuantBees.text = qtdMoedas + " / " + qtdMaxMoedas;
     if (Input.GetKeyDown(KeyCode.Escape))
        {
            managePause();
        }
        // Verifica se todas as moedas foram coletadas
        if (qtdMoedas >= qtdMaxMoedas && !feedbackShown)
        {
            feedbackShown = true; // Garante que a corrotina será chamada apenas uma vez
            StartCoroutine(ShowFeedback());
        }
    }

    public Canvas pauseCanvas;
    public void managePause(){
        if(!isPaused){
            isPaused = true;
            Time.timeScale = 0;
            if (pauseCanvas != null) {
                pauseCanvas.gameObject.SetActive(true);
                pauseCanvas.sortingOrder = 100;
                pauseCanvas.targetDisplay = 0; // Mostra no Display principal (Display 1)
            }
        }else{
            isPaused = false;
            Time.timeScale = 1;
            if (pauseCanvas != null) {
                pauseCanvas.gameObject.SetActive(false);
                pauseCanvas.sortingOrder = 0;
                pauseCanvas.targetDisplay = 1; // Esconde novamente (Display 2)
            }
        }

    }



    private IEnumerator ShowFeedback()
    {
        txtFeedBack.gameObject.SetActive(true); // Exibe o texto de feedback
        txtFeedBack.text = "Parabéns! Você coletou todas as abelhas!";

        // Gradualmente aumenta o alpha (fade in)
        Color color = txtFeedBack.color;
        for (float t = 0; t < 1; t += Time.deltaTime / 1f) // 1 segundo para fade in
        {
            color.a = Mathf.Lerp(0, 1, t);
            txtFeedBack.color = color;
            yield return null;
        }
        color.a = 1;
        txtFeedBack.color = color;

        // Aguarda 2 segundos com o texto visível
        yield return new WaitForSeconds(2f);

        // Gradualmente reduz o alpha (fade out)
        for (float t = 0; t < 1; t += Time.deltaTime / 1f) // 1 segundo para fade out
        {
            color.a = Mathf.Lerp(1, 0, t);
            txtFeedBack.color = color;
            yield return null;
        }
        color.a = 0;
        txtFeedBack.color = color;

        txtFeedBack.gameObject.SetActive(false); // Torna o texto invisível novamente
    }
}
