using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public class PlayerMovementSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        float2 input = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Entities.ForEach((ref PlayerComponent player, ref LocalToWorld transform, ref PhysicsVelocity velocity) => {
                float3 dir = transform.Forward * input.y * player.movementSpeed * deltaTime;
                velocity.Linear += new float3(dir.x, 0.0f, dir.z);
                velocity.Angular = new float3(0.0f, input.x * player.rotationSpeed * deltaTime, 0.0f);
            });
    }
}
