using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Zidle_GameManager : MonoBehaviour {
    
    public static Zidle_GameManager gManagr = null;

    public int ups;
    public int omniPHealth;
    public int targetHealth;
    private int startHealth = 250;
    public float level;
    public Text upsTxt;
    public Text healthTxt;
    public Text healthCounter;
    public Text healthTarget;
    public Text armorTxt;
    public Text levelText;
    public float targetHealthNerf;

    public float result;
    public float critResult;
    public Transform critParent;

    void Awake()
    {
        gManagr = this;
        healthTxt.text = "Health";
        ChangeHealth();
    }

    public void DoHeal(int healAmount, float armor, int critChance, float critDmg)
    {
        int min = 0;
        int max = 100;
        if (Random.Range(min, max) < critChance)
        {
            critResult = healAmount + (healAmount * critDmg / 100);
            healAmount = (int)critResult;
            StartCoroutine("CritFade");
        }
        result = healAmount - (healAmount * (armor / 100));
        omniPHealth += (int)result;
        ChangeHealth();
    }



    #region percentage calculations
    //50 * .25 = 12.5; 50 - 12.5;
    //healAmount * (armor / 100) = result; healamount - result;

    //(int)Mathf.Lerp(minArmor, maxArmor, healAmount / armor);
    //finalValue = currentValue / maxValue * 100;
    #endregion

    void ChangeHealth()
    {
        if (omniPHealth >= targetHealth)
        {
            int overheal = omniPHealth - targetHealth;
            omniPHealth = 0 + overheal;
            level++;
            ups++;
            int tempTargetHealth = startHealth * (int)Mathf.Pow(level, 1.25f);
            targetHealth = tempTargetHealth - (int)(tempTargetHealth * (targetHealthNerf / 100)); 
            healthTarget.text = targetHealth.ToString();
            levelText.text = "Level: " + level;
            upsTxt.text = "Ups: " + ups;
        }
        healthCounter.text = omniPHealth.ToString();
    }

    IEnumerator CritFade()
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

    void ShowInfo()
    {

    }
}
