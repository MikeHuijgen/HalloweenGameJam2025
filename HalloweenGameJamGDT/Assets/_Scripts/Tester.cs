using System;
using System.Collections;
using UnityEngine;

public class Tester : InteractTable
{
    public override void Interact(Action action)
    {
        OnInteractComplete = action;
        print($"Interacted with: {gameObject.name}");
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(2f);
        OnInteractComplete();
    }
}
