using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Parry playerParry = null;
    public SpriteRenderer sprite = null;
    public AudioSource hurtSource = null;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Hitbox: Entro un objeto crack");
        Debug.Log(other.gameObject.tag);

        // Check collision with the black hole
        if (other.gameObject.tag == "BlackHole")
        {
            GameObject mainCamera = GameObject.Find("Main Camera");
            mainCamera.GetComponent<TitleScreen>().startGameOverCoroutine();
            // StartCoroutine(mainCamera.GetComponent<TitleScreen>().gameOver());
        }
		else
		{
            // FALTARIA STUN Y MAS COSITAS

            playerParry.objetoParry = null;
            Destroy(other.gameObject);
            StartCoroutine(Flash());
        }  
    }

    IEnumerator Flash()
    {
        hurtSource.Play();
        this.enabled = false;
        playerParry.parrySwitch = false;
        for (int i = 0; i < 2; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        this.enabled = true;
        playerParry.parrySwitch = true;
    }
}
