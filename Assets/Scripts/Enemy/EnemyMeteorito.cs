using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeteorito : MonoBehaviour
{
    public bool tutorial = false;
    public Animator animator = null;
    public SpriteRenderer render = null;
    public float speed = 5.0f;
    private GameObject player = null;
    public Rigidbody2D rb = null;
    private bool yeeted = false;

    // Start is called before the first frame update
    public void Start()
    {
        if(tutorial) return;
        
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.LogError("Error: Jugador no encontrado");
        }

        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;

        if (direction.x > 0) 
        { 
            render.flipX = true;
            Debug.Log("AAJAJJAJA");
        }
    }


    void Update()
    {
        if (yeeted)
        {
            Vector2 direction = (new Vector2(0, 14) - (Vector2)transform.position).normalized;
            rb.velocity = direction * speed;
            //transform.position = Vector2.MoveTowards(transform.position, , speed * Time.deltaTime);
            transform.Rotate(0, 0, 6.0f * 200 * Time.deltaTime);
        }

        if (transform.position.y <= -10 || transform.position.y >= 12)
            Destroy(this.gameObject);
    }

    public void tutorialStop()
    {
        rb.velocity = Vector2.zero;
    }

    public void yeet()
    {
        animator.Play("Base Layer.EnemyMeteorito_Parried");
        yeeted = true;
    }
}
