using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.PartnerLib
{
    public class HexStringUtil
    {
        // @formatter:off
        static byte[] HEX_CHAR_TABLE = {
        (byte) '0', (byte) '1', (byte) '2', (byte) '3',
        (byte) '4', (byte) '5', (byte) '6', (byte) '7',
        (byte) '8', (byte) '9', (byte) 'a', (byte) 'b',
        (byte) 'c', (byte) 'd', (byte) 'e', (byte) 'f'
    };
        // @formatter:on

        /**
         * Convert a byte array to a hexadecimal string
         * 
         * @param raw
         *            A raw byte array
         * 
         * @return Hexadecimal string
         */
        public static String byteArrayToHexString(byte[] raw)
        {
            byte[] hex = new byte[2 * raw.Length];
            int index = 0;

            foreach (byte b in raw)
            {
                int v = b & 0xFF;
                hex[index++] = HEX_CHAR_TABLE[v >> 4];
                hex[index++] = HEX_CHAR_TABLE[v & 0xF];
            }
            string hexstr = "";
            foreach (byte b in hex)
            {
                hexstr += ((Char)b).ToString();
            }
            
            return hexstr;
        }

        /**
         * Convert a hexadecimal string to a byte array
         * 
         * @param raw
         *            A hexadecimal string
         * 
         * @return The byte array
         */
        public static byte[] hexStringToByteArray(String hex)
        {
            String hexstandard = hex.ToLower();
            char[] l_hex = hexstandard.ToCharArray();
            int sz = hexstandard.Length / 2;
            byte[] bytesResult = new byte[sz];

            int idx = 0;
            for (int i = 0; i < sz; i++)
            {
                bytesResult[i] = (byte)(l_hex[idx]);
                ++idx;
                byte tmp = (byte)(l_hex[idx]);
                ++idx;

                if (bytesResult[i] > HEX_CHAR_TABLE[9])
                {
                    bytesResult[i] -= ((byte)('a') - 10);
                }
                else
                {
                    bytesResult[i] -= (byte)('0');
                }
                if (tmp > HEX_CHAR_TABLE[9])
                {
                    tmp -= ((byte)('a') - 10);
                }
                else
                {
                    tmp -= (byte)('0');
                }

                bytesResult[i] = (byte)(bytesResult[i] * 16 + tmp);
            }
            return bytesResult;
        }
    }
}
