using UnityEngine;

[CreateAssetMenu(fileName = "GameInformation", menuName = "GameInformation")]
public class GameInformationSO : ScriptableObject
{
    [field: SerializeField]
    public int CurrentLevel { get; set; } = 0;
}