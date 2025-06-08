using UnityEngine;

[CreateAssetMenu(fileName = "StateData", menuName = "SO/StateData")]
public class StateDataSO : ScriptableObject
{
    public string stateName;
    public string className;
    public string animParamName;
    //hash는 퍼블릭으로 안하면 저장이 안되서 런타임에서 오류생기니 주의
    public int animationHash;

    private void OnValidate()//에셋의 변화가 생길때
    {
        animationHash = Animator.StringToHash(animParamName);
    }
}
