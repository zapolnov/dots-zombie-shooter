using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics.Systems;
using Unity.Physics;

public class CollisionEventSystem : JobComponentSystem
{
    struct CollisionEventSystemJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<BulletComponent> bulletRef;
        public ComponentDataFromEntity<HealthComponent> healthRef;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity hitEntity, bulletEntity;
            if (bulletRef.HasComponent(triggerEvent.EntityA)) {
                hitEntity = triggerEvent.EntityB;
                bulletEntity = triggerEvent.EntityA;
            } else if (bulletRef.HasComponent(triggerEvent.EntityB)) {
                hitEntity = triggerEvent.EntityA;
                bulletEntity = triggerEvent.EntityB;
            } else
                return;

            var bullet = bulletRef[bulletEntity];
            bullet.destroyed = true;
            bulletRef[bulletEntity] = bullet;

            if (healthRef.HasComponent(hitEntity)) {
                var health = healthRef[hitEntity];
                health.value--;
                healthRef[hitEntity] = health;
            }
        }
    }

    BuildPhysicsWorld buildPhysicsWorldSystem;
    StepPhysicsWorld stepPhysicsWorld;
    EndSimulationEntityCommandBufferSystem endSimulationCommandBuffer;

    protected override void OnCreate()
    {
        buildPhysicsWorldSystem = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        endSimulationCommandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new CollisionEventSystemJob();
        job.bulletRef = GetComponentDataFromEntity<BulletComponent>(isReadOnly: false);
        job.healthRef = GetComponentDataFromEntity<HealthComponent>(isReadOnly: false);
        var jobResult = job.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorldSystem.PhysicsWorld, inputDeps);

        var commandBuffer = endSimulationCommandBuffer.CreateCommandBuffer().AsParallelWriter();
        var result = Entities.ForEach((Entity entity, int entityInQueryIndex, ref BulletComponent bullet) => {
                if (bullet.destroyed)
                    commandBuffer.DestroyEntity(entityInQueryIndex, entity);
            }).Schedule(jobResult);

        endSimulationCommandBuffer.AddJobHandleForProducer(result);
        return result;
    }
}
