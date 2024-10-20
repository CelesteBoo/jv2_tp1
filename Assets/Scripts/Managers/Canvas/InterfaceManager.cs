using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    [Header("TextFields")]
    [SerializeField] private int health;
    [SerializeField] private bool isGameLost = false;
    [SerializeField] private bool isGameWon = false;

    public int Health => health;
    public bool IsGameLost => isGameLost;
    public bool IsGameWon => isGameWon;

    private void Awake()
    {
        var eventChannels = Finder.EventChannels;
        eventChannels.OnGameLost += OnGameLost;
        eventChannels.OnGameWon += OnGameWon;
    }

    public void UpdateHealth(int health)
    {
        this.health = health;
    }

    private void OnGameLost()
    {
        isGameLost = true;
    }

    private void OnGameWon()
    {
        isGameWon = true;
    }
}
