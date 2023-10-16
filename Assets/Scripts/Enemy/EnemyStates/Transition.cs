using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    public Func<bool> IsValid {private set; get;}
    public Func<EnemyState> GetNextState {private set; get;}
    public Transition
    (
        Func<bool> isValid,
        Func<EnemyState> getNextState
    )
    {
        IsValid = isValid;
        GetNextState = getNextState;
    }
}
