using TMPro;
using UnityEngine;

public class GameWon : MonoBehaviour
{
    private InterfaceManager interfaceManager;

    private void Awake()
    {
        interfaceManager = Finder.InterfaceManager;
    }

    private void Update()
    {
        if (interfaceManager.IsGameWon)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
}
