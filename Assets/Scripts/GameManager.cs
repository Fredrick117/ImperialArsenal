using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject solarSystemPrefab;
    public GameObject starPrefab;
    public GameObject planetPrefab;

    public List<StarSystem> starSystemsList;

    public StarSystem currentLoadedSystem;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("GameManager is null");
            return instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        instance = this;

        starSystemsList = new List<StarSystem>();

        // redundant?
        currentLoadedSystem = null;
    }

    public void ButtonCreateStar()
    {
        CreateStar(Input.mousePosition);
    }

    public void ButtonCreatePlanet()
    {
        CreatePlanet(Input.mousePosition);
    }

    public void CreateStar(Vector2 position)
    {
        GameObject.Instantiate(starPrefab, position, Quaternion.identity);
    }

    public void CreatePlanet(Vector2 position)
    {
        GameObject.Instantiate(planetPrefab, position, Quaternion.identity);
    }
}
