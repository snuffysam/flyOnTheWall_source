using System;
using Platformer.Gameplay;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public static int maxHP = 1;

        public static List<String> heartsObtained;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        public static int currentHP = maxHP;

        public static bool HasHeart(string heart){
            return heartsObtained.Contains(heart);
        }

        public static void AddHeart(string heart){
            maxHP++;
            currentHP = maxHP;
            heartsObtained.Add(heart);
        }

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            if (currentHP == 0)
            {
                GetComponent<PlayerController>().PlayerDeath();
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        //Increment the HP of the entity until full.
        public void Heal()
        {
            while (currentHP < maxHP) Increment();
        }

        void Awake()
        {
            //currentHP = maxHP;
            if (heartsObtained == null){
                heartsObtained = new List<string>();
            }
        }
    }
}
