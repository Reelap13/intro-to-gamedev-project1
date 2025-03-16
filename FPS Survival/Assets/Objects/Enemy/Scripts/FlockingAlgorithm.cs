using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingAlgorithm : MonoBehaviour
{
    public static Vector2 Cohesion(Transform agent, List<Transform> flock, float neighborRadius, float fovAngle, Transform leader, int leaderMass)
    {
        if (agent == leader) return Vector2.zero;
        Vector2 centerMass = Vector2.zero;
        Vector2 positionProjection = new(agent.position.x, agent.position.z);
        Vector2 forwardDirectionProjection = new(agent.forward.x, agent.forward.z);
        int neighborCount = 0;

        foreach (var other in flock)
        {
            if (other.position == agent.position) continue;
            Vector2 otherPositionProjection = new(other.position.x, other.position.z);

            float dist = Vector2.Distance(positionProjection, otherPositionProjection);

            if (dist < neighborRadius)
            {
                Vector2 directionToOther = (otherPositionProjection - positionProjection).normalized;
                float angle = Vector2.Angle(forwardDirectionProjection, directionToOther); // Use Vector2.Angle
                if (angle <= fovAngle / 2.0f)
                {
                    if(other == leader)
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


    public static Vector2 Alignment(Transform agent, List<Transform> flock, float neighborRadius, float fovAngle, Transform leader, int leaderMass)
    {
        if (agent == leader) return Vector2.zero;
        Vector2 avgVelocity = Vector2.zero;
        int neighborCount = 0;
        Vector2 positionProjection = new(agent.position.x, agent.position.z);
        Vector2 forwardDirectionProjection = new(agent.forward.x, agent.forward.z);

        foreach (var other in flock)
        {
            if (other.position == agent.position) continue;
            Vector2 otherPositionProjection = new(other.position.x, other.position.z);

            float dist = Vector2.Distance(positionProjection, otherPositionProjection);

            if (dist < neighborRadius)
            {
                Vector2 directionToOther = (otherPositionProjection - positionProjection).normalized;
                float angle = Vector2.Angle(forwardDirectionProjection, directionToOther);
                if (angle <= fovAngle / 2.0f)
                {
                    Vector2 otherVelocity = new Vector2(other.GetComponent<Rigidbody>().velocity.x, other.GetComponent<Rigidbody>().velocity.z);
                    otherVelocity = otherVelocity.normalized;
                    if (other == leader)
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
            return (avgVelocity - new Vector2(agent.GetComponent<Rigidbody>().velocity.x, agent.GetComponent<Rigidbody>().velocity.z)).normalized;
        }

        return Vector2.zero;
    }

    public static Vector2 Separation(Transform agent, List<Transform> flock, float separationDistance, float fovAngle, Transform leader)
    {
        if (agent == leader) return Vector2.zero;
        Vector2 steerAway = Vector2.zero;
        int neighborCount = 0;
        Vector2 positionProjection = new(agent.position.x, agent.position.z);
        Vector2 forwardDirectionProjection = new(agent.forward.x, agent.forward.z);

        foreach (var other in flock)
        {
            if (other.position == agent.position) continue;
            Vector2 otherPositionProjection = new(other.position.x, other.position.z);

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
