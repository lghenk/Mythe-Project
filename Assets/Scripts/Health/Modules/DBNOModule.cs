using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// DBNO (Down But Not Out) allows us to let the user decide to revive, kill or possibly bleed out the enemy
/// </summary>
public class DBNOModule : HealthBaseModule {
    public override void OnDamage(float damageAmount, float curHeath, float startingHealth) {
        throw new NotImplementedException();
    }

 
    public override void OnDeath() {
        throw new NotImplementedException();
    }
}
