using Content.Server.Antag;
using Content.Shared.Mind.Components;

namespace Content.Server._Scp.Other.CustomBriefing;

public sealed partial class ChaosSleepSpyRuleSystem: EntitySystem
{
    [Dependency] private AntagSelectionSystem _antag = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CustomBriefingComponent, MindAddedMessage>(OnMindAdded);
    }

    private void OnMindAdded(Entity<CustomBriefingComponent> ent, ref MindAddedMessage args)
    {
        if (ent.Comp.IsIssued)
            return;

        ent.Comp.IsIssued = true;

        _antag.SendBriefing(
            ent.Owner,
            Loc.GetString(ent.Comp.BriefingText),
            Color.FromHex(ent.Comp.BriefingColor),
            ent.Comp.BriefingSound
        );
    }
}
