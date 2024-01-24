using HarmonyLib;
using modweaver.core;
using UnityEngine;

namespace SkillfulWeapons {
    [ModMainClass]
    public class ModMain : Mod {//TODO: add configs

        CustomWeapon weapon1, weapon2, weapon3, weapon4, weapon5;
        static bool spawned = false;

        public override void Init() {
            //ExplorerStandalone.CreateInstance();//TODO: remove in final version

            NewWeaponsManager.Init(Logger);
            weapon1 = new CustomWeapon("BIG sword", NewWeaponsManager.WeaponType.ParticleBlade);
            NewWeaponsManager.AddNewWeapon(weapon1);
            NewWeaponsManager.OnInitCompleted += Weapon1;

            weapon2 = new CustomWeapon("Blaster", NewWeaponsManager.WeaponType.LaserCannon);
            NewWeaponsManager.AddNewWeapon(weapon2);
            NewWeaponsManager.OnInitCompleted += Weapon2;

            weapon3 = new CustomWeapon("Bazoooka", NewWeaponsManager.WeaponType.RocketLauncher);
            NewWeaponsManager.AddNewWeapon(weapon3);
            NewWeaponsManager.OnInitCompleted += Weapon3;

            weapon4 = new CustomWeapon("Sniper Rifle", NewWeaponsManager.WeaponType.Railvolver);
            NewWeaponsManager.AddNewWeapon(weapon4);
            NewWeaponsManager.OnInitCompleted += Weapon4;

            weapon5 = new CustomWeapon("Rifle", NewWeaponsManager.WeaponType.Railvolver);
            NewWeaponsManager.AddNewWeapon(weapon5);
            NewWeaponsManager.OnInitCompleted += Weapon5;

            Harmony harmony = new Harmony(Metadata.id);
            Logger.Info("Running patches!");
            harmony.PatchAll();
            Logger.Info("Patched!");
        }


        void Weapon1() {
            weapon1.WeaponObject.GetComponent<ParticleBlade>().baseSize = new Vector2(10, 100);
            string path = "modweaver\\mods\\SkillfulWeaponsTextures\\weapon1.png";

            SpriteRenderer sr = weapon1.WeaponObject.GetComponent<SpriteRenderer>();

            Texture2D texture = LoadTextureFromFile(path);
            sr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        void Weapon2() {
            var laser = weapon2.WeaponObject.GetComponent<LaserCannon>();
            laser.beamTime = 0.1f;
            laser.beamRecoil = 0.5f;
            laser.beamPower = 2f;
            laser.beamWidth = 1f;
            laser.beamCooldown = 0.3f;
            laser.maxAmmo = 5;
            laser.laserPointers.transform.localScale *= 0.9f;
            string path = "modweaver\\mods\\SkillfulWeaponsTextures\\weapon2.png";

            SpriteRenderer sr = weapon2.WeaponObject.GetComponent<SpriteRenderer>();

            Texture2D texture = LoadTextureFromFile(path);
            sr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.size *= 8f;
        }
        void Weapon3() {
            var launcher = weapon3.WeaponObject.GetComponent<ProjectileLauncher>();
            launcher.maxAmmo = 10;
            launcher.reloadTime = 5;
            launcher.shotForce = 1000f;

            string path = "modweaver\\mods\\SkillfulWeaponsTextures\\weapon3.png";

            SpriteRenderer sr = weapon3.WeaponObject.GetComponent<SpriteRenderer>();

            Texture2D texture = LoadTextureFromFile(path);
            sr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.size *= 4f;
        }
        void Weapon4() {
            var rifle = weapon4.WeaponObject.GetComponent<Railvolver>();

            rifle.maxAmmo = 4;
            rifle.shotPower = 2000f;
            string path = "modweaver\\mods\\SkillfulWeaponsTextures\\weapon4.png";

            SpriteRenderer sr = weapon4.WeaponObject.GetComponent<SpriteRenderer>();

            Texture2D texture = LoadTextureFromFile(path);
            sr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.size *= 2f;
        }
        void Weapon5() {
            var rifle = weapon5.WeaponObject.GetComponent<Railvolver>();

            rifle.maxAmmo = 30;
            rifle.shotPower = 45f;
            string path = "modweaver\\mods\\SkillfulWeaponsTextures\\weapon5.png";

            SpriteRenderer sr = weapon5.WeaponObject.GetComponent<SpriteRenderer>();

            Texture2D texture = LoadTextureFromFile(path);
            sr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.size *= 3f;
        }



        private Texture2D LoadTextureFromFile(string filePath) {
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(fileData);

            return texture;
        }


        public override void Ready() {
            Logger.Info("SkillfulWeapons mod is ready!");
        }

        public override void OnGUI(ModsMenuPopup ui) {

        }

        [HarmonyPatch(typeof(VersionNumberTextMesh), "Start")]
        public static class VNTMPatch {

            [HarmonyPostfix]
            public static void Postfix(ref VersionNumberTextMesh __instance) {
                __instance.textMesh.text += $"\n<color=red> Skillful Weapons v0.0.1 by Neko_Lover</color>";
            }
        }

        //[HarmonyPatch(typeof(JumpPad), "Push")]
        //public static class JumpPadPatch {

        //    [HarmonyPrefix]
        //    public static void Prefix(ref JumpPad __instance) {
        //        if (spawned) {
        //            return;
        //        }
        //        foreach (var w in NewWeaponsManager.NewWeapons) {
        //            var controllers = LobbyController.instance.GetPlayerControllers();

        //            var health = (SpiderHealthSystem)AccessTools.PropertyGetter(typeof(PlayerController), "spiderHealthSystem")
        //                .Invoke(controllers.First(), Array.Empty<object>());

        //            var spawnedWeapon = UnityEngine.Object.Instantiate(w, health.transform.position, Quaternion.identity);

        //            spawnedWeapon.gameObject.SetActive(true);

        //            var netComp = spawnedWeapon.GetComponent<NetworkObject>();

        //            if ((UnityEngine.Object)(object)netComp != (UnityEngine.Object)null) {
        //                netComp.Spawn(true);
        //                netComp.DestroyWithScene = true;
        //            }
        //        }
        //        spawned = true;

        //    }
        //}
    }
}