using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZigZag : MonoBehaviour
{
    public EnemySpawner pathGenerator = null;
    private Vector2[] waypoints = new Vector2[4];
    public Rigidbody2D rb = null;

    public float speed = 5.0f;
    private Vector2 currentWP;
    private int wpIndex = 0;
    private bool finished = false;
    private bool yeeted = false;

    // Start is called before the first frame update
    void Start()
    {
        pathGenerator = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        waypoints = pathGenerator.generateNewPath();
        currentWP = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (yeeted) 
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 14), speed * Time.deltaTime);
            transform.Rotate(0, 0, 6.0f * 200 * Time.deltaTime);
        }
        else
            move();

        if (transform.position.y <= -10 || transform.position.y >= 14)
        {
            Destroy(this.gameObject);
        }
    }

    private void move()
    {
        if((Vector2) transform.position != currentWP & !finished)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentWP, step);

            if(currentWP.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
            else
            {
                transform.localScale = Vector3.one;

            }
        }
        else if(wpIndex < 3 & (Vector2)transform.position == currentWP)
        {
            wpIndex++;
            currentWP = waypoints[wpIndex];
        }
        else
        {
            finished = true;
            float spawnZoneX = Random.Range(new Vector2(-4f, 0).x, new Vector2(4f, 0).x);
            float spawnZoneY = Random.Range(new Vector2(0, -10).y, new Vector2(0, -14).y);

            Vector2 ranPosInRectangle = new Vector2(spawnZoneX, spawnZoneY);
            

            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, ranPosInRectangle, step); 
        }
    }

    public void yeet() => yeeted = true;
}
