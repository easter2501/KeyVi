using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace keyvi
{
    /// <summary>
    /// 設定画面。
    /// KeyVi設定ファイル「keyviConfig.xml」の読み込み、編集を行う。
    /// </summary>
    public partial class keyviConfig : Form
    {
        //XmlDataDocument xmlDoc;
        DataTable GeneralTable;
        DataTable ApplicationTable;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public keyviConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 設定画面起動時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>keyviConfig.xmlを読み込み、画面に内容を出力する</remarks>
        private void keyviConfig_Load(object sender, EventArgs e)
        {
            this.buttonConfigOK.DialogResult = DialogResult.OK;
            this.buttonConfigCancel.DialogResult = DialogResult.Cancel;

            ReadXML_Config();
            //xmlDoc = new XmlDataDocument(AppConfigDataSet);

            //設定情報を画面に出力
            DrawGeneralConfig();
            DrawApplicationConfig();
        }


        /// <summary>
        /// keyviConfig.xmlを読み込む
        /// </summary>
        /// <remarks>
        /// keyviConfig.xmlを
        /// アプリケーション設定用Datasetと
        /// アプリケーション設定用DataTableと
        /// 全体設定用のDataTableへ読み込み、
        /// アプリケーション設定用Datagridviewへ関連付ける
        /// </remarks>
        private void ReadXML_Config()
        {
            string filePath = EnvVal.KEYVI_CONFIG_FILENAME;
            AppConfigDataSet.ReadXml(filePath);

            //列が自動的に作成されないようにする
            dataGridView1.AutoGenerateColumns = false;

            //Application用の設定情報を取得する
            dataGridView1.DataSource = AppConfigDataSet;
            dataGridView1.DataMember = EnvVal.XML_TABLE_APPLICATION;

            //General用の設定情報を取得する
            GeneralTable = AppConfigDataSet.Tables[EnvVal.XML_TABLE_GENERAL];

            //Application用の設定情報を取得する
            ApplicationTable = AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION];

            //General,Applicationの両DataTableに不足している列を強制的に追加する（設定内容が不足しているXMLファイルを読み込んだとき対策）
            TableAddColumns();
        }

        /// <summary>
        /// General,Applicationの両DataTableに不足している列を強制的に追加する（設定内容が不足しているXMLファイルを読み込んだとき対策）
        /// </summary>
        private void TableAddColumns()
        {
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_NAME )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_NAME );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_ZERO )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_ZERO );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4 )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4 );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_Y )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_Y );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_YY )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_YY );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_P )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_P );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_X )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_X );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_DD )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_DD );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_O )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_O );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTO )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTO );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_U )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_U );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_GG )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_GG );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_W )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_W );
            if (!ApplicationTable.Columns.Contains( EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_B )) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_B );

            if (!ApplicationTable.Columns.Contains(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTI)) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTI);
            if (!ApplicationTable.Columns.Contains(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTA)) ApplicationTable.Columns.Add(EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTA);

            if (!GeneralTable.Columns.Contains(EnvVal.XML_STRING_GENERAL_KEYBOARD)) GeneralTable.Columns.Add(EnvVal.XML_STRING_GENERAL_KEYBOARD);
            if (!GeneralTable.Columns.Contains(EnvVal.XML_STRING_GENERAL_COMMANDENTER)) GeneralTable.Columns.Add(EnvVal.XML_STRING_GENERAL_COMMANDENTER);
            if (!GeneralTable.Columns.Contains(EnvVal.XML_STRING_GENERAL_HENKANKEYTOIMEONOFF)) GeneralTable.Columns.Add(EnvVal.XML_STRING_GENERAL_HENKANKEYTOIMEONOFF);
            if (!GeneralTable.Columns.Contains(EnvVal.XML_STRING_GENERAL_MUHENKANKEYTOESC)) GeneralTable.Columns.Add(EnvVal.XML_STRING_GENERAL_MUHENKANKEYTOESC);
        }

        /// <summary>
        /// keyviConfig.xmlから読み込んだ「全般」に関する設定情報を画面に表示する
        /// </summary>
        private void DrawGeneralConfig()
        {            
            switch (GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_KEYBOARD].ToString().ToUpper())
            {
                case EnvVal.KEYBOADTYPE_JP106:
                    radioJP106.Checked = true;
                    break;

                case EnvVal.KEYBOADTYPE_US:
                    radioUS.Checked = true;
                    break;
            }


            if (GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_COMMANDENTER].ToString().ToUpper() 
                == EnvVal.XML_STRING_TRUE)
            {
                radioCommandEnterEnable.Checked = true;
            }
            else
            {
                radioCommandEnterDisable.Checked = true;
            }

            if (GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_HENKANKEYTOIMEONOFF].ToString().ToUpper() 
                == EnvVal.XML_STRING_TRUE)
            {
                radioHenkanKeyToImeOnOffEnable.Checked = true;
            }
            else
            {
                radioHenkanKeyToImeOnOffDisable.Checked = true;
            }

            if (GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_MUHENKANKEYTOESC].ToString().ToUpper() 
                == EnvVal.XML_STRING_TRUE)
            {
                radioMuhenkanKeyToEscEnable.Checked = true;
            }
            else
            {
                radioMuhenkanKeyToEscDisable.Checked = true;
            }

        }

        /// <summary>
        /// 「全般」に関する設定情報をDatasetに出力する
        /// </summary>
        private void SetGeneralConfig()
        {
            if (radioJP106.Checked == true)
            {
                GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_KEYBOARD] = EnvVal.KEYBOADTYPE_JP106;
            }
            else
            {
                GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_KEYBOARD] = EnvVal.KEYBOADTYPE_US;
            }

            if (radioCommandEnterEnable.Checked == true)
            {
                GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_COMMANDENTER] = EnvVal.XML_STRING_TRUE;
            }
            else
            {
                GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_COMMANDENTER] = EnvVal.XML_STRING_FALSE;
            }

            if (radioHenkanKeyToImeOnOffEnable.Checked == true)
            {
                GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_HENKANKEYTOIMEONOFF] = EnvVal.XML_STRING_TRUE;
            }
            else
            {
                GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_HENKANKEYTOIMEONOFF] = EnvVal.XML_STRING_FALSE;
            }

            if (radioMuhenkanKeyToEscEnable.Checked == true)
            {
                GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_MUHENKANKEYTOESC] = EnvVal.XML_STRING_TRUE;
            }
            else
            {
                GeneralTable.Rows[0][EnvVal.XML_STRING_GENERAL_MUHENKANKEYTOESC] = EnvVal.XML_STRING_FALSE;
            }

        }

        /// <summary>
        /// keyviConfig.xmlから読み込んだ「アプリケーションごとの設定」に関する情報を画面に出力する
        /// </summary>
        private void DrawApplicationConfig()
        {
            dataGridView1.Columns.Clear();

            DataGridViewTextBoxColumn ProcessNameColumn = new DataGridViewTextBoxColumn();
            ProcessNameColumn.DataPropertyName = EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_NAME;
            ProcessNameColumn.Name = EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_NAME;
            ProcessNameColumn.HeaderText = EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_NAME;
            dataGridView1.Columns.Add(ProcessNameColumn);

            DataGridViewTextBoxColumn ProcessFileNameColumn = new DataGridViewTextBoxColumn();
            ProcessFileNameColumn.DataPropertyName = EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME;
            ProcessFileNameColumn.Name = EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME;
            ProcessFileNameColumn.HeaderText = EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME;
            dataGridView1.Columns.Add(ProcessFileNameColumn);

            DataGridViewCheckBoxColumn EnableColumn = new DataGridViewCheckBoxColumn();
            EnableColumn.DataPropertyName = EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE;
            EnableColumn.Name = EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE;
            EnableColumn.HeaderText = EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE;
            dataGridView1.Columns.Add(EnableColumn);

            if(dataGridView1.RowCount >= 1)
            {
                dataGridView1.CurrentCell = dataGridView1[0, 0];
                ApplicationConfigRefresh(0);
            }
        }


        #region 画面の制御
        /// <summary>
        /// 「Add Application」押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>対象アプリケーションのファイルパスを取得する</remarks>
        private void AddApplicationlButton_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = EnvVal.SELECT_APPLICATION_DEFAULT_PATH;
            ofd.Filter = EnvVal.SELECT_APPLICATION_FILTER;
            ofd.Title = EnvVal.SAVE_CONFIG_STRING_TITLE;
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() == DialogResult.OK) 
            {
                AddDataGridViewRow(ofd);
            }
        }

        /// <summary>
        /// 空行を一覧表に追加する
        /// </summary>
        private void AddDataGridViewRow()
        {
            DataRow row;
            row = AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].NewRow();

            row[EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_NAME] = "";
            row[EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME] = "";
            row[EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE] = true;

            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows.Add(row);
            DrawApplicationConfig();
        }

        /// <summary>
        /// 対象アプリケーションを一覧表に追加する
        /// </summary>
        private void AddDataGridViewRow(OpenFileDialog ofd)
        {

            DataRow row;
            row = AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].NewRow();

            row[EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_NAME] = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            row[EnvVal.XML_TABLE_APPLICATION_COLUMN_PROCESS_FILE_NAME] = ofd.FileName;
            row[EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE] = true;

            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows.Add(row);
            DrawApplicationConfig();

        }

        /// <summary>
        /// 「Remove Application」押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>選択された行を一覧表から削除する</remarks>
        private void RemoveApplicationlButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[r.Index].Delete();
            }
            DrawApplicationConfig();
        }

        /// <summary>
        /// アプリケーションごとの設定画面にて、すべてのチェックボックスをONにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.groupBoxMoveKeySingle.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = c as CheckBox;
                    chk.Checked = true;
                }
            }

            foreach (Control c in this.groupBoxMoveKey.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = c as CheckBox;
                    chk.Checked = true;
                }
            }

            foreach (Control c in this.groupBoxEditKeySingle.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = c as CheckBox;
                    chk.Checked = true;
                }
            }

            foreach (Control c in this.groupBoxEditKey.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = c as CheckBox;
                    chk.Checked = true;
                }
            }
        }

        /// <summary>
        /// アプリケーションごとの設定画面にて、すべてのチェックボックスをOFFにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.groupBoxMoveKeySingle.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = c as CheckBox;
                    chk.Checked = false;
                }
            }

            foreach (Control c in this.groupBoxMoveKey.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = c as CheckBox;
                    chk.Checked = false;
                }
            }

            foreach (Control c in this.groupBoxEditKeySingle.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = c as CheckBox;
                    chk.Checked = false;
                }
            }

            foreach (Control c in this.groupBoxEditKey.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = c as CheckBox;
                    chk.Checked = false;
                }
            }
        }
        #endregion

        #region 設定の終了
        /// <summary>
        /// 「OK」押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>設定を保存し、画面を閉じる</remarks>
        private void buttonConfigOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(EnvVal.SAVE_CONFIG_STRING,
                EnvVal.SAVE_CONFIG_STRING_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SetGeneralConfig();

                if (AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows.Count < 1)
                {
                    AddDataGridViewRow();
                }

                foreach (DataGridViewRow dvr in dataGridView1.Rows)
                {
                    if (dvr.Cells[EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE].Equals(false))
                    {
                        AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                        .Rows[2][EnvVal.XML_TABLE_APPLICATION_COLUMN_ENABLE] = "false";
                    }
                }

                AppConfigDataSet.WriteXml(EnvVal.KEYVI_CONFIG_FILENAME);
            }
            
            //this.Close();
        }
        /// <summary>
        /// 「キャンセル」押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>設定を保存せず画面を閉じる</remarks>
        private void buttonConfigCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
        }
        #endregion

        #region アプリケーションごとの動作を読み込み
        /// <summary>
        /// DataGridViewのセル選択時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Rows = " + e.RowIndex.ToString());
            //System.Diagnostics.Debug.WriteLine("Value_KeyH = " + AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[e.RowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H].ToString());
            //System.Diagnostics.Debug.WriteLine("Value_KeyJ = " + AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[e.RowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J].ToString());
            //System.Diagnostics.Debug.WriteLine("Value_KeyK = " + AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[e.RowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K].ToString());
            //System.Diagnostics.Debug.WriteLine("Value_KeyL = " + AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[e.RowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L].ToString());

            ApplicationConfigRefresh((int)e.RowIndex);
        }

        /// <summary>
        /// DataGridViewオブジェクトで選択された行に対応するアプリケーションの動作設定を設定画面に出力する
        /// </summary>
        /// <param name="selectedDataGridViewRowIndex">DataGridで選択されたセルの行番号</param>
        private void ApplicationConfigRefresh(int selectedDataGridViewRowIndex)
        {
            //System.Diagnostics.Debug.WriteLine(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H].ToString());
            //System.Diagnostics.Debug.WriteLine(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J].ToString());
            //System.Diagnostics.Debug.WriteLine(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K].ToString());
            //System.Diagnostics.Debug.WriteLine(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L].ToString());

            try
            {
                KeyH_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H].ToString());

                KeyJ_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J].ToString());

                KeyK_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K].ToString());

                KeyL_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L].ToString());

                KeyZero_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_ZERO].ToString());

                KeyShift4_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4].ToString());

                KeyCtrlF_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF].ToString());

                KeyCtrlB_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB].ToString());

                KeyY_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_Y].ToString());

                KeyYY_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_YY].ToString());

                KeyP_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_P].ToString());

                KeyX_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_X].ToString());

                KeyDD_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_DD].ToString());

                KeyO_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_O].ToString());

                KeyShiftO_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTO].ToString());

                KeyShiftX_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX].ToString());

                KeyCtrlH_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH].ToString());

                KeyU_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_U].ToString());

                KeyGG_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_GG].ToString());
                
                KeyShiftG_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG].ToString());

                KeyW_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_W].ToString());

                KeyB_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_B].ToString());

                KeyShiftI_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTI].ToString());

                KeyShiftA_Refresh(AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                    .Rows[selectedDataGridViewRowIndex][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTA].ToString());


            }
            catch (IndexOutOfRangeException index_ex)
            {
                System.Diagnostics.Debug.WriteLine("keyviConfig.cs:dataGridView1_CellClick IndexOutOfRangeException" + index_ex.Message);
            }
        }

        /// <summary>
        /// 【Shift】+【i】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【W】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyShiftI_Refresh(string XmlValue)
        {
            string KeyShiftI_XmlValue = XmlValue.ToUpper();
            if (KeyShiftI_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyShiftI.Checked = true;
            }
            else
            {
                checkKeyShiftI.Checked = false;
            }
        }

        /// <summary>
        /// 【Shift】+【a】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【W】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyShiftA_Refresh(string XmlValue)
        {
            string KeyShiftA_XmlValue = XmlValue.ToUpper();
            if (KeyShiftA_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyShiftA.Checked = true;
            }
            else
            {
                checkKeyShiftA.Checked = false;
            }
        }


        /// <summary>
        /// 【W】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【W】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyW_Refresh(string XmlValue)
        {
            string KeyH_XmlValue = XmlValue.ToUpper();
            if (KeyH_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyW.Checked = true;
            }
            else
            {
                checkKeyW.Checked = false;
            }
        }
        /// <summary>
        /// 【b】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【Shift】+【W】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyB_Refresh(string XmlValue)
        {
            string KeyH_XmlValue = XmlValue.ToUpper();
            if (KeyH_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyB.Checked = true;
            }
            else
            {
                checkKeyB.Checked = false;
            }
        }


        /// <summary>
        /// 【H】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【H】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyH_Refresh(string XmlValue)
        {
            string KeyH_XmlValue = XmlValue.ToUpper();
            if (KeyH_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyH.Checked = true;
            }
            else
            {
                checkKeyH.Checked = false;
            }
        }

        /// <summary>
        /// 【J】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【J】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyJ_Refresh(string XmlValue)
        {
            string KeyJ_XmlValue = XmlValue.ToUpper();
            if (KeyJ_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyJ.Checked = true;
            }
            else
            {
                checkKeyJ.Checked = false;
            }
        }

        /// <summary>
        /// 【K】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【K】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyK_Refresh(string XmlValue)
        {
            string KeyK_XmlValue = XmlValue.ToUpper();
            if (KeyK_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyK.Checked = true;
            }
            else
            {
                checkKeyK.Checked = false;
            }
        }

        /// <summary>
        /// 【L】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【L】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyL_Refresh(string XmlValue)
        {
            string KeyL_XmlValue = XmlValue.ToUpper();
            if (KeyL_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyL.Checked = true;
            }
            else
            {
                checkKeyL.Checked = false;
            }
        }

        /// <summary>
        /// 【0】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【0】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyZero_Refresh(string XmlValue)
        {
            string KeyZero_XmlValue = XmlValue.ToUpper();
            if (KeyZero_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyZero.Checked = true;
            }
            else
            {
                checkKeyZero.Checked = false;
            }
        }

        /// <summary>
        /// 【Shift】+【4】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【Shift】+【4】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyShift4_Refresh(string XmlValue)
        {
            string KeyShift4_XmlValue = XmlValue.ToUpper();
            if (KeyShift4_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyShift4.Checked = true;
            }
            else
            {
                checkKeyShift4.Checked = false;
            }
        }

        /// <summary>
        /// 【Ctrl】+【F】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【Ctrl】+【F】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyCtrlF_Refresh(string XmlValue)
        {
            string KeyCtrlF_XmlValue = XmlValue.ToUpper();
            if (KeyCtrlF_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyCtrlF.Checked = true;
            }
            else
            {
                checkKeyCtrlF.Checked = false;
            }
        }

        /// <summary>
        /// 【Ctrl】+【B】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【Ctrl】+【B】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyCtrlB_Refresh(string XmlValue)
        {
            string KeyCtrlB_XmlValue = XmlValue.ToUpper();
            if (KeyCtrlB_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyCtrlB.Checked = true;
            }
            else
            {
                checkKeyCtrlB.Checked = false;
            }
        }

        /// <summary>
        /// 【Y】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【Y】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyY_Refresh(string XmlValue)
        {
            string KeyY_XmlValue = XmlValue.ToUpper();
            if (KeyY_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyY.Checked = true;
            }
            else
            {
                checkKeyY.Checked = false;
            }
        }

        /// <summary>
        /// 【YY】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【YY】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyYY_Refresh(string XmlValue)
        {
            string KeyYY_XmlValue = XmlValue.ToUpper();
            if (KeyYY_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyYY.Checked = true;
            }
            else
            {
                checkKeyYY.Checked = false;
            }
        }

        /// <summary>
        /// 【P】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【P】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyP_Refresh(string XmlValue)
        {
            string KeyP_XmlValue = XmlValue.ToUpper();
            if (KeyP_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyP.Checked = true;
            }
            else
            {
                checkKeyP.Checked = false;
            }
        }

        /// <summary>
        /// 【X】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【X】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyX_Refresh(string XmlValue)
        {
            string KeyX_XmlValue = XmlValue.ToUpper();
            if (KeyX_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyX.Checked = true;
            }
            else
            {
                checkKeyX.Checked = false;
            }
        }

        /// <summary>
        /// 【DD】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【DD】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyDD_Refresh(string XmlValue)
        {
            string KeyDD_XmlValue = XmlValue.ToUpper();
            if (KeyDD_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyDD.Checked = true;
            }
            else
            {
                checkKeyDD.Checked = false;
            }
        }

        /// <summary>
        /// 【O】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【O】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyO_Refresh(string XmlValue)
        {
            string KeyO_XmlValue = XmlValue.ToUpper();
            if (KeyO_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyO.Checked = true;
            }
            else
            {
                checkKeyO.Checked = false;
            }
        }

        /// <summary>
        /// 【Shift】+【O】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【Shift】+【O】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyShiftO_Refresh(string XmlValue)
        {
            string KeyShiftO_XmlValue = XmlValue.ToUpper();
            if (KeyShiftO_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyShiftO.Checked = true;
            }
            else
            {
                checkKeyShiftO.Checked = false;
            }
        }

        /// <summary>
        /// 【Shift】+【X】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【Shift】+【X】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyShiftX_Refresh(string XmlValue)
        {
            string KeyShiftX_XmlValue = XmlValue.ToUpper();
            if (KeyShiftX_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyShiftX.Checked = true;
            }
            else
            {
                checkKeyShiftX.Checked = false;
            }
        }

        /// <summary>
        /// 【Ctrl】+【H】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【Ctrl】+【H】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyCtrlH_Refresh(string XmlValue)
        {
            string KeyCtrlH_XmlValue = XmlValue.ToUpper();
            if (KeyCtrlH_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyCtrlH.Checked = true;
            }
            else
            {
                checkKeyCtrlH.Checked = false;
            }
        }

        /// <summary>
        /// 【U】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【U】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyU_Refresh(string XmlValue)
        {
            string KeyU_XmlValue = XmlValue.ToUpper();
            if (KeyU_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyU.Checked = true;
            }
            else
            {
                checkKeyU.Checked = false;
            }
        }

        /// <summary>
        /// 【GG】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【GG】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyGG_Refresh(string XmlValue)
        {
            string KeyGG_XmlValue = XmlValue.ToUpper();
            if (KeyGG_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyGG.Checked = true;
            }
            else
            {
                checkKeyGG.Checked = false;
            }
        }

        /// <summary>
        /// 【Shift】+【G】キーの設定情報を設定画面に出力する
        /// </summary>
        /// <param name="XmlValue">【Shift】+【G】キーの動作設定情報（文字列「true」または「false」）</param>
        private void KeyShiftG_Refresh(string XmlValue)
        {
            string KeyShiftG_XmlValue = XmlValue.ToUpper();
            if (KeyShiftG_XmlValue.Equals(EnvVal.XML_STRING_TRUE))
            {
                checkKeyShiftG.Checked = true;
            }
            else
            {
                checkKeyShiftG.Checked = false;
            }
        }
        #endregion

        #region アプリケーションごとの動作をDataSetに出力

        /// <summary>
        /// チェックボックス【H】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【H】の内容をDatasetに登録する</remarks>
        private void checkKeyH_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H] 
                = checkKeyH.Checked;
        }

        /// <summary>
        /// チェックボックス【J】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【J】の内容をDatasetに登録する</remarks>
        private void checkKeyJ_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J] 
                = checkKeyJ.Checked;
        }

        /// <summary>
        /// チェックボックス【K】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【K】の内容をDatasetに登録する</remarks>
        private void checkKeyK_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K] 
                = checkKeyK.Checked;
        }

        /// <summary>
        /// チェックボックス【L】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【L】の内容をDatasetに登録する</remarks>
        private void checkKeyL_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L] 
                = checkKeyL.Checked;
        }

        /// <summary>
        /// チェックボックス【0】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【0】の内容をDatasetに登録する</remarks>
        private void checkKeyZero_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_ZERO] 
                = checkKeyZero.Checked;
        }

        /// <summary>
        /// チェックボックス【Shift】+【4】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【Shift】+【4】の内容をDatasetに登録する</remarks>
        private void checkKeyShift4_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4] 
                = checkKeyShift4.Checked;
        }

        /// <summary>
        /// チェックボックス【Ctrl】+【F】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【Ctrl】+【F】の内容をDatasetに登録する</remarks>
        private void checkKeyCtrlF_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF] 
                = checkKeyCtrlF.Checked;
        }

        /// <summary>
        /// チェックボックス【Ctrl】+【B】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【Ctrl】+【B】の内容をDatasetに登録する</remarks>
        private void checkKeyCtrlB_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB] 
                = checkKeyCtrlB.Checked;
        }

        /// <summary>
        /// チェックボックス【Y】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【Y】の内容をDatasetに登録する</remarks>
        private void checkKeyY_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_Y] 
                = checkKeyY.Checked;
        }

        /// <summary>
        /// チェックボックス【YY】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【YY】の内容をDatasetに登録する</remarks>
        private void checkKeyYY_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_YY] 
                = checkKeyYY.Checked;
        }

        /// <summary>
        /// チェックボックス【P】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【P】の内容をDatasetに登録する</remarks>
        private void checkKeyP_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_P] 
                = checkKeyP.Checked;
        }

        /// <summary>
        /// チェックボックス【X】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【X】の内容をDatasetに登録する</remarks>
        private void checkKeyX_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_X] 
                = checkKeyX.Checked;
        }

        /// <summary>
        /// チェックボックス【DD】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【DD】の内容をDatasetに登録する</remarks>
        private void checkKeyDD_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_DD] 
                = checkKeyDD.Checked;
        }

        /// <summary>
        /// チェックボックス【O】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【O】の内容をDatasetに登録する</remarks>
        private void checkKeyO_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_O] 
                = checkKeyO.Checked;
        }

        /// <summary>
        /// チェックボックス【Shift】+【O】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【Shift】+【O】の内容をDatasetに登録する</remarks>
        private void checkKeyShiftO_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTO] 
                = checkKeyShiftO.Checked;
        }

        /// <summary>
        /// チェックボックス【Shift】+【X】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【Shift】+【X】の内容をDatasetに登録する</remarks>
        private void checkKeyShiftX_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX] 
                = checkKeyShiftX.Checked;
        }

        /// <summary>
        /// チェックボックス【Ctrl】+【H】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【Ctrl】+【H】の内容をDatasetに登録する</remarks>
        private void checkKeyCtrlH_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH] 
                = checkKeyCtrlH.Checked;
        }

        /// <summary>
        /// チェックボックス【U】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【U】の内容をDatasetに登録する</remarks>
        private void checkKeyU_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_U] 
                = checkKeyU.Checked;
        }


        /// <summary>
        /// チェックボックス【GG】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【GG】の内容をDatasetに登録する</remarks>
        private void checkKeyGG_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_GG]
                = checkKeyGG.Checked;
        }

        /// <summary>
        /// チェックボックス【Shift】+【G】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【Shift】+【G】の内容をDatasetに登録する</remarks>
        private void checkKeyShiftG_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG]
            = checkKeyShiftG.Checked;
        }

        /// <summary>
        /// チェックボックス【W】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【W】の内容をDatasetに登録する</remarks>
        private void checkKeyW_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_W]
            = checkKeyW.Checked;
        }

        /// <summary>
        /// チェックボックス【b】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【b】の内容をDatasetに登録する</remarks>
        private void checkKeyB_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_B]
            = checkKeyB.Checked;
        }


        /// <summary>
        /// チェックボックス【Shift】+【i】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【b】の内容をDatasetに登録する</remarks>
        private void checkKeyShiftI_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTI]
            = checkKeyShiftI.Checked;
        }

        /// <summary>
        /// チェックボックス【Shift】+【i】の変更時に行う処理
        /// </summary>
        /// <param name="sender">イベントの送り元（DataGridViewオブジェクト）</param>
        /// <param name="e">イベント データが格納されているクラス</param>
        /// <remarks>変更後のチェックボックス【b】の内容をDatasetに登録する</remarks>
        private void checkKeyShiftA_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTA]
            = checkKeyShiftA.Checked;
        }
        #endregion

    }
}