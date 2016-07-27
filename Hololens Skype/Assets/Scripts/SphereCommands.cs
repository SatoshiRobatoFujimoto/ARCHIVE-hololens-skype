using UnityEngine;

public class SphereCommands : MonoBehaviour
{
    public GameObject cardCollection;

    void Start()
    {
        Renderer[] rendererObjects = cardCollection.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < rendererObjects.Length; i++)
        {
            rendererObjects[i].enabled = false;
        }
    }
    
    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        Renderer[] renderObjects = cardCollection.GetComponentsInChildren<Renderer>();

        if (!renderObjects[0].enabled)
            cardCollection.GetComponent<FadeObjectInOut>().FadeIn();
    }
}
