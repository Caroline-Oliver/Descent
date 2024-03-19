using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAIMeleeState : CreatureAIState
{
    public double meleeRange = 1;
    public CreatureAIMeleeState (CreatureAI creatureAI) : base(creatureAI){}

    public override void BeginState()
    {
        
    }

    public override void UpdateState()
    {
        if(creatureAI.GetTarget() == null){
            creatureAI.ChangeState(creatureAI.investigateState);
            return;
        }

        if (creatureAI.GetDistanceToTarget() > meleeRange) {
            creatureAI.ChangeState(creatureAI.huntState);
            return;
        }

        // melee attack
    }
}