using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarSystemManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public StarSystem systemProperties;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.color = systemProperties.Star.Color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.selectedStarSystem = systemProperties;
        SceneManager.LoadScene("SystemView");
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
