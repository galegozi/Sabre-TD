using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret; // Look at TurretBlueprint script for an idea of how this works.
    public TurretBlueprint missleLauncher;
    public TurretBlueprint laserBeamer;

    BuildManager buildManager;


    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret ()
    {
        Debug.Log("Standard Turret selected.");
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectMissleLauncher()
    {
        Debug.Log("Missle Launcher selected.");
        buildManager.SelectTurretToBuild(missleLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer selected.");
        buildManager.SelectTurretToBuild(laserBeamer);
    }

}
