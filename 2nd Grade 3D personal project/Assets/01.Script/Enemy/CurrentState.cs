using System;
using Unity.Behavior;

[BlackboardEnum]
public enum CurrentState
{
    Idle,
	Patrol,
	Chase,
	Attack,
	Hit,
	Dead
}
