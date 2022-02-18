using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiagonal : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private bool yeeted = false;

    private void Start()
    {
        Vector2 pos = transform.position;
        rb.velocity = ((Vector2.up * 7) - pos).normalized * speed;
    }

    public void MoveTowards(Vector2 point)
    {
        Vector2 pos = transform.position;
        pos = point - pos;

        rb.velocity = pos.normalized * speed;
    }

    void Update()
    {
        if (yeeted)
        {
            MoveTowards(new Vector2(0, 14));
            transform.Rotate(0, 0, 6.0f * 200 * Time.deltaTime);
        }

        if (transform.position.y >= 12)
        {
            Destroy(this.gameObject);
        }
    }

    public void yeet() => yeeted = true;
}
