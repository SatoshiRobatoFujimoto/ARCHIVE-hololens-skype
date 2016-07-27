using UnityEngine;
using System.Collections;

public class CardCommands : MonoBehaviour {

    public GameObject alertCard;

    void Start()
    {
        Renderer[] rendererObjects = alertCard.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < rendererObjects.Length; i++)
        {
            rendererObjects[i].enabled = false;
        }
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        Renderer[] renderObjects = alertCard.GetComponentsInChildren<Renderer>();
        
        if (!renderObjects[0].enabled)
            alertCard.GetComponent<FadeObjectInOut>().FadeIn();
    }
}
