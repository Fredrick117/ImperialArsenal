using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Galaxy Galaxy;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Galaxy = new Galaxy();

        // Should all of this be done in SystemGenerator, not here?
        SystemGenerator generator = GameObject.FindGameObjectWithTag("SystemGenerator").GetComponent<SystemGenerator>();

        generator.GenerateSystems();
        foreach (StarSystem ss in Galaxy.StarSystems)
        { 
            generator.RenderStarSystem(ss);
        }

        generator.CreateAdjacencyMatrix();
        generator.ConnectSystems();
    }

    public void AddSystem(StarSystem system)
    {
        Galaxy.StarSystems.Add(system);
    }

    public void AddPlanet(StarSystem system, Object obj)
    {
        // Todo: refactor
        Galaxy.StarSystems[Galaxy.StarSystems.IndexOf(system)].Objects.Add(obj);
    }
}