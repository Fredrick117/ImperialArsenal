using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public StarSystem selectedStarSystem;

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

    public void AddPlanet(StarSystem system, Planet p)
    {
        // Todo: refactor
        Galaxy.StarSystems[Galaxy.StarSystems.IndexOf(system)].Planets.Add(p);
    }
}