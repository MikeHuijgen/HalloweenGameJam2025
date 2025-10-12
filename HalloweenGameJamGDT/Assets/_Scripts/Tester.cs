using System;
using UnityEngine;

public class Tester : MonoBehaviour, IInteractTable
{
    public void Interact(Action action)
    {
        print($"Interacted with: {gameObject.name}");
        action();
    }
}
