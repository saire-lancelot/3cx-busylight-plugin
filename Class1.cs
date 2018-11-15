// Erstellt im Klassenbibliotheksmodus die entsprechenden DLL Dateien
// © Balz informatik AG / Lars Wolf 
// Ursprung: https://github.com/Dcnigma/Bizylight-3XC-integration

using BusylightTester;
using System;
using System.Windows.Forms;
using MyPhonePlugins;
using System.Reflection;

namespace MyCRMPlugins
{
    [CRMPluginLoader]
    public class MyCrmPlugin
    {
        private static MyCrmPlugin _instance;
        private static IMyPhoneCallHandler _callHandler;
        private static frmMain frm;
         // disabled because not used
         // private Busylight.SDK busylight = null;

         [CRMPluginInitializer]


        // Aktives Farbprofil (Busylight leuchtet nur, wenn 3CX gestartet wurde):
        // Grün, wenn Verfügar
        // Gelb blinkend, wenn gewählt wird oder wenn es klingelt (Busylight wurde stumm geschaltet!)
        // Rot, wenn aktiv in Gespräch
        // Grün, wenn wieder frei
        // Rot, wenn Abwesend oder anderer Status
        public static void Loader(IMyPhoneCallHandler callHandler)
        {
        // show busylight Mainform Disabled in final version
        frm = new frmMain();
         //   frm.Show();
            _instance = new MyCrmPlugin(callHandler);
         
         // Nach Start von 3CX auf Grün wechseln
         frm.busylight.Light(Busylight.BusylightColor.Green);

         // Test button click another form
         //  frm.btnRedWithSound_Click(null, EventArgs.Empty);      
        }

        public MyCrmPlugin(IMyPhoneCallHandler callHandler)
        {
            _callHandler = callHandler;
            _callHandler.OnCallStatusChanged += CallHandlerOnOnCallStatusChanged;
            _callHandler.OnMyPhoneStatusChanged += CallHandlerOnOnMyPhoneStatusChanged;
            _callHandler.CurrentProfileChanged += CallHandlerCurrentProfileChanged;

        }


        public void CallHandlerOnOnCallStatusChanged(object sender, CallStatus callInfo)
        {
            if (callInfo.State == CallState.Connected)
            {

                // Rot, wenn Anruf entgegengenommen wurde
                // frm.btnRedBlinkingWithoutSound_Click(null, EventArgs.Empty);
                frm.busylight.Light(Busylight.BusylightColor.Red);

                // Blau blinken, wenn auf Stumm geschalten wird 
                if (callInfo.IsMuted)
                {
                    frm.btnBlueBlinkingWithoutSound_Click(null, EventArgs.Empty);
                }

            }
            else if (callInfo.State == CallState.Ringing)
            {
                

                // Gelb mit Sound Blinken (working)
                // frm.btnYellowWithSound_Click(null, EventArgs.Empty);

                // Rot mit Sound Blinken (working)
                // frm.btnYellowWithSound_Click(null, EventArgs.Empty);

                // Gelb ohne Sound Blinken (working)
                frm.btnYellowBlinkindWithoutSound_Click(null, EventArgs.Empty);

            }
            
            // Wenn Gespräch beendet wurde, wieder auf Grün schalten
            else if (callInfo.State == CallState.Ended)
            {
                
                frm.busylight.Light(Busylight.BusylightColor.Green);
              
            }
            // Beim Wählen Gelb blinken
            else if (callInfo.State == CallState.Dialing)
            {
                
                // frm.busylight.Light(Busylight.BusylightColor.Yellow);
                frm.btnYellowBlinkindWithoutSound_Click(null, EventArgs.Empty);
            }
            


       
        }
        // 3CX Profilstatus abfragen
        private void CallHandlerCurrentProfileChanged(object sender, CurrentProfileChangedEventArgs profileInfo)
        {   
           

            var profiles = _callHandler.Profiles;
            UserProfileStatus currentProfile = null;

            foreach(var profile in profiles)
            {
                if(profile.ProfileId == profileInfo.NewProfileId)
                {
                    currentProfile = profile;
                }
            }

            // Farbe je nach 3CX Status ändern
            switch (currentProfile.Name )
            {
                // Auf Grün schalten, wenn verfügbar
                case "Available":
                    frm.busylight.Light(Busylight.BusylightColor.Green);
                    break;
                // Auf Rot schalten, wenn nicht verfügbar
                case "Away":
                    frm.busylight.Light(Busylight.BusylightColor.Red);
                    break;
                // Auf Rot schalten, wenn anderer Status gewählt wird (z.B. Ferien)
                default:
                    frm.busylight.Light(Busylight.BusylightColor.Red);
                    // Eigene Farbe definieren (deaktiviert)
                    // var color = new Busylight.BusylightColor();
                    // color.RedRgbValue = 255;
                    // color.GreenRgbValue = 20;
                    // color.BlueRgbValue = 147;
                    // frm.busylight.Light(color);
                    break;     
            }
            
        }



        private void CallHandlerOnOnMyPhoneStatusChanged(object sender, MyPhoneStatus Status)
        {
        }
        public CallStatus MakeCall(string destination)
        {
            
            // frm.busylight.Light(Busylight.BusylightColor.Red);
            frm.btnRedBlinkingWithoutSound_Click(null, EventArgs.Empty);
            return _callHandler.MakeCall(destination);
        }

    }
}
