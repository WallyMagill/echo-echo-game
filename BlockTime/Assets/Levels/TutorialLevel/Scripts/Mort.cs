using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mort : GenEnemy
{
    public override void UpdateBehavior()
    {
        animationMachine.playAnimation("Walk", true, false, this);
        stateMachine.TrySetState(patrolState, this);
    }
}
