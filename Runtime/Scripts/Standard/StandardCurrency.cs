using TycoonSystem.Core;
using UnityEngine;

namespace TycoonSystem.Standard
{
    [CreateAssetMenu(menuName = "Tycoon System/Standard Currency", order = 1)]
    public class StandardCurrency : AbstractCurrency
    {
        [SerializeField] private string m_currencySymbol = "€";

        public string CurrencySymbol => m_currencySymbol;
    }
}