using UnityEngine;
using System;

public class UpgradeModel
{
    public int SkillPoints
    {
        get => PlayerPrefs.GetInt(UpgradeKeys.SkillPoints, 0);
        set => PlayerPrefs.SetInt(UpgradeKeys.SkillPoints, value);
    }

    public int GetLevel(string key) => PlayerPrefs.GetInt(key, 0);
    public void SetLevel(string key, int level) => PlayerPrefs.SetInt(key, level);

    public float GetValue(UpgradeDefinition def)
    {
        int level = GetLevel(def.UpgradeKey);
        return def.BaseValue + def.IncrementPerLevel * level;
    }

    public bool TryUpgrade(UpgradeDefinition def, out string error)
    {
        error = null;
        int level = GetLevel(def.UpgradeKey);
        if (level >= def.MaxLevel) {
            error = "Maximum level reached.";
            return false;
        }
        if (SkillPoints <= 0) {
            error = "Not enough skill points.";
            return false;
        }

        SetLevel(def.UpgradeKey, level + 1);
        SkillPoints--;
        return true;
    }

    public void ResetAll(UpgradeDefinition[] defs)
    {
        int refunded = 0;
        foreach (var def in defs)
        {
            refunded += GetLevel(def.UpgradeKey);
            SetLevel(def.UpgradeKey, 0);
        }
        SkillPoints += refunded;
    }
}
