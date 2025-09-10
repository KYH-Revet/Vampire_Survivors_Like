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
    abstract public void Damaged(int dmg);
    abstract protected void Dead();
}
