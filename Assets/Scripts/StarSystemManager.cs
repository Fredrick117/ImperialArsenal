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
    private Color32 initColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initColor = spriteRenderer.color;
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
        spriteRenderer.color = initColor;
    }
}
