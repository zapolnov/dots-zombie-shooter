using Unity.Entities;

[GenerateAuthoringComponent]
public struct BulletPrefabComponent : IComponentData
{
    public Entity prefab;
    public float speed;
}
