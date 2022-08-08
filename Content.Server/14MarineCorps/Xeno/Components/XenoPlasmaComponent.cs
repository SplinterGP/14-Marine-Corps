using Content.Shared.Actions.ActionTypes;

namespace Content.Server.Xeno.Components;

[RegisterComponent]
public sealed class XenoPlasmaComponent : Component
{
    [DataField("maxPlasma", required: true)]
    public int MaxPlasma = 0;

    public int CurrentPlasma = 0;

    [DataField("worldTargetActions")]
    public List<WorldTargetAction>? WorldTargetActions;

    [DataField("entityTargetActions")]
    public List<EntityTargetAction>? EntityTargetActions;

    [DataField("instantActions")]
    public List<InstantAction>? InstantActions;
}
