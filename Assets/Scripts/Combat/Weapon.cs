using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon",menuName ="Weapon/MakeWeapon",order =0)]   
    public class Weapon : ScriptableObject
    {
        [SerializeField]
        AnimatorOverrideController animatorOverride;
        [SerializeField]
        GameObject weaponPrefab;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;

        public void SpawnWeapon(Animator animator,Transform handTransform)
        {
            if(animatorOverride!=null)
            animator.runtimeAnimatorController = animatorOverride;
            if(weaponPrefab!=null)
            Instantiate(weaponPrefab, handTransform);
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }
        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
    }
}