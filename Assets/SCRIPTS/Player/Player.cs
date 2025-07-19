using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager class to load scenes

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool IsJumping;
    public bool doubleJump;

    private Rigidbody2D rb;
    private Animator anim;
//oi

    private bool isPaused;

    void Start()
    {
        // Time.timeScale = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Get the Rigidbody component attached to the player GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale!=0){
            Move();
            Jump();
        }


    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Speed * Time.deltaTime;

        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsJumping == false)
            {
                rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if (doubleJump)
                {
                    rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    public void DamageJump(){
        rb.AddForce(new Vector2(0f, JumpForce/2), ForceMode2D.Impulse);
        SceneManager.LoadScene("GameOver"); 



    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            IsJumping = false;
            anim.SetBool("jump", false);
        }

        if (collision.gameObject.tag == "Spike")
        {
            Debug.Log("Player hit by spike!");
            TriggerSceneTransition("GameOver");
        }
       
        if (collision.gameObject.tag == "portal")
        {
            var worldScene = GameObject.Find("worldScene").GetComponent<worldScene>();

            TriggerSceneTransition(worldScene.nomeDasCartas);
        }
       
        // if (collision.gameObject.tag == "portal2")
        // {

        //     TriggerSceneTransition("Fase2Cartas");
        // }
    }

    // Helper method to trigger the scene transition
    void TriggerSceneTransition(string sceneName)
    {
        SceneTransition transition = FindObjectOfType<SceneTransition>();
        if (transition != null)
        {
            transition.FadeToScene(sceneName);
        }
        else
        {
            Debug.LogWarning("SceneTransition script not found in the scene!");
            SceneManager.LoadScene(sceneName); // Fallback in case the transition script is missing
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            IsJumping = true;
        }
    }
}

