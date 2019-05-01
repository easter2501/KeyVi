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
    /// ���C����ʁB
    /// �z�b�g�L�[�̓o�^�A���[�h�ؑցA�z�b�g�L�[�������̏������s���B
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



        #region SystemHotkey�̒�`
        //�A���t�@�x�b�g
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

        //�A���t�@�x�b�g�iShift�L�[���������j
        private SystemHotkey hotkeyShiftH = new SystemHotkey();
        private SystemHotkey hotkeyShiftJ = new SystemHotkey();
        private SystemHotkey hotkeyShiftK = new SystemHotkey();
        private SystemHotkey hotkeyShiftL = new SystemHotkey();

        private SystemHotkey hotkeyShiftX = new SystemHotkey();
        private SystemHotkey hotkeyShiftO = new SystemHotkey();
        private SystemHotkey hotkeyShiftG = new SystemHotkey();
        private SystemHotkey hotkeyShiftI = new SystemHotkey();
        private SystemHotkey hotkeyShiftA = new SystemHotkey();


        //�A���t�@�x�b�g�iCtrl�L�[���������j
        private SystemHotkey hotkeyCtrlF = new SystemHotkey();
        private SystemHotkey hotkeyCtrlB = new SystemHotkey();
        private SystemHotkey hotkeyCtrlH = new SystemHotkey();

        //�����i�L�[�{�[�h��i���j
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

        //�L��
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

        //����L�[
        private SystemHotkey hotkeyHenkan = new SystemHotkey();
        private SystemHotkey hotkeyMuhenkan = new SystemHotkey();
        private SystemHotkey hotkeyEnter = new SystemHotkey();
        private SystemHotkey hotkeyCaps = new SystemHotkey();

        //�Ǝ���`
        private SystemHotkey hotkeyHome = new SystemHotkey();
        private SystemHotkey hotkeyEnd = new SystemHotkey();

        //���[�h�ϊ�
        private SystemHotkey hotkeyEscape = new SystemHotkey();
        private SystemHotkey hotkeyI = new SystemHotkey();

        //���ݒ���
        private EnvVal envVal = new EnvVal();


        //�L�[���������L���[
        private Queue keyCodeQueue = new Queue();

        //���݃A�N�e�B�u�ȃA�v���P�[�V����
        private string NowActiveApplicationFullPath = "";

        #endregion

        #region �R���X�g���N�^
        /// <summary>
        /// �R���X�g���N�^�B
        /// �z�b�g�L�[�̒�`�ƃ��[�h�ݒ�i�N�����̃f�t�H���g�͕ҏW���[�h�j���s���B
        /// </summary>
        public KeyVi()
        {
            InitializeComponent();

            //�N�����͕ҏW���[�h
            envVal.SetNowMode(EnvVal.EDIT_MODE);



            //�A���t�@�x�b�g
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

            //�A���t�@�x�b�g�iShift�L�[���������j
            hotkeyShiftH.Pressed += new EventHandler(hotkeyShiftH_Pressed);
            hotkeyShiftJ.Pressed += new EventHandler(hotkeyShiftJ_Pressed);
            hotkeyShiftK.Pressed += new EventHandler(hotkeyShiftK_Pressed);
            hotkeyShiftL.Pressed += new EventHandler(hotkeyShiftL_Pressed);

            hotkeyShiftX.Pressed += new EventHandler(hotkeyShiftX_Pressed);
            hotkeyShiftO.Pressed += new EventHandler(hotkeyShiftO_Pressed);
            hotkeyShiftG.Pressed += new EventHandler(hotkeyShiftG_Pressed);
            hotkeyShiftI.Pressed += new EventHandler(hotkeyShiftI_Pressed);
            hotkeyShiftA.Pressed += new EventHandler(hotkeyShiftA_Pressed);

            //�A���t�@�x�b�g�iCtrl�L�[���������j
            hotkeyCtrlF.Pressed += new EventHandler(hotkeyCtrlF_Pressed);
            hotkeyCtrlB.Pressed += new EventHandler(hotkeyCtrlB_Pressed);
            hotkeyCtrlH.Pressed += new EventHandler(hotkeyCtrlH_Pressed);

            //�����i�L�[�{�[�h��i���j
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

            //�L��
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

            //����L�[
            hotkeyHenkan.Pressed += new EventHandler(hotkeyHenkan_Presed);
            hotkeyMuhenkan.Pressed += new EventHandler(hotkeyMuhenkan_Pressed);
            hotkeyEnter.Pressed += new EventHandler(hotkeyEnter_Pressed);
            //hotkeyCaps.Pressed += new EventHandler(hotkeyCaps_Pressed);

            //�Ǝ���`
            hotkeyHome.Pressed += new EventHandler(hotKeyHome_Pressed);
            hotkeyEnd.Pressed += new EventHandler(hotKeyEnd_Pressed);

            //���[�h�ϊ�
            hotkeyEscape.Pressed += new EventHandler(hotkeyEscape_Pressed);
            hotkeyI.Pressed += new EventHandler(hotkeyI_Pressed);
            //hotkeyScrollLock.Pressed += new EventHandler(hotkeyScrollLock_Pressed);

            Uninstall();
        }

        #endregion

        #region �z�b�g�L�[�������̏���
        /// <summary>
        /// �y:�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�R�}���h���͗����L���[���N���A������A�L���[�Ɂy:�z��o�^����</remarks>
        void hotkeyColon_Pressed(object sender, EventArgs e)
        {
            //Input.ColonKey();
            keyCodeQueue.Clear();
            keyCodeQueue.Enqueue(":");
        }

        /// <summary>
        /// �yEnter�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yEnter�z�L�[�������_�ŃR�}���h�����L���[���y:q�z�������ꍇ�A
        /// Disable���[�h�ւ̈ڍs�������s��</remarks>
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
        /// �yq�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�L���[�Ɂyq�z��o�^����</remarks>
        void hotkeyQ_Pressed(object sender, EventArgs e)
        {
            keyCodeQueue.Enqueue("q");
        }

        /// <summary>
        /// �yu�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yCtrl�z+�yz�z�i����������j�𔭍s����</remarks>
        void hotkeyU_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_U) == true)
            {
                Input.Undo();
            }
        }

        /// <summary>
        /// �yw�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yCtrl�z+�y���z�i���P��ֈړ��j�𔭍s����</remarks>
        void hotkeyW_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_W) == true)
            {
                Input.MoveNextWord();
            }
        }

        /// <summary>
        /// �yb�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yCtrl�z+�y���z�i�O�P��ֈړ��j�𔭍s����</remarks>
        void hotkeyB_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_B) == true)
            {
                Input.MovePreviewWord();
            }
        }

        /// <summary>
        /// �yh�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�y���z�iLEFT�L�[�j�𔭍s����</remarks>
        void hotkeyH_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H) == true)
            {
                Input.LeftKey();
            }
        }

        /// <summary>
        /// �yj�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�y���z�iDOWN�L�[�j�𔭍s����</remarks>
        void hotkeyJ_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J) == true)
            {
                Input.DownKey();
            }
        }

        /// <summary>
        /// �yk�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�y���z�iUP�L�[�j�𔭍s����</remarks>
        void hotkeyK_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K) == true)
            {
                Input.UpKey();
            }
        }

        /// <summary>
        /// �yl�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�y���z�iRIGHT�L�[�j�𔭍s����</remarks>
        void hotkeyL_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L) == true)
            {
                Input.RightKey();
            }
        }

        /// <summary>
        /// �yy�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�L���[�Ɂyy�z��o�^������A�R�s�[�����iInput.Copy�j�����s����</remarks>
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
        /// �yx�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// /// <remarks>�yDELETE�z�i�폜�j�𔭍s����</remarks>
        void hotkeyX_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_X) == true)
            {
                Input.Delete();
            }
        }

        /// <summary>
        /// �yShift�z+�yx�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yBACKSPACE�z�i��ށj�𔭍s����</remarks>
        void hotkeyShiftX_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX) == true)
            {
                Input.Backspace();
            }
        }

        /// <summary>
        /// �yd�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�L���[�Ɂyd�z��o�^������A�s�폜�����iInput.LineDelete�j�����s����</remarks>
        void hotkeyD_Pressed(object sender, EventArgs e)
        {
            keyCodeQueue.Enqueue("d");
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_DD) == true)
            {
                Input.LineDelete(keyCodeQueue);
            }
        }

        /// <summary>
        /// �yo�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>���ݍs�̉��ɋ�s��}�����AInput���[�h�Ɉڍs����</remarks>
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
        /// �yp�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// /// <remarks>�yCtrl�z+�yv�z�i�\��t���j�𔭍s����</remarks>
        void hotkeyP_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_P) == true)
            {
                Input.Paste();
            }
        }



        /// <summary>
        /// �yShift�z+�yh�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yShift�z+�y���z�iShift�L�[��LEFT�L�[�̓��������j�𔭍s����</remarks>
        void hotkeyShiftH_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H) == true)
            {
                Input.ShiftLeftKey();
            }
        }

        /// <summary>
        /// �yShift�z+�yj�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yShift�z+�y���z�iShift�L�[��DOWN�L�[�̓��������j�𔭍s����</remarks>
        void hotkeyShiftJ_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J) == true)
            {
                Input.ShiftDownKey();
            }
        }

        /// <summary>
        /// �yShift�z+�yk�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yShift�z+�y���z�iShift�L�[��UP�L�[�̓��������j�𔭍s����</remarks>
        void hotkeyShiftK_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K) == true)
            {
                Input.ShiftUpKey();
            }
        }

        /// <summary>
        /// �yShift�z+�yl�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yShift�z+�y���z�iShift�L�[��RIGHT�L�[�̓��������j�𔭍s����</remarks>
        void hotkeyShiftL_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L) == true)
            {
                Input.ShiftRightKey();
            }
        }

 

        /// <summary>
        /// �yShift�z+�yo�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>���ݍs�̏�ɋ�s��}�����AInput���[�h�Ɉڍs����</remarks>
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
        /// �yCtrl�z+�yf�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yPAGEDOWN�z�i1��ʉ��ɃX�N���[���j�𔭍s����</remarks>
        void hotkeyCtrlF_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF) == true)
            {
                Input.PageDown();
            }
        }

        /// <summary>
        /// �yCtrl�z+�yb�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yPAGEUP�z�i1��ʏ�ɃX�N���[���j�𔭍s����</remarks>
        void hotkeyCtrlB_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB) == true)
            {
                Input.PageUp();
            }
        }

        /// <summary>
        /// �yCtrl�z+�yh�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yBACKSPACE�z�i��ށj�𔭍s����</remarks>
        void hotkeyCtrlH_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH) == true)
            {
                Input.CtrlH_Backspace();
            }
        }

        /// <summary>
        /// �R�}���h�����蓖�Ă��Ă��Ȃ��L�[�������������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yF16�z�𔭍s����B
        /// ���s�̃L�[�{�[�h�ɂ�Function�L�[��12�܂ł��������̂ŁA
        /// F16�L�[���u���ۂɉ����v���Ƃ͕s�\�ł��邱�Ƃ���B</remarks>
        void hotkeyDummy_Pressed(object sender, EventArgs e)
        {
            Input.DummyKey();
        }

        /// <summary>
        /// �y0�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yHOME�z�i�s���j�𔭍s����</remarks>
        void hotKeyHome_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_ZERO) == true)
            {
                Input.HomeKey();
            }
        }

        /// <summary>
        /// �yShift�z+�y4�z�i$�j�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�yEND�z�i�s���j�𔭍s����</remarks>
        void hotKeyEnd_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4) == true)
            {
                Input.EndKey();
            }
        }

        /// <summary>
        /// �yg�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�L���[�Ɂyg�z��o�^������A�����ړ������iInput.MoveToDocHead�j�����s����</remarks>
        void hotkeyG_Pressed(object sender, EventArgs e)
        {
            keyCodeQueue.Enqueue("g");
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_GG) == true)
            {
                Input.MoveToDocHead(keyCodeQueue);
            }
        }

        /// <summary>
        /// �yShift�z+�yg�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�����ړ������iInput.MoveToDocEnd�j�����s����</remarks>
        void hotkeyShiftG_Pressed(object sender, EventArgs e)
        {
            if (envVal.IsKeyEnable(NowActiveApplicationFullPath, EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG) == true)
            {
                Input.MoveToDocEnd();
            }
        }

        /// <summary>
        /// �y�ϊ��z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�y���p/�S�p�z�iIME ON/OFF�j�𔭍s����</remarks>
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
        /// �yESC�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Command���[�h�Ɉڍs��A�yESC�z�𔭍s����</remarks>
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
        /// �y���ϊ��z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Command���[�h�Ɉڍs����</remarks>
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
        /// �yi�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Input���[�h�Ɉڍs����</remarks>
        void hotkeyI_Pressed(object sender, EventArgs e)
        {
            if (envVal.GetNowMode() == EnvVal.EDIT_MODE) return;
            envVal.SetNowMode(EnvVal.EDIT_MODE);
            Uninstall();
        }

        /// <summary>
        /// �yShift�z+�yi�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�s���Ɉړ���AInput���[�h�Ɉڍs����</remarks>
        void hotkeyShiftI_Pressed(object sender, EventArgs e)
        {
            if (envVal.GetNowMode() == EnvVal.EDIT_MODE) return;
            Input.HomeKey();
            Input.ShiftKeyUp();
            envVal.SetNowMode(EnvVal.EDIT_MODE);
            Uninstall();
        }

        /// <summary>
        /// �yShift�z+�yA�z�L�[�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�s���Ɉړ���AInput���[�h�Ɉڍs����</remarks>
        void hotkeyShiftA_Pressed(object sender, EventArgs e)
        {
            if (envVal.GetNowMode() == EnvVal.EDIT_MODE) return;
            Input.EndKey();
            Input.ShiftKeyUp();
            envVal.SetNowMode(EnvVal.EDIT_MODE);
            Uninstall();
        }


        #endregion

        #region ���[�h�ؑ�(Command���[�h)

        /// <summary>
        /// Command���[�h�ւ̈ڍs����
        /// </summary>
        /// <remarks>�e��z�b�g�L�[�̓o�^�A�^�X�N�g���C�A�C�R���̐ݒ���s��</remarks>
        private void Install()
        {
            // �z�b�g�L�[�̓o�^����
            hotkeyEscape.HotKey = Keys.None;

            //// �z�b�g�L�[�̓o�^
            //�A���t�@�x�b�g
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

            //�A���t�@�x�b�g�iShift�L�[���������j
            hotkeyShiftH.HotKey = Keys.Shift | Keys.H;
            hotkeyShiftJ.HotKey = Keys.Shift | Keys.J;
            hotkeyShiftK.HotKey = Keys.Shift | Keys.K;
            hotkeyShiftL.HotKey = Keys.Shift | Keys.L;

            hotkeyShiftX.HotKey = Keys.Shift | Keys.X;
            hotkeyShiftO.HotKey = Keys.Shift | Keys.O;
            hotkeyShiftG.HotKey = Keys.Shift | Keys.G;
            hotkeyShiftI.HotKey = Keys.Shift | Keys.I;
            hotkeyShiftA.HotKey = Keys.Shift | Keys.A;

            //�A���t�@�x�b�g�iCtrl�L�[���������j
            hotkeyCtrlF.HotKey = Keys.Control | Keys.F;
            hotkeyCtrlB.HotKey = Keys.Control | Keys.B;
            hotkeyCtrlH.HotKey = Keys.Control | Keys.H;

            //�����i�L�[�{�[�h��i���j
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

            //�L��
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

            //����L�[
            hotkeyHenkan.HotKey = Keys.IMEConvert;
            hotkeyEnter.HotKey = Keys.Enter;

            //�Ǝ���`
            hotkeyHome.HotKey = Keys.D0;
            hotkeyEnd.HotKey = Keys.Shift | Keys.D4;

            //���[�h�ϊ�
            hotkeyI.HotKey = Keys.I;
            //hotkeyScrollLock.HotKey = Keys.Scroll;

            notifyIcon.Icon = iconKeyVi;

        }

