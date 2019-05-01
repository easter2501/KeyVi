using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml;
using CodeProject.SystemHotkey;
using CodeProject.Win32;
using Microsoft.Win32;

namespace keyvi
{
    /// <summary>
    /// メイン画面。
    /// ホットキーの登録、モード切替、ホットキー押下時の処理を行う。
    /// </summary>
    public partial class KeyVi : Form
    {

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        extern static IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private bool flagDoExit = false;
        private static Icon iconKeyVi = new Icon(EnvVal.KEYVI_COMMAND_MODE_ICON_FILENAME);
        private static Icon iconKeyViKb = new Icon(EnvVal.KEYVI_INPUT_MODE_ICON_FILENAME);
        private static Icon iconKeyViDisable = new Icon(EnvVal.KEYVI_DISABLE_MODE_ICON_FILENAME);
        private static Icon iconKeyViIgnore = new Icon(EnvVal.KEYVI_IGNORE_MODE_ICON_FILENAME);
        //private System.Collections.Hashtable pressed = new Hashtable();



        #region SystemHotkeyの定義
        //アルファベット
        private SystemHotkey hotkeyQ = new SystemHotkey();
        private SystemHotkey hotkeyW = new SystemHotkey();
        private SystemHotkey hotkeyE = new SystemHotkey();
        private SystemHotkey hotkeyR = new SystemHotkey();
        private SystemHotkey hotkeyT = new SystemHotkey();
        private SystemHotkey hotkeyY = new SystemHotkey();
        private SystemHotkey hotkeyU = new SystemHotkey();
        //private SystemHotkey hotkeyI = new SystemHotkey();
        private SystemHotkey hotkeyO = new SystemHotkey();
        private SystemHotkey hotkeyP = new SystemHotkey();
        private SystemHotkey hotkeyA = new SystemHotkey();
        private SystemHotkey hotkeyS = new SystemHotkey();
        private SystemHotkey hotkeyD = new SystemHotkey();
        private SystemHotkey hotkeyF = new SystemHotkey();
        private SystemHotkey hotkeyG = new SystemHotkey();
        private SystemHotkey hotkeyH = new SystemHotkey();
        private SystemHotkey hotkeyJ = new SystemHotkey();
        private SystemHotkey hotkeyK = new SystemHotkey();
        private SystemHotkey hotkeyL = new SystemHotkey();
        private SystemHotkey hotkeyZ = new SystemHotkey();
        private SystemHotkey hotkeyX = new SystemHotkey();
        private SystemHotkey hotkeyC = new SystemHotkey();
        private SystemHotkey hotkeyV = new SystemHotkey();
        private SystemHotkey hotkeyB = new SystemHotkey();
        private SystemHotkey hotkeyN = new SystemHotkey();
        private SystemHotkey hotkeyM = new SystemHotkey();

        //アルファベット（Shiftキー同時押下）
        private SystemHotkey hotkeyShiftH = new SystemHotkey();
        private SystemHotkey hotkeyShiftJ = new SystemHotkey();
        private SystemHotkey hotkeyShiftK = new SystemHotkey();
        private SystemHotkey hotkeyShiftL = new SystemHotkey();

        private SystemHotkey hotkeyShiftX = new SystemHotkey();
        private SystemHotkey hotkeyShiftO = new SystemHotkey();
        private SystemHotkey hotkeyShiftG = new SystemHotkey();
        private SystemHotkey hotkeyShiftI = new SystemHotkey();
        private SystemHotkey hotkeyShiftA = new SystemHotkey();


        //アルファベット（Ctrlキー同時押下）
        private SystemHotkey hotkeyCtrlF = new SystemHotkey();
        private SystemHotkey hotkeyCtrlB = new SystemHotkey();
        private SystemHotkey hotkeyCtrlH = new SystemHotkey();

        //数字（キーボード上段部）
        private SystemHotkey hotkey1 = new SystemHotkey();
        private SystemHotkey hotkey2 = new SystemHotkey();
        private SystemHotkey hotkey3 = new SystemHotkey();
        private SystemHotkey hotkey4 = new SystemHotkey();
        private SystemHotkey hotkey5 = new SystemHotkey();
        private SystemHotkey hotkey6 = new SystemHotkey();
        private SystemHotkey hotkey7 = new SystemHotkey();
        private SystemHotkey hotkey8 = new SystemHotkey();
        private SystemHotkey hotkey9 = new SystemHotkey();
        private SystemHotkey hotkey0 = new SystemHotkey();

        //記号
        private SystemHotkey hotkeyMinus = new SystemHotkey();
        private SystemHotkey hotkeyYen = new SystemHotkey();
        private SystemHotkey hotkeyCaret = new SystemHotkey();
        private SystemHotkey hotkeyAtMark = new SystemHotkey();
        private SystemHotkey hotkeyOpenBrace = new SystemHotkey();
        private SystemHotkey hotkeySemicolon = new SystemHotkey();
        private SystemHotkey hotkeyColon = new SystemHotkey();
        private SystemHotkey hotkeyCloseBrace = new SystemHotkey();
        private SystemHotkey hotkeyComma = new SystemHotkey();
        private SystemHotkey hotkeyPeriod = new SystemHotkey();
        private SystemHotkey hotkeySlash = new SystemHotkey();
        private SystemHotkey hotkeyBackSlash = new SystemHotkey();

