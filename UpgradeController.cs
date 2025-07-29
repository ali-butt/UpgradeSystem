using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeController
{
    private readonly UpgradeModel model;
    private readonly UpgradeView view;

    public UpgradeController(UpgradeModel model, UpgradeView view)
    {
        this.model = model;
        this.view = view;

        for (int i = 0; i < view.UpgradeItems.Length; i++)
        {
            var item = view.UpgradeItems[i];
            int index = i;
            item.UpgradeButton.onClick.AddListener(() => Upgrade(index));
        }

        view.ResetButton.onClick.AddListener(ResetUpgrades);
        UpdateView();
    }

    private void Upgrade(int index)
    {
        var def = view.UpgradeItems[index].Definition;
        if (model.TryUpgrade(def, out string error))
        {
            AnimateText(view.UpgradeItems[index].LevelText.transform);
        }
        else
        {
            view.ShowError(error);
        }
        UpdateView();
    }

    private void ResetUpgrades()
    {
        var defs = new UpgradeDefinition[view.UpgradeItems.Length];
        for (int i = 0; i < defs.Length; i++) defs[i] = view.UpgradeItems[i].Definition;
        model.ResetAll(defs);
        UpdateView();
    }

    private void UpdateView()
    {
        foreach (var item in view.UpgradeItems)
        {
            float value = model.GetValue(item.Definition);
            int level = model.GetLevel(item.Definition.UpgradeKey);

            item.ValueText.text = item.Definition.IsPercentage
                ? $"{value:F1}{item.Definition.UnitSuffix}"
                : $"{value}{item.Definition.UnitSuffix}";
            item.LevelText.text = $"Lvl {level}";
            item.Slider.value = level;
        }

        view.SkillPointsText.text = $"Skill Points: {model.SkillPoints}";
    }

    private void AnimateText(Transform t)
    {
        t.DOKill();
        t.DOScale(Vector2.one, 0.2f).From(Vector2.zero).SetEase(Ease.OutBack);
        view.Particles.gameObject.SetActive(true);
        view.Particles.transform.position = t.position;
        view.Particles.Play();
    }
}
