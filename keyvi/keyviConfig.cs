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
    /// �ݒ��ʁB
    /// KeyVi�ݒ�t�@�C���ukeyviConfig.xml�v�̓ǂݍ��݁A�ҏW���s���B
    /// </summary>
    public partial class keyviConfig : Form
    {
        //XmlDataDocument xmlDoc;
        DataTable GeneralTable;
        DataTable ApplicationTable;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public keyviConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �ݒ��ʋN�����̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>keyviConfig.xml��ǂݍ��݁A��ʂɓ��e���o�͂���</remarks>
        private void keyviConfig_Load(object sender, EventArgs e)
        {
            this.buttonConfigOK.DialogResult = DialogResult.OK;
            this.buttonConfigCancel.DialogResult = DialogResult.Cancel;

            ReadXML_Config();
            //xmlDoc = new XmlDataDocument(AppConfigDataSet);

            //�ݒ������ʂɏo��
            DrawGeneralConfig();
            DrawApplicationConfig();
        }


        /// <summary>
        /// keyviConfig.xml��ǂݍ���
        /// </summary>
        /// <remarks>
        /// keyviConfig.xml��
        /// �A�v���P�[�V�����ݒ�pDataset��
        /// �A�v���P�[�V�����ݒ�pDataTable��
        /// �S�̐ݒ�p��DataTable�֓ǂݍ��݁A
        /// �A�v���P�[�V�����ݒ�pDatagridview�֊֘A�t����
        /// </remarks>
        private void ReadXML_Config()
        {
            string filePath = EnvVal.KEYVI_CONFIG_FILENAME;
            AppConfigDataSet.ReadXml(filePath);

            //�񂪎����I�ɍ쐬����Ȃ��悤�ɂ���
            dataGridView1.AutoGenerateColumns = false;

            //Application�p�̐ݒ�����擾����
            dataGridView1.DataSource = AppConfigDataSet;
            dataGridView1.DataMember = EnvVal.XML_TABLE_APPLICATION;

            //General�p�̐ݒ�����擾����
            GeneralTable = AppConfigDataSet.Tables[EnvVal.XML_TABLE_GENERAL];

            //Application�p�̐ݒ�����擾����
            ApplicationTable = AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION];

            //General,Application�̗�DataTable�ɕs�����Ă����������I�ɒǉ�����i�ݒ���e���s�����Ă���XML�t�@�C����ǂݍ��񂾂Ƃ��΍�j
            TableAddColumns();
        }

        /// <summary>
        /// General,Application�̗�DataTable�ɕs�����Ă����������I�ɒǉ�����i�ݒ���e���s�����Ă���XML�t�@�C����ǂݍ��񂾂Ƃ��΍�j
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
        /// keyviConfig.xml����ǂݍ��񂾁u�S�ʁv�Ɋւ���ݒ������ʂɕ\������
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
        /// �u�S�ʁv�Ɋւ���ݒ����Dataset�ɏo�͂���
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
        /// keyviConfig.xml����ǂݍ��񂾁u�A�v���P�[�V�������Ƃ̐ݒ�v�Ɋւ��������ʂɏo�͂���
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


        #region ��ʂ̐���
        /// <summary>
        /// �uAdd Application�v�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�ΏۃA�v���P�[�V�����̃t�@�C���p�X���擾����</remarks>
        private void AddApplicationlButton_Click(object sender, EventArgs e)
        {
            //OpenFileDialog�N���X�̃C���X�^���X���쐬
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
        /// ��s���ꗗ�\�ɒǉ�����
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
        /// �ΏۃA�v���P�[�V�������ꗗ�\�ɒǉ�����
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
        /// �uRemove Application�v�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�I�����ꂽ�s���ꗗ�\����폜����</remarks>
        private void RemoveApplicationlButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION].Rows[r.Index].Delete();
            }
            DrawApplicationConfig();
        }

        /// <summary>
        /// �A�v���P�[�V�������Ƃ̐ݒ��ʂɂāA���ׂẴ`�F�b�N�{�b�N�X��ON�ɂ���
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
        /// �A�v���P�[�V�������Ƃ̐ݒ��ʂɂāA���ׂẴ`�F�b�N�{�b�N�X��OFF�ɂ���
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

        #region �ݒ�̏I��
        /// <summary>
        /// �uOK�v�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�ݒ��ۑ����A��ʂ����</remarks>
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
        /// �u�L�����Z���v�������̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>�ݒ��ۑ�������ʂ����</remarks>
        private void buttonConfigCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
        }
        #endregion

        #region �A�v���P�[�V�������Ƃ̓����ǂݍ���
        /// <summary>
        /// DataGridView�̃Z���I�����ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
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
        /// DataGridView�I�u�W�F�N�g�őI�����ꂽ�s�ɑΉ�����A�v���P�[�V�����̓���ݒ��ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="selectedDataGridViewRowIndex">DataGrid�őI�����ꂽ�Z���̍s�ԍ�</param>
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
        /// �yShift�z+�yi�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yW�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yShift�z+�ya�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yW�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yW�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yW�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yb�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yShift�z+�yW�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yH�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yH�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yJ�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yJ�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yK�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yK�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yL�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yL�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �y0�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�y0�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yShift�z+�y4�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yShift�z+�y4�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yCtrl�z+�yF�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yCtrl�z+�yF�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yCtrl�z+�yB�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yCtrl�z+�yB�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yY�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yY�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yYY�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yYY�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yP�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yP�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yX�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yX�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yDD�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yDD�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yO�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yO�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yShift�z+�yO�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yShift�z+�yO�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yShift�z+�yX�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yShift�z+�yX�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yCtrl�z+�yH�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yCtrl�z+�yH�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yU�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yU�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yGG�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yGG�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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
        /// �yShift�z+�yG�z�L�[�̐ݒ����ݒ��ʂɏo�͂���
        /// </summary>
        /// <param name="XmlValue">�yShift�z+�yG�z�L�[�̓���ݒ���i������utrue�v�܂��́ufalse�v�j</param>
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

        #region �A�v���P�[�V�������Ƃ̓����DataSet�ɏo��

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yH�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yH�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyH_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_H] 
                = checkKeyH.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yJ�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yJ�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyJ_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_J] 
                = checkKeyJ.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yK�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yK�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyK_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_K] 
                = checkKeyK.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yL�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yL�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyL_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_L] 
                = checkKeyL.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�y0�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�y0�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyZero_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_ZERO] 
                = checkKeyZero.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yShift�z+�y4�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yShift�z+�y4�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyShift4_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFT4] 
                = checkKeyShift4.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yCtrl�z+�yF�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yCtrl�z+�yF�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyCtrlF_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLF] 
                = checkKeyCtrlF.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yCtrl�z+�yB�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yCtrl�z+�yB�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyCtrlB_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLB] 
                = checkKeyCtrlB.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yY�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yY�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyY_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_Y] 
                = checkKeyY.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yYY�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yYY�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyYY_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_YY] 
                = checkKeyYY.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yP�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yP�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyP_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_P] 
                = checkKeyP.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yX�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yX�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyX_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_X] 
                = checkKeyX.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yDD�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yDD�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyDD_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_DD] 
                = checkKeyDD.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yO�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yO�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyO_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_O] 
                = checkKeyO.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yShift�z+�yO�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yShift�z+�yO�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyShiftO_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTO] 
                = checkKeyShiftO.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yShift�z+�yX�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yShift�z+�yX�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyShiftX_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTX] 
                = checkKeyShiftX.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yCtrl�z+�yH�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yCtrl�z+�yH�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyCtrlH_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_CTRLH] 
                = checkKeyCtrlH.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yU�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yU�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyU_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_U] 
                = checkKeyU.Checked;
        }


        /// <summary>
        /// �`�F�b�N�{�b�N�X�yGG�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yGG�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyGG_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
                .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_GG]
                = checkKeyGG.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yShift�z+�yG�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yShift�z+�yG�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyShiftG_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTG]
            = checkKeyShiftG.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yW�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yW�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyW_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_W]
            = checkKeyW.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yb�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yb�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyB_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_B]
            = checkKeyB.Checked;
        }


        /// <summary>
        /// �`�F�b�N�{�b�N�X�yShift�z+�yi�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yb�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyShiftI_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTI]
            = checkKeyShiftI.Checked;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�yShift�z+�yi�z�̕ύX���ɍs������
        /// </summary>
        /// <param name="sender">�C�x���g�̑��茳�iDataGridView�I�u�W�F�N�g�j</param>
        /// <param name="e">�C�x���g �f�[�^���i�[����Ă���N���X</param>
        /// <remarks>�ύX��̃`�F�b�N�{�b�N�X�yb�z�̓��e��Dataset�ɓo�^����</remarks>
        private void checkKeyShiftA_CheckedChanged(object sender, EventArgs e)
        {
            AppConfigDataSet.Tables[EnvVal.XML_TABLE_APPLICATION]
            .Rows[dataGridView1.SelectedRows[0].Index][EnvVal.XML_TABLE_APPLICATION_COLUMN_KEY_SHIFTA]
            = checkKeyShiftA.Checked;
        }
        #endregion

    }
}