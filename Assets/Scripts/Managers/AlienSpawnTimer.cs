using UnityEngine;

public class AlienSpawnTimer : MonoBehaviour
{
    [SerializeField, Tooltip("In seconds."), Min(0)] private float delay = 1.5f;
    [SerializeField] private GameObject[] portals;
    [SerializeField] private bool isGameOver = false;


    private Awaitable routine;

    private void Awake()
    {
        var eventChannels = Finder.EventChannels;
        eventChannels.OnGameLost += OnGameLost;
    }

    private void OnEnable()
    {
        routine = SpawningRoutine();
    }

    private void OnDisable()
    {
        routine.Cancel();
    }

    private async Awaitable SpawningRoutine()
    {
        while (isActiveAndEnabled)
        {
            if (!ArePortalsActive())
            {
                isGameOver = true;
                Finder.EventChannels.PublishGameWon();
            }

            if (isGameOver)
            {
                gameObject.SetActive(false);
                return;
            }

            GameObject portal;
            do
            {
                var index = Random.Range(0, portals.Length);
                portal = portals[index];
            } while (portal.activeSelf == false);
            
            portal.GetComponent<AlienSpawner>().SpawnAlien();

            await Awaitable.WaitForSecondsAsync(delay);
        }
    }

    private void OnGameLost()
    {
        isGameOver = true;
    }

    private bool ArePortalsActive()
    {
        int nbActivePortals = 0;
        for (int i = 0; i < portals.Length; i++)
        {
            if (portals[i].activeSelf)
                nbActivePortals++;
        }

        if (nbActivePortals > 0)
            return true;
        return false;
    }
}
