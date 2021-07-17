using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace EmbedBuilderVisualizer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void guna2VSeparator1_Click(object sender, EventArgs e)
		{

		}

		private void guna2Button1_Click(object sender, EventArgs e)
		{
			colorDialog1.ShowDialog();
			guna2VSeparator1.BackColor = colorDialog1.Color;
			guna2VSeparator1.FillColor = colorDialog1.Color;
			guna2VSeparator1.ForeColor = colorDialog1.Color;
		}

		private void guna2TextBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void guna2Button2_Click(object sender, EventArgs e)
		{
			string url = guna2TextBox3.Text;
			WebRequest req = WebRequest.Create(url);
			WebResponse res = req.GetResponse();
			Stream imgStream = res.GetResponseStream();
			Image img1 = Image.FromStream(imgStream);
			imgStream.Close();
			pictureBox1.Image = img1;
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

			string secondurl = guna2TextBox5.Text;
			WebRequest req2 = WebRequest.Create(secondurl);
			WebResponse res2 = req2.GetResponse();
			Stream imgStream2 = res2.GetResponseStream();
			Image img2 = Image.FromStream(imgStream2);
			imgStream2.Close();
			pictureBox2.Image = img2;
			pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

		}

		private void guna2Button3_Click(object sender, EventArgs e)
		{
			EmbedModel data = new EmbedModel();
			data.Colour = guna2ComboBox1.SelectedItem.ToString();
			data.Title = guna2TextBox1.Text;
			data.Description = guna2TextBox2.Text;
			data.Footer = guna2TextBox4.Text;
			data.ThumbnailURL = guna2TextBox3.Text;
			data.ImageURL = guna2TextBox5.Text;
			
			var ouput = JsonConvert.SerializeObject(data);
			File.WriteAllText("EmbedJson.json", ouput);
		}

		private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			Color col = Color.Black;
			switch (guna2ComboBox1.SelectedItem.ToString())
			{
				case "Red":
					col = Color.Red;
					break;
				case "Blue":
					col = Color.Blue;
					break;
				case "White":
					col = Color.White;
					break;
				case "Black":
					col = Color.Black;
					break;
			}
			guna2VSeparator1.BackColor = col;
			guna2VSeparator1.FillColor = col;
			guna2VSeparator1.ForeColor = col;
		}
	}
}
