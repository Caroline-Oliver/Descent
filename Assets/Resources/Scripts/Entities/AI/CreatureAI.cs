using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Aoiti.Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.DefaultInputActions;

public class CreatureAI : MonoBehaviour
{
    public Creature pilotedCreature;
    public Creature targetCreature;

    [Header("Config")]
    public LayerMask obstacles;
    public float sightDistance = 5;

    [Header("Pathfinding")]
    Pathfinder<Vector2> pathfinder;
    [SerializeField] float gridSize = 1f;

    [Header("Combat")]
    [SerializeField] public static double meleeRange = 2f;

    

    CreatureAIState currentState;
    public CreatureAIHuntState huntState{get; private set;}
    public CreatureAIPatrolState patrolState{get; private set;}
    public CreatureAIInvestigateState investigateState{get; private set;}
    public CreatureAIMeleeState meleeState{get; private set;}

    public void ChangeState(CreatureAIState newState) {
        currentState = newState;
        newState.BeginState();
    }

    void Start()
    {
        huntState = new CreatureAIHuntState(this);
        patrolState = new CreatureAIPatrolState(this);
        investigateState = new CreatureAIInvestigateState(this);
        meleeState = new CreatureAIMeleeState(this);

        ChangeState(patrolState);

        pathfinder = new Pathfinder<Vector2>(GetDistance,GetNeighbourNodes,1000);
    }


    void FixedUpdate()
    {
        try {
            GetTarget();
        } catch {
            targetCreature = null;
        }
        
        currentState.UpdateStateBase(); //work the current state

    }

    public void SetColor(Color color) {
        pilotedCreature.GetComponent<SpriteRenderer>().color = color;
    }

    public Creature GetTarget() {
        return targetCreature;
    }

    public double GetDistanceToTarget() {
        float dx = Math.Abs(transform.position.x - targetCreature.transform.position.x);
        float dy = Math.Abs(transform.position.y - targetCreature.transform.position.y);

        double distance = Math.Sqrt(Math.Pow(dx,2) + Math.Pow(dy,2));

        return distance;
    }

    //pathfinding
    public float GetDistance(Vector2 A, Vector2 B)
    {
        return (A - B).sqrMagnitude; //Uses square magnitude to lessen the CPU time.
    }

    Dictionary<Vector2,float> GetNeighbourNodes(Vector2 pos)
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
        for (int i=-1;i<2;i++)
        {
            for (int j=-1;j<2;j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j)*gridSize;
                if (!Physics2D.Linecast(pos,pos+dir, obstacles))
                {
                    neighbours.Add(GetClosestNode( pos + dir), dir.magnitude);
                }
            }

        }
        return neighbours;
    }

    //find the closest spot on the grid to begin our pathfinding adventure
    Vector2 GetClosestNode(Vector2 target){
        return new Vector2(Mathf.Round(target.x/gridSize)*gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    public void GetMoveCommand(Vector2 target, ref List<Vector2> path) //passing path with ref argument so original path is changed
    {
        path.Clear();
        Vector2 closestNode = GetClosestNode(pilotedCreature.transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
        {
            path.Add(target); //add the final position as our last stop
        }
    }

    //simple wrapper to pathfind to our target
    public void GetTargetMoveCommand(ref List<Vector2> path){
        GetMoveCommand(targetCreature.transform.position, ref path);
    }
}