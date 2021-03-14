using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Rage;
using MetroCallouts3.Callouts;
using System.Drawing;
using System.IO;
using LSPD_First_Response.Mod.API;

namespace MetroCallouts3.Api
{

    public class Api
    {
        public static Color[] colores = new Color[142]
            {
      Color.AliceBlue,
      Color.AntiqueWhite,
      Color.Aqua,
      Color.Aquamarine,
      Color.Azure,
      Color.Beige,
      Color.Bisque,
      Color.Black,
      Color.BlanchedAlmond,
      Color.Blue,
      Color.BlueViolet,
      Color.Brown,
      Color.BurlyWood,
      Color.CadetBlue,
      Color.Chartreuse,
      Color.Chocolate,
      Color.Coral,
      Color.CornflowerBlue,
      Color.Cornsilk,
      Color.Crimson,
      Color.Cyan,
      Color.DarkBlue,
      Color.DarkCyan,
      Color.DarkGoldenrod,
      Color.DarkGray,
      Color.DarkGreen,
      Color.DarkKhaki,
      Color.DarkMagenta,
      Color.DarkOliveGreen,
      Color.DarkOrange,
      Color.DarkOrchid,
      Color.DarkRed,
      Color.DarkSalmon,
      Color.DarkSeaGreen,
      Color.DarkSlateBlue,
      Color.DarkSlateGray,
      Color.DarkTurquoise,
      Color.DarkViolet,
      Color.DeepPink,
      Color.DeepSkyBlue,
      Color.DimGray,
      Color.DodgerBlue,
      Color.Empty,
      Color.Firebrick,
      Color.FloralWhite,
      Color.ForestGreen,
      Color.Fuchsia,
      Color.Gainsboro,
      Color.GhostWhite,
      Color.Gold,
      Color.Goldenrod,
      Color.Gray,
      Color.Green,
      Color.GreenYellow,
      Color.Honeydew,
      Color.HotPink,
      Color.IndianRed,
      Color.Indigo,
      Color.Ivory,
      Color.Khaki,
      Color.Lavender,
      Color.LavenderBlush,
      Color.LawnGreen,
      Color.LemonChiffon,
      Color.LightBlue,
      Color.LightCoral,
      Color.LightCyan,
      Color.LightGoldenrodYellow,
      Color.LightGray,
      Color.LightGreen,
      Color.LightPink,
      Color.LightSalmon,
      Color.LightSeaGreen,
      Color.LightSkyBlue,
      Color.LightSlateGray,
      Color.LightSteelBlue,
      Color.LightYellow,
      Color.Lime,
      Color.LimeGreen,
      Color.Linen,
      Color.Magenta,
      Color.Maroon,
      Color.MediumAquamarine,
      Color.MediumBlue,
      Color.MediumOrchid,
      Color.MediumPurple,
      Color.MediumSeaGreen,
      Color.MediumSlateBlue,
      Color.MediumSpringGreen,
      Color.MediumTurquoise,
      Color.MediumVioletRed,
      Color.MidnightBlue,
      Color.MintCream,
      Color.MistyRose,
      Color.Moccasin,
      Color.NavajoWhite,
      Color.Navy,
      Color.OldLace,
      Color.Olive,
      Color.OliveDrab,
      Color.Orange,
      Color.OrangeRed,
      Color.Orchid,
      Color.PaleGoldenrod,
      Color.PaleGreen,
      Color.PaleTurquoise,
      Color.PaleVioletRed,
      Color.PapayaWhip,
      Color.PeachPuff,
      Color.Peru,
      Color.Pink,
      Color.Plum,
      Color.PowderBlue,
      Color.Purple,
      Color.Red,
      Color.RosyBrown,
      Color.RoyalBlue,
      Color.SaddleBrown,
      Color.Salmon,
      Color.SandyBrown,
      Color.SeaGreen,
      Color.SeaShell,
      Color.Sienna,
      Color.Silver,
      Color.SkyBlue,
      Color.SlateBlue,
      Color.SlateGray,
      Color.Snow,
      Color.SpringGreen,
      Color.SteelBlue,
      Color.Tan,
      Color.Teal,
      Color.Thistle,
      Color.Tomato,
      Color.Transparent,
      Color.Turquoise,
      Color.Violet,
      Color.Wheat,
      Color.White,
      Color.WhiteSmoke,
      Color.Yellow,
      Color.YellowGreen
            };

        public static Vector3[] ubicacionesAutopista = new Vector3[] {
        new Vector3(),
        new Vector3(),
        new Vector3(),
        };
        public static Vector3[] spawn_metro = new Vector3[]
        {
            new Vector3(-810, -143, 28),
            new Vector3(-296, -300, 10),
        };
 
       
        
        
        



        public static bool internetDisponible()
        {

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    using (webClient.OpenRead("https://mmods.000webhostapp.com/updatechecker/currentversion.html"))
                        return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public void Spawns()
        {
            Model[] lista1 = new Model[]
           {
                "NINFEF2", "BUS", "COACH", "AIRBUS", "BARRACKS", "BARRACKS2", "BALLER", "BALLER2", "BANSHEE", "BJXL", "BENSON", "BOBCATXL", "BUCCANEER", "BUFFALO", "BUFFALO2", "BULLDOZER", "BULLET", "BURRITO", "BURRITO2", "BURRITO3", "BURRITO4", "BURRITO5", "CAVALCADE", "CAVALCADE2", "GBURRITO", "CAMPER", "CARBONIZZARE", "CHEETAH", "COMET2", "COGCABRIO", "COQUETTE", "GRESLEY", "DUNE2", "HOTKNIFE", "DUBSTA", "DUBSTA2", "DUMP", "DOMINATOR", "EMPEROR", "EMPEROR2", "EMPEROR3", "ENTITYXF", "EXEMPLAR", "ELEGY2", "F620", "FELON", "FELON2", "FELTZER2", "FIRETRUK", "FQ2", "FUGITIVE", "FUTO", "GRANGER", "GAUNTLET", "HABANERO", "INFERNUS", "INTRUDER", "JACKAL", "JOURNEY", "JB700", "KHAMELION", "LANDSTALKER", "MESA", "MESA2", "MESA3", "MIXER", "MINIVAN", "MIXER2", "MULE", "MULE2", "ORACLE", "ORACLE2", "MONROE", "PATRIOT", "PBUS", "PACKER", "PENUMBRA", "PEYOTE", "PHANTOM", "PHOENIX", "PICADOR", "POUNDER", "PRIMO", "RANCHERXL", "RANCHERXL2", "RAPIDGT", "RAPIDGT2", "RENTALBUS", "RUINER", "RIOT", "RIPLEY", "SABREGT", "SADLER", "SADLER2", "SANDKING", "SANDKING2", "SPEEDO", "SPEEDO2", "STINGER", "STOCKADE", "STINGERGT", "SUPERD", "STRATUM", "SULTAN", "AKUMA", "PCJ", "FAGGIO2", "DAEMON", "BATI2"
           };
        }
        public static string[] matriculas = new string[]
        {
            "46EEK574", "45KSL897", "78HSJ326", "63HST45"
        };
        public static void Acabar()
        {
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
            Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
        }
    }

}

