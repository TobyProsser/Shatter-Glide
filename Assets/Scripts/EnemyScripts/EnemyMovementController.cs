using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [Header("Enemy Movement Settings")]
    public bool smallEnemy;
    public float speed;
    public bool moving;

    public float distanceFromPlayer;

    EnemyController enemyController;

    Transform player;

    Vector2 moveToLoc;

    private void Awake()
    {
        enemyController = this.GetComponent<EnemyController>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        moveToLoc = new Vector2(player.position.x + distanceFromPlayer, this.transform.position.y);
        if (moving) StartCoroutine(Move());
    }

    private void LateUpdate()
    {
        if(moving && !smallEnemy) this.transform.position = Vector2.MoveTowards(this.transform.position, moveToLoc, speed * Time.deltaTime);
        else if(smallEnemy) this.transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
    }

    IEnumerator Move()
    {
        float maxDistance = distanceFromPlayer * 4f;
        while (true)
        {
            moveToLoc = new Vector2(player.position.x + Random.Range(distanceFromPlayer, maxDistance), this.transform.position.y + Random.Range(-5, 5));

            yield return new WaitForSeconds(.5f);
        }
    }
}
