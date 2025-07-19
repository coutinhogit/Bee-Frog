using UnityEngine;
using UnityEngine.SceneManagement;


public class NewMonoBehaviourScript1 : MonoBehaviour
{
    public void LoadScene(string cena )
    {
        SceneManager.LoadScene(cena);
    }

    public void GoingTo()
    {
        SceneManager.LoadScene("Fase1");
    }
    
}

//
