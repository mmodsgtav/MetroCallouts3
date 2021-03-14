using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Mod.API;
using Rage;


namespace MetroCallouts3.Callouts
{
    [CalloutInfo("peleametro", CalloutProbability.Medium)]
    public class peleametro : Callout
    {
        public override bool OnBeforeCalloutDisplayed()
        {
            Random rnd1;
            rnd1 = new Random();
            int numero1 = rnd1.Next(1, Api.Api.spawn_metro.Length + 1);
            CalloutPosition = Api.Api.spawn_metro[numero1];
            return base.OnBeforeCalloutDisplayed();
        }
    }
}
