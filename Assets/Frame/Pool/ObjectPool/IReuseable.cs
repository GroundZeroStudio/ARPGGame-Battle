using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReuseable
{
    bool CanReuse();

    void OnReuse(Vector3 position, Vector3 forward);
}
