using System;
using TycoonSystem.Tools;
using UnityEngine;

namespace TycoonSystem.Core
{
    public abstract class AbstractTycoonElement : ScriptableObject, IGenerator, IUpgradeable
    {
        [SerializeField] protected string m_elementName;
        [SerializeField] protected AbstractCurrency m_generatingCurrency;
        [SerializeField] protected AbstractCurrency m_upgradeCurrency;
        [SerializeField] protected float m_baseUpgradeCost;
        [SerializeField] protected float m_baseGenerationValue;
        [SerializeField] protected float m_baseGenerationTime;

        protected int m_currentLevel = 0;
        protected float m_generationValueMultiplier = 1;
        protected float m_generationTimeMultiplier = 1;

        public string ElementName => m_elementName;

        // Upgradeable
        public int CurrentLevel => m_currentLevel;
        public float BaseUpgradeCost => m_baseUpgradeCost;
        public AbstractCurrency UpgradeCurrency => m_upgradeCurrency;

        //Generator
        public AbstractCurrency GeneratingCurrency => m_generatingCurrency;
        public float GeneratingPerProc => m_baseGenerationValue * m_generationValueMultiplier * CurrentLevel;
        public float GeneratingTime => m_baseGenerationTime * m_generationTimeMultiplier;
        public float GeneratingPerSecond => GeneratingPerProc / GeneratingTime;

        protected const float COST_SCALING = 1.15f;

        public event Action onNewLevel;

        public virtual void UpgradeBy(int p_levels)
        {
            float l_cost = this.GetCostForNLevels(p_levels);
            if (!m_upgradeCurrency.CheckAndConsumeFunds(l_cost))
            {
                this.TriggerFail($"Not enough funds to upgrade {this.ElementName} by {p_levels} levels");
                return;
            }
            m_currentLevel += p_levels;
            onNewLevel?.Invoke();
        }

        protected virtual void TriggerFail(string p_failReason)
        {
            Debug.LogWarning(p_failReason, this);
        }

        public string GetDisplayCostForNLevels(int p_nbUpgradeLevels) =>
            ValueTools.GetDisplayValue(this.GetCostForNLevels(p_nbUpgradeLevels));

        protected float GetCostForNLevels(int p_nbUpgradeLevels)
        {
            float l_cost = 0;
            for (int l_id = 1; l_id <= p_nbUpgradeLevels + 1; l_id++)
            {
                l_cost += this.GetCostForLevelN(CurrentLevel + l_id);
            }

            return l_cost;
        }

        protected float GetCostForLevelN(int p_level) => BaseUpgradeCost * Mathf.Pow(COST_SCALING, p_level);
    }
}