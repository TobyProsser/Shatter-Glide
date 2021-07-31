using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public Transform player;

    public GameObject tinyEnemy;
    public GameObject smallEnemy;
    public GameObject bigEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        float xVal = 10;
        int spawnsTillSmallEnemy = 0;
        int spawnsTillBigEnemy = 0;

        while (true)
        {
            if (player.position.x >= xVal - 80)
            {
                float yVal = Random.Range(-10, 10);
                Vector3 spawnLoc = new Vector3(xVal, yVal, 0);

                GameObject curEnemy;
                if (spawnsTillBigEnemy > 3)
                {
                    spawnsTillBigEnemy = 0;
                    curEnemy = Instantiate(bigEnemy, spawnLoc, Quaternion.identity);
                }
                else if (spawnsTillSmallEnemy > 3)
                {
                    spawnsTillSmallEnemy = 0;
                    curEnemy = Instantiate(smallEnemy, spawnLoc, Quaternion.identity);
                }
                else
                {
                    curEnemy = Instantiate(tinyEnemy, spawnLoc, Quaternion.identity);
                }

                yield return new WaitForSeconds(.1f);

                xVal += Random.Range(45, 65);
                spawnsTillSmallEnemy++;
                spawnsTillBigEnemy++;
            }
            else yield return null;
        }
    }
}
