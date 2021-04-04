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
    [CalloutInfo("Accidente de tráfico 1", CalloutProbability.Medium)]
    public class accidente1 : Callout
    {
        public Blip blipPositionOnMap;
        public Vector3 positionOnMap;
        public Vehicle vehicle;
        public Ped victim;
        public bool wasCalloutAccepted;
        public Random rnd;
        public int determiner;
        public float x;
        public float y;
        public float z;
        public float orientacion;
        public override bool OnBeforeCalloutDisplayed()
        {
            wasCalloutAccepted = false;
            rnd = new Random();
            determiner = rnd.Next(1, 3);
            if (determiner == 1) {
                x = -749.05f;
                y = 1616.02f;
                z = 209.26f;
                orientacion = 344.29f;
            }
            if (determiner == 2) {
                x = 1549.34f;
                y = -977.73f;
                z = 58.31f;
                orientacion = 123.86f;
            }
            if (determiner == 3)
            {
                x = 799.93f;
                y = 4492.40f;
                z = 50.54f;
                orientacion = 22.30f;
            }
            positionOnMap = new Vector3(x, y, z);
            //AddMaximumDistanceCheck(1200f, positionOnMap);
            CalloutMessage = "Vehículo se ha salido de la calzada";
            CalloutPosition = positionOnMap;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_GRAND_THEFT_AUTO_03 IN_OR_ON_POSITION", positionOnMap);
            ShowCalloutAreaBlipBeforeAccepting(positionOnMap, 2f);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            Game.DisplayHelp($"Pulsa la tecla {Keys.End.ToString()} en cualquier momento para finalizar el aviso.");
            wasCalloutAccepted = true;
            Model[] VehicleModels = new Model[]
            {
                "NINFEF2", "BUS", "COACH", "AIRBUS", "BARRACKS", "BARRACKS2", "BALLER", "BALLER2", "BANSHEE", "BJXL", "BENSON", "BOBCATXL", "BUCCANEER", "BUFFALO", "BUFFALO2", "BULLDOZER", "BULLET", "BURRITO", "BURRITO2", "BURRITO3", "BURRITO4", "BURRITO5", "CAVALCADE", "CAVALCADE2", "GBURRITO", "CAMPER", "CARBONIZZARE", "CHEETAH", "COMET2", "COGCABRIO", "COQUETTE", "GRESLEY", "DUNE2", "HOTKNIFE", "DUBSTA", "DUBSTA2", "DUMP", "DOMINATOR", "EMPEROR", "EMPEROR2", "EMPEROR3", "ENTITYXF", "EXEMPLAR", "ELEGY2", "F620", "FELON", "FELON2", "FELTZER2", "FIRETRUK", "FQ2", "FUGITIVE", "FUTO", "GRANGER", "GAUNTLET", "HABANERO", "INFERNUS", "INTRUDER", "JACKAL", "JOURNEY", "JB700", "KHAMELION", "LANDSTALKER", "MESA", "MESA2", "MESA3", "MIXER", "MINIVAN", "MIXER2", "MULE", "MULE2", "ORACLE", "ORACLE2", "MONROE", "PATRIOT", "PBUS", "PACKER", "PENUMBRA", "PEYOTE", "PHANTOM", "PHOENIX", "PICADOR", "POUNDER", "PRIMO", "RANCHERXL", "RANCHERXL2", "RAPIDGT", "RAPIDGT2", "RENTALBUS", "RUINER", "RIOT", "RIPLEY", "SABREGT", "SADLER", "SADLER2", "SANDKING", "SANDKING2", "SPEEDO", "SPEEDO2", "STINGER", "STOCKADE", "STINGERGT", "SUPERD", "STRATUM", "SULTAN", "AKUMA", "PCJ", "FAGGIO2", "DAEMON", "BATI2"
            };
            vehicle = new Vehicle(VehicleModels[new Random().Next(VehicleModels.Length)], positionOnMap, orientacion);
            victim = new Ped(positionOnMap);
            Game.DisplayNotification("char_call911", "char_call911", Main.EntryPoint.NombreAgencia(), "~g~Información:~w~", "Un testigo ha proporcionado los siguientes datos a central. Vehículo: ~b~" + vehicle.Model.Name + "~w~ Matrícula: ~b~" + vehicle.LicensePlate);
            blipPositionOnMap = vehicle.AttachBlip();
            blipPositionOnMap.Color = Color.Orange;
            blipPositionOnMap.EnableRoute(Color.Yellow);
            Game.DisplayHelp("Pulsa ~b~" + Keys.End + "~w~ en cualquier momento para finalizar la llamada.", 5000);
            Game.DisplaySubtitle("Localiza el vehículo y atiende a la víctima");
            if (determiner == 2)
            {
                Game.LogTrivial("[MetroCallouts3] Determiner was 2, setting vehicle fire.");
                vehicle.IsOnFire = true;
                Game.DisplayNotification("char_call911", "char_call911", Main.EntryPoint.NombreAgencia(), "~g~Información adicional:~w~", "Conductores de la vía informan de que el vehículo está ardiendo. Acude lo antes posible, corta la carretera, y extingue el incendio.");
            }
            victim.WarpIntoVehicle(vehicle, -1);
           
            NativeFunction.CallByName<uint>("SET_VEHICLE_DOOR_OPEN", vehicle, 4, true, true);
            NativeFunction.CallByName<uint>("SMASH_VEHICLE_WINDOW", vehicle, 6);
            NativeFunction.CallByName<uint>("SET_VEHICLE_ENGINE_HEALTH", vehicle, 0f);
            NativeFunction.CallByName<uint>("SET_VEHICLE_BURNOUT", vehicle, true);
            var dimensions = vehicle.Model.Dimensions;
            var halfWidth = (dimensions.X / 2) * 0.6f;
            var halfLength = dimensions.Y / 2;
            var halfHeight = (dimensions.Z / 2) * 0.7f;

            // Apply random deformities within the ranges of the model
            var num = new Random().Next(15, 45);
            for (var index = 0; index < num; ++index)
            {
                // We use half values here, since this is an OFFSET from center
                var randomInt1 = MathHelper.GetRandomSingle(-halfWidth, halfWidth); // Full width
                var randomInt2 = MathHelper.GetRandomSingle(halfLength * 0.85f, halfLength); // Front end
                var randomInt3 = MathHelper.GetRandomSingle(-halfHeight, 0); // Lower half height
                vehicle.Deform(new Vector3(randomInt1, randomInt2, randomInt3), 5f, 5f);
            }
            
            victim.Kill();
            return base.OnCalloutAccepted();
        }
        public override void Process()
        {
            if (determiner == 2)
            {
                vehicle.IsOnFire = true;
            }
            if (Game.IsKeyDown(Keys.End))
            {
                End();
            }
                base.Process();
        }
        public override void OnCalloutNotAccepted()
        {
            
            base.OnCalloutNotAccepted();
        }
        public override void End()
        {
            if (wasCalloutAccepted)
            {
                if (blipPositionOnMap.Exists()) { blipPositionOnMap.Delete(); }
                MetroCallouts3.Api.Api.Acabar();
            }
            
            base.End();
        }
    }
}
