using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Domain.Entities
{
  [Preserve]
  [Serializable]
  public class StaffEntity
  {
    public string userName { get; set; }
    public string password { get; set; }
    // public StaffEntity(string userName, string password)
    // {
    //   this.userName = userName;
    //   this.password = password;
    // }
  }
}



