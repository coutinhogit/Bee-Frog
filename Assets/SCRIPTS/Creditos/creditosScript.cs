using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class creditosScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image BackgroundImage;
    public TextMeshProUGUI lblTitulo;
    public TextMeshProUGUI lblConteudo;
    public TextMeshProUGUI lblAgradecimento;
    public Button btnMenu;

    bool playingAnimation = true;

    void Start()
    {
        InitiateAnimation();
        Time.timeScale = 1;
        StartCoroutine(animation());
        
    }
// start -1650


    void InitiateAnimation(){
        btnMenu.GetComponent<Image>().color = new Color(0f,0f,0f,0f);
        lblTitulo.color = new Color(0f,0f,0f,0f);
        lblAgradecimento.rectTransform.localScale = new Vector3(0f,0f,0f);
    }


    void jumpAnimation(){
        btnMenu.GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        BackgroundImage.color = new Color(0.35f,0.35f,0.35f);
        lblTitulo.color = new Color(0f,0f,0f,0f);
        lblConteudo.color = new Color(0f,0f,0f,0f);
        lblAgradecimento.rectTransform.localScale = new Vector3(1f,1f,1f);

    }






    IEnumerator animation()
    {
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(trocarCor(BackgroundImage, new Color(1f,1f,1f), new Color(0.35f,0.35f,0.35f), 1.5f)); 

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(trocarCor(lblTitulo, new Color(0f,0f,0f,0f), new Color(1f,1f,1f, 1f), 1f)); 
        
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(moveTitleContent());
        
        yield return StartCoroutine(animationAgradecimento(lblAgradecimento, new Vector3(0f,0f,0f), new Vector3(1.5f,1.5f,1.5f), 1.2f));
        
        yield return StartCoroutine(animationAgradecimento(lblAgradecimento, new Vector3(1.5f,1.5f,1.5f), new Vector3(1f,1f,1f), 0.8f));

        yield return StartCoroutine(trocarCor(btnMenu.GetComponent<Image>(), new Color(0f,0f,0f,0f), new Color(1f,1f,1f, 1f), 1f)); 
        
        // Muda o texto     btnMenu
      //  meuTexto.text = "Texto alterado após 2 segundos!";
    }


    IEnumerator animationAgradecimento(Graphic textMesh, Vector3 scale0, Vector3 scaleFinal, float duracao)
    {
        RectTransform rect = textMesh.rectTransform;
        float tempo = 0f;
        rect.localScale = scale0;

        while (tempo < duracao && playingAnimation)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracao;

            rect.localScale = Vector3.Lerp(scale0, scaleFinal, t);

            yield return null;
        }
        if(playingAnimation)
        rect.localScale = scaleFinal;
    }


/*
CRÉDITOS - GAME
	•	Diego Cavallaro – Fase 1 (Plataforma), Fase 3 (Cartas), Movimentação e integração do personagem no cenário
	•	Gabriel – Fase 2 (Plataforma), Fase 2 (Cartas), Movimentação e integração do personagem no cenário
	•	Fernando – Fase 3 (Plataforma), Fase 1 (Cartas)

EACH USP – Alunos de RP do projeto:
Ana Clara,
Carlos Vaz
Diego Cavallaro,
Eduardo Ferreira,
Eric Schmid,
Estevon Biazussi,
Felipe Yuji, 
Fernando Silva,
Gabriel Coutinho,
Lucas Meira,
Yasmin Iara

Para mais informações sobre o projeto e sobre as mentes por trás de cada etapa, confira o site.

*/


// lblConteudo
    IEnumerator moveTitleContent(){
        float time = 30f;
        StartCoroutine(moverConteudo(lblTitulo, new Vector2(0, -130), new Vector2(0, 130), time/10));
        yield return StartCoroutine(moverConteudo(lblConteudo, new Vector2(0, -1650), new Vector2(0, 2000), time));

    }

    IEnumerator moverConteudo(Graphic content, Vector2 startPos, Vector2 endPos, float duracao)
    {
        float time = 0f;
        RectTransform rect = content.rectTransform;
        rect.anchoredPosition = startPos;
        while (time < duracao && playingAnimation)
        {
            time += Time.deltaTime;
            float t = time / duracao;
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null; 
        }
        if(playingAnimation)
        rect.anchoredPosition = endPos;
    }

    IEnumerator trocarCor(Graphic element, Color corInicial, Color corFinal, float duracao)
    {
        float time = 0f;
        while (time < duracao && playingAnimation)
        {
            time += Time.deltaTime;
            float t = time / duracao;
            element.color = Color.Lerp(corInicial, corFinal, t);
            yield return null; 
        }
        if(playingAnimation)
        element.color = corFinal;
    }
    
    // IEnumerator trocarCorButton(Button element, Color corInicial, Color corFinal, float duracao)
    // {
    //     float time = 0f;
    //     while (time < duracao)
    //     {
    //         time += Time.deltaTime;
    //         float t = time / duracao;
    //         element.color = Color.Lerp(corInicial, corFinal, t);
    //         yield return null; 
    //     }
    //     element.color = corFinal;
    // }
    // Update is called once per frame        
    float valor = 0;
    void Update()
    {
        if(valor == 0){
            valor = Input.GetAxisRaw("Pause");
        }
        else{
            playingAnimation = false;
            jumpAnimation();
        }

    }
}
