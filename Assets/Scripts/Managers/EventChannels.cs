using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventChannels : MonoBehaviour
{
    [SerializeField] private UnityEvent onBallHitPin = new();

    public event UnityAction OnBallHitPin
    {
        add => onBallHitPin.AddListener(value);
        remove => onBallHitPin.RemoveListener(value);
    }

    public void PublishBallHitPin()
    {
        onBallHitPin.Invoke();
    }
}
