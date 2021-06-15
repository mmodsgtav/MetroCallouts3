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
    public class accidente2 : Callout
    {
        public Vehicle victimVehicle1;
        public Vector3 spawnVehicle;
        public Vehicle victimVehicle2;
        public Ped driver1;
        public Ped driver2;
        public Blip blipDriver1;
        public Blip blipDriver2;
        public bool wasCalloutAccepted;
        public override bool OnBeforeCalloutDisplayed()
        {
            spawnVehicle = new Vector3(522.22f, -534.45f, 35.81f);
            wasCalloutAccepted = false;
            CalloutMessage = "Choque entre dos vehículos";
            CalloutPosition = spawnVehicle;
            ShowCalloutAreaBlipBeforeAccepting(spawnVehicle, 1f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE ", spawnVehicle);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            wasCalloutAccepted = true;
            spawnVehicle = new Vector3(522.22f, -534.45f, 35.81f);
            victimVehicle1 = new Vehicle("BLISTA", spawnVehicle, 100.64f);
            spawnVehicle = new Vector3(533.76f, -528.85f, 35.37f);
            victimVehicle2 = new Vehicle("ASEA", spawnVehicle, 309.81f);
            return base.OnCalloutAccepted();
        }
    }
}
