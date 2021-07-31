using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public Transform player;

    public GameObject torch;
    public GameObject heart;

    public List<GameObject> islands = new List<GameObject>();

    void Start()
    {
        StartCoroutine(GenerateMap());
    }

    IEnumerator GenerateMap()
    {
        float xVal = 10;
        int spawnsTillTorch = 0;
        int spawnsTillHeart = 0;
        while (true)
        {
            if (player.position.x >= xVal - 80)
            {
                float yVal = Random.Range(-10, 10);
                Vector3 spawnLoc = new Vector3(xVal, yVal, 0);

                if (spawnsTillHeart > 3)
                {
                    spawnsTillHeart = 0;
                    GameObject curHeart = Instantiate(heart, spawnLoc, Quaternion.identity);
                }
                else
                {
                    GameObject curIsland = Instantiate(islands[Random.Range(0, islands.Count)], spawnLoc, Quaternion.identity);

                    if (spawnsTillTorch > 1)
                    {
                        spawnsTillTorch = 0;
                        GameObject curTorch = Instantiate(torch, curIsland.transform.GetChild(0).transform.position, Quaternion.identity);
                    }
                }

                yield return new WaitForSeconds(.1f);

                xVal += Random.Range(45, 65);
                spawnsTillTorch++;
                spawnsTillHeart++;
            }
            else yield return null;
        }
    }
}
