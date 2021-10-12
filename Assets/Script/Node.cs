using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset; //The towers were spawning sunk in the ground so this will be used to lift em.
    public Color notBuildableColor;

    [Header("Optional")] //This will make an option for turret on a node, we can use this to force specific towers at specific nodes at the...
    public GameObject turret; // ...start of the game

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //From UnityEngine.EventSystems---Prevents clicking throgh shop
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        buildManager.BuildTurretOn(this);
    
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //From UnityEngine.EventSystems---Prevents clicking throgh shop
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notBuildableColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
