using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolSubscriber : IObserver<ObjPool>
{
    // Set the subscription IDisposable (Unsubscriber)
    void SetSubscription(IDisposable subscription);

    // Set the ObjPool reference
    void SetPool(ObjPool pool);
}
