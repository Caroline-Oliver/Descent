using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAIMeleeState : CreatureAIState
{
    private float timestampOfRelease;
    public CreatureAIMeleeState (CreatureAI creatureAI) : base(creatureAI){}

    public override void BeginState()
    {
        // creatureAI.SetColor(Color.red);
    }

    public override void UpdateState()
    {
        if (timer < timestampOfRelease) {
            // continue;
        }
        else if(creatureAI.GetTarget() == null){
            creatureAI.ChangeState(creatureAI.investigateState);
        }

        else if (creatureAI.GetDistanceToTarget() > CreatureAI.meleeRange) {
            creatureAI.ChangeState(creatureAI.huntState);
        }
        
        else {
            Debug.Log("About to attack...");

            creatureAI.pilotedCreature.TurnCreatureToward(creatureAI.targetCreature.transform.position);
            creatureAI.pilotedCreature.Attack();
            timestampOfRelease = timer + creatureAI.pilotedCreature.attackCoolDown;
        }
    }
}