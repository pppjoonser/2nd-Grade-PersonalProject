using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/AnimationChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "AnimationChange", message: "Change to [newAnimation]", category: "Events", id: "e07884464872ab4634e8165caa94fb88")]
public sealed partial class AnimationChange : EventChannel<string> { }

