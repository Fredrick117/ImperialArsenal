using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ButtonManager : MonoBehaviour
{
    public void CreateStarSystemButtonPressed()
    {
        
    }

    public void CreateStarButtonPressed()
    {
        // TODO: if scene is galaxy view, then...
        //GameManager.Instance.starSystemsList
    }

    public void CreatePlanetButtonPressed()
    {
        GameManager.Instance.starSystemsList.Find(x => x.systemName == GameManager.Instance.currentLoadedSystem.systemName);
    }
}
