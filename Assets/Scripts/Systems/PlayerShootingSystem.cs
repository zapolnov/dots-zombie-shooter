using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class PlayerShootingSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (!Input.GetButtonDown("Fire1"))
            return;

        Entities.ForEach((Entity entity, ref BulletPrefabComponent bulletPrefab) => {
                var shooter = EntityManager.GetComponentObject<Shooter>(entity);
                if (shooter == null)
                    Debug.LogError("BulletPrefabComponent is missing Shooter component.");
                else {
                    Entity bullet = EntityManager.Instantiate(bulletPrefab.prefab);
                    EntityManager.SetComponentData(bullet, new Translation{ Value = shooter.gunHole.position });
                    EntityManager.SetComponentData(bullet, new Rotation{ Value = shooter.gunHole.rotation });
                    EntityManager.AddComponentData(bullet, new BulletComponent{ speed = shooter.gunHole.forward * bulletPrefab.speed });
                }
            });
    }
}
