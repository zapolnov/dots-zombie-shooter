using Unity.Entities;

[GenerateAuthoringComponent]
public struct FollowPlayerComponent : IComponentData
{
    bool disabled;
}
