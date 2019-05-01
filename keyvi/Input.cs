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
        /// キューに格納されているデータを取得する
        /// </summary>
        /// <param name="keyQueue">viコマンドが格納されているQueueオブジェクト</param>
        /// <returns>キューに格納されているコマンド（文字列型）</returns>
        /// <remarks>Queueオブジェクトからデータを順に取り出しStringBuilderオブジェクトに追加、
        /// 最後にStringBuilderオブジェクトをStringにキャストして戻り値とする</remarks>
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
        /// Enterキーが押下された時点でDisableモードに移行するかを判断する
        /// </summary>
        /// <param name="keyQueue">viコマンドが格納されているQueueオブジェクト</param>
        /// <returns>Disableモードに移行する：EnvVal.SET_DISABLE_MODE
        /// Disableモードに移行しない：EnvVal.NOT_SET_DISABLE_MODE</returns>
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
        /// クリップボードにコピーする
        /// </summary>
        /// <param name="keyQueue">viコマンドが格納されているQueueオブジェクト</param>
        /// <remarks>viコマンドの内容に応じて通常コピーまたは行コピーを行う</remarks>
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
        /// 行削除処理を行う
        /// </summary>
        /// <param name="keyQueue">viコマンドが格納されているQueueオブジェクト</param>
        /// <remarks>viコマンドの内容に応じて行削除を行う</remarks>
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
        /// 現在行の上に空行を挿入する
        /// </summary>
        public static void PreviewRowInsert()
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("{ENTER}");
            SendKeys.Send("{UP}");
            SendKeys.Send("{HOME}");
        }

        /// <summary>
        /// 現在行の下に空行を挿入する
        /// </summary>
        public static void NextRowInsert()
        {
            SendKeys.Send("{END}");
            SendKeys.Send("{ENTER}");
        }

        /// <summary>
        /// 直前動作の取り消し操作を行う
        /// </summary>
        public static void Undo()
        {
            SendKeys.Send("^z");
        }

        /// <summary>
        /// IMEのON/OFFを切り替える
        /// </summary>
        public static void ImeChange()
        {
            keybd_event((byte)EnvVal.VK_KANJI, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_KANJI, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// カーソル「↑」を発行する
        /// </summary>
        public static void UpKey()
        {
            SendKeys.Send("{UP}");
        }

        /// <summary>
        /// カーソル「↓」を発行する
        /// </summary>
        public static void DownKey()
        {
            SendKeys.Send("{DOWN}");
        }

        /// <summary>
        /// カーソル「←」を発行する
        /// </summary>
        public static void LeftKey()
        {
            SendKeys.Send("{LEFT}");
        }

        /// <summary>
        /// カーソル「→」を発行する
        /// </summary>
        public static void RightKey()
        {
            SendKeys.Send("{RIGHT}");
        }

        /// <summary>
        /// 1画面下にスクロールする
        /// </summary>
        public static void PageDown()
        {
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_NEXT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_NEXT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// 1画面上にスクロールする
        /// </summary>
        public static void PageUp()
        {
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_PRIOR, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_PRIOR, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// クリップボードの内容をペーストする
        /// </summary>
        public static void Paste()
        {
            SendKeys.Send("^v");
        }

        /// <summary>
        /// ダミーとしてF16キー押下を行う
        /// </summary>
        public static void DummyKey()
        {
            SendKeys.Send("{F16}");
        }

        /// <summary>
        /// 行頭へ移動する
        /// </summary>
        public static void HomeKey()
        {
            SendKeys.Send("{HOME}");
        }

        /// <summary>
        /// 行末へ移動する
        /// </summary>
        public static void EndKey()
        {
            SendKeys.Send("{END}");
        }

        /// <summary>
        /// Shiftキーを押しながらカーソル「←」を発行する
        /// </summary>
        public static void ShiftLeftKey()
        {
            keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_LEFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Shiftキーを押しながらカーソル「↓」を発行する
        /// </summary>
        public static void ShiftDownKey()
        {
            //SendKeys.Send("+({DOWN})");
            keybd_event((byte)EnvVal.VK_DOWN, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_DOWN, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Shiftキーを押しながらカーソル「↑」を発行する
        /// </summary>
        public static void ShiftUpKey()
        {
            //SendKeys.Send("+({UP})");
            keybd_event((byte)EnvVal.VK_UP, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_UP, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Shiftキーを押しながらカーソル「→」を発行する
        /// </summary>
        public static void ShiftRightKey()
        {
            //SendKeys.Send("+({RIGHT})");
            keybd_event((byte)EnvVal.VK_RIGHT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_RIGHT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// 削除を行う
        /// </summary>
        public static void Delete()
        {
            //SendKeys.Send("{DELETE}");
            keybd_event((byte)EnvVal.VK_DELETE, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_DELETE, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// 後退を行う
        /// </summary>
        /// <remarks>Shift + x 押下時の処理</remarks>
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
        /// 後退を行う
        /// </summary>
        /// <remarks>Ctrl + h 押下時の処理(内部処理でCtrlの押上/押下を伴う)</remarks>
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
        /// Enterキー押下処理を行う
        /// </summary>
        public static void Enter()
        {
            SendKeys.Send("{ENTER}");
            //keybd_event((byte)VK_RETURN, 0, KEYEVENTF_SILENT, (UIntPtr)0);
            //keybd_event((byte)VK_RETURN, 0, KEYEVENTF_KEYUP | KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// Escキー押下処理を行う
        /// </summary>
        public static void Escape()
        {
            SendKeys.Send("{ESC}");
            //keybd_event((byte)VK_RETURN, 0, KEYEVENTF_SILENT, (UIntPtr)0);
            //keybd_event((byte)VK_RETURN, 0, KEYEVENTF_KEYUP | KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// 変換キー押下処理を行う
        /// </summary>
        public static void Henkan()
        {
            keybd_event((byte)EnvVal.VK_CONVERT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONVERT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// 無変換キー押下処理を行う
        /// </summary>
        public static void Muhenkan()
        {
            keybd_event((byte)EnvVal.VK_NOCONVERT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_NOCONVERT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// 文頭移動処理を行う
        /// </summary>
        /// <param name="keyQueue">viコマンドが格納されているQueueオブジェクト</param>
        /// <remarks>viコマンドの内容に応じて文頭移動処理を行う</remarks>
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
        /// 文末移動処理を行う
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
        /// 単語移動処理を行う（次の単語）
        /// </summary>
        public static void MoveNextWord()
        {
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_RIGHT, 0, EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_RIGHT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
            keybd_event((byte)EnvVal.VK_CONTROL, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }

        /// <summary>
        /// 単語移動処理を行う（前の単語）
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
        /// Shiftキー押上処理を行う
        /// </summary>
        public static void ShiftKeyUp()
        {
            keybd_event((byte)EnvVal.VK_SHIFT, 0, EnvVal.KEYEVENTF_KEYUP | EnvVal.KEYEVENTF_SILENT, (UIntPtr)0);
        }
    }
}
