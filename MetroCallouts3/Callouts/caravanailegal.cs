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
    [CalloutInfo("Caravana Ilegal", CalloutProbability.Low)]
    public class Caravanailegal : Callout
    {
        private Vector3 position;
        private Vector3 position2;
        private Ped suspect;
        
        private Vehicle coche;
        private Random rnd1;
        private int num1;
        private Random rnd2;
        private int num2;
        private Blip blip1;
        private LHandle pursuit;
        private bool isHelpshowed;
        public override bool OnBeforeCalloutDisplayed()
        {
            rnd1 = new Random();
            num1 = rnd1.Next(1, 4);
            if(num1 == 1)
            {
                position = new Vector3(-1217, 4455, 30);
                position2 = new Vector3(-1213, 4458, 30);
            }
            if (num1 == 2)
            {
                position = new Vector3(-885, 4432, 20);
                position2 = new Vector3(-885, 4428, 21);
            }
            if (num1 == 3)
            {
                position = new Vector3(-966, 4357, 11);
                position2 = new Vector3(-965, 4355, 11);
            }
            Model[] VehicleModels = new Model[]
            {
                "Camper", "Journey"
            };
            coche = new Vehicle(VehicleModels[new Random().Next(VehicleModels.Length)], position)
            {
                DirtLevel = 1f
            };
            suspect = new Ped(position2, 35f);
            
            CalloutMessage = "Posible caravana ilegal";
            CalloutPosition = position;
            Functions.PlayScannerAudioUsingPosition("WE_HAVE CARAVANAILEGAL", position);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            
            
            isHelpshowed = false;
            Game.DisplayHelp("Pulse ~b~Fin~w~ en cualquier momento para finalizar la llamada.", 7000);
            
            blip1 = suspect.AttachBlip();
            blip1.EnableRoute(Color.Green);
            Game.DisplaySubtitle("Verifica la licencia de camping del ~y~dueño~w~.", 7500);
            
            return base.OnCalloutAccepted();
        }
        public override void Process()
        {
            rnd2 = new Random();
            num2 = rnd2.Next(1, 3);
            
            if (Game.IsKeyDown(Keys.End))
            {
                if (blip1.Exists()) blip1.Delete();
                if (suspect.Exists()) suspect.Dismiss();
                Functions.PlayScannerAudio("WE_ARE_CODE_4");
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
                base.End();
            }
            
            if(Game.LocalPlayer.Character.DistanceTo(suspect) < 4 && isHelpshowed == false) { Game.DisplayHelp(
                "Pulsa ~b~Y~w~ para hablar con esta persona");}
            if (num2 == 1 && Game.LocalPlayer.Character.DistanceTo(suspect) < 4 && Game.IsKeyDown(Keys.Y) && isHelpshowed == false)
            {
                
                    Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Hola, para acampar en esta zona se necesita licencia de camping.", 7500);
                    GameFiber.Sleep(7500);
                    Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Por supuesto agente, aquí tiene mi licencia.", 5000);
                    GameFiber.Sleep(3500);
                    Game.DisplayNotification("darts", "dart_reticules", "Licencia De Camping San Andreas", "Licencia ~g~valida~w~", "Licencia válida hasta el ~g~05/09/2034~w~.");
                    GameFiber.Sleep(2000);
                    Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + "~w~: Perfecto, disculpe las molestias", 2000);
                    GameFiber.Sleep(1500);
                    if (blip1.Exists()) blip1.Delete();
                    if (suspect.Exists()) suspect.Dismiss();
                    Functions.PlayScannerAudio("WE_ARE_CODE_4");
                    Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
                isHelpshowed = true;
                    End();
            }
            if (num2 == 2 && Game.LocalPlayer.Character.DistanceTo(suspect) < 4 && Game.IsKeyDown(Keys.Y) && isHelpshowed == false)
                {
                    Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Hola, para acampar en esta zona se necesita licencia de camping.", 7500);
                    GameFiber.Sleep(7500);
                    Game.DisplaySubtitle("~y~Persona:~w~ Por supuesto agente, aquí tiene mi licencia.", 5000);
                    GameFiber.Sleep(3500);
                    Game.DisplayNotification("darts", "dart_reticules", "Licencia De Camping San Andreas", "Licencia ~r~no valida~w~", "Licencia caducada desde el ~r~06/10/2017~w~.");
                isHelpshowed = true;
                }
                if (num2 == 3 && Game.LocalPlayer.Character.DistanceTo(suspect) < 4 && Game.IsKeyDown(Keys.Y) && isHelpshowed == false)
                {
                    suspect.Tasks.PlayAnimation("", "", -1, AnimationFlags.None);
                    Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Hola, para acampar en esta zona se necesita licencia de camping.", 7500);
                    GameFiber.Sleep(7500);
                    Game.DisplaySubtitle("~y~Persona:~w~ Tome y déjeme tranquilo.", 3000);
                    GameFiber.Sleep(1500);
                    Game.DisplayNotification("darts", "dart_reticules", "Licencia De Camping San Andreas", "Licencia ~r~no valida~w~", "Licencia caducada desde el ~r~06/10/2017~w~.");
                pursuit = Functions.CreatePursuit();
                Functions.AddPedToPursuit(pursuit, suspect);
                isHelpshowed = true;
                    
                }

            
        }


        public override void End()
        {
            if (blip1.Exists()) blip1.Delete();
            if (suspect.IsCuffed == false)
            {
                if (suspect.Exists()) suspect.Dismiss();
            }
            Functions.PlayScannerAudio("WE_ARE_CODE_4");
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
            base.End();
        }
    }
}
