using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;
using MsHtmHstInterop;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Drawing2D;

namespace DesktopWidget
{
    public partial class WinForm : Form, IDocHostUIHandler
    {
        private AxSHDocVw.AxWebBrowser axWebBrowser1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.ComponentModel.IContainer components;

		public WinForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WinForm));
			this.axWebBrowser1 = new AxSHDocVw.AxWebBrowser();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).BeginInit();
			this.SuspendLayout();
			// 
			// axWebBrowser1
			// 
			this.axWebBrowser1.Enabled = true;
			this.axWebBrowser1.Location = new System.Drawing.Point(8, 8);
			this.axWebBrowser1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser1.OcxState")));
			this.axWebBrowser1.Size = new System.Drawing.Size(300, 150);
			this.axWebBrowser1.TabIndex = 0;
			this.axWebBrowser1.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(this.axWebBrowser1_DocumentComplete);
			// 
			// timer1
			// 
			this.timer1.Interval = 2000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.menuItem1,
						this.menuItem5});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.menuItem2,
						this.menuItem3,
						this.menuItem4});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "&Open ...";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "Exit";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.menuItem6});
			this.menuItem5.Text = "&Help";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "&About ...";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "*.htm";
			this.openFileDialog1.Filter = "HTML (*.htm,*.html)|*.htm";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.axWebBrowser1);
			this.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.ShowInTaskbar = false;
			this.Text = "HTML Widget";
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.Form1_MouseLeave);
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion

		#region init
		private void Form1_Load(object sender, System.EventArgs e)
		{
			this.axWebBrowser1.BeginInit();

			this.Width = ww;
			this.Height = hh;
			this.BackColor = bgcolor;

			this.axWebBrowser1.Left = 10;
			this.axWebBrowser1.Top = 5;
			this.axWebBrowser1.Width = this.ClientSize.Width - 20;
			this.axWebBrowser1.Height = this.ClientSize.Height - 15;

			this.axWebBrowser1.EndInit();

			object o = string.Empty;
			object flags = 0;
			axWebBrowser1.Navigate("about:blank", ref flags, ref o, ref o, ref o);

			ICustomDoc cDoc = (ICustomDoc) this.axWebBrowser1.Document;
			cDoc.SetUIHandler((IDocHostUIHandler) this);

			if(htmlfn==null) htmlfn = "about:blank";
			else
			{
				if (htmlfn.IndexOf(":")<0)
				{
					htmlfn = Directory.GetCurrentDirectory() + "\\" + htmlfn;
				}
			}
			axWebBrowser1.Navigate(htmlfn, ref flags, ref o, ref o, ref o);
		}

		#endregion

		#region show / hide title
		private void Form1_MouseLeave(object sender, System.EventArgs e)
		{
			this.timer1.Enabled = true;
		}

		private void Form1_MouseEnter(object sender, System.EventArgs e)
		{
			showTitle();
			this.timer1.Enabled = false;
		}

		private void hideTitle()
		{
			GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
//			gPath.AddEllipse(0, 25, this.ClientSize.Width, (this.ClientSize.Height/2));

			int hh = (this.Height - this.ClientSize.Height);
			gPath.AddRectangle(new RectangleF(10, hh, this.Width-20, this.Height-hh-10));

			this.Region = new System.Drawing.Region(gPath);
		}

		private void showTitle()
		{
			GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
			gPath.AddRectangle(new RectangleF(0, 0, this.Width, this.Height));
			this.Region = new System.Drawing.Region(gPath);
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			hideTitle();
			this.timer1.Enabled = false;
		}
		#endregion

		#region menu actions
		// Exit ...
		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		// About ...
		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("(C)Copyright 2003 Yiyi Sun.\ryiyisun@yahoo.com\rAll right reserved.", "HTML Widget");
		}

		// Open ...
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if(this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Object o = null;
				axWebBrowser1.Navigate(this.openFileDialog1.FileName, ref o, ref o, ref o, ref o);
			}
		}
		#endregion

		#region IDocHostUIHandler implementation
		void IDocHostUIHandler.EnableModeless(int fEnable)
		{

		}

		void IDocHostUIHandler.FilterDataObject(MsHtmHstInterop.IDataObject pDO, out MsHtmHstInterop.IDataObject ppDORet)
		{
			ppDORet = null;
		}

		void IDocHostUIHandler.GetDropTarget(MsHtmHstInterop.IDropTarget pDropTarget, out MsHtmHstInterop.IDropTarget ppDropTarget)
        {
            ppDropTarget = null;
        }
        
        void IDocHostUIHandler.GetExternal(out object ppDispatch)
        {
            ppDispatch = null;
        }
        
        void IDocHostUIHandler.GetHostInfo(ref _DOCHOSTUIINFO pInfo)
		{
			pInfo.dwFlags |= ( 0x08 /*DOCHOSTUIFLAG_SCROLL_NO*/ |
							   0x04	/*DOCHOSTUIFLAG_NO3DBORDER*/
							 );
        }
        
        void IDocHostUIHandler.GetOptionKeyPath(out string pchKey, uint dw)
        {
            pchKey = null;
        }
        
        void IDocHostUIHandler.HideUI()
        {
        
        }
        
        void IDocHostUIHandler.OnDocWindowActivate(int fActivate)
        {
        
        }
        
        void IDocHostUIHandler.OnFrameWindowActivate(int fActivate)
		{

        }
        
        void IDocHostUIHandler.ResizeBorder(ref MsHtmHstInterop.tagRECT prcBorder, IOleInPlaceUIWindow pUIWindow, int fRameWindow)
        {
        
        }
        
        void IDocHostUIHandler.ShowContextMenu(uint dwID, ref MsHtmHstInterop.tagPOINT ppt, object pcmdtReserved, object pdispReserved)
        {
            
        }
        
        void IDocHostUIHandler.ShowUI(uint dwID, IOleInPlaceActiveObject pActiveObject, IOleCommandTarget pCommandTarget, IOleInPlaceFrame pFrame, IOleInPlaceUIWindow pDoc)
        {
        
        }
        
        void IDocHostUIHandler.TranslateAccelerator(ref tagMSG lpmsg, ref Guid pguidCmdGroup, uint nCmdID)
        {
        
        }
        
        void IDocHostUIHandler.TranslateUrl(uint dwTranslate, ref ushort pchURLIn, IntPtr ppchURLOut)
        {
        
        }
        
        void IDocHostUIHandler.UpdateUI()
        {
        
		}

		#endregion

		#region move window while drag in the client area
		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ReleaseCapture();
			SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
		}

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HTCAPTION = 0x2;
		[DllImportAttribute ("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		[DllImportAttribute ("user32.dll")]
		public static extern bool ReleaseCapture();

		#endregion

		#region get html title
		private void axWebBrowser1_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
		{
			IHTMLDocument2 doc = (IHTMLDocument2) this.axWebBrowser1.Document;
			if (doc.title!=null) this.Text = doc.title;
		}
		#endregion

		#region main function and parameter parsing
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{

			for (int ii = 0; ii < args.Length && args[ii][0] == '-'; ++ii)
			{
				if (args[ii].Equals("-html") && ii + 1 < args.Length )
				{
					htmlfn = args[++ii];
				}
				else if (args[ii].Equals("-width") && ii + 1 < args.Length)
				{
					try
					{
						ww = Convert.ToInt32(args[++ii]);
					}
					catch {}
				}
				else if (args[ii].Equals("-height") && ii + 1 < args.Length)
				{
					try
					{
						hh = Convert.ToInt32(args[++ii]);
					}
					catch {}
				}
				else if (args[ii].Equals("-bgcolor") && ii + 1 < args.Length)
				{
					try
					{
						ColorConverter converter = new ColorConverter();
						bgcolor = (Color) converter.ConvertFrom(args[++ii]);
					}
					catch {}
				}
			}

			Application.Run(new WinForm());
		}

		static string htmlfn = null;
		static int ww = 300;
		static int hh = 300;
		static Color bgcolor = Color.LightGray;
		#endregion
    }
}
