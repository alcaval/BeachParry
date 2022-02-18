using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject gb; //Object to intantiate
    private float spawnZoneX; //Coordinate X for point in path
    private float spawnZoneY; //Coordinate Y for point in path
    private float diffY = 5; //Coordinate Y changeable variable
    private Vector2[] waypoints = new Vector2[4]; //Array of waypoints, basically path for the enemy

    //Updates an array of waypoints to set the path of an enemy
    public Vector2[] generateNewPath()
    {
        for (int i = 0; i < 4; i++)
        {
            spawnZoneX = Random.Range(new Vector2(-3.2f, 0).x, new Vector2(3.2f, 0).x);
            spawnZoneY = Random.Range(new Vector2(0, diffY).y, new Vector2(0, diffY - 2.5f).y);

            Vector2 ranPosInRectangle = new Vector2(spawnZoneX, spawnZoneY);
            //Instantiate(gb, ranPosInRectangle, Quaternion.identity); //Comment this line if you want to stop the debug

            diffY -= 2.5f;
            waypoints[i] = ranPosInRectangle;
        }

        for (int i = 0; i < 4; i++) //DEBUG LOOP
        {
            //Debug.Log(waypoints[i]);
            diffY = 5;
        }

        return waypoints;
    }
}
