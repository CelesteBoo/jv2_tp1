using UnityEngine;
using UnityEngine.Events;

public class EventChannels : MonoBehaviour
{
    [SerializeField] private UnityEvent onGameWon = new();
    [SerializeField] private UnityEvent onGameLost = new();

    public event UnityAction OnGameWon
    {
        add => onGameWon.AddListener(value);
        remove => onGameWon.RemoveListener(value);
    }

    public event UnityAction OnGameLost
    {
        add => onGameLost.AddListener(value);
        remove => onGameLost.RemoveListener(value);
    }

    public void PublishGameWon()
    {
        onGameWon.Invoke();
    }

    public void PublishGameLost()
    {
        onGameLost.Invoke();
    }
}
