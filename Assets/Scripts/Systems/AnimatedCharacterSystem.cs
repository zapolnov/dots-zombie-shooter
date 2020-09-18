using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

public class AnimatedCharacterSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref AnimatedCharacterComponent character, ref PhysicsVelocity velocity) => {
                var animator = EntityManager.GetComponentObject<Animator>(character.animatorEntity);
                animator.SetFloat("speed", math.length(velocity.Linear));
            });
    }
}
