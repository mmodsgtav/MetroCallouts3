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
using MetroCallouts3.Api;

namespace MetroCallouts3.Callouts
{
    [CalloutInfo("Disparos", CalloutProbability.Low)]
    public class Disparos : Callout
    {
        public Vector3 ubicacion;
        public Vector3 ubicacion2;
        public Vector3 ubicacion3;
        public Vector3 ubicacion4;
        public Vector3 ubicacion5;
        public Vector3 ubicacion6;
        public Vector3 ubicacion7;
        public Vector3 ubicacion8;
        public Vector3 ubicacion9;
        //Vehículos
        public Vehicle polcar1;
        public Vehicle polcar2;
        public Vehicle polcar3;
        public Vehicle polcar4;
        public Vehicle polcar5;
        public Vehicle polcar6;
        public Vehicle polcar7;
        public Vehicle polcar8;
        public Vehicle polcar9;
        //Conos
        public Rage.Object cono1;
        public Rage.Object cono2;
        public Rage.Object cono3;
        public Rage.Object cono4;
        public Rage.Object cono5;
        public Rage.Object cono6;
        public Rage.Object cono7;
        public Rage.Object cono8;
        public Rage.Object cono9;
        public Rage.Object cono10;
        public Rage.Object cono11;
        public Rage.Object cono12;
        public Rage.Object cono13;
        public Rage.Object cono14;
        public Rage.Object cono15;
        public Rage.Object cono16;
        public Rage.Object cono17;
        public Rage.Object cono18;
        public Vehicle[] coches;
        public Rage.Object[] conos;
        public Blip bliip;

