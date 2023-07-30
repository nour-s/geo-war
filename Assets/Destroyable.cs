using System;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public Action OnDestroyed { get; internal set; }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
