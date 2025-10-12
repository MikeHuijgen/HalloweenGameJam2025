using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<T>();
        }
        else
        {
            Debug.LogError($"There is already an instance of this mono behaviour on: {gameObject.name}");
            Destroy(this);
        }
    }
}
