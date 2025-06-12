using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(DamageData damageData, Vector3 position, Vector3 normal, AttackDataSO attackData, Entity owner);
}
