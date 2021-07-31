using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject shootingParticle;

    GameObject curPart;
    bool shooting;

    Rigidbody2D rb;

    void Awake()
    {
        shooting = false;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if player shoots and has materials
        if (Input.GetKeyDown(KeyCode.Space) && PlayerController.materials.Count > 0)
        {
            //if there is currently a shooting particle, destroy it
            if (curPart) Destroy(curPart);

            curPart = Instantiate(shootingParticle, Vector3.zero, Quaternion.identity);
            curPart.transform.SetParent(this.transform);
            curPart.transform.localPosition = Vector3.zero;

            shooting = true;
            StartCoroutine(DecreaseMaterials());
        }

        //if player stops shooting or runs out of materials
        if ((Input.GetKeyUp(KeyCode.Space) && shooting) || (PlayerController.materials.Count <= 0 && shooting))
        {
            curPart.GetComponent<ParticleSystem>().Stop();
            Destroy(curPart, 4);

            shooting = false;
        }

        if (curPart)
        {
            curPart.GetComponent<ParticleSystem>().startSpeed = rb.velocity.x + Mathf.Abs(rb.velocity.y) + 4;
            curPart.transform.rotation = this.transform.rotation;
        }
    }

    //while shooting, decrease materials
    IEnumerator DecreaseMaterials()
    {   
        while (shooting)
        {
            //get color from last item in materials list, and set that to the particle systems color
            Color curColor = PlayerController.materials[PlayerController.materials.Count - 1].color;
            curPart.GetComponent<ParticleSystem>().startColor = curColor;
            PlayerController.newSliderColor = curColor;
            //decrease materials amount
            if (PlayerController.materials.Count > 0) PlayerController.materials.RemoveAt(PlayerController.materials.Count - 1);
            else PlayerController.materials = new List<CollectedMaterial>();


            yield return new WaitForSeconds(.02f);

            if (!shooting) break;
        }
    }
}
