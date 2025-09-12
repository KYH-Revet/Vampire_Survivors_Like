using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Serializable]
    public struct CharacterStats
    {
        public int maxHealth;
        public int health;
        public int armor;
        public int speed;
        public int damage;

        public CharacterStats(int health, int armor, int speed, int damage)
        {
            this.maxHealth = health;
            this.health = health;
            this.armor = armor;
            this.speed = speed;
            this.damage = damage;
        }
    }
    abstract protected void Move();    
    abstract protected void Dead();

    // Hp Change
    public enum HpChangeType
    {
        Heal,
        Damage
    }
    abstract public void HpControl(int value, HpChangeType hpChangeType);
}
