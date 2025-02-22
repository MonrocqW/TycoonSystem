using TycoonSystem.Tools;
using UnityEngine;

namespace TycoonSystem.Core
{
    public abstract class AbstractCurrency : ScriptableObject
    {
        [SerializeField] protected string m_currencyName;

        protected float m_currentValue;

        public string CurrencyName => m_currencyName;
        public virtual float CurrentValue => m_currentValue;
        public virtual string DisplayValue => ValueTools.GetDisplayValue(CurrentValue);

        public bool CheckEnoughFunds(float p_needed) => m_currentValue >= p_needed;

        public bool CheckAndConsumeFunds(float p_needed)
        {
            if (!this.CheckEnoughFunds(p_needed))
                return false;
            this.Remove(p_needed);
            return true;
        }

        public void Gain(float p_value)
        {
            m_currentValue += p_value;
        }

        public void Remove(float p_value)
        {
            m_currentValue -= p_value;
        }
    }
}