        //特殊キー
        private SystemHotkey hotkeyHenkan = new SystemHotkey();
        private SystemHotkey hotkeyMuhenkan = new SystemHotkey();
        private SystemHotkey hotkeyEnter = new SystemHotkey();
        private SystemHotkey hotkeyCaps = new SystemHotkey();

        //独自定義
        private SystemHotkey hotkeyHome = new SystemHotkey();
        private SystemHotkey hotkeyEnd = new SystemHotkey();

        //モード変換
        private SystemHotkey hotkeyEscape = new SystemHotkey();
        private SystemHotkey hotkeyI = new SystemHotkey();

        //環境設定情報
        private EnvVal envVal = new EnvVal();


        //キー押下履歴キュー
        private Queue keyCodeQueue = new Queue();

        //現在アクティブなアプリケーション
        private string NowActiveApplicationFullPath = "";

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ。
        /// ホットキーの定義とモード設定（起動時のデフォルトは編集モード）を行う。
        /// </summary>
        public KeyVi()
        {
            InitializeComponent();

            //起動時は編集モード
            envVal.SetNowMode(EnvVal.EDIT_MODE);



            //アルファベット
            hotkeyQ.Pressed += new EventHandler(hotkeyQ_Pressed);
            hotkeyW.Pressed += new EventHandler(hotkeyW_Pressed);
            hotkeyE.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyR.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyT.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyY.Pressed += new EventHandler(hotkeyY_Pressed);
            hotkeyU.Pressed += new EventHandler(hotkeyU_Pressed);
            //hotkeyI.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyO.Pressed += new EventHandler(hotkeyO_Pressed);
            hotkeyP.Pressed += new EventHandler(hotkeyP_Pressed);
            hotkeyA.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyS.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyD.Pressed += new EventHandler(hotkeyD_Pressed);
            hotkeyF.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyG.Pressed += new EventHandler(hotkeyG_Pressed);
            hotkeyH.Pressed += new EventHandler(hotkeyH_Pressed);
            hotkeyJ.Pressed += new EventHandler(hotkeyJ_Pressed);
            hotkeyK.Pressed += new EventHandler(hotkeyK_Pressed);
            hotkeyL.Pressed += new EventHandler(hotkeyL_Pressed);
            hotkeyZ.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyX.Pressed += new EventHandler(hotkeyX_Pressed);
            hotkeyC.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyV.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyB.Pressed += new EventHandler(hotkeyB_Pressed);
            hotkeyN.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyM.Pressed += new EventHandler(hotkeyDummy_Pressed);

            //アルファベット（Shiftキー同時押下）
            hotkeyShiftH.Pressed += new EventHandler(hotkeyShiftH_Pressed);
            hotkeyShiftJ.Pressed += new EventHandler(hotkeyShiftJ_Pressed);
            hotkeyShiftK.Pressed += new EventHandler(hotkeyShiftK_Pressed);
            hotkeyShiftL.Pressed += new EventHandler(hotkeyShiftL_Pressed);

            hotkeyShiftX.Pressed += new EventHandler(hotkeyShiftX_Pressed);
            hotkeyShiftO.Pressed += new EventHandler(hotkeyShiftO_Pressed);
            hotkeyShiftG.Pressed += new EventHandler(hotkeyShiftG_Pressed);
            hotkeyShiftI.Pressed += new EventHandler(hotkeyShiftI_Pressed);
            hotkeyShiftA.Pressed += new EventHandler(hotkeyShiftA_Pressed);

            //アルファベット（Ctrlキー同時押下）
            hotkeyCtrlF.Pressed += new EventHandler(hotkeyCtrlF_Pressed);
            hotkeyCtrlB.Pressed += new EventHandler(hotkeyCtrlB_Pressed);
            hotkeyCtrlH.Pressed += new EventHandler(hotkeyCtrlH_Pressed);

            //数字（キーボード上段部）
            hotkey1.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkey2.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkey3.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkey4.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkey5.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkey6.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkey7.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkey8.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkey9.Pressed += new EventHandler(hotkeyDummy_Pressed);
            //hotkey0.Pressed += new EventHandler(hotkeyDummy_Pressed);

            //記号
            hotkeyMinus.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyYen.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyCaret.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyAtMark.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyOpenBrace.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeySemicolon.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyColon.Pressed += new EventHandler(hotkeyColon_Pressed);
            hotkeyCloseBrace.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyComma.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyPeriod.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeySlash.Pressed += new EventHandler(hotkeyDummy_Pressed);
            hotkeyBackSlash.Pressed += new EventHandler(hotkeyDummy_Pressed);

            //特殊キー
            hotkeyHenkan.Pressed += new EventHandler(hotkeyHenkan_Presed);
            hotkeyMuhenkan.Pressed += new EventHandler(hotkeyMuhenkan_Pressed);
            hotkeyEnter.Pressed += new EventHandler(hotkeyEnter_Pressed);
            //hotkeyCaps.Pressed += new EventHandler(hotkeyCaps_Pressed);

            //独自定義
            hotkeyHome.Pressed += new EventHandler(hotKeyHome_Pressed);
            hotkeyEnd.Pressed += new EventHandler(hotKeyEnd_Pressed);

            //モード変換
            hotkeyEscape.Pressed += new EventHandler(hotkeyEscape_Pressed);
            hotkeyI.Pressed += new EventHandler(hotkeyI_Pressed);
            //hotkeyScrollLock.Pressed += new EventHandler(hotkeyScrollLock_Pressed);

            Uninstall();
        }

