using UnityEngine;

public class EntityAttackCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
{
    [SerializeField] private AttackDataSO[] attackDataList;   //1
    [SerializeField] private float comboWindow = 0.7f;
    [SerializeField] private StatSO attackSpeedStat;
    [SerializeField] private StatSO meleeDamageStat;

    [SerializeField] private DamageCaster damageCaster;

    private Entity _entity;
    private EntityAnimator _entityAnimator;
    private EntityAnimatorTrigger _animatorTrigger;
    private EntityStat _statCompo;


    private readonly int _attackSpeedHash = Animator.StringToHash("ATTACK_SPEED");
    private readonly int _comboCounterHash = Animator.StringToHash("COMBO_COUNTER");

    private float _attackSpeed = 1f;
    private float _lastAttackTime;

    public bool useMouseDirection;
    public int ComboCounter { get; set; } = 0;
    public float AttackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
        }
    }

    public AttackDataSO GetCurrentAttackData()  //2
    {
        Debug.Assert(attackDataList.Length > ComboCounter, "Combo counter is out of range");
        return attackDataList[ComboCounter];
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _entityAnimator = entity.GetCompo<EntityAnimator>();
        _animatorTrigger = entity.GetCompo<EntityAnimatorTrigger>();
        AttackSpeed = 1f;
        _statCompo = entity.GetCompo<EntityStat>();
        damageCaster.InitCaster(_entity); //오너 설정해주고

    }

    private void OnDestroy()
    {
        _animatorTrigger.OnDamageCastTrigger -= HandleDamageCastTrigger;
    }

    private void HandleDamageCastTrigger()
    {
        AttackDataSO attackData = GetCurrentAttackData();
        Vector3 position = damageCaster.transform.position;
        DamageData data = new DamageData();
        damageCaster.CastDamage(data, position, _entity.transform.forward, GetCurrentAttackData());
    }

    public void Attack()
    {
        bool comboCounterOver = ComboCounter > 0;
        bool comboWindowExhaust = Time.time >= _lastAttackTime + comboWindow;
        if (comboCounterOver || comboWindowExhaust)
            ComboCounter = 0;

        _entityAnimator.SetParam(_comboCounterHash, ComboCounter);
    }

    public void EndAttack()
    {
        ComboCounter++;
        _lastAttackTime = Time.time;
    }

    public void AfterInitialize()
    {
        _animatorTrigger.OnDamageCastTrigger += HandleDamageCastTrigger;
    }

    private void HandleAttackSpeedChange(StatSO stat, float currentValue, float prevValue)
    {
        _attackSpeed = currentValue;
    }
}
