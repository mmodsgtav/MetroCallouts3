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
using System.Reflection;
using Rage.Native;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting.Entities;
using System.IO;
using MetroCallouts3.Api;


namespace MetroCallouts3.Callouts
{
    [CalloutInfo("Vehiculo sin gasolina", CalloutProbability.Low)]
    public class Vehiculosingasolina : Callout
    {
        public Vehicle vehicle1;
        private Ped persona;
        private Blip blip1;
        private Vector3 position;
        private Vector3 position2;
        private Vector3 position3;
        private Vector3 position4;
        private Vector3 position_final;
        private Vector3 position_ped;
        private Random rnd;
        private int num;
        public bool installed;
        public bool isHelpShowed;
        public string path1;
        public override bool OnBeforeCalloutDisplayed()
        {
            Color color1 = Api.Api.colores[new Random().Next(Api.Api.colores.Length)];
            
            path1 = Path.GetFullPath(@"mydir");
            Game.LogTrivialDebug("Ruta de instalación: " + path1);
            
            rnd = new Random();
            num = rnd.Next(1, 5);
            Model[] VehicleModels = new Model[]
           {
                "NINFEF2", "BUS", "COACH", "AIRBUS", "BARRACKS", "BARRACKS2", "BALLER", "BALLER2", "BANSHEE", "BJXL", "BENSON", "BOBCATXL", "BUCCANEER", "BUFFALO", "BUFFALO2", "BULLDOZER", "BULLET", "BURRITO", "BURRITO2", "BURRITO3", "BURRITO4", "BURRITO5", "CAVALCADE", "CAVALCADE2", "GBURRITO", "CAMPER", "CARBONIZZARE", "CHEETAH", "COMET2", "COGCABRIO", "COQUETTE", "GRESLEY", "DUNE2", "HOTKNIFE", "DUBSTA", "DUBSTA2", "DUMP", "DOMINATOR", "EMPEROR", "EMPEROR2", "EMPEROR3", "ENTITYXF", "EXEMPLAR", "ELEGY2", "F620", "FELON", "FELON2", "FELTZER2", "FIRETRUK", "FQ2", "FUGITIVE", "FUTO", "GRANGER", "GAUNTLET", "HABANERO", "INFERNUS", "INTRUDER", "JACKAL", "JOURNEY", "JB700", "KHAMELION", "LANDSTALKER", "MESA", "MESA2", "MESA3", "MIXER", "MINIVAN", "MIXER2", "MULE", "MULE2", "ORACLE", "ORACLE2", "MONROE", "PATRIOT", "PBUS", "PACKER", "PENUMBRA", "PEYOTE", "PHANTOM", "PHOENIX", "PICADOR", "POUNDER", "PRIMO", "RANCHERXL", "RANCHERXL2", "RAPIDGT", "RAPIDGT2", "RENTALBUS", "RUINER", "RIOT", "RIPLEY", "SABREGT", "SADLER", "SADLER2", "SANDKING", "SANDKING2", "SPEEDO", "SPEEDO2", "STINGER", "STOCKADE", "STINGERGT", "SUPERD", "STRATUM", "SULTAN", "AKUMA", "PCJ", "FAGGIO2", "DAEMON", "BATI2"
           };
            
            if (num == 1) {
                position = new Vector3(-388, -1346, 44);
                vehicle1 = new Vehicle(VehicleModels[new Random().Next(VehicleModels.Length)], position, 347);
                position_final = new Vector3(-388, -1352, 45);
                Game.LogTrivialDebug("Callout probability 1");

            }
            if (num == 2)
            {
                position2 = new Vector3(-388, -671, 44);
                vehicle1 = new Vehicle(VehicleModels[new Random().Next(VehicleModels.Length)], position2, 360);
                position_final = new Vector3(-388, -667, 45);
                Game.LogTrivialDebug("Callout probability 2");
            }
            if (num==3)
            {
                position3 = new Vector3(-404, -1190, 44);
                vehicle1 = new Vehicle(VehicleModels[new Random().Next(VehicleModels.Length)], position3, 178);
                position_final = new Vector3(-404, -1196, 45);
                Game.LogTrivialDebug("Callout probability 3");
            }
            if (num == 4)
            {
                position4 = new Vector3(-419, -1414, 44);
                vehicle1 = new Vehicle(VehicleModels[new Random().Next(VehicleModels.Length)], position4, 187);
                position_final = new Vector3(-419, -1420, 45);
                Game.LogTrivialDebug("Callout probability 4");
            }
            position_ped = position_final;
            persona = new Ped(position_ped);
            persona.IsPersistent = true;
            
            vehicle1.FuelLevel = 5f;
            vehicle1.IsEngineOn = true;
            
            CalloutMessage = "Vehículo sin gasolina";
            CalloutPosition = position_final;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE_01 0x13B65F4F IN_OR_ON_POSITION", position_final);
            this.ShowCalloutAreaBlipBeforeAccepting(position_final, 30f);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            Random rnd3 = new Random();
            int int3 = rnd3.Next(1, 5);
            vehicle1.LicensePlate = Api.Api.matriculas[int3];
            Persona suspect_persona = LSPD_First_Response.Mod.API.Functions.GetPersonaForPed(persona);
            Game.DisplayNotification("char_call911", "char_call911", Main.EntryPoint.NombreAgencia(), "~g~Información:~w~", "Vehículo: ~b~" + vehicle1.Model.Name + "~w~ Matrícula: ~b~" + vehicle1.LicensePlate + "~w~ Registrado por: ~b~" + suspect_persona.FullName);
            Random rnd2 = new Random();
            int int2 = rnd2.Next(1, 3);
          
            if (File.Exists(Directory.GetCurrentDirectory() + @"\Plugins\lspdfr\Traffic Policer.dll"))
            {
                Game.LogTrivialDebug("MetroCallouts3: Traffic Policer INSTALADO.");
                installed = true;
            }
            else
            {
                installed = false;
                Game.LogTrivialDebug("MetroCallouts3: Traffic Policer NO INSTALADO.");
            }
            if (installed == true)
            {
                Tr();
            }
            else
            {
              Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Aviso", "Hemos detectado que no tienes Traffic Policer instalado. Esta llamada tiene funciones compatibles con este plugin. ¡Prueba a instalarlo!");
            }
            

            isHelpShowed = false;
            blip1 = persona.AttachBlip();
            blip1.Color = Color.GreenYellow;
            blip1.EnableRoute(Color.Green);
            Game.DisplayHelp("Pulsa ~b~" + Keys.End + "~w~ en cualquier momento para finalizar la llamada.", 5000);
            Game.DisplaySubtitle("Ve hacia el ~g~vehículo~w~ y hazte cargo de él.", 5000);

            return base.OnCalloutAccepted();
        }
        public override void OnCalloutNotAccepted()
        {
            if (vehicle1.Exists()) vehicle1.Delete();
            if (persona.Exists()) persona.Dismiss();
            if (blip1.Exists()) blip1.Delete();
            base.OnCalloutNotAccepted();
        }
        public override void Process()
        {
            Persona suspect_persona = LSPD_First_Response.Mod.API.Functions.GetPersonaForPed(persona);
            LSPD_First_Response.Mod.API.Functions.SetVehicleOwnerName(vehicle1, suspect_persona.FullName);
            NativeFunction.CallByName<uint>("SET_VEHICLE_INDICATOR_LIGHTS", vehicle1, 0, true);
            NativeFunction.CallByName<uint>("SET_VEHICLE_INDICATOR_LIGHTS", vehicle1, 1, true);
            if (Game.LocalPlayer.Character.DistanceTo(persona) < 4f && isHelpShowed == false) { Game.DisplayHelp("Pulsa ~b~Y~w~ para hablar con esta persona."); }
            if (Game.IsKeyDown(Keys.End)) {
                End();
            }
            if (Game.LocalPlayer.Character.Position.DistanceTo(persona) < 4f && Game.IsKeyDown(Keys.Y) && isHelpShowed == false)
            {
                persona.Heading = Game.LocalPlayer.Character.Heading / 180;
                Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Hola, ¿que ha ocurrido?", 2000);
                GameFiber.Sleep(2000);
                
                Game.DisplaySubtitle("~r~Persona:~w~ Estaba conduciendo tranquilamente y se me ha olvidado que tenía poca gasolina.", 3000);
                GameFiber.Sleep(3000);
                Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Vale, gracias por la información.", 2500);
                GameFiber.Sleep(2500);
                Game.DisplayHelp("Retira el vehículo de la carretera y sanciona al conductor.", 6000);
                isHelpShowed = true;

            }
            base.Process();
        }
        public override void End()
        {
            if (vehicle1.Exists()) vehicle1.Dismiss();
            if (persona.IsCuffed == false)
            {
                if (persona.Exists()) persona.Dismiss();
            }
            if (blip1.Exists()) blip1.Delete();
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("WE_ARE_CODE_4");
            base.End();
        }
public void Tr()
        {
            Traffic_Policer.API.Functions.SetVehicleInsuranceStatus(vehicle1, false);
            Traffic_Policer.API.Functions.SetPedAlcoholLevel(persona, true);
            Traffic_Policer.API.Functions.SetPedDrugsLevels(persona, true, false);
            
        }
    }
}
