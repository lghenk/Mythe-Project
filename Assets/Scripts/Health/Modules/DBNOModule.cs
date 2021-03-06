﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DBNO (Down But Not Out) allows us to let the user decide to revive, kill or possibly bleed out the enemy
/// </summary>
public class DBNOModule : HealthBaseModule {
    private bool isDBNO = false;
    public bool IsDbno => isDBNO;
    
    public override void OnDamage(float damageAmount, float curHeath, float startingHealth) {
        if (curHeath <= 0 && isDBNO == false) {
            health.SetHealth(5);
            isDBNO = true;
            health.onMessage?.Invoke("DBNO");
        }
        
        health.onDamage?.Invoke(damageAmount, health.CurHealth, startingHealth, health);
    }

 
    public override void OnDeath() {
        if (isDBNO) {
            PlayerKillCounter.instance.AddKill();
            health.onDeath?.Invoke(health);
        }
    }

    public void Resurrect() {
        health.SetHealth(100);
        health.onMessage?.Invoke("Resurrect");
    }
}
