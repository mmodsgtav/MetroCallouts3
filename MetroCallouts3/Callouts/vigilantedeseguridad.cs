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
    [CalloutInfo("Vigilante de seguridad solicita asistencia", CalloutProbability.Low)]
    public class vigilantedeseguridad : Callout
    {
        public Vector3 spawn;
        public Vector3 spawn2;
        public Ped sospechoso;
        public Ped vigilante;
        public Random rnd;
        public int num1;
        public string mensaje;
        public Blip blip1;
        public Blip blip2;
        public bool isHelpShowed;
        public bool isHelpShowed2;
        public Blip blip3;
        public Vector3 entradametro;
        public bool agresion;
        public LHandle persecucion;
        public bool isSecurityFighted;
        public bool accepted;

        public override bool OnBeforeCalloutDisplayed()
        {
            rnd = new Random();
            num1 = rnd.Next(1, 3);
            accepted = false;
            
            if (num1 == 1)
            {
                spawn = new Vector3(-810, -143, 28);
                spawn2 = new Vector3(-809, -141, 28);
                sospechoso = new Ped(spawn, 316);
                vigilante = new Ped("s_m_m_security_01", spawn2, 115);
                entradametro = new Vector3(-854, -128, 13);
                mensaje = "Vigilante de seguridad solicita presencia policial";
            }
            if (num1 == 2)
            {
                entradametro = new Vector3(-854, -128, 13);
                spawn = new Vector3(-296, -300, 10);
                spawn2 = new Vector3(-298, -301, 10);
                sospechoso = new Ped(spawn, 122);
                vigilante = new Ped("s_m_m_security_01", spawn2, 285);
                mensaje = "Vigilante de seguridad solicita asistencia";
            }
            vigilante.IsPersistent = true;
            sospechoso.IsPersistent = true;
            ShowCalloutAreaBlipBeforeAccepting(spawn, 5f);
            CalloutMessage = mensaje;
            CalloutPosition = spawn;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE vigilante1 IN_OR_ON_POSITION", spawn);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            accepted = true;
            isSecurityFighted = false;
            vigilante.Inventory.GiveNewWeapon("WEAPON_NIGHTSTICK", 10, true);
            agresion = false;
            isHelpShowed = false;
            isHelpShowed2 = false;
            blip1 = sospechoso.AttachBlip();
            blip1.Color = Color.Red;
            blip2 = vigilante.AttachBlip();
            blip2.Color = Color.Green;
            blip1.IsFriendly = false;
            blip2.IsFriendly = true;
            blip1.EnableRoute(Color.DarkRed);
            vigilante.RelationshipGroup = "SECURITY_GUARD";
            Game.SetRelationshipBetweenRelationshipGroups("SECURITY_GUAR﻿D", "COP", Relationship.Respect);
            Game.SetRelationshipBetweenRelationshipGroups("COP", "SECURITY_GUARD", Relationship.Respect);
            Game.DisplaySubtitle("Entra al ~y~metro~w~ y habla con el ~g~vigilante~w~ de seguridad.", 5000);
            Game.DisplayHelp("Pulsa ~b~Fin~w~ en cualquier momento para finalizar la llamada.", 15000);
            Random rn2 = new Random();
            
            int num2 = rn2.Next(1, 9);
            Game.LogTrivialDebug("El numero es " + num2);
            if (num2 == 1 || num2 == 3 || num2 == 7)
            {
                
                agresion = true;
                Functions.PlayScannerAudio("WE_HAVE 0x09BEB4C8");
                Game.DisplayNotification("char_call911", "char_call911", Main.EntryPoint.NombreAgencia(), "~r~Código 99~w~", "El vigilante de seguridad está siendo agredido.");
                
                
                
            }
            if (agresion == false)
            {

                if (agresion == false)
                {
                    sospechoso.Tasks.PlayAnimation("random@robbery", "stand_worried_female", 1, AnimationFlags.Loop);
                }
            }
           
            return base.OnCalloutAccepted();
        }
        public override void OnCalloutNotAccepted()
        {
            if (blip1.Exists()) blip1.Delete();
            if (blip2.Exists()) blip2.Delete();
            if (blip3.Exists()) blip3.Delete();
            if (vigilante.Exists()) vigilante.Delete();
            if (sospechoso.Exists()) sospechoso.Delete();
            base.OnCalloutNotAccepted();
        }
        public override void Process()
        {
        
            if (agresion == true && Game.LocalPlayer.Character.Position.DistanceTo(sospechoso) < 10f && isSecurityFighted == false)
            {
                sospechoso.Tasks.FightAgainst(vigilante);
                isSecurityFighted = true;
                Game.LogTrivialDebug("Is securityfighted = " + isSecurityFighted);
            }
            if (Game.IsKeyDown(Keys.End))
            {
                End();
            }
            if (Game.LocalPlayer.Character.Position.DistanceTo(vigilante) < 4f && isHelpShowed==false && agresion == false)
            {
                Game.DisplayHelp("Pulsa ~b~Y~w~ para hablar con el vigilante.", 3000);
            }
            if (agresion == false && Game.LocalPlayer.Character.Position.DistanceTo(vigilante) < 4f && isHelpShowed == false && Game.IsKeyDown(Keys.Y))
            {
                vigilante.PlayAmbientSpeech("GENERIC_HI");

                Game.DisplaySubtitle("~b~Vigilante: ~w~Hola agente, gracias por venir. Hemos retenido a esta persona ya que ha intentado colarse en el metro sin pagar.", 4500);
                GameFiber.Sleep(4500);
                Game.LocalPlayer.Character.PlayAmbientSpeech("GENERIC_THANKS");
                Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Gracias por la información, voy a hablar con el.", 2500);
                Functions.PlayScannerAudio("RANDOMCHAT6");
                Game.LocalPlayer.Character.Tasks.PlayAnimation(new AnimationDictionary("random@arrests"), "generic_radio_chatter", 1, AnimationFlags.UpperBodyOnly | AnimationFlags.SecondaryTask);
                Functions.PlayScannerAudio("");

                isHelpShowed = true;
            }
            vigilante.CanAttackFriendlies = false;
            if ( agresion == false && Game.LocalPlayer.Character.Position.DistanceTo(sospechoso) < 3f && isHelpShowed == true && isHelpShowed2 == false)
            {
                Game.DisplayHelp("Pulsa ~b~Y~w~ para hablar con el sospechoso.");
            }
            if (agresion == false && Game.LocalPlayer.Character.Position.DistanceTo(sospechoso) < 3f && isHelpShowed == true && isHelpShowed2 == false && Game.IsKeyDown(Keys.Y))
            {
                sospechoso.Tasks.Clear();
                Game.LocalPlayer.Character.PlayAmbientSpeech("GENERIC_HEY");
                Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Hola, ¿porque has intentado colarte en el metro?", 3500);
                GameFiber.Sleep(3500);
                Game.DisplaySubtitle("~r~Sospechoso:~w~ Eso es mentira, el vigilante te ha mentido.", 4000);
                sospechoso.PlayAmbientSpeech("GENERIC_INSULT_MED");
                GameFiber.Sleep(4000);
                Game.DisplaySubtitle("~b~" + Main.EntryPoint.getPlayerName() + ":~w~ Gracias, tomaremos las medidas oportunas.", 3500);
                Game.LocalPlayer.Character.Tasks.PlayAnimation(new AnimationDictionary("random@arrests"), "generic_radio_chatter", 1, AnimationFlags.UpperBodyOnly | AnimationFlags.SecondaryTask);
                Functions.PlayScannerAudio("");
                sospechoso.Tasks.Clear();
                Game.DisplayHelp("Arresta al sospechoso o sancionale.");
                Random rnd3 = new Random();
                int int3 = rnd3.Next(1, 3);
                Game.LogTrivialDebug("int3 = " + int3);
                if (int3 == 1)
                {
                    persecucion = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
                    LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(persecucion, true);
                    LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(persecucion, sospechoso);
                    LSPD_First_Response.Mod.API.Functions.AddCopToPursuit(persecucion, vigilante);
                    LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE 0x5699AA80 IN_OR_ON_POSITION", sospechoso.Position);
                    if (isHelpShowed2 == false) { isHelpShowed2 = true; }
                }
                isHelpShowed2 = true;
            }
            if(sospechoso.IsDead || sospechoso.IsCuffed)
            {       
                if (vigilante.Exists()) vigilante.Dismiss();
                if (blip1.Exists()) blip1.Delete();
                if (blip2.Exists()) blip2.Delete();
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "El sospechoso ha muerto o ha sido detenido. Servicio finalizado.");
                Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
                sospechoso.Dismiss();
                base.End();
            }
                base.Process();
        }
        public override void End()
        {
            if (accepted == true)
            {
                if (vigilante.Exists()) vigilante.Dismiss();
                if (blip1.Exists()) blip1.Delete();
                if (blip2.Exists()) blip2.Delete();
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "METRO CALLOUTS 3", "Código 4", "Servicio finalizado.");
                Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
                Game.Console.Print("[MetroCallouts3] Callout was accepted.");
            }
            else
            {
                Game.Console.Print("[MetroCallouts3] Callout was not accepted.");
            }
            base.End();
        }
    }
}