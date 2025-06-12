
using System;
using UnityEngine;

    public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable
    {
        private Entity _entity;
        private EntityActionData _actionData;
        private EntityStat _statCompo;
        private float maxhealth;

        [SerializeField] private StatSO hpStat;
        [SerializeField] private float currentHealth;
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _actionData = entity.GetCompo<EntityActionData>();
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void ApplyDamage(DamageData damage, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO attackData, Entity dealer)
        {
            _actionData.HitPoint = hitPoint;
            _actionData.HitNormal = hitNormal;
            //�˹��� ���߿� ó���Ѵ�.
            //�������� ���߿� ó���Ѵ�.

            currentHealth = Mathf.Clamp(currentHealth - damage.damage, 0, maxhealth);
            if (currentHealth <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
            }

            _entity.OnHitEvent?.Invoke(); //���� ����. ���鷯 ������.
        }

        private void HandleMaxHPChange(StatSO stat, float currentValue, float prevValue)
        {
            float changed = currentValue - prevValue;
            if (changed > 0)
            {
                currentHealth = Mathf.Clamp(currentHealth + changed, 0, maxhealth);
            }
            else
            {
                currentHealth = Mathf.Clamp(currentHealth, 0, maxhealth);
            }
        }
    }