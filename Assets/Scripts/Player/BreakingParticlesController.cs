using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingParticlesController : MonoBehaviour
{
    public GameObject breakingParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Heart") SpawnParticle(collision.transform);
        else if (collision.tag != "Torch" && collision.tag != "Untagged" && collision.tag != "TinyEnemy" && int.Parse(collision.tag) > 0)SpawnParticle(collision.transform);
    }

    void SpawnParticle(Transform curObject)
    {
        GameObject curPart = Instantiate(breakingParticle, this.transform.position, Quaternion.identity);
        ChangeColor(curObject, curPart);

        Destroy(curPart, 2);
    }

    void ChangeColor(Transform curObject, GameObject curParticle)
    {
        //get color of collided object
        Color curColor = curObject.GetComponent<SpriteRenderer>().color;

        //set color of particle system to object
        var main = curParticle.GetComponent<ParticleSystem>().main;
        main.startColor = curColor;
    }
}
