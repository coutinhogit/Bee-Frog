using UnityEngine;
using UnityEngine.SceneManagement;


public class NewMonoBehaviourScript : MonoBehaviour
{
    public void LoadScenes(string cena )
    {
        string nomeDaFase = PlayerPrefs.GetString("nomeDaFase");
        if(nomeDaFase == "")
             nomeDaFase = cena;
        SceneManager.LoadScene(nomeDaFase);
    }

    public void BackToPhase()
    {
        SceneManager.LoadScene("Fase2Plataforma");
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
}
