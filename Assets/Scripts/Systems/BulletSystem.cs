using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;

public class BulletSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle job = Entities.ForEach((ref BulletComponent bullet, ref PhysicsVelocity velocity) => {
                velocity.Linear = bullet.speed;
            }).Schedule(inputDeps);

        return job;
    }
}
