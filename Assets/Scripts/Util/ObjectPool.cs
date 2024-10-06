using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField, Min(1)] private int objectCount = 20;

    private void Awake()
    {
        for (int i = 0; i < objectCount; i++)
        {
            var instance = Instantiate(prefab, transform);
            instance.SetActive(false);
        }
    }

    public GameObject Get()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child.activeSelf == false)
            {
                child.transform.parent = null;
                child.SetActive(true);
                return child;
            }
        }
        return null;
    }

    public void Set(Vector3 position)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child.activeSelf == false)
            {
                child.transform.parent = null;
                child.transform.SetPositionAndRotation(position, Quaternion.identity);
                child.SetActive(true);
                return;
            }
        }
    }

    public void Release(GameObject instance)
    {
        instance.SetActive(false);
        instance.transform.parent = transform;
    }
}
