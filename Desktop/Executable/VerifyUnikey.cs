using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearCanvas.Common;

namespace ClearCanvas.Desktop.Executable
{
    class VerifyUnikey
    {
        //private static ushort _password1 = 65143;
        //private static ushort _password2 = 39181;

        public static bool IsValidUser()
        {
            long[] hid = new long[32];
            long[] keyhand = new long[8];
            long keynumber = 0;
            long ret;

            string pwdStr1 = ProductInformation.Password1;
            string pwdStr2 = ProductInformation.Password2;
            ushort pwd1 = Convert.ToUInt16(pwdStr1);
            ushort pwd2 = Convert.ToUInt16(pwdStr2);

            ret = Unikey.Net.Apis.UnikeyAppApis.UniKey_Find(out keyhand, out hid, ref keynumber);
            if (ret != 0)
            {
                return false;
            }

            ret = Unikey.Net.Apis.UnikeyAppApis.UniKey_User_Logon(keyhand[0], pwd1, pwd2);
            if (ret != 0)
            {
                return false;
            }

            // Read memory 
            bool isExpired = false;
            byte[] buf = new byte[200];
            ret = Unikey.Net.Apis.UnikeyAppApis.UniKey_Read_Memory(keyhand[0], 0, buf.Length, buf);
            if (ret == 0)
            {
                try
                {
                    string expiredDateStr = Encoding.ASCII.GetString(buf);
                    DateTime expiredDate = DateTime.Parse(expiredDateStr);
                    DateTime currentDate = DateTime.Now;

                    if (currentDate.CompareTo(expiredDate) > 0)
                    {
                        isExpired = true;
                    }
                }
                catch (Exception ex)
                {
                    isExpired = true;
                }
            }

            ret = Unikey.Net.Apis.UnikeyAppApis.UniKey_Logoff(ref keyhand[0]);
            if (ret != 0)
            {
                return false;
            }

            return !isExpired;
        }
    }
}
