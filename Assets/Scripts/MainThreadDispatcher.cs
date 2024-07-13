#if USES_WEBSOCKET
using System.Collections.Generic;
using UnityEngine;
public class MainThreadDispatcher : MonoBehaviour
{
    private static MainThreadDispatcher instance;

    private Queue<System.Action> actionQueue = new Queue<System.Action>();

    public static MainThreadDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MainThreadDispatcher>();
                if (instance == null)
                {
                    GameObject go = new GameObject("MainThreadDispatcher");
                    instance = go.AddComponent<MainThreadDispatcher>();
                }
            }
            return instance;
        }
    }

    public void QueueOnMainThread(System.Action action)
    {
        lock (actionQueue)
        {
            actionQueue.Enqueue(action);
        }
    }

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        lock (actionQueue)
        {
            while (actionQueue.Count > 0)
            {
                actionQueue.Dequeue().Invoke();
            }
        }
    }
}
#endif