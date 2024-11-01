using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class GlobalVariable_Search_Devices : MonoBehaviour
{
   public static DeviceModel device;
   public static string jbName;
   public static string moduleName;
   public static List<DeviceModel> all_Device_Models;
   //! Lưu ý chỉ lấy những thiết bị cần xem, không phải toàn bộ thiết bị kết nối đến 1 module (chỉ nằm trong 2 file Excel - các cảm biến )
   public static List<DeviceModel> all_Device_GrapperA = new List<DeviceModel>();
   public static List<DeviceModel> devices_Model_By_Grapper = new List<DeviceModel>(); // just Function and Code
   public static List<string> devices_Model_For_Filter = new List<string>(); // just Function and Code

   /* public static List<string> devicesGrapperA_code = new List<string>(){
             "01TT005",
             "01TT007",
             "01TT008",
             "01TT009",
             "01TT010",
             "01TT011",
             "01TT012",
             "01TT015",
             "02LT002",
             "02LT003",
             "02LT004",
             "02TT002",
             "02TT003A",
             "02TT003B",
             "02TT004",
             "02TT007",
             "02TT009",
             "02TT011",
             "02TT013",
             "02TT016",
             "02TT018",
             "02TT020",
             "02TT023",
             "02TT024",
             "02TT025",
             "02TT026",
             "02TT027",
             "02TT028",
             "02TT029",
             "02TT030",
             "02TT031",
             "02TT032",
             "02TT033",
             "02TT034",
             "02TT035",
             "02TT036",
             "02TT037",
             "02TT038",
             "02TT039",
             "02TT040",
             "03LT001",
             "09LDT016",
             "09LT010",
             "09LT018",
             "09LT019",

          };*/

   /* public static List<string> devicesGrapperA_function = new List<string>()
      {
      "Nhiệt độ dầu bả dê búa đập phía Tự do",
      "Nhiệt độ dầu bả dê búa đập phía khớp nối",
      "Nhiệt độ dầu bả dê Motor búa đập phía khớp nối",
      "Nhiệt độ cuộn dây pha 1 Motor búa đập",
      "Nhiệt độ cuộn dây pha 2 Motor búa đập",
      "Nhiệt độ cuộn dây pha 3 Motor búa đập",
      "Nhiệt độ dầu bả dê Motor búa đập phía tự do",
      "Nhiệt độ phòng MCC sân mía",
      "Đo mức bồn nước chè échaudage số 1",
      "Đo mức bồn nước chè Soutirage",
      "Đo mức bồn nước chè échaudage số 2",
      "Nhiệt độ nước chè echaudage vào gia nhiệt 3",
      "Nhiệt độ nước chè echaudage ra khỏi gia nhiệt 1",
      "Nhiệt độ nước chè echaudage ra khỏi gia nhiệt 2",
      "Nhiệt độ nước chè ra bơm N˚1",
      "Nhiệt độ nước chè ra bơm N˚4",
      "Nhiệt độ nước chè ra bơm N˚6",
      "Nhiệt độ nước chè ra bơm N˚8",
      "Nhiệt độ nước chè ra bơm N˚10",
      "Nhiệt độ nước chè ra bơm N˚13",
      "Nhiệt độ nước chè ra bơm số 3",
      "Nhiệt độ nước thẩm thấu",
      "Nhiệt độ dầu Motor che ép phía khớp nối",
      "Nhiệt độ pha R Motor che ép",
      "Nhiệt độ pha W Motor che ép",
      "Nhiệt độ pha B Motor che ép",
      "Nhiệt độ dầu Motor che ép phía tự do",
      "Nhiệt độ nước làm mát che ép",
      "Nhiệt độ trục đỉnh che lớn phía nhà máy",
      "Nhiệt độ trục nạp che lớn phía nhà máy",
      "Nhiệt độ trục ra che lớn phía nhà máy",
      "Nhiệt độ trục đỉnh che lớn phía nhà bã",
      "Nhiệt độ trục nạp che lớn phía nhà bã",
      "Nhiệt độ trục ra che lớn phía nhà bã",
      "Nhiệt độ trục đỉnh che nhỏ phía nhà máy",
      "Nhiệt độ trục nạp che nhỏ phía nhà máy",
      "Nhiệt độ trục ra che nhỏ phía nhà máy",
      "Nhiệt độ trục đỉnh che nhỏ phía nhà bã",
      "Nhiệt độ trục nạp che nhỏ phía nhà bã",
      "Nhiệt độ trục ra che nhỏ phía nhà bã",
      "Đo mức bồn nước chè hỗn hợp qua cân",
      "Đo mức bồn nước ngưng tụ Ballon G'",
      "Đo mức nước trong ống bơm Canal",
      "Đo mức bồn nước sử dụng",
      "Đo mức bồn nước nóng"
      };*/

}


