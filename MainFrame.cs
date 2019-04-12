using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using Microsoft.Win32;

namespace CustomDiscordRichPresence
{
    public partial class MainFrame : Form
    {

        public static DiscordRpcClient client;

        public static String ClientID = Properties.Settings.Default.ClientID;

        public static String Details = Properties.Settings.Default.Details;
        public static String State = Properties.Settings.Default.State;

        public static String LargeImageKey = Properties.Settings.Default.LargeImageKey;
        public static String LargeImageText = Properties.Settings.Default.LargeImageText;

        public static String SmallImageKey = Properties.Settings.Default.SmallImageKey;
        public static String SmallImageText = Properties.Settings.Default.SmallImageText;



        public MainFrame()
        {
            InitializeComponent();

            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
            if (!key.GetSubKeyNames().Contains("CustomDiscordRichPresence"))
            {
                key.CreateSubKey("CustomDiscordRichPresence");
                key = key.OpenSubKey("CustomDiscordRichPresence", true);
                key.SetValue("ClientID", MainFrame.ClientID);
                key.SetValue("Details", MainFrame.Details);
                key.SetValue("State", MainFrame.State);
                key.SetValue("LargeImageKey", MainFrame.LargeImageKey);
                key.SetValue("LargeImageText", MainFrame.LargeImageText);
                key.SetValue("SmallImageKey", MainFrame.SmallImageKey);
                key.SetValue("SmallImageText", MainFrame.SmallImageText);
            }
            else
            {
                key = key.OpenSubKey("CustomDiscordRichPresence", true);
            }
            MainFrame.ClientID = key.GetValue("ClientID").ToString();
            MainFrame.Details = key.GetValue("Details").ToString();
            MainFrame.State = key.GetValue("State").ToString();
            MainFrame.LargeImageKey = key.GetValue("LargeImageKey").ToString();
            MainFrame.LargeImageText = key.GetValue("LargeImageText").ToString();
            MainFrame.SmallImageKey = key.GetValue("SmallImageKey").ToString();
            MainFrame.SmallImageText = key.GetValue("SmallImageText").ToString();

            textBoxClientID.Text = ClientID;
            textBoxDetails.Text = Details;
            textBoxState.Text = State;
            textBoxLargeImageKey.Text = LargeImageKey;
            textBoxLargeImageText.Text = LargeImageText;
            textBoxSmallImageKey.Text = SmallImageKey;
            textBoxSmallImageText.Text = SmallImageText;

            register();
            update();
        }

        public static void register()
        {
            client = new DiscordRpcClient(ClientID);
            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };
            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };
            client.Initialize();
        }

        public static void update()
        {
            client.SetPresence(new RichPresence()
            {
                Details = Details,
                State = State,
                Assets = new Assets()
                {
                    LargeImageKey = LargeImageKey,
                    LargeImageText = LargeImageText,
                    SmallImageKey = SmallImageKey,
                    SmallImageText = SmallImageText

                }
            });
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if(textBoxClientID.Text != ClientID)
            {
                Properties.Settings.Default.ClientID = ClientID = textBoxClientID.Text;
                register();
            }
            Properties.Settings.Default.Details = Details = textBoxDetails.Text;
            Properties.Settings.Default.State = State = textBoxState.Text;
            Properties.Settings.Default.LargeImageKey= LargeImageKey = textBoxLargeImageKey.Text;
            Properties.Settings.Default.LargeImageText = LargeImageText = textBoxLargeImageText.Text;
            Properties.Settings.Default.SmallImageKey = SmallImageKey = textBoxSmallImageKey.Text;
            Properties.Settings.Default.SmallImageText = SmallImageText = textBoxSmallImageText.Text;
            update();
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
            key.CreateSubKey("CustomDiscordRichPresence");
            key = key.OpenSubKey("CustomDiscordRichPresence", true);
            key.SetValue("ClientID", MainFrame.ClientID);
            key.SetValue("Details", MainFrame.Details);
            key.SetValue("State", MainFrame.State);
            key.SetValue("LargeImageKey", MainFrame.LargeImageKey);
            key.SetValue("LargeImageText", MainFrame.LargeImageText);
            key.SetValue("SmallImageKey", MainFrame.SmallImageKey);
            key.SetValue("SmallImageText", MainFrame.SmallImageText);
            key.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://patreon.com/Freddi_xyz");
        }
    }
}
