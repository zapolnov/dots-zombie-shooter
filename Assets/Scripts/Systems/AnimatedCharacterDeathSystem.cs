using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

public class AnimatedCharacterDeathSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref AnimatedCharacterComponent character, ref HealthComponent health) => {
                var animator = EntityManager.GetComponentObject<Animator>(character.animatorEntity);
                if (health.value <= 0) {
                    animator.SetTrigger("die");
                    EntityManager.RemoveComponent<HealthComponent>(entity);
                }
            });
    }
}
