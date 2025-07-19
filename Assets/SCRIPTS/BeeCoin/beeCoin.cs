using UnityEngine;

using System.Collections;

public class beeCoin : MonoBehaviour
{

  Vector3 coinPosition;
  private float degree = 0;


  private void OnTriggerEnter2D(Collider2D other)
  {
    var player = other.GetComponent<Player>();
    if (player != null)
    {
        var worldScene = GameObject.Find("worldScene").GetComponent<worldScene>();
        worldScene.addCoin(1);
        Destroy(gameObject);
    }
  }

  private void Start()
  {
    coinPosition  = transform.position;
  }

  void Update()
  {
    // Animação de Subir e descer da abelha
    if(Time.timeScale!=0){
      transform.position = new Vector3(transform.position.x, coinPosition.y+Mathf.Sin(Mathf.PI*(degree%360)/180)*0.1f,  transform.position.z);
      // Animação de "Rotação" da abelha
      transform.localScale = new Vector3(Mathf.Cos(Mathf.PI*(degree%720)/360f), 1f, 1f);
      degree +=0.5f;
    }
  }


}
