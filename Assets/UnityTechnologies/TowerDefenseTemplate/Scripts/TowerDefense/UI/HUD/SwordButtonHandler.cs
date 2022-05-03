using System;
using Core.Economy;
using TowerDefense.Level;
using TowerDefense.Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefense.UI.HUD
{
    /// <summary>
    /// A button controller for spawning towers
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class SwordButtonHandler : MonoBehaviour
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
        /// Fires when the button is tapped
        /// </summary>
        // public event Action<Tower> buttonTapped;

        /// <summary>
        /// Fires when the pointer is outside of the button bounds
        /// and still down
        /// </summary>
        // public event Action<Tower> draggedOff;

        /// <summary>
        /// Cached reference to level currency
        /// </summary>
        Currency m_Currency;

        /// <summary>
        /// The attached rect transform
        /// </summary>
        RectTransform m_RectTransform;

        // public virtual void OnDrag(PointerEventData eventData)
        // {
        //     if (!RectTransformUtility.RectangleContainsScreenPoint(m_RectTransform, eventData.position))
        //     {
        //         if (draggedOff != null)
        //         {
        //         }
        //     }
        // }

        /// <summary>
        /// Define the button information for the tower
        /// </summary>
        /// <param name="towerData">
        /// The tower to initialize the button with
        /// </param>
        public void InitializeButton()
        {

            // if (towerData.levels.Length > 0)
            // {
            // 	TowerLevel firstTower = towerData.levels[0];
            // 	buttonText.text = firstTower.cost.ToString();
            // 	towerIcon.sprite = firstTower.levelData.icon;
            // }
            // else
            // {
            // 	Debug.LogWarning("[Tower Spawn Button] No level data for tower");
            // }

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

        /// <summary>
        /// Cache the rect transform
        /// </summary>
        protected virtual void Awake()
        {
            m_RectTransform = (RectTransform)transform;
        }

        protected virtual void Start()
        {
            if (!LevelManager.instanceExists)
            {
                Debug.LogError("[UI] No level manager for tower list");
            }
            else
            {
                InitializeButton();
            }
        }

        /// <summary>
        /// Unsubscribe from events
        /// </summary>
        // protected virtual void OnDestroy()
        // {
        //     if (m_Currency != null)
        //     {
        //         m_Currency.currencyChanged -= UpdateButton;
        //     }
        // }

        /// <summary>
        /// The click for when the button is tapped
        /// </summary>
        // public void OnClick()
        // {
        //     if (buttonTapped != null)
        //     {

        //     }
        // }

        /// <summary>
        /// Update the button's button state based on cost
        /// </summary>
        void UpdateButton()
        {
            if (m_Currency == null)
            {
                buyButton.interactable = false;
                energyIcon.color = energyInvalidColor;
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
    }
}