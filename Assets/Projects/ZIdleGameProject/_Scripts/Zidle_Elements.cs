using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zidle_Elements : MonoBehaviour {
    //Komen nog lists voor alle var's

    public Transform particleTarget;

    public int fireDmg;
    public Text fireLvlCounter;

    public int airLevel;
    private int airDmg;
    public Text airLvlCounter;
    private bool canCrit;

    public float waterLevel;
    public Text waterLvlCounter;

    public int earthShred;
    public int earthLevel;
    public Text earthLvlCounter;

    public int lightningLevel;
    public int lightningChance;
    public Text lightningLvlCounter;

    public int metalLevel;
    public int metalDmg;
    private int metalDmgBase;
    public Text metalLvlCounter;

    public int lifeLevel;
    public Text lifeLvlCounter;

    public int timeLevel;
    public Text timeLvlCounter;
    

    void Start () {
        earthShred = 50;
        waterLevel = 1;
        lightningChance = 0;
        canCrit = false;
        metalDmgBase = 50;
        metalDmg = metalDmgBase;
        Zidle_GameManager.gManagr.armorTxt.text = "Armor: " + earthShred.ToString();
        particleTarget = GameObject.FindGameObjectWithTag("OrbManager").transform;
	}

    public void ShootParticle(int i)
    {
        Transform tempHolder = particleTarget.GetChild(i).GetChild(0);
        tempHolder.LookAt(particleTarget);
        tempHolder.GetComponent<ParticleSystem>().Play();
    }
    
    public void ClickFire()
    {   
        int clickDmg = 50 + 50 * fireDmg;
        Zidle_GameManager.gManagr.DoHeal(clickDmg, earthShred, lightningChance, metalDmg);
        ShootParticle(1);
    }

    public void Fire()
    {   
        if (Zidle_GameManager.gManagr.ups > 0)
        {
            fireDmg++;
            fireLvlCounter.text = fireDmg.ToString();
            Zidle_GameManager.gManagr.ups--;
            Zidle_GameManager.gManagr.upsTxt.text = "Ups: " + Zidle_GameManager.gManagr.ups.ToString();
        }
    }

    public void Air()
    {
        if (Zidle_GameManager.gManagr.ups > 0)
        {
            StopCoroutine("Aclicker");
            airLevel++;
            airDmg = 25 + 25 * airLevel;
            StartCoroutine("Aclicker");
            Zidle_GameManager.gManagr.ups--;
            airLvlCounter.text = airLevel.ToString();
            Zidle_GameManager.gManagr.upsTxt.text = "Ups: " + Zidle_GameManager.gManagr.ups.ToString();
            print("Current air level is: " + airLevel + " Current AirDmg is: " + airDmg);
        }
    }

    IEnumerator Aclicker()
    {
        while (true)
        {
            if (canCrit == true)
                Zidle_GameManager.gManagr.DoHeal(airDmg, earthShred, lightningChance, metalDmg);
            else
                Zidle_GameManager.gManagr.DoHeal(airDmg, earthShred, 0, metalDmg);
            ShootParticle(2);
            yield return new WaitForSeconds(2f / (waterLevel / .5f));
        }
    }
    
    public void Water()
    {
        if (Zidle_GameManager.gManagr.ups > 0)
        {
            waterLevel++;
            float waterLevelTemp = waterLevel - 1;
            waterLvlCounter.text = waterLevelTemp.ToString();
            Zidle_GameManager.gManagr.ups--;
            Zidle_GameManager.gManagr.upsTxt.text = "Ups: " + Zidle_GameManager.gManagr.ups.ToString();
        }
    }

    public void Earth()
    {
        if (Zidle_GameManager.gManagr.ups > 0)
        {   
            earthLevel++;
            earthLvlCounter.text = earthLevel.ToString();
            earthShred = 50 - (earthLevel * 2);
            Zidle_GameManager.gManagr.armorTxt.text = "Armor: " + earthShred.ToString();
            Zidle_GameManager.gManagr.ups--;
            Zidle_GameManager.gManagr.upsTxt.text = "Ups: " + Zidle_GameManager.gManagr.ups.ToString();
        }
    }

    public void Lightning() 
    {   //crit chance - de ability zorgt ervoor dat Air ook kan critten
        if (Zidle_GameManager.gManagr.ups > 0)
        {
            lightningLevel++;
            lightningChance = lightningLevel * 2;
            lightningLvlCounter.text = lightningLevel.ToString();
            Zidle_GameManager.gManagr.ups--;
            Zidle_GameManager.gManagr.upsTxt.text = "Ups: " + Zidle_GameManager.gManagr.ups.ToString();
        }
    }

    public void Metal()
    {   //crit dmg
        if (Zidle_GameManager.gManagr.ups > 0)
        {
            metalLevel++;
            metalDmg = metalDmgBase + (metalLevel * 2);
            metalLvlCounter.text = metalLevel.ToString();
            Zidle_GameManager.gManagr.ups--;
            Zidle_GameManager.gManagr.upsTxt.text = "Ups: " + Zidle_GameManager.gManagr.ups.ToString();
        }
    }

    public void Life()
    {   //reduce targethealth - .5% * upgrade lvl
        if (Zidle_GameManager.gManagr.ups > 0)
        {
            lifeLevel++;
            Zidle_GameManager.gManagr.targetHealthNerf = .5f * lifeLevel;
            lifeLvlCounter.text = lifeLevel.ToString();
            Zidle_GameManager.gManagr.ups--;
            Zidle_GameManager.gManagr.upsTxt.text = "Ups: " + Zidle_GameManager.gManagr.ups.ToString();
        }
    }

    //public void Time()
    //{   //reduce cooldown of abilities
    //    if (Zidle_GameManager.gManagr.ups > 0)
    //    {

    //        Zidle_GameManager.gManagr.ups--;
    //        Zidle_GameManager.gManagr.upsTxt.text = "Ups: " + Zidle_GameManager.gManagr.ups.ToString();
    //    }
    //}
}

#region ToDo; Custom Inspector- Rename list elements.
//[CustomEditor (typeof(Zidle_Elements))]
//public class Zidle_ElementsEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//    }
//}
#endregion
