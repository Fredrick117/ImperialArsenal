using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SystemGenerator : MonoBehaviour
{
    // Number of systems to generate (must be a perfect square)
    [SerializeField]
    private int N;

    // Maximum distance between systems
    [SerializeField]
    private float maxConnectionDistance;

    [SerializeField]
    private Sprite systemSprite;

    // Space in between spawn areas and edges of the viewport
    [SerializeField]
    private float spacing = 25f;

    // Adjacency matrix
    private int[,] AdjMatrix;

    private RectTransform mapContainer;

    private int cellsPerRow;
    private float rowWidth, cellSize;

    private void Awake()
    {
        mapContainer = gameObject.GetComponent<RectTransform>();

        cellsPerRow = (int)Mathf.Sqrt(N);
        rowWidth = mapContainer.rect.width;
        cellSize = rowWidth / cellsPerRow;

        for (int i = 0; i < cellsPerRow; i++)
        {
            for (int j = 0; j < cellsPerRow; j++)
            {
                CreateSpawnArea(cellSize, i, j);
            }
        }
    }

    /// <summary>
    /// Called to generate the raw data for all star systems.
    /// This data includes:
        /// - Name
        /// - X/Y location
        /// - Star system type (singular, binary, trinary)
        /// - All celestial objects within each star system
    /// Once this data has been generated, it is placed within the "Galaxy" variable in GameManager.
    /// </summary>
    public void GenerateSystems()
    {
        List<StarSystem> systemList = new List<StarSystem>();

        ShuffleArray(StarSystemNames);

        // TODO: use math, not game objects.
        GameObject[] spawnAreaList = GameObject.FindGameObjectsWithTag("SpawnArea");

        for (int i = 0; i < N; i++)
        {
            int numObjects = Random.Range(1, 14);

            // DANGER: make sure dictionary is always larger than N
            string starName = StarSystemNames[i];

            //Star s = new Star(starName, 0, 0, Color.yellow);

            List<Planet> objects = new List<Planet>();

            //objects.Add(s);

            RectTransform r = spawnAreaList[i].GetComponent<RectTransform>();
            float xMin = (r.anchoredPosition.x - (cellSize / 2)) + spacing;
            float xMax = (r.anchoredPosition.x + (cellSize / 2)) - spacing;
            float yMin = (r.anchoredPosition.y - (cellSize / 2)) + spacing;
            float yMax = (r.anchoredPosition.y + (cellSize / 2)) - spacing;

            for (int j = 0; j < numObjects - 1; j++)
            {
                objects.Add(new Planet(starName + " " + ToRomanNumerals(j + 2), RandomEnumValue<PlanetType>(), Random.Range(25, 325)));
            }

            StarSystem ss = new StarSystem(
                starName,
                RandomEnumValue<SystemType>(),
                Random.Range(xMin, xMax),
                Random.Range(yMin, yMax),
                // TODO: separate star and planets
                objects
            );

            systemList.Add(ss);
        }

        GameManager.Instance.Galaxy.StarSystems = systemList;
    }

    /// <summary>
    /// Renders a star system (along with a text label) on screen.
    /// </summary>
    /// <param name="starSystem">The star system to render</param>
    public GameObject RenderStarSystem(StarSystem starSystem)
    {
        GameObject gameObject = new GameObject("System", typeof(Image));
        gameObject.tag = "StarSystem";
        gameObject.transform.SetParent(mapContainer, false);
        gameObject.GetComponent<Image>().sprite = systemSprite;
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(starSystem.Xlocation, starSystem.Ylocation);
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.sizeDelta = new Vector2(11, 11);

        GameObject nameGameObject = new GameObject("SystemText", typeof(RectTransform));
        nameGameObject.transform.SetParent(gameObject.transform, false);

        Text text = nameGameObject.AddComponent<Text>();
        Font font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = font;
        text.text = starSystem.Name;
        text.alignment = TextAnchor.UpperCenter;

        RectTransform textRT = nameGameObject.GetComponent<RectTransform>();

        textRT.sizeDelta = new Vector2(textRT.sizeDelta.x, 33f);
        textRT.anchoredPosition = new Vector2(textRT.anchoredPosition.x, -33f);

        StarSystemManager manager = gameObject.AddComponent<StarSystemManager>();
        manager.systemProperties = starSystem;

        return gameObject;
    }

    /// <summary>
    /// Creates a GameObject whose primary purpose is to guide the placement of star systems on screen.
    /// N spawn areas will be created, and a star system will be placed inside the bounds of this area.
    /// </summary>
    /// <param name="cellSize">The width/height of each spawn area</param>
    /// <param name="i">Current row iteration</param>
    /// <param name="j">Current column iteration</param>
    private void CreateSpawnArea(float cellSize, int row, int col)
    {
        GameObject gameObject = new GameObject("SpawnArea", typeof(Image));
        gameObject.tag = "SpawnArea";
        gameObject.transform.SetParent(mapContainer, false);
        gameObject.GetComponent<Image>().color = Color.clear;
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.sizeDelta = new Vector2(cellSize, cellSize);
        Vector2 spawnPosition = new Vector2((cellSize / 2) + (row * cellSize), (mapContainer.rect.height - (cellSize / 2)) - (col * cellSize));
        rect.anchoredPosition = spawnPosition;
    }

    /// <summary>
    /// Creates an adjacency matrix that can be used to identify which star systems are connected.
    /// Connectivity is based on the distance between two star systems: if the distance between any two systems is less than the maxConnectionDistance,
    /// those two systems can be considered "adjacent" or "connected".
    /// </summary>
    public void CreateAdjacencyMatrix()
    {
        AdjMatrix = new int[N, N];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                float dist = Vector2.Distance(GetStarSystemVector(i), GetStarSystemVector(j));

                if (dist != 0 && dist <= maxConnectionDistance)
                {
                    AdjMatrix[i, j] = 1;
                }
            }
        }
    }

    /// <summary>
    /// Renders a connection that takes the form of a translucent blue line and represents the adjacency relationship between two star systems.
    /// </summary>
    /// <param name="systemA">The position of the original star system</param>
    /// <param name="systemB">The position of another star system that is connected to system A</param>
    private void RenderSystemConnectors(Vector2 systemA, Vector2 systemB)
    {
        GameObject connector = new GameObject("Connector", typeof(Image));
        connector.transform.SetParent(mapContainer, false);
        connector.GetComponent<Image>().color = new Color(0, 255, 255, 0.05f);

        RectTransform rect = connector.GetComponent<RectTransform>();
        Vector2 direction = (systemB - systemA).normalized;
        float distance = Vector2.Distance(systemA, systemB);
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.sizeDelta = new Vector2(distance, 2f);
        rect.anchoredPosition = systemA + direction * distance * .5f;
        rect.localEulerAngles = new Vector3(0, 0, (Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI));
    }

    /// <summary>
    /// Reads the adjacency matrix, rendering the connectors on screen for each connection.
    /// 
    /// TODO: IS THIS NECESSARY? SHOULDN'T THE CONNECTORS BE RENDERED WHEN THE ADJACENCY IS CREATED IN THE FIRST PLACE?
    /// </summary>
    public void ConnectSystems()
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (AdjMatrix[i, j] == 1)
                {
                    StarSystem systemI = GameManager.Instance.Galaxy.StarSystems[i];
                    StarSystem systemJ = GameManager.Instance.Galaxy.StarSystems[j];

                    RenderSystemConnectors(new Vector2(systemI.Xlocation, systemI.Ylocation), new Vector2(systemJ.Xlocation, systemJ.Ylocation));
                }
            }
        }
    }

    /// <summary>
    /// Selects a random element from an enumeration.
    /// </summary>
    /// <typeparam name="T">The enum that we wish to select a random element from</typeparam>
    /// <returns>A random value from the declared enumeration</returns>
    private T RandomEnumValue<T>()
    {
        var vals = System.Enum.GetValues(typeof(T));
        return (T)vals.GetValue(Random.Range(0, vals.Length - 1));
    }

    /// <summary>
    /// Converts an integer to it's Roman Numeral equivalent.
    /// </summary>
    /// <param name="num">The integer that is to be converted</param>
    /// <returns>A string value representing the integer in Roman Numeral format.</returns>
    private string ToRomanNumerals(int num)
    {
        // TODO: add more
        switch (num)
        {
            case 1:
                return "I";
            case 2:
                return "II";
            case 3:
                return "III";
            case 4:
                return "IV";
            case 5:
                return "V";
            case 6:
                return "VI";
            case 7:
                return "VII";
            case 8:
                return "VIII";
            case 9:
                return "IX";
            case 10:
                return "X";
            case 11:
                return "XI";
            case 12:
                return "XII";
            case 13:
                return "XIII";
            case 14:
                return "XIV";
            case 15:
                return "XV";
            default:
                return "";
        }
    }

    /// <summary>
    /// Shuffles an array with elements of type T. Based on the Fisher-Yates algorithm: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm
    /// </summary>
    /// <typeparam name="T">Type of the array</typeparam>
    /// <param name="array">The array that is to be shuffled</param>
    private static void ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length - 2; i++)
        {
            int j = Random.Range(i, array.Length - 1);
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    /// <summary>
    /// A helper method that improves readability by converting the float values of a star system's location into a Vector2 value.
    /// </summary>
    /// <param name="i">Iteration value that is used to look up star systems</param>
    /// <returns>A star system's position in Vector2 format</returns>
    private Vector2 GetStarSystemVector(int i)
    {
        return new Vector2(GameManager.Instance.Galaxy.StarSystems[i].Xlocation, GameManager.Instance.Galaxy.StarSystems[i].Ylocation);
    }

    // A collection of possible names for star systems.
    private string[] StarSystemNames = new string[] {
        "Ahnar",
        "Zubana",
        "Komle",
        "Usta",
        "Zyb",
        "Sama",
        "Alhim",
        "Falha",
        "Zamok",
        "Korolis Ray",
        "FR-9097b",
        "Vovio",
        "Ovhil",
        "Cassio",
        "Urgarius",
        "Irle",
        "Daryus",
        "Demyr",
        "Ionia",
        "Paradis",
        "Marley",
        "Hizuru",
        "Caelum",
        "Meritum",
        "Harena",
        "Hio",
        "Urs",
        "Gav",
        "091782098731f",
        "X39-57-c",
        "8970987c",
        "Ymir",
        "Hildr",
        "Signy",
        "Siv",
        "Skadi",
        "Nanna",
        "Papur",
        "Atla",
        "Eir",
        "Freya",
        "Saga",
        "Skul",
        "Var",
        "Bol",
        "Flot",
        "Duble",
        "Snotra",
        "Yggdrasil",
        "Eostre",
        "Tyr",
        "Yngvi",
        "Ull",
        "Kral",
        "Lum",
        "Hut",
        "Nic",
        "Rad",
        "Aust",
        "Mica",
        "Gart",
        "Feric",
        "Ira",
        "Lerach",
        "Roq",
        "Horus",
        "Midnight",
        "Litza",
        "Drost",
        "Rusl",
        "Nanua",
        "Roub",
        "Angel",
    };
}