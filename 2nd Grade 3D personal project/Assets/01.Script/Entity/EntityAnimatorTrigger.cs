using UnityEngine;
using System;

public class EntityAnimatorTrigger : MonoBehaviour, IEntityComponent
{
    public Action OnAnimationEndTrigger;
    public Action<bool> OnRollingStatusChange;
    public Action OnAttackVFXTrigger;
    public Action<bool> OnManualRotationTrigger;
    public Action OnDamageCastTrigger;

    private Entity _entity;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    private void AnimationEnd() //�ż��� �� ��Ÿ���� �ȵȴ�. (�̺�Ʈ �̸��� �����ϰ� ������ ��.)
    {
        OnAnimationEndTrigger?.Invoke();
    }

    private void RollingStart() => OnRollingStatusChange?.Invoke(true);
    private void RollingEnd() => OnRollingStatusChange?.Invoke(false);
    private void PlayerAttackVFX() => OnAttackVFXTrigger?.Invoke();

    private void StartManualRotation() => OnManualRotationTrigger?.Invoke(true);
    private void StopManualRotation() => OnManualRotationTrigger?.Invoke(false);

    private void DamageCast() => OnDamageCastTrigger?.Invoke();
}
