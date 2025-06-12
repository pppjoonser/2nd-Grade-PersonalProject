using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChangeClipAction", story: "[MainAnimator] change [oldClip] to [newClip]", category: "Action", id: "1913a4e796e10b84d9878a06b82d92ed")]
public partial class ChangeClipAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimator> MainAnimator;
    [SerializeReference] public BlackboardVariable<string> OldClip;
    [SerializeReference] public BlackboardVariable<string> NewClip;

    protected override Status OnStart()
    {
        int oldHash = Animator.StringToHash(OldClip.Value);
        int newHash = Animator.StringToHash(NewClip.Value);
        MainAnimator.Value.SetParam(oldHash, false);
        MainAnimator.Value.SetParam(newHash, true);

        OldClip.Value = NewClip.Value;
        return Status.Success;
    }
}

