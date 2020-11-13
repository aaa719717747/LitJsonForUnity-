/***
*   Company:Frozen Wolf Sudio
*	Title："LockstepFrame" Lockstep框架项目
*		主题：XXX
*	Description：
*		功能：XXX
*	Date：2019
*	Version：0.1版本
*	Author：xxx
*	Modify Recoder：
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
namespace Ys.IO
{
  public class IOTOOL
  {
    public static string Unity_Dirpath = Application.persistentDataPath + "/SL";
    /// <summary>
    /// 判断文件是否存在
    /// </summary>
    public static bool IsFileExists(string fileName)
    {
      return File.Exists(string.Format("{0}/{1}.ys", Unity_Dirpath, fileName));
    }
    /// <summary>
    /// 判断文件夹是否存在
    /// </summary>
    public static bool IsDirectoryExists()
    {
      return Directory.Exists(Unity_Dirpath);
    }
    /// <summary>
    /// 创建一个文本文件    
    /// </summary>
    /// <param name="fileName">文件路径</param>
    /// <param name="content">文件内容</param>
    public static void CreateFile(string content, string name)
    {
      StreamWriter streamWriter = File.CreateText(string.Format("{0}/{1}.ys",Unity_Dirpath,name));
      Debug.Log("路径:"+ string.Format("{0}/{1}.ys", Unity_Dirpath, name));
      streamWriter.Write(content);
      streamWriter.Close();
    }
    /// <summary>
    /// 创建一个文件夹
    /// </summary>
    public static void CreateDirectory()
    {
      //文件夹存在则返回
      if (IsDirectoryExists())
        return;
      Directory.CreateDirectory(Unity_Dirpath);
    }
    /// <summary>
    /// 删除文件
    /// </summary>
    public static void Delete(string fileName)
    {
      File.Delete(string.Format("{0}/{1}.ys", Unity_Dirpath, fileName));
    }
    /// <summary>
    /// Rijndael加密算法
    /// </summary>
    /// <param name="pString">待加密的明文</param>
    /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
    /// <param name="iv">iv向量,长度为128（byte[16])</param>
    /// <returns></returns>
    public static string RijndaelEncrypt(string pString, string pKey)
    {
      //密钥
      byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
      //待加密明文数组
      byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(pString);


      //Rijndael解密算法
      RijndaelManaged rDel = new RijndaelManaged();
      rDel.Key = keyArray;
      rDel.Mode = CipherMode.ECB;
      rDel.Padding = PaddingMode.PKCS7;
      ICryptoTransform cTransform = rDel.CreateEncryptor();


      //返回加密后的密文
      byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
      return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }
    /// <summary>
    /// ijndael解密算法
    /// </summary>
    /// <param name="pString">待解密的密文</param>
    /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
    /// <param name="iv">iv向量,长度为128（byte[16])</param>
    /// <returns></returns>
    public static String RijndaelDecrypt(string pString, string pKey)
    {
      //解密密钥
      byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
      //待解密密文数组
      Debug.Log(pString);
      byte[] toEncryptArray = Convert.FromBase64String(pString);


      //Rijndael解密算法
      RijndaelManaged rDel = new RijndaelManaged();
      rDel.Key = keyArray;
      rDel.Mode = CipherMode.ECB;
      rDel.Padding = PaddingMode.PKCS7;
      ICryptoTransform cTransform = rDel.CreateDecryptor();


      //返回解密后的明文
      byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
      return UTF8Encoding.UTF8.GetString(resultArray);
    }
  }
  
}
