using LSPD_First_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage;
using Rage.Native;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting.Entities;

//Identify where your Callouts folder is.
namespace MetroCallouts3.Callouts
{
    //Name the callout, and set the probability.
    [CalloutInfo("Persona Sospechosa", CalloutProbability.Low)]

    //Let PursuitinProgress inherit the Callout class.
    public class personasospechosaconarma : Callout
    {
        public LHandle pursuit;
        public Vector3 SpawnPoint;
        public Blip myBlip;
        public Ped mySuspect;
        public Vehicle myVehicle;

        public override bool OnBeforeCalloutDisplayed()
        {
            //Get a valid spawnpoint for the callout.
            SpawnPoint = new Vector3(-904f, -2316f, -3f);

            
            mySuspect = new Ped("g_m_y_mexgoon_03", SpawnPoint, 61f);
            mySuspect.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", (short) 100, true);
            mySuspect.Tasks.FightAgainstClosestHatedTarget(200f, 1);
            mySuspect.IsPersistent = true;
            mySuspect.BlockPermanentEvents = true;
            if (!mySuspect.Exists()) return false;
            this.ShowCalloutAreaBlipBeforeAccepting(SpawnPoint, 30f);
            this.AddMinimumDistanceCheck(20f, SpawnPoint);

            //Set the callout message(displayed in the notification), and the position(also shown in the notification)
            this.CalloutMessage = "Persona Sospechosa";
            this.CalloutPosition = SpawnPoint;

            //Play the scanner audio using SpawnPoint to identify "POSITION" stated in "IN_OR_ON_POSITION". These audio files can be found in GTA V > LSPDFR > Police Scanner.
            Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT PERSONASOSPECHOSA IN_OR_ON_POSITION", this.SpawnPoint);

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            //Attach myBlip to mySuspect to show where they are.
            myBlip = mySuspect.AttachBlip();
            myBlip.IsFriendly = false;

            //Display a message to let the user know what to do.
            Game.DisplaySubtitle("Entra al metro y localiza al ~r~sospechoso~w~.", 7500);

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            base.OnCalloutNotAccepted();

            //Clean up what we spawned earlier, since the player didn't accept the callout.

            //This states that if mySuspect exists, then we need to delete it.
            if (mySuspect.Exists()) mySuspect.Delete();

            //This states that if myBlip exists, then we need to delete it.
            if (myBlip.Exists()) myBlip.Delete();
        }

        public override void Process()
        {
            base.Process();
            mySuspect.Tasks.Wander();
            {
                if (Game.LocalPlayer.Character.DistanceTo(mySuspect) < 30f) { mySuspect.Tasks.FightAgainst(Game.LocalPlayer.Character); }
            }
            if (mySuspect.IsDead || mySuspect.IsCuffed)
            {
                if (mySuspect.Exists()) mySuspect.Dismiss();
                if (myBlip.Exists()) myBlip.Delete();
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
                Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
                base.End();
            }
        }

        public override void End()
        {
            if (mySuspect.Exists()) mySuspect.Dismiss();
            if (myBlip.Exists()) myBlip.Delete();
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
            Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
            base.End();
        }
    }
}
