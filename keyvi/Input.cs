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
using CodeProject.SystemHotkey;
using CodeProject.Win32;

namespace keyvi
{
    public class Input
    {
        [DllImport("user32.dll")]
        public static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);




        /// <summary>
        /// �L���[�Ɋi�[����Ă���f�[�^���擾����
        /// </summary>
        /// <param name="keyQueue">vi�R�}���h���i�[����Ă���Queue�I�u�W�F�N�g</param>
        /// <returns>�L���[�Ɋi�[����Ă���R�}���h�i������^�j</returns>
        /// <remarks>Queue�I�u�W�F�N�g����f�[�^�����Ɏ��o��StringBuilder�I�u�W�F�N�g�ɒǉ��A
        /// �Ō��StringBuilder�I�u�W�F�N�g��String�ɃL���X�g���Ė߂�l�Ƃ���</remarks>
        private static string getQueueString(Queue keyQueue)
        {
            int i;
            int queueCount;
            StringBuilder commandStr = new StringBuilder();
            object[] objArray;

            queueCount = keyQueue.Count;

            if (queueCount > EnvVal.MAX_QUEUE_LENGTH)
            {
                for (i = 0; i < (queueCount - EnvVal.MAX_QUEUE_LENGTH); i++)
                {
                    keyQueue.Dequeue();
                }
            }
            keyQueue.TrimToSize();
            objArray = keyQueue.ToArray();

            for (i = 0; i < objArray.Length; i++)
            {
                //Console.WriteLine("q1 Member = " + objArray[i]);
                commandStr.Append(objArray[i]);
            }
            //Console.WriteLine("commandStr = " + commandStr);
            return commandStr.ToString().ToString();
        }

        /// <summary>
        /// Enter�L�[���������ꂽ���_��Disable���[�h�Ɉڍs���邩�𔻒f����
        /// </summary>
        /// <param name="keyQueue">vi�R�}���h���i�[����Ă���Queue�I�u�W�F�N�g</param>
        /// <returns>Disable���[�h�Ɉڍs����FEnvVal.SET_DISABLE_MODE
        /// Disable���[�h�Ɉڍs���Ȃ��FEnvVal.NOT_SET_DISABLE_MODE</returns>
        public static int Enter(Queue keyQueue)
        {
            string queueString = getQueueString(keyQueue);

            if (queueString == EnvVal.DISABLE_MODE_COMMAND)
            {
                keyQueue.Clear();
                return EnvVal.SET_DISABLE_MODE;
            }
            else
            {
                keyQueue.Clear();
                return EnvVal.NOT_SET_DISABLE_MODE;
            }
        }


        /// <summary>
        /// �N���b�v�{�[�h�ɃR�s�[����
        /// </summary>
        /// <param name="keyQueue">vi�R�}���h���i�[����Ă���Queue�I�u�W�F�N�g</param>
        /// <remarks>vi�R�}���h�̓��e�ɉ����Ēʏ�R�s�[�܂��͍s�R�s�[���s��</remarks>
        public static void Copy(Queue keyQueue, bool LineCopyEnable, bool CopyEnable)
        {
            string queueString = getQueueString(keyQueue);

            if (queueString == EnvVal.LINE_COPY && LineCopyEnable == true)
            {

                keybd_event((byte)EnvVal.VK_DOWN, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_DOWN, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);


                keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_C, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_C, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keyQueue.Clear();
            }
            else
            {
                if (CopyEnable == true)
                {
                    SendKeys.Send("^c");
                }
            }

        }

        /// <summary>
        /// �s�폜�������s��
        /// </summary>
        /// <param name="keyQueue">vi�R�}���h���i�[����Ă���Queue�I�u�W�F�N�g</param>
        /// <remarks>vi�R�}���h�̓��e�ɉ����čs�폜���s��</remarks>
        public static void LineDelete(Queue keyQueue)
        {
            string queueString = getQueueString(keyQueue);

            if (queueString == EnvVal.LINE_DELETE)
            {

                keybd_event((byte)EnvVal.VK_DOWN, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_DOWN, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);


                keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_X, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_X, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keyQueue.Clear();
            }
        }

