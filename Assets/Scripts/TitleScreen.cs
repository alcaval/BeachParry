using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

	public bool newGame = true;

	public bool gameOverStarted = false;

	[Header("Sonidos")]
	public AudioSource cinematicAudioSource = null;
	public AudioClip musicaMenu = null;
	public AudioClip musicaCinematica = null;
	public AudioClip musicaCombate = null;
	public AudioClip gameOverSoundEffect = null;
	public AudioClip gameOverJingle = null;

	//Cinematica
	[Header("Cinematica")]
	public ScreenShake cameraScreenShake = null;
	public GameObject background = null;
	public GameObject player = null;
	public Animator playerAnimator = null;


	// Update is called once per frame
	void Start()
    {
		cinematicAudioSource.clip = musicaMenu;
		cinematicAudioSource.volume = 0.025f;
		cinematicAudioSource.Play();
		StartCoroutine(GameStart1());
    }


	private IEnumerator waitForKeyPress(KeyCode key)
	{
		
		bool done = false;
		while (!done)
		{
			if (Input.GetKeyDown(key))
			{
				done = true;
			}
			yield return null;
		}
	}

	private IEnumerator tutorial()
	{
		
		GameObject tutorialEnemy = GameObject.Find("TutorialEnemy");
		GameObject parry = GameObject.Find("Parry");
		GameObject mainCamera = GameObject.Find("Main Camera");
		GameObject tutorialText = GameObject.Find("TutorialText");

		tutorialEnemy.GetComponent<EnemyMeteorito>().tutorial = false;
		tutorialEnemy.GetComponent<EnemyMeteorito>().Start();
		bool done = false;

		while (!done)
		{
			if (parry.GetComponent<Parry>().objetoParry != null)
			{
				tutorialEnemy.GetComponent<EnemyMeteorito>().tutorialStop();
				mainCamera.GetComponent<Greyscale>().enabled = true;
				tutorialText.GetComponent<CanvasGroup>().alpha = 1;


				while (!done)
				{
					if (Input.GetKeyDown(KeyCode.Space))
					{
						mainCamera.GetComponent<Greyscale>().enabled = false;
						tutorialText.GetComponent<CanvasGroup>().alpha = 0;
						player.GetComponent<Movement>().tutorialJump();
						parry.GetComponent<Parry>().animator.SetTrigger("HitRock");
						newGame = false;
						done = true;
					}
					yield return null;
				}
			}
			yield return null;
		}
		
	}

	private IEnumerator cinematica()
	{
		cameraScreenShake.TriggerShake(5f);
		cinematicAudioSource.clip = musicaCinematica;
		cinematicAudioSource.Play();
		
		playerAnimator.SetTrigger("GameStart");

		while(background.transform.position != Vector3.zero || player.transform.position != Vector3.zero)
		{
			background.transform.position = Vector2.MoveTowards(background.transform.position, Vector2.zero, 5f * Time.deltaTime);
			player.transform.position = Vector2.MoveTowards(player.transform.position, Vector2.zero, 5f * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}

		cinematicAudioSource.Stop();
	}

	private IEnumerator GameStart1()
	{

		if (newGame)
		{

			GameObject tutorialEnemy = GameObject.Find("TutorialEnemy");

			tutorialEnemy.GetComponent<EnemyMeteorito>().tutorialStop();
			
			yield return waitForKeyPress(KeyCode.Space);

			GameObject title = GameObject.Find("Title");
			GameObject pressStart = GameObject.Find("StartGame");
			title.GetComponent<CanvasGroup>().alpha = 0;
			pressStart.GetComponent<CanvasGroup>().alpha = 0;


			yield return cinematica();
		}

		cinematicAudioSource.loop = true;
		cinematicAudioSource.clip = musicaCombate;
		cinematicAudioSource.Play();

		if(newGame)
		{
			yield return tutorial();
		}

		GameObject scoreManager = GameObject.Find("ScoreManager");
		scoreManager.GetComponent<ScoreManager>().ResetScore();

		GameObject tipText = GameObject.Find("TipText");

		// Reiniciar estado de juego
		gameOverStarted = false;
		player.transform.localScale = Vector3.one;
		player.transform.rotation = Quaternion.identity;
		player.GetComponentInChildren<Parry>().parrySwitch = true;

		tipText.GetComponent<CanvasGroup>().alpha = 1;
		yield return new WaitForSeconds(1.5f);
		tipText.GetComponent<CanvasGroup>().alpha = 0;

		
		GameObject spawnManager = GameObject.Find("SpawnManager");

		Debug.Log("Se lanzan las corutinas");
		StartCoroutine(player.GetComponent<Movement>().startGame());
		scoreManager.GetComponent<ScoreManager>().startCoroutines();
		spawnManager.GetComponent<SpawnManager>().startRoutines();

		yield return null;
	}

	public void startGameOverCoroutine()
	{
		if(gameOverStarted)
		{
			return;
		} 
		else
		{
			gameOverStarted = true;
			StartCoroutine(gameOver());
		}
	}

	public IEnumerator gameOver()
	{
		yield return gameOverCoroutine();

		cinematicAudioSource.Stop();
		cinematicAudioSource.clip = gameOverJingle;
		cinematicAudioSource.Play();

		GameObject spawnManager = GameObject.Find("SpawnManager");
		GameObject scoreManager = GameObject.Find("ScoreManager");
		GameObject gameOverScreen = GameObject.Find("GameOverScreen");

		StartCoroutine(player.GetComponent<Movement>().gameOver());
		scoreManager.GetComponent<ScoreManager>().stopCoroutines();
		spawnManager.GetComponent<SpawnManager>().stopRoutines();
		gameOverScreen.GetComponent<SpriteRenderer>().enabled = true;
		gameOverScreen.GetComponent<CanvasGroup>().alpha = 1;

		yield return new WaitForSeconds(0.5f);

		yield return waitForKeyPress(KeyCode.Space);

		gameOverScreen.GetComponent<SpriteRenderer>().enabled = false;
		gameOverScreen.GetComponent<CanvasGroup>().alpha = 0;

		StartCoroutine(GameStart1());
	}


	public IEnumerator gameOverCoroutine()
	{
		player.GetComponentInChildren<Parry>().parrySwitch = false;
		cinematicAudioSource.Stop();
		cinematicAudioSource.loop = false;
		cinematicAudioSource.clip = gameOverSoundEffect;
		cinematicAudioSource.Play();

		while(player.transform.localScale.x > 0)
		{
			player.transform.Rotate(Vector3.forward * 10f);
			player.transform.localScale = new Vector3(player.transform.localScale.x - 0.01f, player.transform.localScale.y - 0.01f, 1f);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(1f);
	}

}
