using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Physics;

public class FreezeVerticalRotationSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle job = Entities.ForEach((ref FreezeVerticalRotationComponent tag, ref PhysicsMass mass) => {
                mass.InverseInertia.xz = new float2(0.0f);
            }).Schedule(inputDeps);

        return job;
    }
}
