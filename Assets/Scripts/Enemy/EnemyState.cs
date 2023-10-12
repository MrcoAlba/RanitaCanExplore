using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController enemyController;
    public List<Transition> transitions;
    public EnemyState(EnemyController enemyController){
        this.enemyController = enemyController;
        transitions = new List<Transition>();
    }
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnFinish();
}
