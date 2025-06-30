using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimation : GenEnemy
{
    public override void UpdateBehavior() 
    {
        animationMachine.playAnimation("Stagger", false, true, this);

    }
}
