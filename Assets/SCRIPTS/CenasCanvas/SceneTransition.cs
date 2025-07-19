using System.Collections; // Necessário para usar IEnumerator
using UnityEngine;
using UnityEngine.UI; // Necessário para usar o tipo Image
using UnityEngine.SceneManagement; // Necessário para carregar cenas

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage; // Arraste a imagem preta aqui no Inspector
    public float fadeDuration = 1f; // Duração do fade

    private void Start()
    {
        // Garante que a tela começa transparente
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0);
        }
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            if (fadeImage != null)
            {
                fadeImage.color = new Color(0, 0, 0, timer / fadeDuration);
            }
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
