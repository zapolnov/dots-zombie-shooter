using Unity.Entities;

[GenerateAuthoringComponent]
public struct PlayerComponent : IComponentData
{
    public float movementSpeed;
    public float rotationSpeed;
}
