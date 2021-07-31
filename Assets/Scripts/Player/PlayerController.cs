using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static List<CollectedMaterial> materials = new List<CollectedMaterial>();

    public int storageAmount = 100;
    public int maxHealth;
    int curHealth = 0;

    public Slider healthSlider;

    public float sliderChangeSpeed;
    public Slider matSlider;
    public Image sliderFill;
    public static Color newSliderColor;

    public GameObject fire;

    Rigidbody2D rb;

    public DarknessController darknessController;

    void Awake()
    {
        matSlider.maxValue = storageAmount;
        materials = new List<CollectedMaterial>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        newSliderColor = Color.white;

        curHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Torch") HitTorch(collision.transform);
        else if (collision.tag == "TinyEnemy")
        {
            curHealth -= 10;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Heart")
        {
            curHealth += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.tag != "Untagged" && int.Parse(collision.tag) > 0) StoreMaterial(collision.transform);
    }

    private void LateUpdate()
    {
        matSlider.value = materials.Count;
        sliderFill.color = newSliderColor;

        healthSlider.value = curHealth;
    }

    void StoreMaterial(Transform curObject)
    {
        //If storage is not full, add material to storage
        if (materials.Count < storageAmount)
        {
            //turn collision's string tag to an int and add it to list
            //int value = int.Parse(curObject.tag);
            int value = 0;
            //create curMat with current values and add it to list
            CollectedMaterial curMat = new CollectedMaterial
            {
                value = value,
                color = curObject.GetComponent<SpriteRenderer>().color
            };

            materials.Add(curMat);

            newSliderColor = curObject.GetComponent<SpriteRenderer>().color;
        }

        Destroy(curObject.gameObject);
    }

    void HitTorch(Transform curTorch)
    {
        //Reset to day
        darknessController.SetDay();

        //spawn fire
        Vector3 spawnLoc = curTorch.GetChild(0).transform.position;

        Instantiate(fire, spawnLoc, Quaternion.identity);
    }
}
