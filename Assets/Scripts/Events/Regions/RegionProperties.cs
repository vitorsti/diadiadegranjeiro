using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventProperties;

public class RegionProperties : MonoBehaviour
{
    public Region region;
    public RegionValuesContainer regionData;
    [Header("----- Estado Atual -----")]
    public bool isHealthy = true;
    public int regionHealth;

    [Header("----- Controladores -----")]
    public int maxHealth;
    [SerializeField] int regionGAP;
    [SerializeField] int healthModify;
    int _isHealthy;

    private void Awake()
    {

        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        maxHealth = (int)regionData.GetMaxHealth(region.ToString());
        regionGAP = (int)regionData.GetRegionGap(region.ToString());
        healthModify = (int)regionData.GetHealthModfy(region.ToString());
        _isHealthy = regionData.GetHealthStatus(region.ToString());

        if (regionData.GetHealth(region.ToString()) == 0)
        {
            regionData.SetHealth(region.ToString(), maxHealth);
            regionHealth = (int)regionData.GetHealth(region.ToString());
        }
        else
            regionHealth = (int)regionData.GetHealth(region.ToString());

        if (regionHealth > maxHealth)
        {
            regionHealth = maxHealth;
            regionData.SetHealth(region.ToString(), regionHealth);
        }

        if (_isHealthy == 0)
        {
            isHealthy = false;
        }
        else
            isHealthy = true;

    }

    void Update()
    {
        if (regionHealth != (int)regionData.GetHealth(region.ToString()))
            regionHealth = (int)regionData.GetHealth(region.ToString());
        if (maxHealth != (int)regionData.GetMaxHealth(region.ToString()))
            maxHealth = (int)regionData.GetMaxHealth(region.ToString());
        if (regionGAP != (int)regionData.GetRegionGap(region.ToString()))
            regionGAP = (int)regionData.GetRegionGap(region.ToString());
        if (healthModify != (int)regionData.GetHealthModfy(region.ToString()))
            healthModify = (int)regionData.GetHealthModfy(region.ToString());
        // if (_isHealthy != (int)regionData.GetHealthStatus(region.ToString()))
        //     _isHealthy = (int)regionData.GetHealthStatus(region.ToString());

        // if (_isHealthy == 0)
        // {
        //     isHealthy = false;
        // }
        // else
        //     isHealthy = true;
    }

    public void DecreaseHealth()
    {
        if (regionHealth > 0 && ((regionHealth - healthModify) >= 0))
        {
            regionHealth -= healthModify;
            regionData.SetHealth(region.ToString(), regionHealth);
            CheckRegionHealth();
        }
    }

    public void IncreaseHealth()
    {
        if (regionHealth < maxHealth && ((regionHealth + healthModify) <= maxHealth/*100*/))
        {
            regionHealth += healthModify;
            regionData.SetHealth(region.ToString(), regionHealth);
            CheckRegionHealth();
        }
    }

    public void MaxHealth()
    {
        if (regionHealth < maxHealth && ((regionHealth + healthModify) <= maxHealth/*100*/))
        {
            regionHealth = maxHealth;
            regionData.SetHealth(region.ToString(), maxHealth);
            CheckRegionHealth();
        }
    }

    public void CheckRegionHealth()
    {
        if (regionHealth > regionGAP)
        {
            isHealthy = true;
            regionData.SetHealthStatus(region.ToString(), 1);
        }
        else
        {
            isHealthy = false;
            regionData.SetHealthStatus(region.ToString(), 0);
            RegionManager regionManager = GetComponent<RegionManager>();
            regionManager.SpawnTask();
        }
    }
}