#endregion

        #region ���[�h�ؑ�(Input���[�h)

        /// <summary>
        /// Input���[�h�ւ̈ڍs����
        /// </summary>
        /// <remarks>�e��z�b�g�L�[�̉����ACommand���[�h�ڍs�p�z�b�g�L�[�̓o�^�A�^�X�N�g���C�A�C�R���̐ݒ���s��</remarks>
        private void Uninstall()
        {
            //// �z�b�g�L�[�̓o�^����
            //�A���t�@�x�b�g
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

            //�A���t�@�x�b�g�iShift�L�[���������j
            hotkeyShiftH.HotKey = Keys.None;
            hotkeyShiftJ.HotKey = Keys.None;
            hotkeyShiftK.HotKey = Keys.None;
            hotkeyShiftL.HotKey = Keys.None;

            hotkeyShiftX.HotKey = Keys.None;
            hotkeyShiftO.HotKey = Keys.None;
            hotkeyShiftG.HotKey = Keys.None;
            hotkeyShiftI.HotKey = Keys.None;
            hotkeyShiftA.HotKey = Keys.None;

            //�A���t�@�x�b�g�iCtrl�L�[���������j
            hotkeyCtrlF.HotKey = Keys.None;
            hotkeyCtrlB.HotKey = Keys.None;
            hotkeyCtrlH.HotKey = Keys.None;

            //�����i�L�[�{�[�h��i���j
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

            //�L��
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

            //����L�[
            hotkeyHenkan.HotKey = Keys.IMEConvert;
            hotkeyEnter.HotKey = Keys.None;


            //�Ǝ���`
            hotkeyHome.HotKey = Keys.None;
            hotkeyEnd.HotKey = Keys.None;

            //���[�h�ϊ�
            hotkeyI.HotKey = Keys.None;
            hotkeyEscape.HotKey = Keys.Escape;
            hotkeyMuhenkan.HotKey = Keys.IMENonconvert;

            notifyIcon.Icon = iconKeyViKb;
        }
        #endregion

        #region ���[�h�ؑ�(Disable���[�h)

        /// <summary>
        /// Disable���[�h�ւ̈ڍs����
        /// </summary>
        /// <remarks>�e��z�b�g�L�[�̉������s���BDisable���[�h�Ɉڍs����ƁA�L�[�{�[�h������KeyVi��L���ɂ��邱�Ƃ͂ł��Ȃ��Ȃ�B</remarks>
        private void Disable()
        {
            //// �z�b�g�L�[�̓o�^����
            //�A���t�@�x�b�g
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

            //�A���t�@�x�b�g�iShift�L�[���������j
            hotkeyShiftH.HotKey = Keys.None;
            hotkeyShiftJ.HotKey = Keys.None;
            hotkeyShiftK.HotKey = Keys.None;
            hotkeyShiftL.HotKey = Keys.None;

            hotkeyShiftX.HotKey = Keys.None;
            hotkeyShiftO.HotKey = Keys.None;
            hotkeyShiftG.HotKey = Keys.None;
            hotkeyShiftI.HotKey = Keys.None;
            hotkeyShiftA.HotKey = Keys.None;

            //�A���t�@�x�b�g�iCtrl�L�[���������j
            hotkeyCtrlF.HotKey = Keys.None;
            hotkeyCtrlB.HotKey = Keys.None;
            hotkeyCtrlH.HotKey = Keys.None;

            //�����i�L�[�{�[�h��i���j
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

            //�L��
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

            //����L�[
            //hotkeyHenkan.HotKey = Keys.None;      //�ϊ��L�[�ɂ��IME ON/OFF�̓��[�h�s��
            hotkeyEnter.HotKey = Keys.None;
            hotkeyCaps.HotKey = Keys.Capital;    //CapsLock �� Ctrl�̓��[�h�s��

            //�Ǝ���`
            hotkeyHome.HotKey = Keys.None;
            hotkeyEnd.HotKey = Keys.None;

            //���[�h�ϊ�
            hotkeyI.HotKey = Keys.None;
            hotkeyEscape.HotKey = Keys.None;
            hotkeyMuhenkan.HotKey = Keys.None;

            notifyIcon.Icon = iconKeyViDisable;

            envVal.setKeyViDisable();
        }
        #endregion

        #region ���[�h�ؑ�(Ignore���[�h)

        /// <summary>
        /// Ignore���[�h�ւ̈ڍs����
        /// </summary>
        /// <remarks>KeyVi�𓮍삳���Ȃ��A�v���P�[�V�������A�N�e�B�u�ȏꍇ�̂݁A�e��z�b�g�L�[����������B</remarks>
        private void Ignore()
        {
            if (envVal.isKeyViEnable() == false) return;

            Disable();
            envVal.setKeyViEnable();

            notifyIcon.Icon = iconKeyViIgnore;
            envVal.setKeyViIgnore();
        }

        #endregion




        #region ��ʂ̒�`

        /// <summary>
        /// ���C����ʋN�����̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Input���[�h�N�����̃f�t�H���g�Ƃ��Đݒ��Atimer�C�x���g�𔭐�������B</remarks>
        private void KeyVi_Load(object sender, EventArgs e)
        {
            //viHotKey.Uninstall();
            timer1.Interval = EnvVal.INTERVAL_SEC;
            timer1.Enabled = true;
            timer1.Start();
            Uninstall();

            ////�C�x���g���C�x���g�n���h���Ɋ֘A�t����
            ////�t�H�[���R���X�g���N�^�Ȃǂ̓K���Ȉʒu�ɋL�q���Ă��悢
            SystemEvents.SessionEnding +=
                new SessionEndingEventHandler(SystemEvents_SessionEnding);
        }

        /// <summary>
        /// ���O�I�t�A�V���b�g�_�E�����悤�Ƃ��Ă���Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            AppClose();
        }

        /// <summary>
        /// ���C����ʔ�\�����̏���
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
                        //Console.WriteLine("OS�̃V���b�g�_�E���ɂ��");
                        AppClose();
                        break;
                    case CloseReason.None:
                    default:
                        //Console.WriteLine("���m�̗��R");
                        break;
                }
            }

        }

        /// <summary>
        /// �^�X�N�g���C�A�C�R�����N���b�N�����ۂ̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�@���N���b�N���F���C����ʂ�\��
        /// �@�E�N���b�N���F�^�X�N�g���C�ɃR���e�L�X�g���j���[��\��</remarks>
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
        /// �R���e�L�X�g���j���[�u�I���v�N���b�N���̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>KeyVi���I��������</remarks>
        private void menuitemExit_Click(object sender, EventArgs e)
        {
            AppClose();
        }

        /// <summary>
        /// �A�v���P�[�V�����̏I������
        /// </summary>
        private void AppClose()
        {
            timer1.Stop();
            flagDoExit = true;
            Close();
        }

        /// <summary>
        /// �A�v���P�[�V�����I�����̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyVi_FormClosed(object sender, FormClosedEventArgs e)
        {
            //viHotKey.Uninstall();
            Uninstall();

            //�C�x���g���J������
            //�t�H�[��Dispose���\�b�h���̊�{�N���X��Dispose���\�b�h�Ăяo���̑O��
            //�L�q���Ă��悢
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
        /// �R���e�L�X�g���j���[�uKeyVi��L���ɂ���/KeyVi�𖳌��ɂ���v�N���b�N���̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>KeyVi�̗L��/������؂�ւ���</remarks>
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
        /// ���C����ʂ�Timer�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Ignore���[�h�ւ̈ڍs/Ignore���[�h����̕����������s���B</remarks>
        private void timer1_Tick(object sender, EventArgs e)
        {

            #region �A�N�e�B�u�E�C���h�E�̎擾
            IntPtr hwnd = GetForegroundWindow();      // �őO�ʃE�B���h�E�� hwnd ���擾
            uint processId;

            //System.Diagnostics.Process current;
            //current = System.Diagnostics.Process.GetProcessById((int)processId);
            //System.Diagnostics.Debug.WriteLine("ProcessName : " + current.ProcessName);
            //System.Diagnostics.Debug.WriteLine("ProcessFileName : " + current.MainModule.FileName);

            try
            {
                // ���݃A�N�e�B�u�ȃv���Z�X���擾���܂��B
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
        /// �R���e�L�X�g���j���[�u�ݒ�v�N���b�N���̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>KeyVi�̐ݒ��ʂ�\������B�ݒ�I����ɐݒ���̍ēǂݍ��݂��s���B</remarks>
        private void menuitemKeyViConfig_Click(object sender, EventArgs e)
        {
            keyviConfig f = new keyviConfig();
            //MessageBox.Show("���ꂩ��KeyViConfig��\�����܂��B");

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                //MessageBox.Show("OK��������܂���");
            }
            else
            {
                //MessageBox.Show("Cancel��������܂���");
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