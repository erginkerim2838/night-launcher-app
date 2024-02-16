using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Levania_Launcher
{
    public partial class SplashScreen : Form
    {

        public SplashScreen()
        {
            InitializeComponent();
        }
        private string githubUrl = "https://raw.githubusercontent.com/erginkerim2838/night-launcher/main/version";
        private string version = "1.0.0";
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DiscordRPC)
            {
                RPC();
            }
            loader.Start();
        }

        private DiscordRPC.EventHandlers handlers;
        private DiscordRPC.RichPresence presence;
        void RPC()
        {
            this.handlers = default(DiscordRPC.EventHandlers);
            DiscordRPC.Initialize("1207339306713223259", ref this.handlers, true, null);
            this.presence.details = "Night Launcher";
            this.presence.state = "Yukleniyor Ekraninda.";
            this.presence.largeImageKey = "logo";
            this.presence.largeImageText = "Night Launcher !";
            this.presence.startTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            DiscordRPC.UpdatePresence(ref this.presence);
        }

        private void loader_Tick(object sender, EventArgs e)
        {
            guna2Panel2.Width += 5;
            if (guna2Panel2.Width >= 0)
            {
                CheckVersion();
                label2.Text = "Version Kontrol Ediliyor..";
            }
            else if (guna2Panel2.Width >= 150)
            {
                label2.Text = "Version Kontrol Edildi.";

            }
            if (guna2Panel2.Width >= 300)
            {
                label2.Text = "İnternet Bağlantısı Kontrol Ediliyor...";
            }
            if (guna2Panel2.Width >= 400)
            {
                label2.Text = "İnternet Bağlantısı Kontrol Edildi.";


            }
            if (guna2Panel2.Width >= 560)
            {
                label2.Text = "Kontroller Başarılı. Başlatılıyor...";
            }
            if (guna2Panel2.Width >= guna2Panel1.Width)
            {
                LoginScreen ls = new LoginScreen();
                ls.Show();
                Hide();
                loader.Stop();
            }


        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void CheckVersion()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string remoteVersion = client.DownloadString(githubUrl).Trim();

                    if (remoteVersion != version)
                    {
                        loader.Stop();
                        MessageBox.Show("Yeni Güncelleme Mevcut !");
                        Process.Start("https://github.com/Fr3zyy/LevaniaLauncher");
                        Application.Exit();

                    }
                }
            }
            catch (Exception ex)
            {
                loader.Stop();
                MessageBox.Show("İnternet Bağlantısı Bulunamadı !");
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
