using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private Text planetText;
    [SerializeField] private float textVerticalOffset;

    private SpriteRenderer spriteRenderer;

    private Planet planetData;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = planetData.Color;

        planetText.text = planetData.Name;
        Vector3 planetPosition = Camera.main.ScreenToWorldPoint(transform.position);
        planetText.transform.position = new Vector3(planetPosition.x, planetPosition.y + textVerticalOffset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlanetData(Planet data)
    {
        planetData = data;
    }
}
