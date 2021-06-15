using LSPD_First_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using MetroCallouts3;
using MetroCallouts3.Callouts;
using Rage.Native;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting.Entities;

namespace MetroCallouts3.Callouts
{
    [CalloutInfo("Compañeros están siendo atacados", CalloutProbability.Medium)]
    public class asaltoapolicias : Callout
    {
        public Vector3 spawn;
        public Blip blipcompañero1;
        public Blip blipcompañero2;
        public Blip blipEnemigo1;
        public Blip blipEnemigo2;
        public Blip blipEnemigo3;
        public Blip blipEnemigo4;
        public Blip blipEnemigo5;
        public Ped compañero1;
        public Ped compañero2;
        public Vehicle patrulla;
        public Vector3 spawnEnemigo1;
        public Vector3 spawnEnemigo2;
        public Vector3 spawnenemigo3;
        public Vector3 spawncompañero1;
        public Vector3 spawncompañero2;
        public int discriminer;
        public int discriminer2;
        public LHandle persecucion;
        public Ped enemigo1;
        public Ped enemigo2;
        public Ped enemigo3;
        public float heading;
        public bool wascalloutaccepted;
        public bool done;
        public float patrolHeading;
        public override bool OnBeforeCalloutDisplayed()
        {
            done = false;
            wascalloutaccepted = false;
            discriminer2 = 1;
            discriminer = MetroCallouts3.Api.Api.getDiscriminer(1, 2);
            Game.LogTrivial($"[MetroCallouts3] Discriminer value for asaltoapolicia.cs is {discriminer}");
            heading = 0f;
            if (discriminer == 1)
            {
                spawn = new Vector3(193.68f, -931.98f, 30.25f);
                spawncompañero1 = new Vector3(195.68f, -928.98f, 30.69f);
                spawncompañero2 = new Vector3(197.95f, -934.42f, 30.69f);
                spawnEnemigo1 = new Vector3(197.74f, -942.84f, 30.69f);
                spawnEnemigo2 = new Vector3(209.58f, -939.80f, 30.69f);
                spawnenemigo3 = new Vector3(205.35f, -915.94f, 30.69f);
                patrolHeading = 237.97f;

            }
            if (discriminer == 2)
            {
                spawn = new Vector3(-1329.95f, -645.01f, 26.35f);
                spawncompañero1 = new Vector3(-1329.29f, -648.03f, 26.60f);
                spawncompañero2 = new Vector3(-1333.72f, -642.83f, 26.90f);
                spawnEnemigo1 = new Vector3(-1333.73f, -632.35f, 27.46f);
                spawnEnemigo2 = new Vector3(-1338.91f, -649.64f, 26.91f);
                spawnenemigo3 = new Vector3(-1322.58f, -652.80f, 26.52f);
                patrolHeading = 37.51f;
            }
            if (discriminer == 3)
            {

            }
            CalloutMessage = "Compañeros solicitan apoyo urgente";
            CalloutPosition = spawn;
            ShowCalloutAreaBlipBeforeAccepting(spawn, 3f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_ASSAULT_PEACE_OFFICER_03 IN_OR_ON_POSITION", spawn);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            
            wascalloutaccepted = true;
            enemigo3 = new Ped(spawnenemigo3, heading);
            enemigo2 = new Ped(spawnEnemigo2, heading);
            enemigo1 = new Ped(spawnEnemigo1, heading);
            while (enemigo1.Model.Name == "s_m_y_cop_01"){    enemigo1 = new Ped(spawnEnemigo1);}
            while (enemigo2.Model.Name == "s_m_y_cop_01"){    enemigo2 = new Ped(spawnEnemigo1);}
            while (enemigo3.Model.Name == "s_m_y_cop_01"){    enemigo3 = new Ped(spawnEnemigo1);}
            compañero1 = new Ped("s_m_y_cop_01", spawncompañero1, 301.95f);
            compañero2 = new Ped("s_m_y_cop_01", spawncompañero2, 255f);
            
            blipcompañero1 = compañero1.AttachBlip();
            blipcompañero1.Color = Color.Blue;
            blipcompañero2 = compañero2.AttachBlip();
            blipcompañero2.Color = Color.Blue;
            blipcompañero1.EnableRoute(Color.Yellow);
            patrulla = new Vehicle("POLICE", spawn, patrolHeading);
            patrulla.IsEngineOn = true;
            patrulla.IsSirenOn = true;
            patrulla.IsSirenSilent = true;
            

            compañero1.Inventory.GiveNewWeapon("WEAPON_NIGHTSTICK", 10, true);
            compañero2.Inventory.GiveNewWeapon("WEAPON_NIGHTSTICK", 10, true);
            compañero1.IsInvincible = true;
            compañero2.IsInvincible = true;
            enemigo1.IsInvincible = true;
            enemigo2.IsInvincible = true;
            enemigo3.IsInvincible = true;
            enemigo1.Tasks.FightAgainst(compañero1);
            enemigo2.Tasks.FightAgainst(compañero2);
            enemigo3.Tasks.FightAgainst(compañero1);
            MetroCallouts3.Api.Api.displayCalloutMessage("Información:", "Compañeros reportan que están siendo increpados por un grupo de personas. Responde lo antes posible.");
            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            patrulla.IsEngineOn = true;
            
            
            if (Game.LocalPlayer.Character.DistanceTo(patrulla) <= 15f && done == false && discriminer2 == 1)
            {
                persecucion = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
                LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(persecucion, enemigo3);
                LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(persecucion, enemigo2);
                LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(persecucion, enemigo1);
                LSPD_First_Response.Mod.API.Functions.AddCopToPursuit(persecucion, compañero1);
                LSPD_First_Response.Mod.API.Functions.AddCopToPursuit(persecucion, compañero2);
                LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(persecucion, true);
                enemigo1.IsInvincible = false;
                enemigo2.IsInvincible = false;
                enemigo3.IsInvincible = false;
                blipcompañero1.Delete();
                blipcompañero2.Delete();
                LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE 0x5699AA80 IN_OR_ON_POSITION", enemigo1.Position);
                done = true;
            }
            if (discriminer2 == 2 && Game.LocalPlayer.Character.DistanceTo(patrulla) <= 15f && done == false)
            {
                blipEnemigo1 = enemigo1.AttachBlip();
                blipEnemigo1 = enemigo1.AttachBlip();
                blipEnemigo2 = enemigo2.AttachBlip();
                blipEnemigo2.Color = Color.DarkRed;
                blipEnemigo3 = enemigo3.AttachBlip();
                blipEnemigo3.Color = Color.DarkRed;
                enemigo1.IsInvincible = false;
                enemigo2.IsInvincible = false;
                enemigo3.IsInvincible = false;
                Game.DisplaySubtitle("¡Deten a los 3 agresores!", 5000);
                GameFiber.Wait(5000);
                done = true;
            }
            if (discriminer2 == 2 & Game.LocalPlayer.Character.IsAiming)
            {
                
            }
            if (Game.IsKeyDown(Keys.End))
            {
                End();
            }
            base.Process();
        }

        public void turnOnExtras(Vehicle vehicle, int extra)
        {
            if (Api.Api.doesExtraExist(vehicle, extra))
            {
                NativeFunction.Natives.SET_VEHICLE_EXTRA(vehicle, extra, !true);
                Game.LogTrivial($"[MetroCallouts3] Extra {extra} encendido.");
            }
        }
        public override void End()
        {
            if (wascalloutaccepted)
            {
                MetroCallouts3.Api.Api.Acabar();
                if (compañero1.Exists())
                {
                    compañero1.Dismiss();
                }
                if (compañero2.Exists()) { compañero2.Dismiss(); }
                if (blipcompañero1.Exists()) { blipcompañero1.Delete(); }
                if (blipcompañero2.Exists()) { blipcompañero2.Delete(); }
                if (blipEnemigo1.Exists()) { blipEnemigo1.Delete(); }
                if (blipEnemigo2.Exists()) { blipEnemigo2.Delete(); }
                if (blipEnemigo3.Exists()) { blipEnemigo3.Delete(); }
                if (blipEnemigo4.Exists()) { blipEnemigo4.Delete(); }
                if (blipEnemigo5.Exists()) { blipEnemigo5.Delete(); }
                if (enemigo1.Exists() && enemigo1.IsDead == false && enemigo1.IsCuffed == false){ enemigo1.Dismiss(); }
                if (enemigo2.Exists() && enemigo2.IsDead == false && enemigo2.IsCuffed == false){ enemigo2.Dismiss(); }
                if (enemigo3.Exists() && enemigo3.IsDead == false && enemigo3.IsCuffed == false){ enemigo3.Dismiss(); }


            }
            base.End();
        }
        
    }
}
