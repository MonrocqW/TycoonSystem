using UnityEngine;

namespace TycoonSystem.Tools
{
    public static class ValueTools
    {
        private static string[] s_displaySymbols = new string[] {"", "k", "M", "G", "T", "P", "E", "Z", "Y", "R", "Q"};

        public static string GetDisplayValue(float p_value)
        {
            int l_tenPower = (int)Mathf.Log(p_value, 1000);
            return (p_value / Mathf.Pow(1000, l_tenPower)).ToString("F1") + " " + s_displaySymbols[l_tenPower];
        }
    }
}