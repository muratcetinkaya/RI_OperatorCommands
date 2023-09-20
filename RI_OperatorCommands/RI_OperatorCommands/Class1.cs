using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;


namespace RI_OperatorCommands
{
    public class Commands
    {


        #region
        public string HostName;

        private const string cnstApp = "frrjiftest";
        private const string cnstSection = "setting";

        private Random rnd = new Random();

        private FRRJIf.Core mobjCore;
        private FRRJIf.DataTable mobjDataTable;
        private FRRJIf.DataTable mobjDataTable2;
        private FRRJIf.DataCurPos mobjCurPos;
        private FRRJIf.DataCurPos mobjCurPosUF;
        private FRRJIf.DataCurPos mobjCurPos2;
        private FRRJIf.DataTask mobjTask;
        private FRRJIf.DataTask mobjTaskIgnoreMacro;
        private FRRJIf.DataTask mobjTaskIgnoreKarel;
        private FRRJIf.DataTask mobjTaskIgnoreMacroKarel;
        private FRRJIf.DataPosReg mobjPosReg;
        private FRRJIf.DataPosReg mobjPosReg2;
        private FRRJIf.DataPosRegXyzwpr mobjPosRegXyzwpr;
        private FRRJIf.DataPosRegMG mobjPosRegMG;
        private FRRJIf.DataSysVar mobjSysVarInt;
        private FRRJIf.DataSysVar mobjSysVarInt2;
        private FRRJIf.DataSysVar mobjSysVarReal;
        private FRRJIf.DataSysVar mobjSysVarReal2;
        private FRRJIf.DataSysVar mobjSysVarString;
        private FRRJIf.DataSysVarPos mobjSysVarPos;
        private FRRJIf.DataSysVar[] mobjSysVarIntArray;
        private FRRJIf.DataNumReg mobjNumReg;
        private FRRJIf.DataNumReg mobjNumReg2;
        private FRRJIf.DataNumReg mobjNumReg3;
        private FRRJIf.DataAlarm mobjAlarm;
        private FRRJIf.DataAlarm mobjAlarmCurrent;
        private FRRJIf.DataSysVar mobjVarString;
        private FRRJIf.DataString mobjStrReg;
        private FRRJIf.DataString mobjStrRegComment;
        private FRRJIf.DataString mobjReg_Comment;

        #endregion

        //github.com/muratcetinkaya
        

        private readonly object connectLock = new object();

