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
    [CalloutInfo("llamadauno", CalloutProbability.VeryHigh)]
    public class llamadauno : Callout
    {
        public virtual bool OnBeforeCalloutDisplayed()
        {
            return base.OnBeforeCalloutDisplayed();
        }
    }
