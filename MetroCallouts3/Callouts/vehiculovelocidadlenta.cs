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
    [CalloutInfo("Vehiculo lento", CalloutProbability.Low)]
    public class vehiculovelocidadlenta : Callout
    {
        public Vector3 spawn;
        public Ped persona;
        public Vehicle coche;
        public Blip blip1;
        public Persona persona_persona;
        public override bool OnBeforeCalloutDisplayed()
        {
            spawn = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(150f, 700f));
            this.CalloutPosition = spawn;
            this.ShowCalloutAreaBlipBeforeAccepting(spawn, 30f);
            
            Model[] VehicleModels1 = new Model[]
           {
               "STOCKADE", "TRACTOR", "TRACTOR2" , "TAILGATER", "FAGGIO2", "CADDY", "CADDY2", "BUS", "MULE", "MULE2", "TRASH", 
           };
            coche = new Vehicle(VehicleModels1[new Random().Next(VehicleModels1.Length)], spawn);
            if (coche.Model.Name == "STOCKADE")
            {
                persona = new Ped("s_m_m_armoured_02", spawn, 90f);
            }
            else { persona = new Ped(spawn); }
            
            persona.IsPersistent = true;
            CalloutMessage = "Vehículo circulando a velocidad lenta";
            Functions.PlayScannerAudioUsingPosition("WE_HAVE 0x1C20369B IN_OR_ON_POSITION", spawn);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            persona_persona = LSPD_First_Response.Mod.API.Functions.GetPersonaForPed(persona);
            LSPD_First_Response.Mod.API.Functions.SetVehicleOwnerName(coche, persona_persona.FullName);
            Game.DisplayHelp("Pulsa ~b~Fin~w~ en cualquier momento para finalizar la llamada", 10000);
            persona.WarpIntoVehicle(coche, -1);
            persona.Tasks.CruiseWithVehicle(coche, 3f, VehicleDrivingFlags.FollowTraffic | VehicleDrivingFlags.Normal);
            blip1 = coche.AttachBlip();
            blip1.Color = Color.DarkRed;
            blip1.EnableRoute(Color.Red);
            return base.OnCalloutAccepted();
            
        }
        public override void Process()
        {

            if (Game.IsKeyDown(Keys.End))
            {
                End();
                Game.LogTrivialDebug("Fin pulsado.");
            }
            bool test = true;
            if (Game.LocalPlayer.Character.Position.DistanceTo(coche) < 25f && test == true) {
                Game.DisplaySubtitle("Realiza una parada de tráfico al sospechoso.", 3000);
                test = false;
            }
            base.Process();
        }
        public override void OnCalloutNotAccepted()
        {
            if (blip1.Exists()) blip1.Delete();
            if (persona.Exists()) persona.Delete();
            if (coche.Exists()) coche.Delete();
            base.OnCalloutNotAccepted();
        }
        public override void End()
        {
            if (blip1.Exists()) blip1.Delete();
            
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
            Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
            base.End();
        }
    }

    
}
