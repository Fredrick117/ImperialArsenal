using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public int numStarSystems;

    public static GameManager Instance;
    public StarSystem selectedStarSystem;

    [SerializeField]
    private GameObject starSystemPrefab;

    public StarSystem[] galaxy;

    public float borderPadding;

    public float minDistBetweenSystems;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        galaxy = new StarSystem[numStarSystems];
        GenerateStarSystems();
        RenderGalaxyView();
    }

    private void GenerateStarSystems()
    {
        ShuffleArray(StarSystemNames);

        for (int i = 0; i < numStarSystems; i++)
        {
            int numObjects = Random.Range(1, 14);

            // DANGER: make sure dictionary is always larger than N
            string starName = StarSystemNames[i];

            SpaceObject[] objects = new SpaceObject[numObjects];

            float xMin = (-Camera.main.orthographicSize * Screen.width / Screen.height) + borderPadding;
            float xMax = (Camera.main.orthographicSize * Screen.width / Screen.height) - borderPadding;
            float yMin = -Camera.main.orthographicSize + borderPadding;
            float yMax = Camera.main.orthographicSize - borderPadding;

            for (int j = 0; j < numObjects - 1; j++)
            {
                objects[j] = new Planet(starName + " " + ToRomanNumerals(j + 2), RandomEnumValue<PlanetType>(), Random.Range(25, 325), Random.Range(10, 25));
            }

            Star star = new Star(starName, Random.Range(0.15f, 0.25f), RandomEnumValue<StarType>());

            StarSystem ss = new StarSystem(
                starName,
                //Random.Range(xMin, xMax),
                //Random.Range(yMin, yMax),
                objects,
                star
            );

            galaxy[i] = ss;
        }
    }

    private void RenderGalaxyView()
    {
        float xMin = (-Camera.main.orthographicSize * Screen.width / Screen.height) + borderPadding;
        float xMax = (Camera.main.orthographicSize * Screen.width / Screen.height) - borderPadding;
        float yMin = -Camera.main.orthographicSize + borderPadding;
        float yMax = Camera.main.orthographicSize - borderPadding;

        foreach (StarSystem ss in galaxy)
        {
            bool canSpawn = false;
            while (!canSpawn)
            {
                float x = Random.Range(xMin, xMax);
                float y = Random.Range(yMin, yMax);

                Collider2D nearbyStarSystems = Physics2D.OverlapCircle(new Vector2(x, y), minDistBetweenSystems, LayerMask.GetMask("StarSystems"));

                if (!nearbyStarSystems)
                {
                    canSpawn = true;
                    GameObject starSystemGO = GameObject.Instantiate(starSystemPrefab, new Vector2(x, y), Quaternion.identity);
                    starSystemGO.GetComponent<StarSystemManager>().systemProperties = ss;
                    //starSystemGO.GetComponent<SpriteRenderer>().color = ss.Star.Color;
                    //starSystemGO.transform.localScale = new Vector3(ss.Star.Radius, ss.Star.Radius, ss.Star.Radius);
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
        return num switch
        {
            1 => "I",
            2 => "II",
            3 => "III",
            4 => "IV",
            5 => "V",
            6 => "VI",
            7 => "VII",
            8 => "VIII",
            9 => "IX",
            10 => "X",
            11 => "XI",
            12 => "XII",
            13 => "XIII",
            14 => "XIV",
            15 => "XV",
            _ => "",
        };
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
        return new Vector2(GameManager.Instance.galaxy[i].Xlocation, GameManager.Instance.galaxy[i].Ylocation);
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