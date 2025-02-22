using System;
using TMPro;
using TycoonSystem.Core;
using TycoonSystem.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace TycoonSystem.UI
{
    public class TycoonElementUI : MonoBehaviour
    {
        [SerializeField] private AbstractTycoonElement m_element;
        [SerializeField] private TextMeshProUGUI m_elementName;
        [SerializeField] private TextMeshProUGUI m_elementLvl;
        [SerializeField] private TextMeshProUGUI m_upgradeCost;
        [SerializeField] private Slider m_generatingSlider;
        [SerializeField] private TextMeshProUGUI m_generatingPerSec;
        [SerializeField] private TextMeshProUGUI m_generatingPerProc;
        [SerializeField] private TextMeshProUGUI m_generatingTime;
        [SerializeField] private Button m_upgradeBtn;

        private float m_timeLastProc;

        private void Awake()
        {
            m_elementName.text = m_element.ElementName;
            m_upgradeBtn.onClick.AddListener(this.UpgradeElement);
            this.UpdateUI();
            m_element.onNewLevel += this.UpdateUI;
            m_timeLastProc = Time.time;
        }

        private void Update()
        {
            if (m_element.CurrentLevel == 0)
                return;

            float l_progress = (Time.time - m_timeLastProc) / m_element.GeneratingTime;
            if (l_progress >= 1)
            {
                m_element.GeneratingCurrency.Gain(((int) l_progress) * m_element.GeneratingPerProc);
                l_progress -= (int) l_progress;
                m_timeLastProc = Time.time;
            }

            //if(m_element.GeneratingTime > 1)
                m_generatingSlider.value = l_progress;
            m_generatingTime.text =
                $"{(l_progress * m_element.GeneratingTime).ToString("F1")} / {m_element.GeneratingTime.ToString("F1")}";
        }

        private void UpdateUI()
        {
            this.UpdateLevel();
            this.UpdateCost();
            this.UpdateGenerating();
        }

        private void UpdateGenerating()
        {
            bool l_showPerProc = m_element.GeneratingTime > 1;
            m_generatingPerProc.gameObject.SetActive(l_showPerProc);
            m_generatingPerSec.gameObject.SetActive(!l_showPerProc);
            m_generatingPerProc.text = ValueTools.GetDisplayValue(m_element.GeneratingPerProc);
            m_generatingPerSec.text = ValueTools.GetDisplayValue(m_element.GeneratingPerSecond);
        }

        private void UpgradeElement()
        {
            if (m_element.CurrentLevel == 0)
                m_timeLastProc = Time.time;
            // TODO static option to choose number of upgrades
            m_element.UpgradeBy(1);
        }

        private void UpdateLevel()
        {
            m_elementLvl.text = m_element.CurrentLevel.ToString();
        }

        private void UpdateCost()
        {
            // TODO static option to choose number of upgrades
            m_upgradeCost.text = m_element.GetDisplayCostForNLevels(1);
        }
    }
}