using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;
#nullable enable

[Preserve]

[Serializable]
public class AccountModel
{
  public string userName { get; set; } = string.Empty;
  public string password { get; set; } = string.Empty;
  public AccountModel(string userName, string password)
  {
    this.userName = userName;
    this.password = password;
  }
}


