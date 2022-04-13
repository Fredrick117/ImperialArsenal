using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneRenderer : MonoBehaviour
{
    // used for both planets and stars
    public Sprite celestialObjSprite;
    RectTransform mapContainer;
    StarSystem currentStarSystem;

    private void Awake()
    {
        currentStarSystem = GameManager.Instance.selectedStarSystem;
        mapContainer = GameObject.Find("mapContainer").GetComponent<RectTransform>();

        // render sun in middle of scene (ignore system type for now)
        GameObject star = new GameObject("Star", typeof(Image));
        star.transform.SetParent(mapContainer);
        Image img = star.GetComponent<Image>();
        img.sprite = celestialObjSprite;
        img.color = Color.yellow;

        RectTransform starRT = star.GetComponent<RectTransform>();
        starRT.anchoredPosition = Vector2.zero;
        starRT.sizeDelta = new Vector2(25, 25);

        print(currentStarSystem.Planets.Count);
        foreach (Planet p in currentStarSystem.Planets)
        {
            GameObject planet = new GameObject("Planet", typeof(Image));
            planet.transform.SetParent(mapContainer);
            Image planetImg = planet.GetComponent<Image>();
            planetImg.sprite = celestialObjSprite;

            print(p.PlanetType);

            switch (p.PlanetType) 
            {
                case PlanetType.Arctic:
                    planetImg.color = new Color32(200, 219, 250, 255);
                    break;
                case PlanetType.Lava:
                    planetImg.color = new Color32(255, 80, 36, 255);
                    break;
                case PlanetType.Terrestrial:
                    planetImg.color = new Color32(72, 150, 74, 255);
                    break;
                case PlanetType.Desert:
                    planetImg.color = new Color32(235, 214, 141, 255);
                    break;
                case PlanetType.Ocean:
                    planetImg.color = new Color32(24, 69, 122, 255);
                    break;
                case PlanetType.GasGiant:
                    planetImg.color = new Color(147, 83, 212, 255);
                    break;
            }

            RectTransform planetRT = planet.GetComponent<RectTransform>();
            planetRT.anchoredPosition = Random.insideUnitCircle.normalized * p.DistanceFromStar;
            planetRT.sizeDelta = new Vector2(10, 10);

            // TODO: draw the planet's orbit

        }
    }
}
