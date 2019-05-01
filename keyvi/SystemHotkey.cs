using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using CodeProject.Win32;
using System.Text;
using System.Runtime.InteropServices;

namespace CodeProject.SystemHotkey
{


	/// <summary>
	/// Handles a System Hotkey
	/// </summary>
	public class SystemHotkey : System.ComponentModel.Component,IDisposable
	{
		private System.ComponentModel.Container components = null;
		protected DummyWindowWithEvent m_Window=new DummyWindowWithEvent();	//window for WM_Hotkey Messages
		protected Keys m_HotKey=Keys.None;
		protected bool isRegistered=false;
		public event System.EventHandler Pressed;
		public event System.EventHandler Error;

        protected KeyCodeHash keyCodeHash = new KeyCodeHash();



        //[DllImport("user32.dll")]
        //static extern uint MapVirtualKey(uint uCode, uint uMapType);

        //[DllImport("user32.dll")]
        //static extern int GetKeyNameText(uint lParam, [Out] StringBuilder lpString, int nSize);

        //[DllImport("user32", EntryPoint = "GetKeyNameText")]
        //static extern int GetKeyNameTextA(int lParam, string lpBuffer, int nSize);


		public SystemHotkey(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			m_Window.ProcessMessage+=new MessageEventHandler(MessageEvent);

		}

		public SystemHotkey()
		{
			InitializeComponent();
			if (!DesignMode)
			{
				m_Window.ProcessMessage+=new MessageEventHandler(MessageEvent);
			}
		}

		public new void Dispose()
		{
			if (isRegistered)
			{
				if (UnregisterHotkey())
					System.Diagnostics.Debug.WriteLine("Unreg: OK");
			}
			System.Diagnostics.Debug.WriteLine("Disposed");
		}
	#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion


		protected void MessageEvent(object sender,ref Message m,ref bool Handled)
		{	//Handle WM_Hotkey event
            string keyCodeStr = "";

            keyCodeStr = keyCodeHash.getKeyHash(m.LParam.ToString("X2").Substring(0, 2));
            System.Diagnostics.Debug.WriteLine("Message = " + m);

			if ((m.Msg==(int)Win32.Msgs.WM_HOTKEY)&&(m.WParam==(IntPtr)this.GetType().GetHashCode()))
			{
				Handled=true;
				System.Diagnostics.Debug.WriteLine("HOTKEY pressed!");
				if (Pressed!=null) Pressed(this,EventArgs.Empty);
			}

        }
	
		protected bool UnregisterHotkey()
		{	//unregister hotkey
			return Win32.User32.UnregisterHotKey(m_Window.Handle,this.GetType().GetHashCode());
		}

		protected bool RegisterHotkey(Keys key)
		{	//register hotkey
			int mod=0;
			Keys k2=Keys.None;

			if (((int)key & (int)Keys.Alt)==(int)Keys.Alt) {mod+=(int)Win32.Modifiers.MOD_ALT;k2=Keys.Alt;}
			if (((int)key & (int)Keys.Shift)==(int)Keys.Shift) {mod+=(int)Win32.Modifiers.MOD_SHIFT;k2=Keys.Shift;}
			if (((int)key & (int)Keys.Control)==(int)Keys.Control) {mod+=(int)Win32.Modifiers.MOD_CONTROL;k2=Keys.Control;}

			System.Diagnostics.Debug.Write(mod.ToString()+" ");
			System.Diagnostics.Debug.WriteLine((((int)key)-((int)k2)).ToString());

			return Win32.User32.RegisterHotKey(m_Window.Handle,this.GetType().GetHashCode(),(int)mod,((int)key)-((int)k2));
		}

		public bool IsRegistered
		{
			get{return isRegistered;}
		}


		[DefaultValue(Keys.None)]
		public Keys HotKey
		{
			get { return m_HotKey; }
			set 
			{
				if (DesignMode) {m_HotKey=value;return;}	//Don't register in Designmode
				if ((isRegistered)&&(m_HotKey!=value))	//Unregister previous registered Hotkey
				{
					if (UnregisterHotkey())
					{
						System.Diagnostics.Debug.WriteLine("Unreg: OK");
						isRegistered=false;
					}
					else 
					{
						if (Error!=null) Error(this,EventArgs.Empty);
						System.Diagnostics.Debug.WriteLine("Unreg: ERR");
					}
				}
				if (value==Keys.None) {m_HotKey=value;return;}
				if (RegisterHotkey(value))	//Register new Hotkey
				{
					System.Diagnostics.Debug.WriteLine("Reg: OK");
					isRegistered=true;
				}
				else 
				{
					if (Error!=null) Error(this,EventArgs.Empty);
					System.Diagnostics.Debug.WriteLine("Reg: ERR");
				}
				m_HotKey=value;
			}
		}
	}


    public class KeyCodeHash
    {
        private Hashtable keyHash = new Hashtable();

        public KeyCodeHash()
        {
            keyHash["31"] = "1";
            keyHash["32"] = "2";
            keyHash["33"] = "3";
            keyHash["34"] = "4";
            keyHash["35"] = "5";
            keyHash["36"] = "6";
            keyHash["37"] = "7";
            keyHash["38"] = "8";
            keyHash["39"] = "9";
            keyHash["30"] = "0";
            keyHash["BD"] = "-";
            keyHash["DE"] = "^";
            keyHash["DC"] = "\\";
            keyHash["51"] = "q";
            keyHash["57"] = "w";
            keyHash["45"] = "e";
            keyHash["52"] = "r";
            keyHash["54"] = "t";
            keyHash["59"] = "y";
            keyHash["55"] = "u";
            keyHash["49"] = "i";
            keyHash["4F"] = "o";
            keyHash["50"] = "p";
            keyHash["C0"] = "@";
            keyHash["DB"] = "[";
            keyHash["41"] = "a";
            keyHash["53"] = "s";
            keyHash["44"] = "d";
            keyHash["46"] = "f";
            keyHash["47"] = "g";
            keyHash["48"] = "h";
            keyHash["4A"] = "j";
            keyHash["4B"] = "k";
            keyHash["4C"] = "l";
            keyHash["BB"] = ";";
            keyHash["BA"] = ":";
            keyHash["DD"] = "]";
            keyHash["5A"] = "z";
            keyHash["58"] = "x";
            keyHash["43"] = "c";
            keyHash["56"] = "v";
            keyHash["42"] = "b";
            keyHash["4E"] = "n";
            keyHash["4D"] = "m";
            keyHash["BC"] = ",";
            keyHash["BE"] = ".";
            keyHash["BF"] = "/";
            keyHash["E2"] = "\\";
        }

        public string getKeyHash(string i_str)
        {
            return (string)keyHash[i_str];
        }
    }
}
