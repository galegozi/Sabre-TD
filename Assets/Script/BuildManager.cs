using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        // Ref to buildmanager anywhere, preventing the nodes from having individual build manager instead it will be an instance.
        // This only really works if you have only one script of BuildManager running so keep that in mind. if statement covers this.
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
            return;
        }

        instance = this;

    }

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI NodeUI;
    
    //VVVVVVVVV this is a "property". Only allows us to 'get' something, this variable cannot be set. same as  writing a small function that
    // checks if turretToBuild is not equal to null.
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn (Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost) //checks to see if you can afford the turret.
        {
            Debug.Log("Not enough money to build that.");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);//This is for the animation when building a turret.
        Destroy(effect, 5f);

        Debug.Log("Turret build! Money left:" + PlayerStats.Money);
    }

    //eneable one, disavle the other so players cant select a new tower while upgrading another.
    public void SelectNode (Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        NodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        NodeUI.Hide();
    }
    public void SelectTurretToBuild (TurretBlueprint turret)
    {
        turretToBuild = turret;
        selectedNode = null;

        NodeUI.Hide();
    }

}
