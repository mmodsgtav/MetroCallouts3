using LSPD_First_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage;
using System.Drawing;
using Rage.Native;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting.Entities;

namespace MetroCallouts3.Callouts
{
    [CalloutInfo("Cádaver encontrado", CalloutProbability.Low)]
    public class Cuerpoenelmetro : Callout
    {
        private Ped mySuspect;
        private Vector3 position;
        private Blip myBlip;
        private bool help;
        public override bool OnBeforeCalloutDisplayed()
        {
            this.position = new Vector3(-904f, -2316f, -3f);
            this.mySuspect = new Ped("a_c_pig", position, 61f)
            {
                IsPersistent = true,
                BlockPermanentEvents = true
            };
            this.mySuspect.Kill();
            this.ShowCalloutAreaBlipBeforeAccepting(position, 10f);
            this.AddMinimumDistanceCheck(20f, position);
            this.CalloutMessage = "Cadaver de animal encontrado";
            this.CalloutPosition = position;
            Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT ASSISTANCE_REQUIRED IN_OR_ON_POSITION", this.position);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            help = false;
            myBlip = mySuspect.AttachBlip();
            myBlip.Color = Color.Yellow;
            myBlip.EnableRoute(Color.Yellow);
            Game.DisplaySubtitle("Entra al metro y localiza el animal.", 7500);
            return base.OnCalloutAccepted();
        }
        public override void OnCalloutNotAccepted()
        {
            base.OnCalloutNotAccepted();
            if (mySuspect.Exists()) mySuspect.Delete();
            if (myBlip.Exists()) myBlip.Delete();
        }
        public override void Process()
        {
            base.Process();
            {
                if (Game.LocalPlayer.Character.Position.DistanceTo(mySuspect) < 10f && help == false)
                {
                    Game.DisplayHelp("Llama al forense para retirar el cuerpo.");
                    help = true;
                }
            }
        }
        public override void End()
        {
            if (mySuspect.Exists()) mySuspect.Dismiss();
            if (myBlip.Exists()) myBlip.Delete();
            Game.DisplayNotification("Código 4, servicio finalizado.");
            Functions.PlayScannerAudio("WE_ARE_CODE_4");
            base.End();
        }

    }

    
}
