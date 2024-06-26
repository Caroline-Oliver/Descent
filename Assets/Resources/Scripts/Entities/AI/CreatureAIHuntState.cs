using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAIHuntState : CreatureAIState
{

    public CreatureAIHuntState(CreatureAI creatureAI) : base(creatureAI){}


    public override void BeginState()
    {
        // orange
        // creatureAI.SetColor(new Color(1.0f, 0.64f, 0.0f));
    }

    public override void UpdateState()
    {
        if(creatureAI.GetTarget() != null){
            if (creatureAI.GetDistanceToTarget() <= CreatureAI.meleeRange) {
                creatureAI.ChangeState(creatureAI.meleeState);
            }
            else {
                creatureAI.pilotedCreature.MoveCreatureToward(creatureAI.GetTarget().transform.position);
            }
        }else{
            creatureAI.ChangeState(creatureAI.investigateState);
        }

    }
}