        public bool ConnectTo(string IPAddress, int DataPosRegStartIndex, int DataPosRegEndIndex)
        {
            lock (connectLock)
            {
                try
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                    mobjCore = new FRRJIf.Core();

                    // You need to set data table before connecting.
                    mobjDataTable = mobjCore.DataTable;

                    {
                        mobjAlarm = mobjDataTable.AddAlarm(FRRJIf.FRIF_DATA_TYPE.ALARM_LIST, 5, 0);
                        mobjAlarmCurrent = mobjDataTable.AddAlarm(FRRJIf.FRIF_DATA_TYPE.ALARM_CURRENT, 1, 0);
                        mobjCurPos = mobjDataTable.AddCurPos(FRRJIf.FRIF_DATA_TYPE.CURPOS, 1);
                        mobjCurPosUF = mobjDataTable.AddCurPosUF(FRRJIf.FRIF_DATA_TYPE.CURPOS, 1, 15);
                        mobjCurPos2 = mobjDataTable.AddCurPos(FRRJIf.FRIF_DATA_TYPE.CURPOS, 2);
                        mobjTask = mobjDataTable.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 1);
                        mobjTaskIgnoreMacro = mobjDataTable.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK_IGNORE_MACRO, 1);
                        mobjTaskIgnoreKarel = mobjDataTable.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK_IGNORE_KAREL, 1);
                        mobjTaskIgnoreMacroKarel = mobjDataTable.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK_IGNORE_MACRO_KAREL, 1);
                        mobjPosReg = mobjDataTable.AddPosReg(FRRJIf.FRIF_DATA_TYPE.POSREG, 1, DataPosRegStartIndex, DataPosRegEndIndex);
                        mobjPosReg2 = mobjDataTable.AddPosReg(FRRJIf.FRIF_DATA_TYPE.POSREG, 2, 1, 4);
                        mobjSysVarInt = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$FAST_CLOCK");
                        mobjSysVarInt2 = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[10].$TIMER_VAL");
                        mobjSysVarReal = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_REAL, "$MOR_GRP[1].$CURRENT_ANG[1]");
                        mobjSysVarReal2 = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_REAL, "$DUTY_TEMP");
                        mobjSysVarString = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_STRING, "$TIMER[10].$COMMENT");
                        mobjSysVarPos = mobjDataTable.AddSysVarPos(FRRJIf.FRIF_DATA_TYPE.SYSVAR_POS, "$MNUTOOL[1,1]");
                        mobjVarString = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_STRING, "$[HTTPKCL]CMDS[1]");
                        mobjNumReg = mobjDataTable.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 1, 200);
                        mobjNumReg2 = mobjDataTable.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_REAL, 1, 200);
                        mobjPosRegXyzwpr = mobjDataTable.AddPosRegXyzwpr(FRRJIf.FRIF_DATA_TYPE.POSREG_XYZWPR, 1, 1, 10);
                        mobjPosRegMG = mobjDataTable.AddPosRegMG(FRRJIf.FRIF_DATA_TYPE.POSREGMG, "C,J6", DataPosRegStartIndex, DataPosRegEndIndex);
                        mobjReg_Comment = mobjDataTable.AddString(FRRJIf.FRIF_DATA_TYPE.NUMREG_COMMENT, 1, 10);
                        mobjStrReg = mobjDataTable.AddString(FRRJIf.FRIF_DATA_TYPE.STRREG, 1, 10);
                        mobjStrRegComment = mobjDataTable.AddString(FRRJIf.FRIF_DATA_TYPE.STRREG_COMMENT, 1, 200);
                    }

                    // 2nd data table.
                    // You must not set the first data table.
                    mobjDataTable2 = mobjCore.DataTable2;
                    mobjNumReg3 = mobjDataTable2.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 1, 5);
                    mobjSysVarIntArray = new FRRJIf.DataSysVar[10];
                    mobjSysVarIntArray[0] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[1].$TIMER_VAL");
                    mobjSysVarIntArray[1] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[2].$TIMER_VAL");
                    mobjSysVarIntArray[2] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[3].$TIMER_VAL");
                    mobjSysVarIntArray[3] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[4].$TIMER_VAL");
                    mobjSysVarIntArray[4] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[5].$TIMER_VAL");
                    mobjSysVarIntArray[5] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[6].$TIMER_VAL");
                    mobjSysVarIntArray[6] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[7].$TIMER_VAL");
                    mobjSysVarIntArray[7] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[8].$TIMER_VAL");
                    mobjSysVarIntArray[8] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[9].$TIMER_VAL");
                    mobjSysVarIntArray[9] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[10].$TIMER_VAL");

                    //get host name
                    if (mobjCore.Connect(IPAddress.Trim()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;


                    }




                }

                catch (Exception ex)
                {
                    MessageBox.Show("FAULT!!!!" + ex.Message);
                    return false;

                }
            }

        }

        public bool SetDO(int DO, int Value)
        {
            try
            {
                Array buf = new short[1];
                buf.SetValue((short)Value, 0);
                if (mobjCore.WriteSDO(DO, ref buf, 1))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("FAULT: " + ex.Message);
                return false;
            }

        }

        public bool WriteReg_float(int R, float[] Value, int Count)
        {
            lock (connectLock)
            {
                try
                {
                    if (mobjNumReg2.SetValues(R, Value, Count))
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }



                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                    return false;

                }

            }


        }


        public bool WriteReg_int(int R, int[] Value, int Count)
        {
            lock (connectLock)
            {
                try
                {
                    if (mobjNumReg.SetValues(R, Value, Count))
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }



                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                    return false;

                }

            }


        }

        public bool WriteReg_Comment(int R, string Value)
        {
            lock (connectLock)
            {
                try
                {
                    if (mobjReg_Comment.SetValue(R, Value))
                    {
                        mobjReg_Comment.Update();
                        return true;
                    }
                    else
                    {

                        return false;

                    }


                }


                catch (Exception ex)
                {
                    MessageBox.Show("string update error" + ex.Message.ToString());
                    return false;
                }
            }

        }

        public bool WriteJointPos(int PR, float[] Value, short UF, short UT)

        {
            lock (connectLock)
            {

                try {

                    if(mobjPosReg.SetValueJoint(PR, Value, UF, UT))
                        {
                        return true;
                        }

                    else {
                        return false;
                         }



                }
                catch(Exception ex)
                {
                    MessageBox.Show("Set jointpos CATCHED:" + "" + ex.Message.ToString());

                    return false;
                }
        
            }

        }

        public void DisconnectRobot()
        {
            try
            {
                mobjCore.Disconnect();
                mobjCore = null;
                mobjDataTable = null;
                mobjAlarm = null;
                mobjAlarmCurrent = null;
                mobjCurPos = null;
                mobjCurPosUF = null;
                mobjCurPos2 = null;
                mobjTask = null;
                mobjTaskIgnoreMacro = null;
                mobjTaskIgnoreKarel = null;
                mobjTaskIgnoreMacroKarel = null;
                mobjPosReg = null;
                mobjPosReg2 = null;
                mobjSysVarInt = null;
                mobjSysVarInt2 = null;
                mobjSysVarReal = null;
                mobjSysVarReal2 = null;
                mobjSysVarString = null;
                mobjSysVarPos = null;
                mobjVarString = null;
                mobjNumReg = null;
                mobjNumReg2 = null;
                mobjPosRegXyzwpr = null;
                mobjPosRegMG = null;
                mobjStrReg = null;
                mobjStrRegComment = null;


             


            }



            catch(Exception ex)
            {

                MessageBox.Show("Disconnect Fault: " + ex.Message);
                
            }
        }


        public bool WriteLineerPos(int PR, float X, float Y, float Z, float W, float P, float R, float E1, float E2, float E3,
            short C1, short C2, short C3, short C4, short C5, short C6, short C7, short UF, short UT)
        {
            lock(connectLock)
            {
                try {
                    if(mobjPosReg.SetValueXyzwpr2(PR, X, Y, Z, W, P, R, E1, E2, E3, C1, C2, C3, C4, C5, C6, C7, UF, UT))
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show("xyzwpr catched" + ex.Message.ToString());
                    return false;

                }
            }
        }

        public bool ReadDO(int DO, ref Array Result, int Count)
        {
            lock (connectLock)
            {
                try
                {
                    mobjDataTable.Refresh();
                    if(mobjCore.ReadSDO(DO, ref Result, Count))
                    {
                        return true;

                    }
                    else
                    {

                        return false;
                    }

                }

                catch(Exception ex)
                {
                    MessageBox.Show("READ DO CATCHCED"+ex.Message.ToString());
                    return false;


                }

            }


        }

        public bool ReadReg(int R, ref object Result)
        {
            lock (connectLock)
            {
                try
                {
                    mobjDataTable.Refresh();
                    if (mobjNumReg2.GetValue(R, ref Result))
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                catch(Exception ex)
                {
                    MessageBox.Show("READ REGISTER CATCHED" +""+ ex.Message.ToString());

                    return false;
                }
            }

        }

        public bool ReadDI (int DI, ref Array Result, int Count)
        {
            lock(connectLock)
            {
                try
                {
                    mobjDataTable.Refresh();

                    if (mobjCore.ReadSDI(DI, ref Result, Count))
                    {

                        return true;
                    }
                    else
                    {

                        return false;

                    }





                }

                catch (Exception ex)
                {
                    MessageBox.Show("READ DI CATCHED" + "" + ex.Message.ToString());

                    return false;

                }

            }

        }

        public bool ReadPRJoint(int PR, ref float[] Result) {

            lock (connectLock)
            {
                try
                {
                    short ut = 0, validJ = 0;
                    mobjDataTable.Refresh();
                    if(mobjPosReg.GetValueJoint(PR, ref Result[0], ref Result[1], ref Result[2], ref Result[3], ref Result[4], ref Result[5], ref Result[6], ref Result[7], ref Result[8], ref ut, ref validJ))
                    {
                        return true;

                    }

                    else
                    {
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("READ jointpos CATCHED:" + "" + ex.Message.ToString());

                    return false;

                }

            }





        }

        public bool ReadPRCart(int PR, ref float[] Result)
        {
            lock (connectLock)
            {

                try
                {
                    float X = 0, Y = 0, Z = 0, W = 0, P = 0, R = 0, E1 = 0, E2 = 0, E3 = 0;
                    short C1 = 0, C2 = 0, C3 = 0, C4 = 0, C5 = 0, C6 = 0, C7 = 0;
                    short ut = 0, validc = 0, uf = 0;
                    mobjDataTable.Refresh();
                    if(mobjPosReg.GetValueXyzwpr(PR, ref X, ref Y, ref Z, ref W, ref P, ref R, ref E1, ref E2, ref E3, ref C1, ref C2, ref C3, ref C4,
                         ref C5, ref C6, ref C7, ref uf, ref ut, ref validc))
                    {
                        Result[0]= X;
                        Result[1]= Y;
                        Result[2]= Z;
                        Result[3]= W;
                        Result[4]= P;
                        Result[5]= R;
                        return true;
                    }
                    else
                    {

                        return false;
                    }

                }

                catch (Exception ex)
                {

                    MessageBox.Show("READ cartpos CATCHED:" + "" + ex.Message.ToString());
                    return false;

                }


            }



        }

        public bool ReadActualPos (ref Array xyzwpr)
        {
            lock (connectLock)
            {
                try
                {
                    Array config = new short[7];
                    Array joint = new float[9];
                    short intUF = 0;
                    short intUT = 0;
                    short intValidC = 0;
                    short intValidJ = 0;
                    mobjDataTable.Refresh();
                    mobjCurPos.GetValue(ref xyzwpr, ref config, ref joint, ref intUF, ref intUT, ref intValidC, ref intValidJ);
                    return true;



                }


                catch(Exception ex)
                {

                    MessageBox.Show("READ LPOS CATCHED:" + "" + ex.Message.ToString());
                    return false;

                }
            }

        }

        public bool WriteCartPOS(int PR, float X, float Y, float Z, float W, float P, float R, float E1, float E2, float E3,
            short C1, short C2, short C3, short C4, short C5, short C6, short C7, short UF, short UT)
        {
            lock (connectLock)
            {
                try
                {
                    if (mobjPosReg.SetValueXyzwpr2(PR, X, Y, Z, W, P, R, E1, E2, E3, C1, C2, C3, C4, C5, C6, C7, UF, UT))
                    {
                        return true;
                    }

                    else
                    {
                        return false;


                    }
                }

                catch(Exception ex)
                {
                    MessageBox.Show("Write cartpos CATCHED:" + "" + ex.Message.ToString());
                    return false;

                }

            }
        }


    }




}
