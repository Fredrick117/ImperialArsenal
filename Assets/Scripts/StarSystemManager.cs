using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarSystemManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public StarSystem systemProperties;
    Image sprite;
    Color initColor;

    private void Awake()
    {
        sprite = GetComponent<Image>();
        initColor = sprite.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.selectedStarSystem = systemProperties;
        SceneManager.LoadScene("SystemView");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sprite.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sprite.color = initColor;
    }
}
