using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Esd.EnergyPec.UpdaterOnline
{
    public static class ZipOrDeCompressHelper
    {
        #region 压缩、解压缩文件
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        public static void CompressFile(string sourceFile, string destinationFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                //if (checkCount != buffer.Length) throw new ApplicationException();
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (GZipStream compressStream = new GZipStream(destinationStream, CompressionMode.Compress))
                    {
                        byte[] buffer = new byte[1024 * 64];
                        int checkCount = 0;
                        while ((checkCount = sourceStream.Read(buffer, 0, buffer.Length)) >= buffer.Length)
                        {
                            compressStream.Write(buffer, 0, buffer.Length);
                        }
                        compressStream.Write(buffer, 0, checkCount);
                    }
                }
            }
        }

        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        public static void DeCompressFile(string sourceFile, string destinationFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open))
            {
                byte[] quartetBuffer = new byte[4];
                const int bufferLength = 1024 * 64;
                /*压缩文件的流的最后四个字节保存的是文件未压缩前的长度信息，
                 * 把该字节数组转换成int型，可获取文件长度。
                 * int position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);*/
                byte[] buffer = new byte[1024 * 64];
                using (GZipStream decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true))
                {

                    using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create))
                    {
                        int bytesRead = 0;
                        while ((bytesRead = decompressedStream.Read(buffer, 0, bufferLength)) >= bufferLength)
                        {
                            destinationStream.Write(buffer, 0, bufferLength);
                        }
                        destinationStream.Write(buffer, 0, bytesRead);
                        destinationStream.Flush();
                    }
                }
            }

        }
        #endregion
    }
}
