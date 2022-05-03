using System;
using Core.Economy;
using UnityEngine;
using TowerDefense.Level;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefense.UI.HUD
{
    public class WeaponUpgradeButton : MonoBehaviour
    {
        /// <summary>
        /// The text attached to the button
        /// </summary>
        public Text buttonText;

        public Image towerIcon;

        public Button buyButton;

        public Image energyIcon;

        public Color energyDefaultColor;

        public Color energyInvalidColor;

        public int cost;

        /// <summary>
        /// Cached reference to level currency
        /// </summary>
        Currency m_Currency;


        public void InitializeButton()
        {
            if (LevelManager.instanceExists)
            {
                m_Currency = LevelManager.instance.currency;
                m_Currency.currencyChanged += UpdateButton;
            }
            else
            {
                Debug.LogWarning("[Tower Spawn Button] No level manager to get currency object");
            }
            UpdateButton();
        }

        void UpdateButton()
        {
            if (m_Currency == null)
            {
                return;
            }

            // Enable button
            if (m_Currency.CanAfford(cost) && !buyButton.interactable)
            {
                buyButton.interactable = true;
                energyIcon.color = energyDefaultColor;
            }
            else if (!m_Currency.CanAfford(cost) && buyButton.interactable)
            {
                buyButton.interactable = false;
                energyIcon.color = energyInvalidColor;
            }
        }

        public void OnClick()
        {
            m_Currency.TryPurchase(cost);
            cost = 10000;
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeButton();
        }

        // Update is called once per frame
        void Update()
        {
            InitializeButton();
        }
    }
}
