using UnityEngine;

[CreateAssetMenu(fileName = "GameInformation", menuName = "GameInformation", order = 0)]
public class GameInformationSO : ScriptableObject
{
    [field: SerializeField]
    public int CurrentLevel { get; set; } = 0;
}