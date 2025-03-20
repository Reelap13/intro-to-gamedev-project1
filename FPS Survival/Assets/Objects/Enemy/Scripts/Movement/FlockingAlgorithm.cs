using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingAlgorithm : MonoBehaviour
{
    public static Vector2 Cohesion(EnemyMovement agent, List<EnemyMovement> flock, float neighborRadius, float fovAngle, int leaderMass)
    {
        if (agent.State == EnemyMovementState.LEADER) return Vector2.zero;
        Vector2 centerMass = Vector2.zero;
        Vector2 positionProjection = new(agent.Transform.position.x, agent.Transform.position.z);
        Vector2 forwardDirectionProjection = new(agent.Transform.forward.x, agent.Transform.forward.z);
        int neighborCount = 0;

        foreach (var other in flock)
        {
            if (other.Transform.position == agent.Transform.position) continue;
            Vector2 otherPositionProjection = new(other.Transform.position.x, other.Transform.position.z);

            float dist = Vector2.Distance(positionProjection, otherPositionProjection);

            if (dist < neighborRadius)
            {
                Vector2 directionToOther = (otherPositionProjection - positionProjection).normalized;
                float angle = Vector2.Angle(forwardDirectionProjection, directionToOther); // Use Vector2.Angle
                if (angle <= fovAngle / 2.0f)
                {
                    if(other.State == EnemyMovementState.LEADER)
                    {
                        centerMass += otherPositionProjection * leaderMass;
                        neighborCount += leaderMass;
                    }
                    else
                    {
                        centerMass += otherPositionProjection;
                        neighborCount++;
                    }
                }
            }
        }

        if (neighborCount > 0)
        {
            centerMass /= neighborCount;
            return (centerMass - positionProjection).normalized;
        }

        return Vector2.zero;
    }


    public static Vector2 Alignment(EnemyMovement agent, List<EnemyMovement> flock, float neighborRadius, float fovAngle, int leaderMass)
    {
        if (agent.State == EnemyMovementState.LEADER) return Vector2.zero;
        Vector2 avgVelocity = Vector2.zero;
        int neighborCount = 0;
        Vector2 positionProjection = new(agent.Transform.position.x, agent.Transform.position.z);
        Vector2 forwardDirectionProjection = new(agent.Transform.forward.x, agent.Transform.forward.z);

        foreach (var other in flock)
        {
            if (other.Transform.position == agent.Transform.position) continue;
            Vector2 otherPositionProjection = new(other.Transform.position.x, other.Transform.position.z);

            float dist = Vector2.Distance(positionProjection, otherPositionProjection);

            if (dist < neighborRadius)
            {
                Vector2 directionToOther = (otherPositionProjection - positionProjection).normalized;
                float angle = Vector2.Angle(forwardDirectionProjection, directionToOther);
                if (angle <= fovAngle / 2.0f)
                {
                    Vector2 otherVelocity = new Vector2(other.Transform.forward.x, other.Transform.forward.z);
                    otherVelocity = otherVelocity.normalized;
                    if (other.State == EnemyMovementState.LEADER)
                    {
                        avgVelocity += otherVelocity * leaderMass;
                        neighborCount += leaderMass;
                    }
                    else
                    {
                        avgVelocity += otherVelocity;
                        neighborCount++;
                    }
                }
            }
        }

        if (neighborCount > 0)
        {
            avgVelocity /= neighborCount;
            return (avgVelocity - new Vector2(agent.Transform.forward.x, agent.Transform.forward.z)).normalized;
        }

        return Vector2.zero;
    }

    public static Vector2 Separation(EnemyMovement agent, List<EnemyMovement> flock, float separationDistance, float fovAngle)
    {
        if (agent.State == EnemyMovementState.LEADER) return Vector2.zero;
        Vector2 steerAway = Vector2.zero;
        int neighborCount = 0;
        Vector2 positionProjection = new(agent.Transform.position.x, agent.Transform.position.z);
        Vector2 forwardDirectionProjection = new(agent.Transform.forward.x, agent.Transform.forward.z);

        foreach (var other in flock)
        {
            if (other.Transform.position == agent.Transform.position) continue;
            Vector2 otherPositionProjection = new(other.Transform.position.x, other.Transform.position.z);

            float dist = Vector2.Distance(positionProjection, otherPositionProjection);

            if (dist < separationDistance)
            {
                Vector2 directionToOther = (otherPositionProjection - positionProjection).normalized;
                float angle = Vector2.Angle(forwardDirectionProjection, directionToOther);

                if (angle <= fovAngle / 2.0f)
                {
                    Vector2 diff = (positionProjection - otherPositionProjection);
                    diff /= dist;
                    steerAway += diff;
                    neighborCount++;
                }
            }
        }

        if (neighborCount > 0)
        {
            steerAway /= neighborCount;
            return steerAway.normalized;
        }

        return Vector2.zero;
    }
}