        public bool acabado;
        public override bool OnBeforeCalloutDisplayed()
        {
            ubicacion = new Vector3(-184.5361f, -1626.276f, 33.09416f);
            ubicacion2 = new Vector3(-182.6135f, -1690.803f, 32.68624f);
            ubicacion3 = new Vector3(-161.6393f, -1719.891f, 29.59787f);
            ubicacion4 = new Vector3(-225.1227f, -1718.02f, 32.98656f);
            ubicacion5 = new Vector3(-188.0977f, -1657.252f, 33.47849f);
            ubicacion6 = new Vector3(-188.3831f, -1664.344f, 33.47571f);
            ubicacion7 = new Vector3(-187.3826f, -1670.732f, 33.46046f);
            ubicacion8 = new Vector3(-174.2461f, -1666.337f, 33.63322f);
            ubicacion9 = new Vector3(-241.4234f, -1661.417f, 33.52936f);
            CalloutMessage = "Posibles disparos";
            CalloutPosition = ubicacion;
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            acabado = false;
            polcar1 = new Vehicle(Main.EntryPoint.getpatrol1(), ubicacion, 147.0262f);
            polcar2 = new Vehicle(Main.EntryPoint.getpatrol2(), ubicacion2, -43.88238f);
            polcar3 = new Vehicle(Main.EntryPoint.getpatrol5(), ubicacion3, 1.233666f);
            polcar4 = new Vehicle(Main.EntryPoint.getpatrol5(), ubicacion4, 65.41182f);
            polcar5 = new Vehicle(Main.EntryPoint.getpatrol5(), ubicacion5, -0.1320802f);
            polcar6 = new Vehicle(Main.EntryPoint.getpatrol5(), ubicacion6, 1.722642f);
            polcar7 = new Vehicle(Main.EntryPoint.getpatrol5(), ubicacion7, 2.747851f);
            polcar8 = new Vehicle(Main.EntryPoint.getpatrol5(), ubicacion8, 34.15631f);
            polcar9 = new Vehicle(Main.EntryPoint.getpatrol5(), ubicacion9, 127.2988f);
            Vehicle[] coches = { polcar1, polcar2, polcar3, polcar4, polcar5, polcar6, polcar7, polcar8, polcar9 };
            Rage.Object[] conos = { cono1, cono2, cono3, cono4, cono5, cono6, cono7, cono8, cono9, cono10, cono11, cono12,
            cono13, cono14, cono15, cono16, cono17, cono18};
            //Cohes spawned

            ubicacion = new Vector3(-188.8431f, -1621.668f, 32.44833f);
            ubicacion2 = new Vector3(-187.8006f, -1621.828f, 32.48982f);
            ubicacion3 = new Vector3(-186.4022f, -1622.073f, 32.59673f);
            ubicacion4 = new Vector3(-184.9262f, -1622.285f, 32.59221f);
            ubicacion5 = new Vector3(-183.3875f, -1622.601f, 32.58293f);
            ubicacion6 = new Vector3(-182.2606f, -1622.711f, 32.52029f);
            ubicacion7 = new Vector3(-180.9781f, -1622.792f, 32.47971f);
            ubicacion8 = new Vector3(-179.7088f, -1622.815f, 32.37806f);
            ubicacion9 = new Vector3(-177.9263f, -1622.288f, 32.26541f);

            cono1 = new Rage.Object("prop_mp_cone_02", ubicacion, 0f);
            cono2 = new Rage.Object("prop_mp_cone_02", ubicacion2, 0f);
            cono3 = new Rage.Object("prop_mp_cone_02", ubicacion3, 0f);
            cono4 = new Rage.Object("prop_mp_cone_02", ubicacion4, 0f);
            cono5 = new Rage.Object("prop_mp_cone_02", ubicacion5, 0f);
            cono6 = new Rage.Object("prop_mp_cone_02", ubicacion6, 0f);
            cono7 = new Rage.Object("prop_mp_cone_02", ubicacion7, 0f);
            cono8 = new Rage.Object("prop_mp_cone_02", ubicacion8, 0f);
            cono9 = new Rage.Object("prop_mp_cone_02", ubicacion9, 0f);

            ubicacion = new Vector3(-182.002f, -1696.318f, 31.71732f);
            ubicacion2 = new Vector3(-180.5566f, -1694.664f, 31.83724f);
            ubicacion3 = new Vector3(-179.3281f, -1693.23f, 31.90673f);
            ubicacion4 = new Vector3(-178.5793f, -1692.362f, 31.93768f);
            ubicacion5 = new Vector3(-177.4594f, -1691.518f, 31.89664f);
            ubicacion6 = new Vector3(-175.7103f, -1690.426f, 31.79028f);
            ubicacion7 = new Vector3(-173.8704f, -1689.311f, 31.64768f);
            ubicacion8 = new Vector3(-176.5895f, -1691.024f, 31.85254f);
            ubicacion9 = new Vector3(-174.6829f, -1689.854f, 31.70682f);

            cono10 = new Rage.Object("prop_mp_cone_02", ubicacion, 0f);
            cono11 = new Rage.Object("prop_mp_cone_02", ubicacion2, 0f);
            cono12 = new Rage.Object("prop_mp_cone_02", ubicacion3, 0f);
            cono13 = new Rage.Object("prop_mp_cone_02", ubicacion4, 0f);
            cono14 = new Rage.Object("prop_mp_cone_02", ubicacion5, 0f);
            cono15 = new Rage.Object("prop_mp_cone_02", ubicacion6, 0f);
            cono16 = new Rage.Object("prop_mp_cone_02", ubicacion7, 0f);
            cono17 = new Rage.Object("prop_mp_cone_02", ubicacion8, 0f);
            cono18 = new Rage.Object("prop_mp_cone_02", ubicacion9, 0f);

            bliip = polcar1.AttachBlip();
            bliip.Color = Color.Yellow;
            bliip.EnableRoute(Color.LightYellow);
            
            return base.OnCalloutAccepted();
        }
        public bool yaa = false;
        public override void Process()
        {
            if (Game.IsKeyDown(Keys.End)) { End(); }
            if (acabado == true)
            {
                Game.DisplayNotification("char_call911", "char_call911", Main.EntryPoint.NombreAgencia(), "~r~Código 4~w~", "Rétirese de la zona para finalizar el aviso.");
                yaa = true;
            }
            if (yaa == true)
            {
                End();
            }
            base.Process();
        }
        public override void End()
        {
            polcar1.Delete();
            polcar2.Delete();
            polcar3.Delete();
            polcar4.Delete();
            polcar5.Delete();
            polcar6.Delete();
            polcar7.Delete();
            polcar8.Delete();
            polcar9.Delete();

            cono1.Delete();
            cono2.Delete();
            cono3.Delete();
            cono5.Delete();
            cono6.Delete();
            cono7.Delete();
            cono8.Delete();
            cono9.Delete();
            cono10.Delete();
            cono11.Delete();
            cono12.Delete();
            cono13.Delete();
            cono14.Delete();
            cono15.Delete();
            cono16.Delete();
            cono17.Delete();
            cono18.Delete();
            Api.Api.Acabar();
            base.End(); 
        }
    }
}