        #endregion

        #region ホットキー押下時の処理
        /// <summary>
        /// 【:】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>コマンド入力履歴キューをクリアした後、キューに【:】を登録する</remarks>
        void hotkeyColon_Pressed(object sender, EventArgs e)
        {
            //Input.ColonKey();
            keyCodeQueue.Clear();
            keyCodeQueue.Enqueue(":");
        }

        /// <summary>
        /// 【Enter】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【Enter】キー押下時点でコマンド履歴キューが【:q】だった場合、
        /// Disableモードへの移行処理を行う</remarks>
        void hotkeyEnter_Pressed(object sender, EventArgs e)
        {
            if (Input.Enter(keyCodeQueue) == EnvVal.SET_DISABLE_MODE)
            {
                menuitemEnableKeyVi.Text = EnvVal.SET_KEYVI_ENABLE;
                Disable();
            }
            if (envVal.IsCommandEnter())
            {
                Input.Enter();
                
            }
        }

        /// <summary>
        /// 【q】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>キューに【q】を登録する</remarks>
        void hotkeyQ_Pressed(object sender, EventArgs e)
        {
            keyCodeQueue.Enqueue("q");
        }

        /// <summary>
        /// 【u】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【Ctrl】+【z】（操作取り消し）を発行する</remarks>
        void hotkeyU_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_U) == true)
            {
                Input.Undo();
            }
        }

        /// <summary>
        /// 【w】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【Ctrl】+【→】（次単語へ移動）を発行する</remarks>
        void hotkeyW_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_W) == true)
            {
                Input.MoveNextWord();
            }
        }

        /// <summary>
        /// 【b】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【Ctrl】+【←】（前単語へ移動）を発行する</remarks>
        void hotkeyB_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_B) == true)
            {
                Input.MovePreviewWord();
            }
        }

        /// <summary>
        /// 【h】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【←】（LEFTキー）を発行する</remarks>
        void hotkeyH_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H) == true)
            {
                Input.LeftKey();
            }
        }

        /// <summary>
        /// 【j】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【↓】（DOWNキー）を発行する</remarks>
        void hotkeyJ_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J) == true)
            {
                Input.DownKey();
            }
        }

        /// <summary>
        /// 【k】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【↑】（UPキー）を発行する</remarks>
        void hotkeyK_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K) == true)
            {
                Input.UpKey();
            }
        }

        /// <summary>
        /// 【l】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【→】（RIGHTキー）を発行する</remarks>
        void hotkeyL_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L) == true)
            {
                Input.RightKey();
            }
        }

        /// <summary>
        /// 【y】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>キューに【y】を登録した後、コピー処理（Input.Copy）を実行する</remarks>
        void hotkeyY_Pressed(object sender, EventArgs e)
        {
            bool CopyEnable = false;
            bool LineCopyEnable = false;

            CopyEnable = envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_Y);
            LineCopyEnable = envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_YY);
            keyCodeQueue.Enqueue("y");
            Input.Copy(keyCodeQueue,LineCopyEnable, CopyEnable);
        }

        /// <summary>
        /// 【x】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// /// <remarks>【DELETE】（削除）を発行する</remarks>
        void hotkeyX_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_X) == true)
            {
                Input.Delete();
            }
        }

        /// <summary>
        /// 【Shift】+【x】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【BACKSPACE】（後退）を発行する</remarks>
        void hotkeyShiftX_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX) == true)
            {
                Input.Backspace();
            }
        }

        /// <summary>
        /// 【d】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>キューに【d】を登録した後、行削除処理（Input.LineDelete）を実行する</remarks>
        void hotkeyD_Pressed(object sender, EventArgs e)
        {
            keyCodeQueue.Enqueue("d");
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_DD) == true)
            {
                Input.LineDelete(keyCodeQueue);
            }
        }

        /// <summary>
        /// 【o】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>現在行の下に空行を挿入し、Inputモードに移行する</remarks>
        void hotkeyO_Pressed(object sender, EventArgs e)
        {
            if (envVal.GetNowMode() == EnvVal.EDIT_MODE) return;
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_O) == true)
            {
                envVal.SetNowMode(EnvVal.EDIT_MODE);
                Uninstall();
                Input.NextRowInsert();
            }
        }

        /// <summary>
        /// 【p】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// /// <remarks>【Ctrl】+【v】（貼り付け）を発行する</remarks>
        void hotkeyP_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_P) == true)
            {
                Input.Paste();
            }
        }



        /// <summary>
        /// 【Shift】+【h】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【Shift】+【←】（ShiftキーとLEFTキーの同時押下）を発行する</remarks>
        void hotkeyShiftH_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H) == true)
            {
                Input.ShiftLeftKey();
            }
        }

        /// <summary>
        /// 【Shift】+【j】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【Shift】+【↓】（ShiftキーとDOWNキーの同時押下）を発行する</remarks>
        void hotkeyShiftJ_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J) == true)
            {
                Input.ShiftDownKey();
            }
        }

        /// <summary>
        /// 【Shift】+【k】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【Shift】+【↑】（ShiftキーとUPキーの同時押下）を発行する</remarks>
        void hotkeyShiftK_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K) == true)
            {
                Input.ShiftUpKey();
            }
        }

        /// <summary>
        /// 【Shift】+【l】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【Shift】+【→】（ShiftキーとRIGHTキーの同時押下）を発行する</remarks>
        void hotkeyShiftL_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L) == true)
            {
                Input.ShiftRightKey();
            }
        }

 

        /// <summary>
        /// 【Shift】+【o】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>現在行の上に空行を挿入し、Inputモードに移行する</remarks>
        void hotkeyShiftO_Pressed(object sender, EventArgs e)
        {
            if (envVal.GetNowMode() == EnvVal.EDIT_MODE) return;
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTO) == true)
            {
                envVal.SetNowMode(EnvVal.EDIT_MODE);
                Uninstall();
                Input.PreviewRowInsert();
            }
        }

        /// <summary>
        /// 【Ctrl】+【f】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【PAGEDOWN】（1画面下にスクロール）を発行する</remarks>
        void hotkeyCtrlF_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF) == true)
            {
                Input.PageDown();
            }
        }

        /// <summary>
        /// 【Ctrl】+【b】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【PAGEUP】（1画面上にスクロール）を発行する</remarks>
        void hotkeyCtrlB_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB) == true)
            {
                Input.PageUp();
            }
        }

        /// <summary>
        /// 【Ctrl】+【h】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【BACKSPACE】（後退）を発行する</remarks>
        void hotkeyCtrlH_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH) == true)
            {
                Input.CtrlH_Backspace();
            }
        }

        /// <summary>
        /// コマンドが割り当てられていないキーを押下した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【F16】を発行する。
        /// 現行のキーボードにはFunctionキーは12までしか無いので、
        /// F16キーを「実際に押す」ことは不可能であることから。</remarks>
        void hotkeyDummy_Pressed(object sender, EventArgs e)
        {
            Input.DummyKey();
        }

        /// <summary>
        /// 【0】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【HOME】（行頭）を発行する</remarks>
        void hotKeyHome_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_ZERO) == true)
            {
                Input.HomeKey();
            }
        }

        /// <summary>
        /// 【Shift】+【4】（$）キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【END】（行末）を発行する</remarks>
        void hotKeyEnd_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4) == true)
            {
                Input.EndKey();
            }
        }

        /// <summary>
        /// 【g】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>キューに【g】を登録した後、文頭移動処理（Input.MoveToDocHead）を実行する</remarks>
        void hotkeyG_Pressed(object sender, EventArgs e)
        {
            keyCodeQueue.Enqueue("g");
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_GG) == true)
            {
                Input.MoveToDocHead(keyCodeQueue);
            }
        }

        /// <summary>
        /// 【Shift】+【g】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>文末移動処理（Input.MoveToDocEnd）を実行する</remarks>
        void hotkeyShiftG_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG) == true)
            {
                Input.MoveToDocEnd();
            }
        }

        /// <summary>
        /// 【変換】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>【半角/全角】（IME ON/OFF）を発行する</remarks>
        void hotkeyHenkan_Presed(object sender, EventArgs e)
        {
            if (envVal.IsHenkanKeyToImeOnOff())
            {
                Input.ImeChange();
            }
            else
            {
                Input.Henkan();
            }
            
        }

        /// <summary>
        /// 【ESC】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Commandモードに移行後、【ESC】を発行する</remarks>
        void hotkeyEscape_Pressed(object sender, EventArgs e)
        {
            if (envVal.GetNowMode() == EnvVal.COMMAND_MODE)
            {
                //SendKeys.Send("{ESC}");
                Input.Escape();
                return;
            }
            
            envVal.SetNowMode(EnvVal.COMMAND_MODE);
            Install();
        }

        /// <summary>
        /// 【無変換】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Commandモードに移行する</remarks>
        void hotkeyMuhenkan_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsHenkanKeyToImeOnOff())
            {
                if (envVal.GetNowMode() == EnvVal.COMMAND_MODE)
                {
                    return;
                }

                envVal.SetNowMode(EnvVal.COMMAND_MODE);
                Install();
            }
            else
            {
                Input.Muhenkan();
            }
        }

        /// <summary>
        /// 【i】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Inputモードに移行する</remarks>
        void hotkeyI_Pressed(object sender, EventArgs e)
        {
            if (envVal.GetNowMode() == EnvVal.EDIT_MODE) return;
            envVal.SetNowMode(EnvVal.EDIT_MODE);
            Uninstall();
        }

        /// <summary>
        /// 【Shift】+【i】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>行頭に移動後、Inputモードに移行する</remarks>
        void hotkeyShiftI_Pressed(object sender, EventArgs e)
        {
            if (envVal.GetNowMode() == EnvVal.EDIT_MODE) return;
            Input.HomeKey();
            Input.ShiftKeyUp();
            envVal.SetNowMode(EnvVal.EDIT_MODE);
            Uninstall();
        }

        /// <summary>
        /// 【Shift】+【A】キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>行末に移動後、Inputモードに移行する</remarks>
        void hotkeyShiftA_Pressed(object sender, EventArgs e)
        {
            if (envVal.GetNowMode() == EnvVal.EDIT_MODE) return;
            Input.EndKey();
            Input.ShiftKeyUp();
            envVal.SetNowMode(EnvVal.EDIT_MODE);
            Uninstall();
        }


        #endregion

        #region モード切替(Commandモード)

        /// <summary>
        /// Commandモードへの移行処理
        /// </summary>
        /// <remarks>各種ホットキーの登録、タスクトレイアイコンの設定を行う</remarks>
        private void Install()
        {
            // ホットキーの登録解除
            hotkeyEscape.HotKey = Keys.None;

            //// ホットキーの登録
            //アルファベット
            hotkeyQ.HotKey = Keys.Q;
            hotkeyW.HotKey = Keys.W;
            hotkeyE.HotKey = Keys.E;
            hotkeyR.HotKey = Keys.R;
            hotkeyT.HotKey = Keys.T;
            hotkeyY.HotKey = Keys.Y;
            hotkeyU.HotKey = Keys.U;
            //hotkeyI.HotKey = Keys.I;
            hotkeyO.HotKey = Keys.O;
            hotkeyP.HotKey = Keys.P;
            hotkeyA.HotKey = Keys.A;
            hotkeyS.HotKey = Keys.S;
            hotkeyD.HotKey = Keys.D;
            hotkeyF.HotKey = Keys.F;
            hotkeyG.HotKey = Keys.G;
            hotkeyH.HotKey = Keys.H;
            hotkeyH.HotKey = Keys.H;
            hotkeyJ.HotKey = Keys.J;
            hotkeyK.HotKey = Keys.K;
            hotkeyL.HotKey = Keys.L;
            hotkeyZ.HotKey = Keys.Z;
            hotkeyX.HotKey = Keys.X;
            hotkeyC.HotKey = Keys.C;
            hotkeyV.HotKey = Keys.V;
            hotkeyB.HotKey = Keys.B;
            hotkeyN.HotKey = Keys.N;
            hotkeyM.HotKey = Keys.M;

            //アルファベット（Shiftキー同時押下）
            hotkeyShiftH.HotKey = Keys.Shift | Keys.H;
            hotkeyShiftJ.HotKey = Keys.Shift | Keys.J;
            hotkeyShiftK.HotKey = Keys.Shift | Keys.K;
            hotkeyShiftL.HotKey = Keys.Shift | Keys.L;

            hotkeyShiftX.HotKey = Keys.Shift | Keys.X;
            hotkeyShiftO.HotKey = Keys.Shift | Keys.O;
            hotkeyShiftG.HotKey = Keys.Shift | Keys.G;
            hotkeyShiftI.HotKey = Keys.Shift | Keys.I;
            hotkeyShiftA.HotKey = Keys.Shift | Keys.A;

            //アルファベット（Ctrlキー同時押下）
            hotkeyCtrlF.HotKey = Keys.Control | Keys.F;
            hotkeyCtrlB.HotKey = Keys.Control | Keys.B;
            hotkeyCtrlH.HotKey = Keys.Control | Keys.H;

            //数字（キーボード上段部）
            hotkey1.HotKey = Keys.D1;
            hotkey2.HotKey = Keys.D2;
            hotkey3.HotKey = Keys.D3;
            hotkey4.HotKey = Keys.D4;
            hotkey5.HotKey = Keys.D5;
            hotkey6.HotKey = Keys.D6;
            hotkey7.HotKey = Keys.D7;
            hotkey8.HotKey = Keys.D8;
            hotkey9.HotKey = Keys.D9;
            //hotkey0.HotKey = Keys.D0;

            //記号
            hotkeyMinus.HotKey = Keys.OemMinus;
            hotkeyYen.HotKey = Keys.Oem5;
            hotkeyCaret.HotKey = Keys.Oem7;
            hotkeyAtMark.HotKey = Keys.Oemtilde;
            hotkeyOpenBrace.HotKey = Keys.OemOpenBrackets;
            hotkeySemicolon.HotKey = Keys.Oemplus;
            switch (envVal.KeyBoardType())
            {
                case EnvVal.KEYBOADTYPE_JP106: 
                    hotkeyColon.HotKey = Keys.Oem1;
                    break;
                case EnvVal.KEYBOADTYPE_US:
                    hotkeyColon.HotKey = Keys.Shift | Keys.Oem1;
                    break;
            }
            
            hotkeyCloseBrace.HotKey = Keys.Oem6;
            hotkeyComma.HotKey = Keys.Oemcomma;
            hotkeyPeriod.HotKey = Keys.OemPeriod;
            hotkeySlash.HotKey = Keys.OemQuestion;
            hotkeyBackSlash.HotKey = Keys.OemBackslash;

            //特殊キー
            hotkeyHenkan.HotKey = Keys.IMEConvert;
            hotkeyEnter.HotKey = Keys.Enter;

            //独自定義
            hotkeyHome.HotKey = Keys.D0;
            hotkeyEnd.HotKey = Keys.Shift | Keys.D4;

            //モード変換
            hotkeyI.HotKey = Keys.I;
            //hotkeyScrollLock.HotKey = Keys.Scroll;

            notifyIcon.Icon = iconKeyVi;

        }

