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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ys.IO;
using LitJson;
using System.IO;

namespace Ys
{
  public class JsonManager : MonoBehaviour
  {
    public bool isFirstGame;
    public bool isRijndaelEncrypt;
    public string filename_json;
    public DefaultDataScriptObject defaultDataScriptObject;
    [Header("可视化Json数据,请勿编辑!")]
    public JsonInfo jsonInfo=new JsonInfo();
    private void Start()
    {
      InitLocalLowIOFile();
      print(jsonInfo.userData.PlayerName);
    }
    /// <summary>
    /// 初始化本地文件数据操作
    /// </summary>
    private void InitLocalLowIOFile()
    {
      IOTOOL.CreateDirectory();
      if (isFirstGame)
      {
        IOTOOL.Delete(filename_json);
      }

      if (!IOTOOL.IsFileExists(filename_json))
      {
        Debug.Log("第一次初始化数据");
        IOTOOL.CreateFile("第一次初始化数据User", filename_json);
        jsonInfo = defaultDataScriptObject.StaicJsonInfo;
        Sava();
      }
      else
      {
        Debug.Log(filename_json + " 文件已经存在!!");
      }
     
      Load();
    }
    private void OnGUI()
    {
      if (GUI.Button(new Rect(100,100,100,100),"Save"))
      {
        Sava();
      }
      if (GUI.Button(new Rect(100, 200, 100, 100), "修改"))
      {
        jsonInfo.userData.PlayerName = "修改了名字";
      }
    }
    
    public void Sava()
    {
      string sys = JsonMapper.ToJson(jsonInfo);
      print(sys);
      if (sys != "")
      {
        Debug.Log("开始写入");
        //对字符串进行加密,32位加密密钥
        if (isRijndaelEncrypt) sys =IOTOOL.RijndaelEncrypt(sys, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        StreamWriter streamWriter = File.CreateText(string.Format("{0}/{1}.ys", IOTOOL.Unity_Dirpath, filename_json));
        streamWriter.Write(sys);
        streamWriter.Close();
        Debug.Log("开始写入="+ sys);
      }
    }
    public void Load()
    {
      StreamReader streamReader = File.OpenText(string.Format("{0}/{1}.ys", IOTOOL.Unity_Dirpath, filename_json));
      string readsys = streamReader.ReadToEnd();
      streamReader.Close();
      Debug.Log(readsys);
      if (readsys != "")
      {
        //对数据进行解密，32位解密密钥
        if (isRijndaelEncrypt) readsys = IOTOOL.RijndaelDecrypt(readsys, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        jsonInfo = JsonMapper.ToObject<JsonInfo>(readsys);
      }
      else
      {
        Sava();
        jsonInfo = JsonMapper.ToObject<JsonInfo>(readsys);
      }
    }
  }

}
