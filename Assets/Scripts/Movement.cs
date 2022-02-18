using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

	public Parry parryPlayer = null;
	public bool KnockbackOn = false;


	private Rigidbody2D rb;
	public float horizontalSpeed;
	public float verticalSpeed;
	public float parryForce;
	public float acceleration;
	public float screenSize;
	public float bottomPosition;
	public bool init;

	float horizontalMove = 0f;
	float maxSpeed = 0f;
	Vector2 initialPosition;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		init = false;

	}

	public IEnumerator startGame()
	{
		init = true;
		rb.velocity = Vector2.up * (verticalSpeed * Time.deltaTime);
		maxSpeed = rb.velocity.y;
		yield return null;
	}

	public IEnumerator gameOver()
	{
		init = false;
		rb.velocity = Vector2.zero;
		transform.position = initialPosition;
		yield return null;
	}

	// Update is called once per frame
	void Update()
    {
        if (init)
		{

			horizontalMove = Input.GetAxisRaw("Horizontal") * horizontalSpeed * Time.deltaTime;

			if (rb.velocity.y < maxSpeed && !KnockbackOn)
			{
				rb.velocity = new Vector2(horizontalMove, Mathf.Min(rb.velocity.y + (acceleration * Time.deltaTime), maxSpeed));
			}

			if (transform.position.x <= -screenSize) transform.position = new Vector2(-screenSize, transform.position.y);
			if (transform.position.x >= screenSize) transform.position = new Vector2(screenSize, transform.position.y);
			if (transform.position.y <= bottomPosition) transform.position = new Vector2(transform.position.x, bottomPosition);

		}

	}
	
	private void FixedUpdate()
	{
		if (init)
		{

			rb.velocity = new Vector2(horizontalMove, rb.velocity.y);

		}
		
	}

	public void Knockback()
    {
		// Debug.Log("PAPAPAPA: " + rb.velocity.y + (acceleration * Time.deltaTime));
		// Debug.Log("PAPAPAPA 2: " + maxSpeed);
		StartCoroutine(KnockbackCoroutine());
		// rb.velocity = new Vector2(horizontalMove, parryForce * Time.deltaTime);
		Debug.Log("PAPAPAP: " + (rb.velocity.y + (acceleration * Time.deltaTime)) );
		Debug.Log("maxSpeed: " + maxSpeed);
	}

	private IEnumerator KnockbackCoroutine()
	{
		KnockbackOn = true;
		rb.velocity = new Vector2(horizontalMove, parryForce * Time.deltaTime);
		yield return new WaitForSeconds(parryPlayer.invicibilityTime);
		KnockbackOn = false;
	}

	public void tutorialJump()
	{
		// StartCoroutine(KnockbackCoroutine());
		// transform.position = new Vector2(transform.position.x, parryForce * Time.deltaTime);
		initialPosition = new Vector2(transform.position.x, parryForce * Time.deltaTime);
	}

}
