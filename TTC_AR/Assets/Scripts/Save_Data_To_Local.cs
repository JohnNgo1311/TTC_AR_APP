using System;
using UnityEngine;
using System.Collections.Generic;

public static class Save_Data_To_Local
{
    //! Lưu danh sách string vào PlayerPrefs
    public static void SaveStringList(string key, List<string> stringList)
    {
        // Nối các string trong danh sách lại với nhau, sử dụng một ký tự phân cách
        string joinedString = string.Join(",", stringList);
        PlayerPrefs.SetString(key, joinedString);
    }
    //! Sử dụng danh sách string đã được lưu
    public static List<string> GetStringList(string key)
    {
        // Kiểm tra nếu key tồn tại trong PlayerPrefs
        if (PlayerPrefs.HasKey(key))
        {
            // Lấy chuỗi đã lưu từ PlayerPrefs
            string savedString = PlayerPrefs.GetString(key);
            // Tách chuỗi ra thành danh sách string dựa trên ký tự phân cách
            List<string> stringList = new List<string>(savedString.Split(','));
            return stringList;
        }
        // Nếu không có dữ liệu, trả về danh sách rỗng
        else return new List<string>();
    }
}
