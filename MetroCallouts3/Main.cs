using LSPD_First_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage;
using Rage.Native;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting.Entities;
using MetroCallouts3.Callouts;
using MetroCallouts3.Api;
using System.Net;
using System.Reflection;

public class Main : Plugin
{

    public override void Initialize()
    {

        LSPD_First_Response.Mod.API.Functions.OnOnDutyStateChanged += OnOnDutyStateChangedHandler;

        
        Game.LogTrivial("Metro Callouts 3" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + " ha cargado correctamente.");
        if (Api.internetDisponible())
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] url = wc.DownloadData("https://metrocallouts3.000webhostapp.com/updatechecker/currentversion.html");

            string webData = System.Text.Encoding.UTF8.GetString(url);

            if (Assembly.GetExecutingAssembly().GetName().Version + "" == webData)
            {
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Desarrollado por ~b~mmodsgtav~w~.", "Está ~g~actualizado~w~. Versión: ~g~" + Assembly.GetExecutingAssembly().GetName().Version);
            }
            else
            {
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Desarrollado por ~b~mmodsgtav~w~.", "~r~No está actualizado~w~ y hay ~g~ una nueva versión disponible.");
            }
        }
        if (Api.internetDisponible() == false)
        {
            Game.DisplayNotification("Metro Callouts 3 no pudo comprobar actualizaciones.");
        }

        Game.LogTrivial("Entra en servicio para cargar Metro Callouts 3.");
    }

    public override void Finally()
    {
        Game.LogTrivial("Metro callouts 3 ha sido limpiado correctamente.");
    }
    private static void OnOnDutyStateChangedHandler(bool OnDuty)
    {

        if (OnDuty)
        {

            RegisterCallouts();


            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Desarrollado por ~b~mmodsgtav~w~.", "Ha cargado ~g~correctamente~w~.");
        }
    }

    private static void RegisterCallouts()
    {
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(personasospechosaconarma));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(Cuerpoenelmetro));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(vandalismo1));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(personaardiendo1));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(robodevehiculoespecial1));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(Caravanailegal));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(Vehiculosingasolina));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(vehiculovelocidadlenta));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(vigilantedeseguridad));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(Disparos));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(accidente1));
        LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(asaltoapolicias));
        

        //Functions.RegisterCallout(typeof(peleametro));
    }
    public class EntryPoint
    {
        public static InitializationFile initialiseFile()
        {
            
            InitializationFile ini = new InitializationFile("Plugins/LSPDFR/MetroCallouts3.ini");
            ini.Create();
            return ini;
        }
        public static String getPlayerName()
        {
            
            InitializationFile ini = initialiseFile();
            
            string playerName = ini.ReadString("principal", "nombre", "Agente");
            if (playerName.Length > 12)
            {
                playerName = "Agente";
            }


            return playerName;
        }
        public static String NombreAgencia()
        {
            InitializationFile ini = initialiseFile();
            string agencia_main = ini.ReadString("principal", "agencia", "Policía Nacional");
            if (agencia_main.Length > 15)
            {
                agencia_main = "Policía Nacional";
            }
            return agencia_main;
        }
        public static String getpatrol1()
        {

            InitializationFile ini = initialiseFile();

            string patrol1 = ini.ReadString("vehiculos", "patrulla1", "POLICE");
            Game.Console.Print($"[Metro Callouts 3:] patrulla1 = {patrol1}");
            return patrol1;
        }
        public static String getpatrol2()
        {

            InitializationFile ini = initialiseFile();

            string patrol1 = ini.ReadString("vehiculos", "patrulla2", "POLICE2");
            Game.Console.Print($"[Metro Callouts 3:] patrulla2 = {patrol1}");
            return patrol1;
        }
        public static String getpatrol3()
        {

            InitializationFile ini = initialiseFile();

            string patrol1 = ini.ReadString("vehiculos", "patrulla3", "POLICE3");
            Game.Console.Print($"[Metro Callouts 3:] patrulla3 = {patrol1}");
            return patrol1;
        }
        public static String getpatrol4()
        {

            InitializationFile ini = initialiseFile();

            string patrol1 = ini.ReadString("vehiculos", "patrulla4", "POLICE4");
            Game.Console.Print($"[Metro Callouts 3:] patrulla4 = {patrol1}");
            return patrol1;
        }
        public static String getpatrol5()
        {

            InitializationFile ini = initialiseFile();

            string patrol1 = ini.ReadString("vehiculos", "patrulla5", "POLICET");
            Game.Console.Print($"[Metro Callouts 3:] patrulla5 = {patrol1}");
            return patrol1;
        }
        public static void Main()
        {
            string nombre_final;
            string agencia_final;
            string patrol1;
            string patrol2;
            string patrol3;
            string patrol4;
            string patrol5;
            try
            {
                nombre_final = getPlayerName();
                agencia_final = NombreAgencia();
                patrol1 = getpatrol1();
                patrol2 = getpatrol2();
                patrol3 = getpatrol3();
                patrol4 = getpatrol4();
                patrol5 = getpatrol5();
            }
            catch
            {
                agencia_final = "Policía Nacional";
                nombre_final = "Agente";
                patrol1 = "POLICE";
                patrol2 = "POLICE2";
                patrol3 = "POLICE3";
                patrol4 = "POLICE4";
                patrol5 = "POLICET";
            }
        }
    }
}
