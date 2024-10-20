using UnityEngine;
using TMPro;

public class GameLost : MonoBehaviour
{
    private InterfaceManager interfaceManager;

    private void Awake()
    {
        interfaceManager = Finder.InterfaceManager;
    }

    private void Update()
    {
        if (interfaceManager.IsGameLost)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
}
