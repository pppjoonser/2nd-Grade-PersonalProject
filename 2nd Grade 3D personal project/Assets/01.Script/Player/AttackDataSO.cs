using UnityEngine;

[CreateAssetMenu(fileName = "AttackDataSO", menuName = "SO/AttackDataSO")]
public class AttackDataSO : ScriptableObject
{
    public string attackName;
    public float movementPower;
    public float damageMultiplier = 1f;
    public float damageIncrease = 0;
    public bool isPowerAttack = false;

    public float knockBackForce;
    public float knockBackDuration;

    private void OnEnable()
    {
        attackName = this.name;
    }
}
