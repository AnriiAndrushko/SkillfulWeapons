using UnityEngine;
using static SkillfulWeapons.NewWeaponsManager;

namespace SkillfulWeapons {
    internal class CustomWeapon {

        public WeaponType Type;
        public GameObject WeaponObject;
        public string Name;
        public CustomWeapon(string name, WeaponType inheritFrom) {
            Name = name;
            Type = inheritFrom;
        }
    }
}
