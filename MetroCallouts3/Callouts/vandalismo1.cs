using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using Rage.Native;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MetroCallouts3.Callouts
{
    [CalloutInfo("Vandalismo", CalloutProbability.Low)]
    public class vandalismo1 : Callout
    {
        private Vehicle victim;
        private Vector3 position;
        private Blip myblip;
        private Blip myblip2;
        private Ped testigo;
        private Vector3 position2;
        private Ped sospechoso1;
        private Ped sospechoso2;
        private Vector3 spawn1;
        private Vehicle vehiculo;
        private Blip sospechoso;
        private Vector3 destino;
        public bool isHelpShowed;

        public override bool OnBeforeCalloutDisplayed()
        {
            this.CalloutMessage = "Vandalismo";
            this.position = new Vector3(-886f, -2188f, 8f);
            this.CalloutPosition = this.position;
            this.destino = new Vector3(2105f, 2904f, 47f);
            this.spawn1 = new Vector3(-801f, -1990f, 9f);
            Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT ASSISTANCE_REQUIRED IN_OR_ON_POSITION UNITS_RESPOND_CODE_02_01", this.position);
            this.position2 = new Vector3(-885f, -2198f, 8f);
            this.victim = new Vehicle("STRETCH", this.position, 42);
            this.AddMinimumDistanceCheck(20f, this.position);
            this.ShowCalloutAreaBlipBeforeAccepting(this.position, 5f);
            this.testigo = new Ped(this.position2, 1f);
            this.vehiculo = new Vehicle("ASEA",spawn1);
            this.sospechoso1 = new Ped("g_m_y_mexgoon_03", this.spawn1, 1f);
            this.sospechoso2 = new Ped("g_m_y_mexgoon_01", this.spawn1, 1f);
            this.sospechoso1.WarpIntoVehicle(this.vehiculo, -1);
            this.sospechoso2.WarpIntoVehicle(this.vehiculo, -2);
            this.sospechoso1.Tasks.DriveToPosition(this.destino, 9f, VehicleDrivingFlags.Normal);
            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            isHelpShowed = false;
            Game.DisplaySubtitle("Habla con el ~y~testigo~w~ e investiga la escena.", 7500);
            this.myblip = ((Entity)this.testigo).AttachBlip();
            this.sospechoso1.RelationshipGroup = "GANG1";
            this.sospechoso2.RelationshipGroup = "GANG1";
            Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Gang1, RelationshipGroup.Cop, Relationship.Hate);
            this.myblip2 = ((Entity)this.victim).AttachBlip();
            this.myblip2.EnableRoute(Color.Green);
            this.myblip2.Color = Color.Green ;
            this.myblip.Color = Color.Yellow;
            this.victim.Deform(position, 20f, 30f);
            this.victim.EngineHealth = 50;
            this.victim.FuelTankHealth = 650;
            this.victim.DirtLevel = 3;
            NativeFunction.CallByName<uint>("SMASH_VEHICLE_WINDOW", victim, 0);
            NativeFunction.CallByName<uint>("SMASH_VEHICLE_WINDOW", victim, 1);
            NativeFunction.CallByName<uint>("SMASH_VEHICLE_WINDOW", victim, -1);
            NativeFunction.CallByName<uint>("SET_VEHICLE_DOOR_OPEN", victim, 5, true, true);
            NativeFunction.CallByName<uint>("SET_VEHICLE_DOOR_OPEN", victim, 3, true, true);
            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (EntityExtensions.Exists((IHandleable)this.victim))
                ((Entity)this.victim).Delete();
            if (EntityExtensions.Exists((IHandleable)this.myblip2))
                this.myblip2.Delete();
            if (EntityExtensions.Exists((IHandleable)this.myblip))
                this.myblip.Delete();
            if (EntityExtensions.Exists((IHandleable)this.testigo))
                ((Entity)this.testigo).Delete();
            if (EntityExtensions.Exists((IHandleable)this.victim))
                ((Entity)this.victim).Delete();
            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();
            if (Game.LocalPlayer.Character.Position.DistanceTo(position2) < 10f && isHelpShowed == false)
                Game.DisplayHelp("Pulsa T para hablar con el testigo.", 7500);
            if (Game.IsKeyDown(Keys.T) && (Game.LocalPlayer.Character.Position.DistanceTo(position2) < 10f) && isHelpShowed == false)
            {
                Game.DisplaySubtitle("~y~Testigo~w~: Hola agente. Estaba saliendo del hotel y he visto a unas personas pegando patadas a la limusina", 7500);
                GameFiber.Sleep(7500);
                Game.DisplaySubtitle("~y~Testigo~w~: Despues han huído en un vehículo.", 2000);
                GameFiber.Sleep(2000);
                Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ ¿Que vehículo?", 3000);
                GameFiber.Sleep(3000);
                Game.DisplaySubtitle("~y~Testigo~w~: Un ~r~" + vehiculo.Model.Name, 3000);
                GameFiber.Sleep(3000);
                Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Muchas gracias por su ayuda.", 3000);
                GameFiber.Sleep(1500);
                int num = (int)Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Se ha localizado al ~r~sospechoso~w~.", "Intercepta al ~r~sospechoso~w~ y arréstalo.");
                this.myblip.DisableRoute();
                this.sospechoso = ((Entity)this.vehiculo).AttachBlip();
                this.sospechoso.Color = Color.DarkRed;
                this.sospechoso.EnableRoute(Color.DarkRed);
                Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN_01 IN_OR_ON_POSITION", this.spawn1);
                isHelpShowed = true;
            }
            if (this.sospechoso2.IsCuffed && this.sospechoso1.IsCuffed)
            {
                if (EntityExtensions.Exists((IHandleable)this.testigo))
                    ((Entity)this.testigo).Dismiss();
                if (EntityExtensions.Exists((IHandleable)this.myblip))
                    this.myblip.Delete();
                if (EntityExtensions.Exists((IHandleable)this.victim))
                    ((Entity)this.victim).Dismiss();
                if (EntityExtensions.Exists((IHandleable)this.myblip2))
                    this.myblip2.Delete();
                if (EntityExtensions.Exists((IHandleable)this.sospechoso))
                    this.sospechoso.Delete();
                int num = (int)Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
                Functions.PlayScannerAudio("WE_ARE_CODE_4");
                base.End();
            }
            if (((Entity)this.sospechoso2).IsDead && ((Entity)this.sospechoso1).IsDead)
            {
                if (EntityExtensions.Exists((IHandleable)this.testigo))
                    ((Entity)this.testigo).Dismiss();
                if (EntityExtensions.Exists((IHandleable)this.myblip))
                    this.myblip.Delete();
                if (EntityExtensions.Exists((IHandleable)this.victim))
                    ((Entity)this.victim).Dismiss();
                if (EntityExtensions.Exists((IHandleable)this.myblip2))
                    this.myblip2.Delete();
                if (EntityExtensions.Exists((IHandleable)this.sospechoso))
                    this.sospechoso.Delete();
                int num = (int)Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
                Functions.PlayScannerAudio("WE_ARE_CODE_4");
                base.End();
            }
            Vector3 position = Game.LocalPlayer.Character.Position;
        ;
        }

        public override void End()
        {

            if (testigo.Exists()) testigo.Dismiss();
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("WE_ARE_CODE_4");
            base.End();
        }
    }
}
