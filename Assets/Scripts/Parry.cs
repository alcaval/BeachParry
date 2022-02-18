using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public bool DebugConsole = false;

    
    [SerializeField] public Animator animator = null;
    [SerializeField] private GameObject colliderPlayer = null;
    [SerializeField] public float invicibilityTime = 1.0f;
    [SerializeField] private Movement movement = null;

    private Coroutine iframes = null;

    public GameObject objetoParry = null;
    public ScreenShake cameraScreenShake = null;
    public bool parrySwitch = true;

    public AudioSource parrySource = null;

    public ScoreManager scoreManager = null;
    
    void Update()
    {
        if(Input.GetButtonDown("Parry") && objetoParry != null && parrySwitch)
        {
            cameraScreenShake.TriggerShake();

            parrySource.Play();

            // Faltarian muchas cosas aqui
            // Aclaración: Si hay varios objetos no problem, haces parry con uno y palante.
            
            if(objetoParry.tag.EndsWith("Meteorito"))
            {
                if(DebugConsole) Debug.Log("Parry: Meteorito");
                scoreManager.UpdateScore(scoreManager.scoreMeteorito);
                objetoParry.GetComponent<EnemyMeteorito>().yeet();
                animator.SetTrigger("HitRock");
            }
            else if(objetoParry.tag.EndsWith("Diagonal"))
            {
                if(DebugConsole) Debug.Log("Parry: Diagonal");
                scoreManager.UpdateScore(scoreManager.scoreDiagonal);
                objetoParry.GetComponent<EnemyDiagonal>().yeet();
                animator.SetTrigger("ParryDown");
            }
            else if(objetoParry.tag.EndsWith("ZigZag"))
            {
                if(DebugConsole) Debug.Log("Parry: ZigZag");
                scoreManager.UpdateScore(scoreManager.scoreZigZag);
                objetoParry.GetComponent<EnemyZigZag>().yeet();
                animator.SetTrigger("HitRock");
            }

            //Destroy(objetoParry); // PLACEHOLDER, CAMBIAR LUEGO PARA LA ANIMACIÓN
            if(iframes != null)
            {
                StopCoroutine(iframes);
            }
            
            iframes = StartCoroutine(IFramesCoroutine()); // iframes (Desactivar collider de PlayerCollision x segundos).

            movement.Knockback();
        }
    }

    private IEnumerator IFramesCoroutine()
    {
        // colliderPlayer.enabled = false;  
        colliderPlayer.SetActive(false); 
        yield return new WaitForSeconds(invicibilityTime);
        // colliderPlayer.enabled = true;
        colliderPlayer.SetActive(true); 
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(DebugConsole) Debug.Log("Parry: Entro un objeto");

        if(objetoParry == null && other.gameObject.tag.StartsWith("Enemy")) // Así cogemos el primer objeto que entre
        {
            if(DebugConsole) Debug.Log("Parry: Se asigna objetoParry");
            objetoParry = other.gameObject;
        }    
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(DebugConsole) Debug.Log("Parry: Sale un objeto");
        objetoParry = null;
    }
}
