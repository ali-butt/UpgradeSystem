using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade System/Upgrade Definition")]
public class UpgradeDefinition : ScriptableObject
{
    public string UpgradeKey;
    public string DisplayName;
    public float BaseValue;
    public float IncrementPerLevel;
    public float MaxLevel = 100;
    public bool IsPercentage;
    public string UnitSuffix;
}
