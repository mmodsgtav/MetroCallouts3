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
    [CalloutInfo("Robo de vehículo especial", CalloutProbability.Low)]
    public class robodevehiculoespecial1 : Callout
    {
        private Vehicle robado;

        private Ped sospechoso;
        private Ped operario1;

        private Vector3 spawn;
        private Vector3 destino;
        private Vector3 operario2location;
        private Vector3 sospechosolocation;
        private Vector3 carspawn;

        private Blip operariosblip;
        private Blip sospechosoblip;

        public LHandle persecucion;

        public bool isHelpShpwed;
        public bool pursuit_;
        public bool hasPursuitStarted;

        public Random rnd;

        public int num;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawn = new Vector3(-1028, -2881, 14);
            destino = World.GetNextPositionOnStreet(spawn.Around(400f, 800f));
            operario2location = new Vector3(-1028, -2880, 14);
            operario1 = new Ped("s_m_y_airworker", spawn, 180);
            sospechosolocation = new Vector3(-1248, -2877, 14);
            sospechoso = new Ped(sospechosolocation, 155);
            carspawn = new Vector3(-1299, -2878, 14);
            Model[] VehicleModels1 = new Model[]
            {
               "RIPLEY", "AIRTUG"
            };

            robado = new Vehicle(VehicleModels1[new Random().Next(VehicleModels1.Length)], carspawn, 244f);
            Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT ASSISTANCE_REQUIRED IN_OR_ON_POSITION", this.operario2location);
            this.CalloutPosition = spawn;
            this.CalloutMessage = "Robo de vehiculo especial.";
            robado.IsPersistent = true;
            operario1.IsPersistent = true;
            sospechoso.WarpIntoVehicle(robado, -1);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            hasPursuitStarted = false;
            rnd = new Random();
            num = rnd.Next(1, 3);
            if (num == 1)
            {
                pursuit_ = true;
            }
            else
            {
                pursuit_ = false;
            }
            Game.LogTrivialDebug("Persecución:  " + pursuit_);
            isHelpShpwed = false;

            sospechoso.Tasks.CruiseWithVehicle(robado, 3.7f, VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.RespectIntersections | VehicleDrivingFlags.DriveAroundPeds); 
            operariosblip = operario1.AttachBlip();
            operariosblip.EnableRoute(Color.Green);
            operariosblip.IsFriendly = true;
            Game.DisplaySubtitle("Habla con el ~b~operario~w~.", 3000);
            return base.OnCalloutAccepted();
        }
        public override void OnCalloutNotAccepted()
        {

               
            if (robado.Exists()) robado.Delete();
            if (operario1.Exists()) operario1.Dismiss();
            if (operariosblip.Exists()) operariosblip.Delete();
            if (sospechoso.Exists()) sospechoso.Delete();
            if (sospechosoblip.Exists()) sospechosoblip.Delete();
            base.OnCalloutNotAccepted();
        }
        public override void Process()
        {
            
            if (Game.LocalPlayer.Character.DistanceTo(operario1) < 3f && isHelpShpwed == false)
            {
                Game.DisplayHelp("Pulsa ~b~Y~w~ para hablar con el operario", 2500);
            }
            if (Game.LocalPlayer.Character.Position.DistanceTo(operario1) < 3f && Game.IsKeyDown(Keys.Y) && isHelpShpwed == false)
            {
                
                Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ ¿Hola, eres tu el que ha llamado al 112?", 3500);
                GameFiber.Sleep(3500);
                Game.DisplaySubtitle("~g~Operario:~w~ Si.", 2500);
                GameFiber.Sleep(2500);
                Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Cuéntame que ha ocurrido.", 3000);
                GameFiber.Sleep(3000);
                Game.DisplaySubtitle("~g~Operario: ~w~Una persona ha venido, me ha sacado a la fuerza del vehículo y me lo ha robado.", 5000);
                GameFiber.Sleep(5000);
                Game.DisplaySubtitle("~g~Operario: ~w~ el vehiculo era un ~r~" + robado.Model.Name, 1800);
                GameFiber.Sleep(1200);
                Game.LocalPlayer.Character.Tasks.PlayAnimation(new AnimationDictionary("random@arrests"), "generic_radio_chatter", 1, AnimationFlags.UpperBodyOnly | AnimationFlags.SecondaryTask);
                GameFiber.Sleep(2000);
                sospechosoblip = sospechoso.AttachBlip();
                sospechosoblip.Color = Color.Red;
                sospechosoblip.IsFriendly = false;
                sospechosoblip.EnableRoute(Color.DarkRed);
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", Main.EntryPoint.NombreAgencia(), "Intercepta al ~r~sospechoso~w~ y arréstalo.");
                Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN_01 IN_OR_ON_POSITION", sospechosolocation);
                isHelpShpwed = true;
            }
            if (pursuit_ == true && Game.LocalPlayer.Character.DistanceTo(sospechoso) < 15f && hasPursuitStarted == false)
            {
                persecucion = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
                LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(persecucion, sospechoso);
                Functions.PlayScannerAudioUsingPosition("WE_HAVE 0x1D09DF42 IN_OR_ON_POSITION", sospechoso.Position);
                
                LSPD_First_Response.Mod.API.Functions.RequestBackup(Game.LocalPlayer.Character.Position, EBackupResponseType.Pursuit, EBackupUnitType.StateUnit);
                hasPursuitStarted = true;
            }
            if (Game.IsKeyDown(Keys.End))
            {
                if (robado.Exists()) robado.Dismiss();
                if (operario1.Exists()) operario1.Dismiss();
                if (sospechosoblip.Exists()) sospechosoblip.Delete();
                if (operariosblip.Exists()) operariosblip.Delete();
                if (sospechoso.Exists()) sospechoso.Dismiss();
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
                Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
                base.End();
            }
            if (sospechoso.IsDead || sospechoso.IsCuffed)
            {
                Game.DisplayHelp("Pulsa ~b~Fin~w~ en cualquier momento para finalizar la llamada", 2000);
               
            }
            base.Process();
        }
        public override void End()
        {
            if (robado.Exists()) robado.Dismiss();
            if (operario1.Exists()) operario1.Dismiss();
            if (sospechosoblip.Exists()) sospechosoblip.Delete();
            if (operariosblip.Exists()) operariosblip.Delete();
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
            Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
            base.End();
        }
    }
}
