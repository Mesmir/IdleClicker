using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Elements/fuctions  

Fire        - onmousedown, hp += clickdmg;    
Air         - autoclicker, Coroutine? (add clickdmg + upgradesCount, over time.deltatime +/* waterUpgradesCount)    Mathf. formules??
Water       - autoclick speed, ^
Earth       - armor shred, every level increases armor shred by 2%- omniP has a base armor of 50% 
Lightning   - Crit chance, calculate crit on current press or before hand
Metal       - Crit dmg, clickdmg * 50% + 5%(per upgrade)

Life        - reduce the amount of health required to heal, next level health -= life level * life% (reduced % in higher lvls?)
Time        - reduce the cooldown of skills, ^


    -Fire; click dmg
    /Air; autoclickers  <<< F$CK THIS SH!T >>> Wordt autoclick dmg
    |Water; autoclick speed
    -Earth; armor shred/penetration
    \Lightning; (auto)click crit %
    \Metal; crit dmg
    
    Life; Less life needed to heal
    Time; Cooldown reduction
*/
#region Abilities
/*
    Fire; increases click dmg x2 + 10% for every 5 upgrade lvls -for 30 sec?
    Air; increases dmg x2 + 10% for every 5 upgrade lvls -for 30 sec?
    Water; Rinse and repeat -> Reduce the cooldown of the next ability used by 15% +5% p/lvl
    Earth; increase Armor shred bij an additional 50% for 30 seconds
    Lghtnng; ??allow Air to temporarily crit aswell??
    Metal; Empower -> increase the effectiveness of the next ability
    Life; for the next 3 levels, have a % chance to gain an additional Ups - base 10% + 10% every 5 upgrade lvls
    Time; increase time by 1.5 or 2 (yet to decide which) for 20sec - every 5 upgrade lvls increase the duration by 3 sec
*/
#endregion

#region ToDo List
/*
Gmanager.DoHeal - percentage calculation converte naar een Mathf
ChangeUpps function fixen(, voor clarity en ease of use met aanroepen).

    Abilities Complete System

    stat screen
    tooltips
    Musick + Visualizer
    sounds
    Visuals
    Save/load
    (Main) Menu
    Options/Settings
*/
#endregion

#region Ideas
/* 
    Camera Shake bij crit
    Display dmg numbers? Left side mouse, right side Air - net als bij scrollingMilkText van WoW
    crit chance 100%+ Overcrit? - warframe style

    Particle mouse follower, verandert van Pfx gebasseerd op het laatst geupgrade element
    Hardcap op sommige elements - mogelijk verdere upgrades ervan switchen naar andere functionaliteit, zoals de ability ervan
    lvl requirements om up te graden
    Cost van ups omhoog
*/
#endregion

#region Concept
/*Concept;
    Een omni particle heeft zen powers is verloren
    Click the omni to heal it back to full hp and get to the next level and gain upgrade points (ups)
    Use to ups to upgrade elements.
*/
#endregion
