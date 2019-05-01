using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace keyvi
{

    
    /// <summary>
    /// keyvi環境変数と設定情報の管理を行う。
    /// </summary>
    /// <remarks>KeyViで使用する定数はすべてこのクラス内に宣言すること。</remarks>
    public class EnvVal
    {
        #region 変数の定義
        //KeyVi設定情報
        private XmlDocument XmlKeyViConfig = new XmlDocument();

        private string KeyBoard = "";
        private bool CommandEnter = false;
        private bool HenkanKeyToImeOnOff = false;
        private bool MuhenkanKeyToEsc = false;

        //アプリケーションごとのキー有効/無効ハッシュ
        private Hashtable KeyH_Hash = new Hashtable();
        private Hashtable KeyJ_Hash = new Hashtable();
        private Hashtable KeyK_Hash = new Hashtable();
        private Hashtable KeyL_Hash = new Hashtable();
        private Hashtable KeyZero_Hash = new Hashtable();
        private Hashtable KeyShift4_Hash = new Hashtable();
        private Hashtable KeyCtrlF_Hash = new Hashtable();
        private Hashtable KeyCtrlB_Hash = new Hashtable();
        private Hashtable KeyY_Hash = new Hashtable();
        private Hashtable KeyYY_Hash = new Hashtable();
        private Hashtable KeyP_Hash = new Hashtable();
        private Hashtable KeyX_Hash = new Hashtable();
        private Hashtable KeyDD_Hash = new Hashtable();
        private Hashtable KeyO_Hash = new Hashtable();
        private Hashtable KeyShiftO_Hash = new Hashtable();
        private Hashtable KeyShiftX_Hash = new Hashtable();
        private Hashtable KeyCtrlH_Hash = new Hashtable();
        private Hashtable KeyU_Hash = new Hashtable();
        private Hashtable KeyGG_Hash = new Hashtable();
        private Hashtable KeyShiftG_Hash = new Hashtable();
        private Hashtable KeyW_Hash = new Hashtable();
        private Hashtable KeyB_Hash = new Hashtable();
        private Hashtable KeyShiftI_Hash = new Hashtable();
        private Hashtable KeyShiftA_Hash = new Hashtable();


        //KeyViの無視
        private bool keyViIgnore = false;
        #endregion

        #region 定数の定義
        //キーボードの種類
        public const string KEYBOADTYPE_JP106 = "JP106";
        public const string KEYBOADTYPE_US = "US";

        //アクティブなアプリケーションを取得する間隔（単位：ミリ秒）
        public const int INTERVAL_SEC = 1000;

        //viのモード
        private int viModeVal;                //現在のモード
        public const int COMMAND_MODE = 1;  //コマンドモード
        public const int EDIT_MODE = 2;     //編集モード

        //KeyViの有効/無効
        private bool keyViEnable;
        public const string SET_KEYVI_ENABLE = "KeyViを有効にする";
        public const string SET_KEYVI_DISABLE = "KeyViを無効にする";

        //ダイアログ関連
        public const string SAVE_CONFIG_STRING = "変更を保存します。よろしいですか？";
        public const string SAVE_CONFIG_STRING_TITLE = "KeyVi　設定情報の保存";
        public const string SELECT_APPLICATION_DEFAULT_PATH = @"C:\";
        public const string SELECT_APPLICATION_FILTER = "実行ファイル(exe)|*.exe";
        public const string SELECT_APPLICATION_TITLE = "実行ファイルを選択してください";

        //キー押下履歴キューの最大保持数
        public const int MAX_QUEUE_LENGTH = 2;

        //複数キーによるコマンド
        public const int SET_DISABLE_MODE = 1;      //:q →　KeyVi無効モードへ移行
        public const int NOT_SET_DISABLE_MODE = 0;  //KeyVi無効モードへ移行しない
        public const int CUT_LINE = 2;              //dd →　行切り取り
        public const int COPY_LINE = 3;             //yy →　行コピー
        public const string DISABLE_MODE_COMMAND = ":q";
        public const string LINE_DELETE = "dd";
        public const string LINE_COPY = "yy";
        public const string DOC_HEADER = "gg";

        //KeyVi設定ファイル名
        public const string KEYVI_CONFIG_FILENAME = "keyviConfig.xml";

        //アイコンファイル名
        public const string KEYVI_INPUT_MODE_ICON_FILENAME = "keyviInputMode.ico";
        public const string KEYVI_COMMAND_MODE_ICON_FILENAME = "keyviCommandMode.ico";
        public const string KEYVI_DISABLE_MODE_ICON_FILENAME = "keyviDisableMode.ico";
        public const string KEYVI_IGNORE_MODE_ICON_FILENAME = "keyviIgnoreMode.ico";


        //アプリケーションごとの設定を記述したXMLテーブル名
        public const string XML_TABLE_APPLICATION = "Application";

        //全体の設定を記述したXMLテーブル名
        public const string XML_TABLE_GENERAL = "General";

        //全体の設定
        public const string XML_TABLE_GENERAL_KEYBOARD_PATH = "/Application_Table/General/KeyBoard";
        public const string XML_TABLE_GENERAL_COMMANDENTER_PATH = "/Application_Table/General/CommandEnter";
        public const string XML_TABLE_GENERAL_HENKAN_IME_ON_OFF = "/Application_Table/General/HenkanKeyToImeOnOff";
        public const string XML_TABLE_GENERAL_MUHENKAN_ESC = "/Application_Table/General/MuhenkanKeyToEsc";
        public const string XML_STRING_TRUE = "TRUE";
        public const string XML_STRING_FALSE = "FALSE";
        public const string XML_STRING_GENERAL_KEYBOARD = "KeyBoard";
        public const string XML_STRING_GENERAL_COMMANDENTER = "CommandEnter";
        public const string XML_STRING_GENERAL_HENKANKEYTOIMEONOFF = "HenkanKeyToImeOnOff";
        public const string XML_STRING_GENERAL_MUHENKANKEYTOESC = "MuhenkanKeyToEsc";

        //アプリケーションごとの設定
        public const string XML_TABLE_APPLICATION_PATH = "/Application_Table/Application";
        public const string XML_TABLE_APPLICATION_COLUMN_PROCESS_NAME = "ProcessName";
        public const string XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME = "ProcessFileName";
        public const string XML_TABLE_APPLICATION_COLUMN_ENABLE = "Enable";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_H = "KeyH";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_J = "KeyJ";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_K = "KeyK";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_L = "KeyL";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_ZERO = "KeyZero";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4 = "KeyShift4";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF = "KeyCtrlF";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB = "KeyCtrlB";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_Y = "KeyY";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_YY = "KeyYY";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_P = "KeyP";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_X = "KeyX";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_DD = "KeyDD";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_O = "KeyO";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTO = "KeyShiftO";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX = "KeyShiftX";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH = "KeyCtrlH";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_U = "KeyU";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_GG = "KeyGG";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG = "KeyShiftG";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_W = "KeyW";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_B = "KeyB";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTI = "KeyShiftI";
        public const string XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTA = "KeyShiftA";

        #endregion



        #region 仮想キーコードの定義

        public const byte KEYEVENTF_EXTENDEDKEY = 1;
        public const byte KEYEVENTF_KEYUP = 2;
        public const byte VK_BACK = 0x08;
        public const byte VK_RETURN = 0x0D;

        public const byte VK_KANJI = 0x19;
        public const byte KEYEVENTF_SILENT = 0x04;
        public const byte VK_LMENU = 0xA4;
        public const byte VK_SHIFT = 0x10;
        public const byte VK_SCROLL = 0x91;
        public const byte VK_PRIOR = 0x21;
        public const byte VK_NEXT = 0x22;
        public const byte VK_END = 0x23;
        public const byte VK_HOME = 0x24;

        public const byte VK_C = 0x43;
        public const byte VK_X = 0x58;


        public const byte VK_LEFT = 0x25;
        public const byte VK_UP = 0x26;
        public const byte VK_RIGHT = 0x27;
        public const byte VK_DOWN = 0x28;

        public const byte VK_DELETE = 0x2E;
        public const byte VK_CAPITAL = 0x14;
        public const byte VK_CONTROL = 0x11;

        public const byte VK_CONVERT = 0x1C;
        public const byte VK_NOCONVERT = 0x1D;
        #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EnvVal()
        {
            viModeVal = EDIT_MODE;
            keyViEnable = true;
            keyViIgnore = false;
            //Ykey_Count = 0;

            RefreshConfig();
        }


        #region 設定ファイル関連
        /// <summary>
        /// 設定情報の再読み込み
        /// </summary>
        /// <remarks>XMLファイルから設定情報を読み込む</remarks>
        public void RefreshConfig()
        {
            //XMLファイルから設定情報を読み込む
            XmlKeyViConfig.Load(EnvVal.KEYVI_CONFIG_FILENAME);

            //「General」に関する設定情報をXMLDocumentオブジェクトから読み込む
            ReadGeneralConfig();

            //各キーに関する設定情報をXMLDocumentオブジェクトから読み込み
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H, ref KeyH_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J, ref KeyJ_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K, ref KeyK_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L, ref KeyL_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_ZERO, ref KeyZero_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4, ref KeyShift4_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF, ref KeyCtrlF_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB, ref KeyCtrlB_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_Y, ref KeyY_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_YY, ref KeyYY_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_P, ref KeyP_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_X, ref KeyX_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_DD, ref KeyDD_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_O, ref KeyO_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTO, ref KeyShiftO_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX, ref KeyShiftX_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH, ref KeyCtrlH_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_U, ref KeyU_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_GG, ref KeyGG_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG, ref KeyShiftG_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_W, ref KeyW_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_B, ref KeyB_Hash);

            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTI, ref KeyShiftI_Hash);
            SetKeyConfig(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTA, ref KeyShiftA_Hash);

        }

        /// <summary>
        /// 各キーに関する設定情報をXMLDocumentオブジェクトから読み込み、ハッシュテーブルに格納する
        /// </summary>
        /// <param name="ColumnName">ハッシュテーブル格納対象のキー名</param>
        /// <param name="KeyHash">設定情報格納対象のハッシュテーブル</param>
        private void SetKeyConfig(string ColumnName, ref Hashtable KeyHash)
        {
            XmlNodeList list = XmlKeyViConfig.SelectNodes(EnvVal.XML_TABLE_APPLICATION_PATH);
            string XmlProcessName = "";
            string Enable = "";
            KeyHash.Clear();

            foreach (XmlNode node in list)
            {
                Enable = EnvVal.XML_STRING_FALSE;
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {

                    if (node.ChildNodes[i].Name == EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME)
                    {
                        XmlProcessName = node.ChildNodes[i].InnerText.ToString();
                    }
                    if (node.ChildNodes[i].Name == ColumnName)
                    {
                        if (node.ChildNodes[i].InnerText.ToString().ToUpper() == EnvVal.XML_STRING_TRUE)
                        {
                            Enable = EnvVal.XML_STRING_TRUE;
                        }
                        else
                        {
                            Enable = EnvVal.XML_STRING_FALSE;
                        }
                    }
                }


                if (KeyHash.ContainsKey(XmlProcessName) == false)
                {
                    KeyHash.Add(XmlProcessName, Enable);
                }
            }
        }

        /// <summary>
        /// 「General」に関する設定情報をXMLDocumentオブジェクトから読み込む
        /// </summary>
        private void ReadGeneralConfig()
        {
            KeyBoard = XmlKeyViConfig.SelectNodes(EnvVal.XML_TABLE_GENERAL_KEYBOARD_PATH).Item(0).InnerText.ToString();

            if (XmlKeyViConfig.SelectNodes(EnvVal.XML_TABLE_GENERAL_COMMANDENTER_PATH).Item(0).InnerText.ToString().ToUpper() == EnvVal.XML_STRING_TRUE)
            {
                CommandEnter = true;
            }
            else
            {
                CommandEnter = false;
            }

            if (XmlKeyViConfig.SelectNodes(EnvVal.XML_TABLE_GENERAL_HENKAN_IME_ON_OFF).Item(0).InnerText.ToString().ToUpper() == EnvVal.XML_STRING_TRUE)
            {
                HenkanKeyToImeOnOff = true;
            }
            else
            {
                HenkanKeyToImeOnOff = false;
            }

            if (XmlKeyViConfig.SelectNodes(EnvVal.XML_TABLE_GENERAL_MUHENKAN_ESC).Item(0).InnerText.ToString().ToUpper() == EnvVal.XML_STRING_TRUE)
            {
                MuhenkanKeyToEsc = true;
            }
            else
            {
                MuhenkanKeyToEsc = false;
            }
        }

        /// <summary>
        /// 現在アクティブなアプリケーションがIgnoreモードの対象であるかを判断する
        /// </summary>
        /// <param name="AppPath">現在アクティブなアプリケーションのフルパス</param>
        /// <returns>Ignoreモードの対象：true　Ignoreモードの対象外：false</returns>
        public bool IsIgnoreApplication(string AppPath)
        {

            XmlNodeList list = XmlKeyViConfig.SelectNodes(EnvVal.XML_TABLE_APPLICATION_PATH);

            string XmlProcessName = "";
            string XmlEnable = "";

            bool AppIsIgnore = true;
            bool AppIsNotIgnore = false;

            foreach (XmlNode node in list)
            {
                XmlEnable = EnvVal.XML_STRING_FALSE;

                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (node.ChildNodes[i].Name == EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME)
                    {
                        XmlProcessName = node.ChildNodes[i].InnerText.ToString();
                    }
                    if (node.ChildNodes[i].Name == EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE)
                    {
                        XmlEnable = node.ChildNodes[i].InnerText.ToString().ToUpper();
                    }
                }
                if (AppPath == XmlProcessName && XmlEnable == EnvVal.XML_STRING_FALSE)
                {
                    return AppIsIgnore;
                }
            }
            return AppIsNotIgnore;
        }



        /// <summary>
        /// コマンドモードでのEnterキーイベント発行が有効かどうかを返す
        /// </summary>
        /// <returns>有効：True　無効：False</returns>
        public bool IsCommandEnter()
        {
            return CommandEnter;
        }

        /// <summary>
        /// 変換キーでのIME ON/OFFが有効かどうかを返す
        /// </summary>
        /// <returns>有効：True　無効：False</returns>
        public bool IsHenkanKeyToImeOnOff()
        {
            return HenkanKeyToImeOnOff;
        }

        /// <summary>
        /// 無変換キーでのCommandモード移行が有効かどうかを返す
        /// </summary>
        /// <returns>有効：True　無効：False</returns>
        public bool IsMuhenkanKeyToEsc()
        {
            return MuhenkanKeyToEsc;
        }

        /// <summary>
        /// 設定されているキーボードの種類を返す
        /// </summary>
        /// <returns>日本語：JP106　英語：US</returns>
        public string KeyBoardType()
        {
            return KeyBoard;
        }

        /// <summary>
        /// 現在アクティブなアプリケーションで、キーが有効であるかを判断する
        /// </summary>
        /// <param name="AppPath">現在アクティブなアプリケーションのフルパス</param>
        /// <param name="KeyValue">有効/無効を判断する対象のキー名</param>
        /// <returns>有効：true　無効：false</returns>
        public bool IsKeyEnable(string AppPath, string KeyValue)
        {
            bool KeyEnable = true;
            bool KeyDisable = false;
            string KeyHashVal = "";

            switch (KeyValue)
            {
                case XML_TABLE_APPLICATION_COLUMN_KEY_H:
                    KeyHashVal = (string)KeyH_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_J:
                    KeyHashVal = (string)KeyJ_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_K:
                    KeyHashVal = (string)KeyK_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_L:
                    KeyHashVal = (string)KeyL_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_ZERO:
                    KeyHashVal = (string)KeyZero_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4:
                    KeyHashVal = (string)KeyShift4_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF:
                    KeyHashVal = (string)KeyCtrlF_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB:
                    KeyHashVal = (string)KeyCtrlB_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_Y:
                    KeyHashVal = (string)KeyY_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_YY:
                    KeyHashVal = (string)KeyYY_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_P:
                    KeyHashVal = (string)KeyP_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_X:
                    KeyHashVal = (string)KeyX_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_DD:
                    KeyHashVal = (string)KeyDD_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_O:
                    KeyHashVal = (string)KeyO_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTO:
                    KeyHashVal = (string)KeyShiftO_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX:
                    KeyHashVal = (string)KeyShiftX_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH:
                    KeyHashVal = (string)KeyCtrlH_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_U:
                    KeyHashVal = (string)KeyU_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_GG:
                    KeyHashVal = (string)KeyGG_Hash[AppPath];
                    break;
                
                case XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG:
                    KeyHashVal = (string)KeyShiftG_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_W:
                    KeyHashVal = (string)KeyW_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_B:
                    KeyHashVal = (string)KeyB_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTI:
                    KeyHashVal = (string)KeyShiftI_Hash[AppPath];
                    break;

                case XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTA:
                    KeyHashVal = (string)KeyShiftA_Hash[AppPath];
                    break;
                
                default:
                    KeyHashVal = "";
                    break;
            }

            if (KeyHashVal == EnvVal.XML_STRING_FALSE)
            {
                return KeyDisable;
            }
            else
            {
                return KeyEnable;
            }
        }


        #endregion


        # region モード設定関連
        /// <summary>
        /// 現時点でのモードを返す
        /// </summary>
        /// <returns>COMMAND_MODEまたはEDIT_MODE</returns>
        public int GetNowMode()
        {
            return viModeVal;
        }

        /// <summary>
        /// モード情報を設定する
        /// </summary>
        /// <param name="SetModeVal">COMMAND_MODEまたはEDIT_MODE</param>
        public void SetNowMode(int SetModeVal)
        {
            viModeVal = SetModeVal;
        }

        /// <summary>
        /// KeyViを無効（Disableモード）に設定する
        /// </summary>
        public void setKeyViDisable()
        {
            keyViEnable = false;
        }

        /// <summary>
        /// KeyViを有効に設定する
        /// </summary>
        public void setKeyViEnable()
        {
            keyViEnable = true;
        }

        /// <summary>
        /// KeyViが有効かどうかを返す
        /// </summary>
        /// <returns>有効：true　無効：false</returns>
        public bool isKeyViEnable()
        {
            return keyViEnable;
        }

        /// <summary>
        /// KeyViをIgnoreモード（無視）に設定する
        /// </summary>
        public void setKeyViIgnore()
        {
            keyViIgnore = true;
        }

        /// <summary>
        /// KeyViのIgnoreモードを無効にする
        /// </summary>
        public void setKeyViNotIgnore()
        {
            keyViIgnore = false;
        }

        /// <summary>
        /// KeyViが無視（Ignoreモード）かどうかを返す
        /// </summary>
        /// <returns>Ignoreモード：true　Ignoreモードではない：false</returns>
        public bool IsKeyViIgnore()
        {
            return keyViIgnore;
        }
        #endregion

    }
}
