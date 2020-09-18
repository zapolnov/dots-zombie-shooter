using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct AnimatedCharacterComponent : IComponentData
{
    public Entity animatorEntity;
}
