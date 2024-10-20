using UnityEngine;
using TMPro;

public class Interface : MonoBehaviour
{
    private InterfaceManager interfaceManager;
    private TMP_Text text;

    private void Awake()
    {
        interfaceManager = Finder.InterfaceManager;
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.text = interfaceManager.Health.ToString();
    }
}
