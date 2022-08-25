using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetManager : MonoBehaviour
{
    public Planet planetProperties;
    public GameObject textGameObject;
    public float textVerticalOffset;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = planetProperties.Color;
        // place the star system's name directly under it.
        Text nameText = textGameObject.GetComponent<Text>();
        nameText.text = planetProperties.Name;
        Vector3 planetPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textGameObject.transform.position = new Vector3(planetPosition.x, planetPosition.y + textVerticalOffset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
