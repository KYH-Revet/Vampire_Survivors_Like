using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Weapon : Item
{
    public struct WeaponStats
    {
        public int level;
        public int damage;
        public float range;
        public float attackSpeed;
        public float durability;
        public WeaponStats(int level, int damage, float range, float attackSpeed, float durability)
        {
            this.level = level;
            this.damage = damage;
            this.range = range;
            this.attackSpeed = attackSpeed;
            this.durability = durability;
        }
    }
    public WeaponStats weaponStats;

    public override void Initialize()
    {
        itemType = ItemType.Weapon;

        weaponStats = new WeaponStats(1, 10, 1.5f, 1.0f, 100f);
    }
    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        if (GameManager.gameState == GameManager.GameState.Playing)
            Use();
    }
}
