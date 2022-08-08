using Content.Server.Actions;
using Content.Server.Xeno.Components;
using Content.Shared.Actions;
using Content.Shared.Actions.ActionTypes;
using Content.Shared.Throwing;

namespace Content.Server.Xeno.Systems;

public sealed class XenoEvolutionSystem : EntitySystem
{
    [Dependency] private readonly ActionsSystem _action = default!;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<XenoEvolutionComponent, ComponentStartup>(OnStartup);
    }
    private void OnStartup(EntityUid uid, XenoEvolutionComponent component, ComponentStartup args)
    {
        XenoEvolveActionEvent evolveEvent = new();
        var evolvAction = new InstantAction(){
            Event = evolveEvent,
            Name = "Evolve",
            Description = "Open the Evolve UI",
            EntityIcon = uid,

        };
        _action.AddAction(uid, evolvAction,null);

    }
    public override void Update(float frameTime)
    {
        base.Update(frameTime);
        foreach (var comp in EntityQuery<XenoEvolutionComponent>())
        {
            if (comp.Accumulator > comp.TimeNeededToEvolve)
                continue;
            comp.Accumulator += frameTime;
        }
    }

}

public sealed class XenoEvolveActionEvent : InstantActionEvent {};
