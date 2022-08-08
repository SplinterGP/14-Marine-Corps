

using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.List;

namespace Content.Server.Xeno.Components;

[RegisterComponent]
public sealed class XenoEvolutionComponent : Component
{
    public float Accumulator = 0f;
    [DataField("timeNeededToEvolve", required: true)]
    public float TimeNeededToEvolve = 0f;

    [DataField("entitiesToEvolve", customTypeSerializer: typeof(PrototypeIdListSerializer<EntityPrototype>))]
    public List<string> EntityIds { get; } = new List<string>();
}
