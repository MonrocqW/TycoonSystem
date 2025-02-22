using TMPro;
using TycoonSystem.Core;
using UnityEngine;

namespace TycoonSystem.UI
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private AbstractCurrency m_currency;
        [SerializeField] private TextMeshProUGUI m_currencyName;
        [SerializeField] private TextMeshProUGUI m_currencyValue;

        private void Awake()
        {
            m_currencyName.text = m_currency.CurrencyName;
            m_currencyValue.text = m_currency.DisplayValue;
        }

        private void Update()
        {
            m_currencyValue.text = m_currency.DisplayValue;
        }
    }
}