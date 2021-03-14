using LSPD_First_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage;
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
    [CalloutInfo("Persona Ardiendo", CalloutProbability.Low)]
    public class personaardiendo1 : Callout
    {
        private Vector3 spawn;
        private Ped persona;
        private Blip punto;
        public bool isHelpShowed;
        public override bool OnBeforeCalloutDisplayed()
        {
            spawn = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(300f));
            persona = new Ped(spawn);
            this.CalloutMessage = "Persona en llamas";
            this.CalloutPosition = spawn;
            Functions.PlayScannerAudioUsingPosition("WE_HAVE PERSONA_ARDIENDO IN_OR_ON_POSITION", spawn);
            ShowCalloutAreaBlipBeforeAccepting(spawn, 40f);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            isHelpShowed = false;
            punto = persona.AttachBlip();
            punto.EnableRoute(Color.Green);
            if (Game.LocalPlayer.Character.DistanceTo(persona) < 25f)
            {
                Game.DisplayHelp("Pulsa T para llamar a los bomberos.", 5000);
            }
            if (Game.LocalPlayer.Character.DistanceTo(persona) < 25f && Game.IsKeyDown(Keys.T))
            {
                Functions.RequestB﻿ackup(Game.LocalPlayer.Character.Position, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.Firetruck);
                Game.DisplayNotification("Los bomberos están de camino a su posición actual.");
                Functions.PlayScannerAudioUsingPosition("BACKUP IN_OR_ON_POSITION", Game.LocalPlayer.Character.Position);
            }
            return base.OnCalloutAccepted();
        }
        public override void OnCalloutNotAccepted()
        {
            if (punto.Exists()) punto.Delete();
            if (persona.Exists()) persona.Delete();
            base.OnCalloutNotAccepted();
        }
        public override void Process()
        {
            if (Game.LocalPlayer.Character.DistanceTo(persona) < 50f) { persona.IsOnFire = true; }
            if (Game.LocalPlayer.Character.DistanceTo(persona) < 25f && isHelpShowed == false)
            {
                Game.DisplayHelp("Pulsa T para llamar a los bomberos.", 3000);
            }
            if (Game.LocalPlayer.Character.DistanceTo(persona) < 25f && Game.IsKeyDown(Keys.T) && isHelpShowed == false)
            {
                Functions.RequestB﻿ackup(Game.LocalPlayer.Character.Position, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.Firetruck);
                Game.DisplayNotification("Los bomberos están de camino a su posición actual.");
                Functions.PlayScannerAudioUsingPosition("BACKUP IN_OR_ON_POSITION", Game.LocalPlayer.Character.Position);
                isHelpShowed = true;
            }
            base.Process();

        }
        public override void End()
        {
            if (punto.Exists()) punto.Delete();
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
            Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
            base.End();
        }
    }
}