        /// <summary>
        /// ���ݍs�̏�ɋ�s��}������
        /// </summary>
        public static void PreviewRowInsert()
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("{ENTER}");
            SendKeys.Send("{UP}");
            SendKeys.Send("{HOME}");
        }

        /// <summary>
        /// ���ݍs�̉��ɋ�s��}������
        /// </summary>
        public static void NextRowInsert()
        {
            SendKeys.Send("{END}");
            SendKeys.Send("{ENTER}");
        }

        /// <summary>
        /// ���O����̎�����������s��
        /// </summary>
        public static void Undo()
        {
            SendKeys.Send("^z");
        }

        /// <summary>
        /// IME��ON/OFF��؂�ւ���
        /// </summary>
        public static void ImeChange()
        {
            keybd_event((byte)EnvVal.VK_KANJI, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_KANJI, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// �J�[�\���u���v�𔭍s����
        /// </summary>
        public static void UpKey()
        {
            SendKeys.Send("{UP}");
        }

        /// <summary>
        /// �J�[�\���u���v�𔭍s����
        /// </summary>
        public static void DownKey()
        {
            SendKeys.Send("{DOWN}");
        }

        /// <summary>
        /// �J�[�\���u���v�𔭍s����
        /// </summary>
        public static void LeftKey()
        {
            SendKeys.Send("{LEFT}");
        }

        /// <summary>
        /// �J�[�\���u���v�𔭍s����
        /// </summary>
        public static void RightKey()
        {
            SendKeys.Send("{RIGHT}");
        }

        /// <summary>
        /// 1��ʉ��ɃX�N���[������
        /// </summary>
        public static void PageDown()
        {
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_NEXT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_NEXT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// 1��ʏ�ɃX�N���[������
        /// </summary>
        public static void PageUp()
        {
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_PRIOR, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_PRIOR, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// �N���b�v�{�[�h�̓��e���y�[�X�g����
        /// </summary>
        public static void Paste()
        {
            SendKeys.Send("^v");
        }

        /// <summary>
        /// �_�~�[�Ƃ���F16�L�[�������s��
        /// </summary>
        public static void DummyKey()
        {
            SendKeys.Send("{F16}");
        }

        /// <summary>
        /// �s���ֈړ�����
        /// </summary>
        public static void HomeKey()
        {
            SendKeys.Send("{HOME}");
        }

        /// <summary>
        /// �s���ֈړ�����
        /// </summary>
        public static void EndKey()
        {
            SendKeys.Send("{END}");
        }

        /// <summary>
        /// Shift�L�[�������Ȃ���J�[�\���u���v�𔭍s����
        /// </summary>
        public static void ShiftLeftKey()
        {
            keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Shift�L�[�������Ȃ���J�[�\���u���v�𔭍s����
        /// </summary>
        public static void ShiftDownKey()
        {
            //SendKeys.Send("+({DOWN})");
            keybd_event((byte)EnvVal.VK_DOWN, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_DOWN, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Shift�L�[�������Ȃ���J�[�\���u���v�𔭍s����
        /// </summary>
        public static void ShiftUpKey()
        {
            //SendKeys.Send("+({UP})");
            keybd_event((byte)EnvVal.VK_UP, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_UP, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Shift�L�[�������Ȃ���J�[�\���u���v�𔭍s����
        /// </summary>
        public static void ShiftRightKey()
        {
            //SendKeys.Send("+({RIGHT})");
            keybd_event((byte)EnvVal.VK_RIGHT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_RIGHT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// �폜���s��
        /// </summary>
        public static void Delete()
        {
            //SendKeys.Send("{DELETE}");
            keybd_event((byte)EnvVal.VK_DELETE, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_DELETE, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// ��ނ��s��
        /// </summary>
        /// <remarks>Shift + x �������̏���</remarks>
        public static void Backspace()
        {
            //keybd_event((byte)VK_CONTROL, 0, KEYEVENTF_KEYUP | KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_BACK, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_BACK, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            //keybd_event((byte)VK_CONTROL, 0, KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// ��ނ��s��
        /// </summary>
        /// <remarks>Ctrl + h �������̏���(����������Ctrl�̉���/�����𔺂�)</remarks>
        public static void CtrlH_Backspace()
        {
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            //keybd_event((byte)VK_SHIFT, 0, KEYEVENTF_KEYUP | KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_BACK, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_BACK, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            //keybd_event((byte)VK_SHIFT, 0, KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Enter�L�[�����������s��
        /// </summary>
        public static void Enter()
        {
            SendKeys.Send("{ENTER}");
            //keybd_event((byte)VK_RETURN, 0, KEYEVENTF_SILENT, (UIntPtr)0);
            //keybd_event((byte)VK_RETURN, 0, KEYEVENTF_KEYUP | KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Esc�L�[�����������s��
        /// </summary>
        public static void Escape()
        {
            SendKeys.Send("{ESC}");
            //keybd_event((byte)VK_RETURN, 0, KEYEVENTF_SILENT, (UIntPtr)0);
            //keybd_event((byte)VK_RETURN, 0, KEYEVENTF_KEYUP | KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// �ϊ��L�[�����������s��
        /// </summary>
        public static void Henkan()
        {
            keybd_event((byte)EnvVal.VK_CONVERT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONVERT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// ���ϊ��L�[�����������s��
        /// </summary>
        public static void Muhenkan()
        {
            keybd_event((byte)EnvVal.VK_NOCONVERT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_NOCONVERT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// �����ړ��������s��
        /// </summary>
        /// <param name="keyQueue">vi�R�}���h���i�[����Ă���Queue�I�u�W�F�N�g</param>
        /// <remarks>vi�R�}���h�̓��e�ɉ����ĕ����ړ��������s��</remarks>
        public static void MoveToDocHead(Queue keyQueue)
        {
            string queueString = getQueueString(keyQueue);

            if (queueString == EnvVal.DOC_HEADER)
            {
                keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_HOME, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
                keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

                keyQueue.Clear();
            }
        }

        /// <summary>
        /// �����ړ��������s��
        /// </summary>
        public static void MoveToDocEnd()
        {

            keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_END, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_END, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

            keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// �P��ړ��������s���i���̒P��j
        /// </summary>
        public static void MoveNextWord()
        {
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_RIGHT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_RIGHT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// �P��ړ��������s���i�O�̒P��j
        /// </summary>
        public static void MovePreviewWord()
        {
            //keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);

            //keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Shift�L�[���㏈�����s��
        /// </summary>
        public static void ShiftKeyUp()
        {
            keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }
    }
}
