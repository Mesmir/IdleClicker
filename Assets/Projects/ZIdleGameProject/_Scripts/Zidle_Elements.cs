using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Zidle_Elements : MonoBehaviour
{

    public Transform center, critParent;
    public Text healthText, targetHealthText, upgradePointsTxt, armorText, levelText;

    public int targetHealth;
    [Range(0, 1f)]
    public float targetHealthLevelMultiplier;

    private int level = 1;
    public int upgradePoints;
    private int health;
    private int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            TryLevelling();
            //update text
            healthText.text = health.ToString();
        }
    }
    [Range(0, 1f)]
    public float armor;
    public float Armor
    {
        get
        {
            return armor - earth.Percentage();
        }
    }

    public Fire fire;
    public Air air;
    public Water water;
    public Earth earth;
    public Lightning lightning;
    public Metal metal;
    public Life life;

    public class Elemental
    {
        public string keyBoundToElementalLevelling;
        public int level = 1;
        public Text levelText;

        public void LevelUp()
        {
            level++;
            levelText.text = level.ToString();
        }
    }

    #region All Elementals

    [Serializable]
    public class Fire : Elemental
    {
        public ParticleSystem particleSystem;

        public int damage;
        public virtual int Damage()
        {
            return damage * level;
        }
    }

    [Serializable]
    public class Air : Fire
    {
        //waarom bestaat dit?
    }

    [Serializable]
    public class Water : Elemental
    {
        public float refreshRate;
        public float RefreshRate()
        {
            return refreshRate / level;
        }
    }

    [Serializable]
    public class Earth : PercentageBased
    {
        [Range(0, 1f)]
        public float maxReduction;

        public override float Percentage()
        {
            return Mathf.Clamp(base.Percentage(), 0, maxReduction);
        }
    }

    [Serializable]
    public class Lightning : PercentageBased
    {
        [Range(0, 1f)]
        public float percentage2;
        private float Percentage2()
        {
            return percentage2 * level;
        }

        public override float Percentage()
        {
            return Mathf.Lerp(base.Percentage(), Percentage2(), UnityEngine.Random.Range(0, 1f));
        }
    }

    [Serializable]
    public class Metal : PercentageBased
    {

    }

    [Serializable]
    public class Life : Earth
    {

    }

    #endregion

    #region Elemental Types

    [Serializable]
    public class PercentageBased : Elemental
    {
        [Range(0, 1f)]
        public float percentage;
        public virtual float Percentage()
        {
            return percentage * level;
        }
    }

    #endregion

    private void Awake()
    {
        upgradePointsTxt.text = "Upgrade Points: " + upgradePoints;
        targetHealthText.text = targetHealth.ToString();
        UpdateArmorText();
        levelText.text = "level: " + level;
        StartCoroutine(AutoClick());
    }

    private void UpdateArmorText()
    {
        armorText.text = Mathf.RoundToInt((Armor * 100)).ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(fire.keyBoundToElementalLevelling))
            UpgradeFire();
        if (Input.GetKeyDown(air.keyBoundToElementalLevelling))
            UpgradeAir();
        if (Input.GetKeyDown(water.keyBoundToElementalLevelling))
            UpgradeWater();
        if (Input.GetKeyDown(earth.keyBoundToElementalLevelling))
            UpgradeEarth();
        if (Input.GetKeyDown(lightning.keyBoundToElementalLevelling))
            UpgradeLightning();
        if (Input.GetKeyDown(metal.keyBoundToElementalLevelling))
            UpgradeMetal();
        if (Input.GetKeyDown(life.keyBoundToElementalLevelling))
            UpgradeLife();
    }

    #region Upgrade Elementals

    private bool CanConsumeUpgradePoint()
    {
        if (upgradePoints > 0)
        {           
            upgradePoints--;
            upgradePointsTxt.text = "Upgrade Points: " + upgradePoints;
            return true;
        }
        return false;
    }

    public void UpgradeFire()
    {
        if (CanConsumeUpgradePoint())
            fire.LevelUp();
    }

    public void UpgradeAir()
    {
        if (CanConsumeUpgradePoint())
            air.LevelUp();
    }

    public void UpgradeWater()
    {
        if (CanConsumeUpgradePoint())
            water.LevelUp();
    }

    public void UpgradeEarth()
    {
        if (CanConsumeUpgradePoint())
        {
            earth.LevelUp();
            UpdateArmorText();
        }
    }

    public void UpgradeLightning()
    {
        if (CanConsumeUpgradePoint())
            lightning.LevelUp();
    }

    public void UpgradeMetal()
    {
        if (CanConsumeUpgradePoint())
            metal.LevelUp();
    }

    public void UpgradeLife()
    {
        if (CanConsumeUpgradePoint())
            life.LevelUp();
    }

    #endregion

    private IEnumerator AutoClick()
    {
        while (true)
        {
            DealDamage(air.Damage());
            ShootParticle(air);
            yield return new WaitForSeconds(water.RefreshRate());
        }
    }

    /// <summary>
    /// Connect To Target Button
    /// </summary>
	public void Click()
    {
        DealDamage(fire.Damage());
        ShootParticle(fire);
    }

    private void ShootParticle(Fire fireInherited)
    {
        fireInherited.particleSystem.transform.LookAt(center);
        fireInherited.particleSystem.Play();
    }

    private void DealDamage(int damage)
    {
        Health += CalculateDamage(damage);
    }

    private void TryLevelling()
    {
        if (health < targetHealth)
            return;

        level++;
        levelText.text = "level: " + level;
        upgradePoints++;
        upgradePointsTxt.text = "Upgrade Points: " + upgradePoints;

        health = 0;
        targetHealth = Mathf.RoundToInt(targetHealth * (1 + targetHealthLevelMultiplier * (1 - life.Percentage()))); ///deze berekening is blijkbaar anti oki do
        targetHealthText.text = targetHealth.ToString();
    }

    private int CalculateDamage(int damage)
    {
        int calculatedDamage = damage;

        //does it crit?
        if (UnityEngine.Random.Range(0, 1f) <= lightning.Percentage())
        {
            calculatedDamage = Mathf.RoundToInt((float)calculatedDamage * (1 + metal.Percentage())); // Cast is not redundant
            StartCoroutine(CritFade());
        }

        //armor reduction
        calculatedDamage = Mathf.RoundToInt((float)calculatedDamage * (1 - Armor)); // CaSt Is NoT ReDunDaNT

        return calculatedDamage;
    }

    private IEnumerator CritFade()
    {   // on crit, red flash- fades fast
        for (float f = 1f; f >= .0f; f -= .2f)
        {
            foreach (Transform child in critParent)
            {
                Color c = child.GetComponent<Image>().color;
                c.a = f;
                child.GetComponent<Image>().color = c;
                yield return null;
            }
        }
    }
}
