using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestLibVLCSharp
{
    public partial class Form1 : Form
    {
        public LibVLCSharp.WinForms.VideoView videoView2;
        private LibVLC _libVLC;
        private MediaPlayer _mp;

        public Form1()
        {
            if (!DesignMode)
            {
                Core.Initialize();
            }

            InitializeComponent();

            videoView2 = new LibVLCSharp.WinForms.VideoView();

            this.SuspendLayout();

            videoView2.BackColor = System.Drawing.Color.Black;
            videoView2.Location = new System.Drawing.Point(0, 0);
            videoView2.MediaPlayer = null;
            videoView2.Name = "videoView2";
            videoView2.Size = new System.Drawing.Size(1280, 768);
            videoView2.TabIndex = 0;
            videoView2.Text = "videoView2";

            videoView2.Visible = false;

            this.Controls.Add(videoView2);
            videoView2.Click += VideoView2_Click;
            this.ResumeLayout();

        }

        private void _mp_EncounteredError(object sender, EventArgs e)
        {

        }

        private void VideoView2_Click(object sender, EventArgs e)
        {

            videoView2.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnGo_Click(object sender, EventArgs e)
        {

            if (chkYouTube.Checked)
            {
                _libVLC = new LibVLC("--verbose=2");

                    Media media = new Media(_libVLC, "https://www.youtube.com/watch?v=GfO-3Oir-qM", FromType.FromLocation);
                    await media.Parse(MediaParseOptions.ParseNetwork);

                    _mp = new MediaPlayer(media.SubItems.First());

                        videoView2.MediaPlayer = _mp;
                        videoView2.Visible = true;
                        videoView2.BringToFront();
                        var r = _mp.Play();




            }
            else
            {
                LibVLC _libVLC = new LibVLC("--verbose=2");

                Media media = new Media(_libVLC, txtURL.Text.Trim(), FromType.FromLocation);

                MediaPlayer _mp = new MediaPlayer(_libVLC);

                _mp.EncounteredError += _mp_EncounteredError;
                videoView2.MediaPlayer = _mp;
                videoView2.Visible = true;
                videoView2.BringToFront();
                _mp.Play(media);
            }

        }

        private void cboResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableGoButton();
        }

        private void EnableGoButton()
        {
            if (cboResolution.SelectedIndex >= 0 && txtURL.Text.Trim() != String.Empty)
            {
                btnGo.Enabled = true;
            }
            else
            {
                btnGo.Enabled = false;
            }
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            EnableGoButton();
        }
    }
}
