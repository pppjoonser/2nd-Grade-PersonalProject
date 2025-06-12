using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StateChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StateChange", message: "state change to [newValue]", category: "Events", id: "a5f5dd23041e33155d432256e07eb25b")]
public sealed partial class StateChange : EventChannel<CurrentState> { }

