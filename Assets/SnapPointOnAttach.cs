using System.Collections;
using System.Collections.Generic;
using TowerDefense.UI.HUD;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapPointOnAttach : MonoBehaviour
{
    public GameUI ui;
    public GameObject emptyBlock;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameUI.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttach(SelectEnterEventArgs args)
    {
        args.interactable.gameObject.layer = LayerMask.NameToLayer("PlacementGhosts");
        args.interactable.gameObject.transform.position = transform.position + new Vector3(0, 1, 0);
        args.interactable.gameObject.transform.rotation = transform.rotation;
        if (ui.BuyTower(args.interactable.gameObject.transform))
        {
            emptyBlock.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
