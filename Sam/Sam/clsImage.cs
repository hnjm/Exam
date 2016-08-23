using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Sam
{
    class clsImage
    {

        public Byte[] ConvertToByteArray(double[] dblData)
        {
            Byte[] bytOutput;
            int intBytes = dblData.Length * 8;
            IntPtr heapBytes;

            // allocate unmanaged space in which to store sound
            heapBytes = Marshal.AllocHGlobal(intBytes);

            // copy mydata to unmanaged space
            Marshal.Copy(dblData, 0, heapBytes, dblData.Length);

            // create byte array with enough space to hold data
            bytOutput = new Byte[intBytes];
            
            // copy sound data from unmanaged space into byte array
            Marshal.Copy(heapBytes, bytOutput, 0, intBytes);

            // free unmanaged space
            Marshal.FreeHGlobal(heapBytes);

            return (bytOutput);
        }

        public Byte[] ConvertToByteArray(float[] fltData)
        {
            Byte[] bytOutput;
            int intBytes = fltData.Length * 4;
            IntPtr heapBytes;

            heapBytes = Marshal.AllocHGlobal(intBytes);
            Marshal.Copy(fltData, 0, heapBytes, fltData.Length);
            bytOutput = new Byte[intBytes];
            Marshal.Copy(heapBytes, bytOutput, 0, intBytes);
            Marshal.FreeHGlobal(heapBytes);
            return (bytOutput);
        }


        public double[] ConvertToDoubleArray(Byte[] bytData)
        {
            
            double[] dblOutput;
            int intBytes = bytData.Length;
            IntPtr heapBytes;

            // alllocate unmanaged space in which to store sound
            heapBytes = Marshal.AllocHGlobal(intBytes);

            // copy mydata to unmanaged space
            Marshal.Copy(bytData, 0, heapBytes, bytData.Length);

            // create byte array with enough space to hold data
            dblOutput = new double[intBytes/8];

            // copy sound data from unmanaged space into byte array
            Marshal.Copy(heapBytes, dblOutput,0, intBytes/8);

            // free unmanaged space
            Marshal.FreeHGlobal(heapBytes);

            return (dblOutput);
        }


        public Byte[] ConvertToByteArray(Int32[] iData)
        {
            Byte[] bytOutput;
            int intBytes = iData.Length * 4;
            IntPtr heapBytes;

            // allocate unmanaged space in which to store sound
            heapBytes = Marshal.AllocHGlobal(intBytes);

            // copy mydata to unmanaged space
            Marshal.Copy(iData, 0, heapBytes, iData.Length);

            // create byte array with enough space to hold data
            bytOutput = new Byte[intBytes];

            // copy sound data from unmanaged space into byte array
            Marshal.Copy(heapBytes, bytOutput, 0, intBytes);

            // free unmanaged space
            Marshal.FreeHGlobal(heapBytes);

            return (bytOutput);
        }


        public Int32[] ConvertToInt32Array(Byte[] bytData)
        {

            Int32[] iOutput;
            int intBytes = bytData.Length;
            IntPtr heapBytes;

            // alllocate unmanaged space in which to store sound
            heapBytes = Marshal.AllocHGlobal(intBytes);

            // copy mydata to unmanaged space
            Marshal.Copy(bytData, 0, heapBytes, bytData.Length);

            // create byte array with enough space to hold data
            iOutput = new Int32[intBytes / 4];

            // copy sound data from unmanaged space into byte array
            Marshal.Copy(heapBytes, iOutput, 0, intBytes / 4);

            // free unmanaged space
            Marshal.FreeHGlobal(heapBytes);

            return (iOutput);
        }


    }
}
