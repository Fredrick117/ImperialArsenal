using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject planetPrefab;

    public static GameManager Instance;

    private Planet[] Planets;

    private Star star;

    [SerializeField] private const int NUM_PLANETS = 4;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeStarSystem();
        RenderSystemView();
    }

    private void InitializeStarSystem()
    {
        ShuffleArray(StarSystemNames);
        Planets = new Planet[NUM_PLANETS];

        string starName = StarSystemNames[Random.Range(0, StarSystemNames.Length)];

        float distanceFromStar = 0.5f;
        for (int i = 0; i < NUM_PLANETS; i++)
        {
            Vector2 randomPointOnCircle = Random.insideUnitCircle.normalized * distanceFromStar;
            Planets[i] = new Planet(starName + " " + ToRomanNumerals(i + 2), RandomEnumValue<PlanetType>(), randomPointOnCircle.x, randomPointOnCircle.y);
            distanceFromStar *= 2;
        }

        star = new(starName, Random.Range(0.15f, 0.25f), RandomEnumValue<StarType>());
    }

    public void RenderSystemView()
    {
        foreach (Planet p in Planets)
        {
            GameObject planet = GameObject.Instantiate(planetPrefab, new Vector2(p.X, p.Y), Quaternion.identity);
            planet.GetComponent<PlanetManager>().SetPlanetData(p);
        }
    }

    /// <summary>
    /// Selects a random element from an enumeration.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
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
        "Korolis",
        "Vovio",
        "Ovhil",
        "Cassio",
        "Irle",
        "Darius",
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