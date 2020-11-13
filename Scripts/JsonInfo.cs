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
using Ys.Json;
namespace Ys
{
  [System.Serializable]
  public class JsonInfo
  {
    public UserData userData;
  }
  [System.Serializable]
  public class UserData
  {
    public string PlayerName;
    public List<string> goodList = new List<string>();
  }
}
