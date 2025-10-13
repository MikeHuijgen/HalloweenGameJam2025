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
        var cursorlLock = new MouseCursorLocker();
        cursorlLock.UnLockCursor();
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(2f);
        OnInteractComplete();
        var cursorlLock = new MouseCursorLocker();
        cursorlLock.LockCursor();
    }
}