#endregion

        #region モード切替(Inputモード)

        /// <summary>
        /// Inputモードへの移行処理
        /// </summary>
        /// <remarks>各種ホットキーの解除、Commandモード移行用ホットキーの登録、タスクトレイアイコンの設定を行う</remarks>
        private void Uninstall()
        {
            //// ホットキーの登録解除
            //アルファベット
            hotkeyQ.HotKey = Keys.None;
            hotkeyW.HotKey = Keys.None;
            hotkeyE.HotKey = Keys.None;
            hotkeyR.HotKey = Keys.None;
            hotkeyT.HotKey = Keys.None;
            hotkeyY.HotKey = Keys.None;
            hotkeyU.HotKey = Keys.None;
            //hotkeyI.HotKey = Keys.None;
            hotkeyO.HotKey = Keys.None;
            hotkeyP.HotKey = Keys.None;
            hotkeyA.HotKey = Keys.None;
            hotkeyS.HotKey = Keys.None;
            hotkeyD.HotKey = Keys.None;
            hotkeyF.HotKey = Keys.None;
            hotkeyG.HotKey = Keys.None;
            hotkeyH.HotKey = Keys.None;
            hotkeyJ.HotKey = Keys.None;
            hotkeyK.HotKey = Keys.None;
            hotkeyL.HotKey = Keys.None;
            hotkeyZ.HotKey = Keys.None;
            hotkeyX.HotKey = Keys.None;
            hotkeyC.HotKey = Keys.None;
            hotkeyV.HotKey = Keys.None;
            hotkeyB.HotKey = Keys.None;
            hotkeyN.HotKey = Keys.None;
            hotkeyM.HotKey = Keys.None;

            //アルファベット（Shiftキー同時押下）
            hotkeyShiftH.HotKey = Keys.None;
            hotkeyShiftJ.HotKey = Keys.None;
            hotkeyShiftK.HotKey = Keys.None;
            hotkeyShiftL.HotKey = Keys.None;

            hotkeyShiftX.HotKey = Keys.None;
            hotkeyShiftO.HotKey = Keys.None;
            hotkeyShiftG.HotKey = Keys.None;
            hotkeyShiftI.HotKey = Keys.None;
            hotkeyShiftA.HotKey = Keys.None;

            //アルファベット（Ctrlキー同時押下）
            hotkeyCtrlF.HotKey = Keys.None;
            hotkeyCtrlB.HotKey = Keys.None;
            hotkeyCtrlH.HotKey = Keys.None;

            //数字（キーボード上段部）
            hotkey1.HotKey = Keys.None;
            hotkey2.HotKey = Keys.None;
            hotkey3.HotKey = Keys.None;
            hotkey4.HotKey = Keys.None;
            hotkey5.HotKey = Keys.None;
            hotkey6.HotKey = Keys.None;
            hotkey7.HotKey = Keys.None;
            hotkey8.HotKey = Keys.None;
            hotkey9.HotKey = Keys.None;
            //hotkey0.HotKey = Keys.None;

            //記号
            hotkeyMinus.HotKey = Keys.None;
            hotkeyYen.HotKey = Keys.None;
            hotkeyCaret.HotKey = Keys.None;
            hotkeyAtMark.HotKey = Keys.None;
            hotkeyOpenBrace.HotKey = Keys.None;
            hotkeySemicolon.HotKey = Keys.None;
            hotkeyColon.HotKey = Keys.None;
            hotkeyCloseBrace.HotKey = Keys.None;
            hotkeyComma.HotKey = Keys.None;
            hotkeyPeriod.HotKey = Keys.None;
            hotkeySlash.HotKey = Keys.None;
            hotkeyBackSlash.HotKey = Keys.None;

            //特殊キー
            hotkeyHenkan.HotKey = Keys.IMEConvert;
            hotkeyEnter.HotKey = Keys.None;


            //独自定義
            hotkeyHome.HotKey = Keys.None;
            hotkeyEnd.HotKey = Keys.None;

            //モード変換
            hotkeyI.HotKey = Keys.None;
            hotkeyEscape.HotKey = Keys.Escape;
            hotkeyMuhenkan.HotKey = Keys.IMENonconvert;

            notifyIcon.Icon = iconKeyViKb;
        }
        #endregion

        #region モード切替(Disableモード)

        /// <summary>
        /// Disableモードへの移行処理
        /// </summary>
        /// <remarks>各種ホットキーの解除を行う。Disableモードに移行すると、キーボード押下でKeyViを有効にすることはできなくなる。</remarks>
        private void Disable()
        {
            //// ホットキーの登録解除
            //アルファベット
            hotkeyQ.HotKey = Keys.None;
            hotkeyW.HotKey = Keys.None;
            hotkeyE.HotKey = Keys.None;
            hotkeyR.HotKey = Keys.None;
            hotkeyT.HotKey = Keys.None;
            hotkeyY.HotKey = Keys.None;
            hotkeyU.HotKey = Keys.None;
            //hotkeyI.HotKey = Keys.None;
            hotkeyO.HotKey = Keys.None;
            hotkeyP.HotKey = Keys.None;
            hotkeyA.HotKey = Keys.None;
            hotkeyS.HotKey = Keys.None;
            hotkeyD.HotKey = Keys.None;
            hotkeyF.HotKey = Keys.None;
            hotkeyG.HotKey = Keys.None;
            hotkeyH.HotKey = Keys.None;
            hotkeyJ.HotKey = Keys.None;
            hotkeyK.HotKey = Keys.None;
            hotkeyL.HotKey = Keys.None;
            hotkeyZ.HotKey = Keys.None;
            hotkeyX.HotKey = Keys.None;
            hotkeyC.HotKey = Keys.None;
            hotkeyV.HotKey = Keys.None;
            hotkeyB.HotKey = Keys.None;
            hotkeyN.HotKey = Keys.None;
            hotkeyM.HotKey = Keys.None;

            //アルファベット（Shiftキー同時押下）
            hotkeyShiftH.HotKey = Keys.None;
            hotkeyShiftJ.HotKey = Keys.None;
            hotkeyShiftK.HotKey = Keys.None;
            hotkeyShiftL.HotKey = Keys.None;

            hotkeyShiftX.HotKey = Keys.None;
            hotkeyShiftO.HotKey = Keys.None;
            hotkeyShiftG.HotKey = Keys.None;
            hotkeyShiftI.HotKey = Keys.None;
            hotkeyShiftA.HotKey = Keys.None;

            //アルファベット（Ctrlキー同時押下）
            hotkeyCtrlF.HotKey = Keys.None;
            hotkeyCtrlB.HotKey = Keys.None;
            hotkeyCtrlH.HotKey = Keys.None;

            //数字（キーボード上段部）
            hotkey1.HotKey = Keys.None;
            hotkey2.HotKey = Keys.None;
            hotkey3.HotKey = Keys.None;
            hotkey4.HotKey = Keys.None;
            hotkey5.HotKey = Keys.None;
            hotkey6.HotKey = Keys.None;
            hotkey7.HotKey = Keys.None;
            hotkey8.HotKey = Keys.None;
            hotkey9.HotKey = Keys.None;
            //hotkey0.HotKey = Keys.None;

            //記号
            hotkeyMinus.HotKey = Keys.None;
            hotkeyYen.HotKey = Keys.None;
            hotkeyCaret.HotKey = Keys.None;
            hotkeyAtMark.HotKey = Keys.None;
            hotkeyOpenBrace.HotKey = Keys.None;
            hotkeySemicolon.HotKey = Keys.None;
            hotkeyColon.HotKey = Keys.None;
            hotkeyCloseBrace.HotKey = Keys.None;
            hotkeyComma.HotKey = Keys.None;
            hotkeyPeriod.HotKey = Keys.None;
            hotkeySlash.HotKey = Keys.None;
            hotkeyBackSlash.HotKey = Keys.None;

            //特殊キー
            //hotkeyHenkan.HotKey = Keys.None;      //変換キーによるIME ON/OFFはモード不問
            hotkeyEnter.HotKey = Keys.None;
            hotkeyCaps.HotKey = Keys.Capital;    //CapsLock → Ctrlはモード不問

            //独自定義
            hotkeyHome.HotKey = Keys.None;
            hotkeyEnd.HotKey = Keys.None;

            //モード変換
            hotkeyI.HotKey = Keys.None;
            hotkeyEscape.HotKey = Keys.None;
            hotkeyMuhenkan.HotKey = Keys.None;

            notifyIcon.Icon = iconKeyViDisable;

            envVal.setKeyViDisable();
        }
        #endregion

        #region モード切替(Ignoreモード)

        /// <summary>
        /// Ignoreモードへの移行処理
        /// </summary>
        /// <remarks>KeyViを動作させないアプリケーションがアクティブな場合のみ、各種ホットキーを解除する。</remarks>
        private void Ignore()
        {
            if (envVal.isKeyViEnable() == false) return;

            Disable();
            envVal.setKeyViEnable();

            notifyIcon.Icon = iconKeyViIgnore;
            envVal.setKeyViIgnore();
        }

        #endregion




        #region 画面の定義

        /// <summary>
        /// メイン画面起動時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Inputモード起動時のデフォルトとして設定後、timerイベントを発生させる。</remarks>
        private void KeyVi_Load(object sender, EventArgs e)
        {
            //viHotKey.Uninstall();
            timer1.Interval = EnvVal.INTERVAL_SEC;
            timer1.Enabled = true;
            timer1.Start();
            Uninstall();

            ////イベントをイベントハンドラに関連付ける
            ////フォームコンストラクタなどの適当な位置に記述してもよい
            SystemEvents.SessionEnding +=
                new SessionEndingEventHandler(SystemEvents_SessionEnding);
        }

        /// <summary>
        /// ログオフ、シャットダウンしようとしているとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            AppClose();
        }

        /// <summary>
        /// メイン画面非表示時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyVi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!flagDoExit)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
                e.Cancel = true;
            }
            else
            {

                switch (e.CloseReason)
                {
                    case CloseReason.WindowsShutDown:
                        //Console.WriteLine("OSのシャットダウンによる");
                        AppClose();
                        break;
                    case CloseReason.None:
                    default:
                        //Console.WriteLine("未知の理由");
                        break;
                }
            }

        }

        /// <summary>
        /// タスクトレイアイコンをクリックした際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>　左クリック時：メイン画面を表示
        /// 　右クリック時：タスクトレイにコンテキストメニューを表示</remarks>
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
            case MouseButtons.Left:
                Show();
                WindowState = FormWindowState.Normal;
                Activate();
                break;
            case MouseButtons.Right:
                break;
            }
        }

        /// <summary>
        /// コンテキストメニュー「終了」クリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>KeyViを終了させる</remarks>
        private void menuitemExit_Click(object sender, EventArgs e)
        {
            AppClose();
        }

        /// <summary>
        /// アプリケーションの終了処理
        /// </summary>
        private void AppClose()
        {
            timer1.Stop();
            flagDoExit = true;
            Close();
        }

        /// <summary>
        /// アプリケーション終了時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyVi_FormClosed(object sender, FormClosedEventArgs e)
        {
            //viHotKey.Uninstall();
            Uninstall();

            //イベントを開放する
            //フォームDisposeメソッド内の基本クラスのDisposeメソッド呼び出しの前に
            //記述してもよい
            SystemEvents.SessionEnding -=
                new SessionEndingEventHandler(SystemEvents_SessionEnding);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyVi_Shown(object sender, EventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// コンテキストメニュー「KeyViを有効にする/KeyViを無効にする」クリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>KeyViの有効/無効を切り替える</remarks>
        private void menuitemEnableKeyVi_Click(object sender, EventArgs e)
        {
            if (envVal.isKeyViEnable())
            {
                envVal.setKeyViDisable();
                menuitemEnableKeyVi.Text = EnvVal.SET_KEYVI_ENABLE;
                Disable();

            }
            else
            {
                envVal.setKeyViEnable();
                menuitemEnableKeyVi.Text = EnvVal.SET_KEYVI_DISABLE;
                if (envVal.GetNowMode() == EnvVal.COMMAND_MODE)
                {
                    Install();
                }
                if (envVal.GetNowMode() == EnvVal.EDIT_MODE)
                {
                    Uninstall();
                }
            }
        }

        /// <summary>
        /// メイン画面のTimerイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Ignoreモードへの移行/Ignoreモードからの復旧処理を行う。</remarks>
        private void timer1_Tick(object sender, EventArgs e)
        {

            #region アクティブウインドウの取得
            IntPtr hwnd = GetForegroundWindow();      // 最前面ウィンドウの hwnd を取得
            uint processId;

            //System.Diagnostics.Process current;
            //current = System.Diagnostics.Process.GetProcessById((int)processId);
            //System.Diagnostics.Debug.WriteLine("ProcessName : " + current.ProcessName);
            //System.Diagnostics.Debug.WriteLine("ProcessFileName : " + current.MainModule.FileName);

            try
            {
                // 現在アクティブなプロセスを取得します。
                GetWindowThreadProcessId(hwnd, out processId);

                NowActiveApplicationFullPath = Process.GetProcessById((int)processId).MainModule.FileName;
                System.Diagnostics.Debug.WriteLine("ProcessFileName : " + NowActiveApplicationFullPath);
            }
            catch (Win32Exception)
            {
                NowActiveApplicationFullPath = "";
            }


            if (envVal.IsIgnoreApplication(NowActiveApplicationFullPath))
            {
                System.Diagnostics.Debug.WriteLine("Ignore");
                Ignore();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Not Ignore");
                if (envVal.IsKeyViIgnore() == true)
                {
                    if (envVal.GetNowMode() == EnvVal.COMMAND_MODE)
                    {
                        Install();
                    }
                    else
                    {
                        Uninstall();
                    }
                    envVal.setKeyViNotIgnore();
                }
            }
            #endregion

        }

        /// <summary>
        /// コンテキストメニュー「設定」クリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>KeyViの設定画面を表示する。設定終了後に設定情報の再読み込みを行う。</remarks>
        private void menuitemKeyViConfig_Click(object sender, EventArgs e)
        {
            keyviConfig f = new keyviConfig();
            //MessageBox.Show("これからKeyViConfigを表示します。");

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                //MessageBox.Show("OKが押されました");
            }
            else
            {
                //MessageBox.Show("Cancelが押されました");
            }
            f.Dispose();
            envVal.RefreshConfig();
            if (envVal.GetNowMode() == EnvVal.EDIT_MODE)
            {
                Uninstall();
            }
            else
            {
                Install();
            }
        }
        #endregion
    }
}