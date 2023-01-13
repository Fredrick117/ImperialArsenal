using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public GameObject objectSelectionCanvas;
    public GameObject galaxyObjectSelectionPanel;
    public GameObject systemObjectSelectionPanel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // RMB to select which object to place (star or planet?)
        if (Input.GetMouseButtonDown(1))
        {
            if (SceneManager.GetActiveScene().name == "GalaxyView")
            {
                if (galaxyObjectSelectionPanel.activeSelf != true)
                    galaxyObjectSelectionPanel.SetActive(true);

                float adjustmentLengthX = galaxyObjectSelectionPanel.GetComponent<RectTransform>().rect.width / 2;
                float adjustmentLengthY = galaxyObjectSelectionPanel.GetComponent<RectTransform>().rect.height / 2;

                galaxyObjectSelectionPanel.transform.position = new Vector2(Input.mousePosition.x + adjustmentLengthX, Input.mousePosition.y - adjustmentLengthY);
            }
            else if (SceneManager.GetActiveScene().name == "SystemView")
            {
                if (systemObjectSelectionPanel.activeSelf != true)
                    systemObjectSelectionPanel.SetActive(true);

                float adjustmentLengthX = systemObjectSelectionPanel.GetComponent<RectTransform>().rect.width / 2;
                float adjustmentLengthY = systemObjectSelectionPanel.GetComponent<RectTransform>().rect.height / 2;

                systemObjectSelectionPanel.transform.position = new Vector2(Input.mousePosition.x + adjustmentLengthX, Input.mousePosition.y - adjustmentLengthY);
            }
        }
    }

    //private void PlaceSolarSystem()
    //{
    //    Vector2 spawnPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

    //    GameObject.Instantiate(solarSystemPrefab, spawnPosition, Quaternion.identity);
    //}
}
