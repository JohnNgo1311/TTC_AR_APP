using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;


[Preserve]

[Serializable]
public class AccountModel
{
  public string userName { get; set; }
  public string password { get; set; }
  public AccountModel(string userName, string password)
  {
    this.userName = userName;
    this.password = password;
  }
}


