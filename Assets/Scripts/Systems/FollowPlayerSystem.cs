using UnityEngine;
using UnityEngine.AI;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class FollowPlayerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float3 targetPosition = float3.zero;
        Entities.ForEach((Entity entity, ref LocalToWorld transform, ref FollowTargetComponent tag) => {
                targetPosition = transform.Position;
            });

        Entities.ForEach((Entity entity, ref NavMeshAgentComponent agent) => {
                var navMeshAgent = EntityManager.GetComponentObject<NavMeshAgent>(entity);
                if (navMeshAgent != null) {
                    navMeshAgent.SetDestination(targetPosition);
                    EntityManager.SetComponentData(agent.moveEntity, new Translation{ Value = navMeshAgent.transform.position });
                }
            });
    }
}
