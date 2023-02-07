using Alumni.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Service
{
    public class FileUploadService
    {
        [HttpPost]
        public FlagTips FileLoad(HttpPostedFileBase httpPostedFileBase, string Stu_Empno)
        {
            FlagTips flagTips = new FlagTips();
            flagTips.IsSuccess = false;
            flagTips.Msg = "照片上传失败 Photo upload failed";
            if (httpPostedFileBase != null)
            {
                try
                {
                    string fileName = Path.GetFileName(httpPostedFileBase.FileName);//原始文件名称

                    byte[] fileData = ReadFileBytes(httpPostedFileBase);//文件流转化为二进制字节

                    string result = SaveFile(Stu_Empno, fileData, fileName);//文件保存
                    if (string.IsNullOrEmpty(result))
                    {
                        return flagTips;
                    }
                    flagTips.IsSuccess = true;
                    flagTips.Msg = result;
                    return flagTips;
                }
                catch (Exception ex)
                {
                    return flagTips;
                }
            }
            else
            {
                return flagTips;
            }
        }

        /// <summary>
        /// 将文件流转化为二进制字节
        /// </summary>
        /// <param name="fileData">图片文件流</param>
        /// <returns></returns>
        private byte[] ReadFileBytes(HttpPostedFileBase fileData)
        {
            byte[] data;
            using (Stream inputStream = fileData.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileExtension">文件扩展名</param>
        /// <param name="fileData">图片二进制文件信息</param>
        /// <returns></returns>
        private string SaveFile(string Stu_Empno, byte[] fileData, string fileName)
        {
            string result;
            try
            {
                string saveName = Stu_Empno + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName; //保存文件名称

                // 文件上传后的保存路径
                string basePath = "UploadFile";
                string filePath = $"{System.Web.HttpContext.Current.Server.MapPath("~/UploadFile")}/{ saveName }";
                string serverDir = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/"));
                if (!System.IO.Directory.Exists(serverDir))
                {
                    System.IO.Directory.CreateDirectory(serverDir);
                }
                System.IO.File.WriteAllBytes(filePath, fileData);//WriteAllBytes创建一个新的文件，按照对应的文件流写入，假如已存在则覆盖
                                                                 //返回完整的图片保存地址
                result = "/" + basePath + "/" + saveName;
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}