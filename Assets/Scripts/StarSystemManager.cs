using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarSystemManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public StarSystem systemProperties;
    public GameObject textGameObject;
    public float textVerticalOffset;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.color = systemProperties.Star.Color;
        // place the star system's name directly under it.
        Text nameText = textGameObject.GetComponent<Text>();
        nameText.text = systemProperties.Name;
        Vector3 starSystemPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textGameObject.transform.position = new Vector3(starSystemPosition.x, starSystemPosition.y + textVerticalOffset, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.selectedStarSystem = systemProperties;
        SceneManager.LoadScene("SystemView");
        GameManager.Instance.RenderSystemView();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        spriteRenderer.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.color = systemProperties.Star.Color;
    }
}
