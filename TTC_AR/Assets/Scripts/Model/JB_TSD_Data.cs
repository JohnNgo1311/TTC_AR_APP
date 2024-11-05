using System;
using System.Collections.Generic;

[Serializable]
public class JB_TSD_Data
{
  public Dictionary<string, List<string>> JB_TSD_Wiring { get; set; }
  public Dictionary<string, List<string>> JB_TSD_Location { get; set; }
}
[Serializable]
// Nếu JSON của bạn là một mảng, bạn có thể tạo một lớp wrapper
public class JB_TSD_Wrapper
{
  public List<JB_TSD_Data> Data { get; set; }
}