using Content.Server.Actions;
using Content.Server.Xeno.Components;
using Content.Shared.Actions;
using Content.Shared.Throwing;

namespace Content.Server.Xeno.Systems;

public sealed class XenoPlasmaSystem : EntitySystem
{
    [Dependency] private readonly ActionsSystem _action = default!;

    [Dependency] private readonly ThrowingSystem _throwingSystem = default!;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<XenoPlasmaComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<XenoPlasmaComponent, XenoPounceEvent>(OnXenoPounce);
        SubscribeLocalEvent<XenoPlasmaComponent, XenoFlingEvent>(OnXenoFling);


    }
    private void OnStartup(EntityUid uid, XenoPlasmaComponent component, ComponentStartup args)
    {
        component.CurrentPlasma = component.MaxPlasma;
        if(component.InstantActions != null)
        {
            _action.AddActions(uid, component.InstantActions, null);
        }
        if(component.WorldTargetActions != null)
        {
            _action.AddActions(uid, component.WorldTargetActions, null);
        }
        if(component.EntityTargetActions != null)
        {
            _action.AddActions(uid, component.EntityTargetActions, null);
        }
    }
    private void OnXenoPounce(EntityUid uid, XenoPlasmaComponent component, XenoPounceEvent args)
    {
        if(args.Handled)
            return;

        if(component.CurrentPlasma < args.PlasmaCost)
            return;

        var fieldDir = Transform(component.Owner).WorldPosition;
        var playerDir = args.Target.Position;
        var dirrer = playerDir - fieldDir;

        _throwingSystem.TryThrow(uid, dirrer, 99999);

    }
    private void OnXenoFling(EntityUid uid, XenoPlasmaComponent component, XenoFlingEvent args)
    {
        if(args.Handled)
            return;
        if(component.CurrentPlasma < args.PlasmaCost)
            return;
        var fieldDir = Transform(component.Owner).WorldPosition;
        var playerDir = Transform(args.Target).WorldPosition;
        var thingthong = playerDir - fieldDir;
        var whatever = thingthong.ToAngle();
        var direction = whatever.ToVec();

        _throwingSystem.TryThrow(args.Target, direction*5, 10);

    }
}


public sealed class XenoPounceEvent : WorldTargetActionEvent
{
    [DataField("plasmaCost")]
    public int PlasmaCost = 0;

}

public sealed class XenoFlingEvent : EntityTargetActionEvent
{
    [DataField("plasmaCost")]
    public int PlasmaCost = 0;
}
