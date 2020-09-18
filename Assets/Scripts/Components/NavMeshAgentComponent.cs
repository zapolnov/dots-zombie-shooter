using Unity.Entities;

[GenerateAuthoringComponent]
public struct NavMeshAgentComponent : IComponentData
{
    public Entity moveEntity;
}
