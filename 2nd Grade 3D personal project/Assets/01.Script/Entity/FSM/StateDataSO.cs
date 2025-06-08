using UnityEngine;

[CreateAssetMenu(fileName = "StateData", menuName = "SO/StateData")]
public class StateDataSO : ScriptableObject
{
    public string stateName;
    public string className;
    public string animParamName;
    //hash�� �ۺ����� ���ϸ� ������ �ȵǼ� ��Ÿ�ӿ��� ��������� ����
    public int animationHash;

    private void OnValidate()//������ ��ȭ�� ���涧
    {
        animationHash = Animator.StringToHash(animParamName);
    }
}
