using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Module;
using Domain.Entities;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class JBUseCase
    {
        private IJBRepository _IJBRepository;

        public JBUseCase(IJBRepository IjbRepository)
        {
            _IJBRepository = IjbRepository;
        }
        //! Select(), ToList(), ToDictionary() đề phải duyệt qua toàn bộ danh sách, duyệt tới đâu lưu tới đó 
        //! => dùng Foreach để chỉ quét 1 lần, tối ưu được thời gian xử lý
        public async Task<List<JBBasicDto>> GetListJBGeneralAsync(string grapperId)
        {
            try
            {
                var jBEntities = await _IJBRepository.GetListJBGeneralAsync(grapperId);
                if (jBEntities == null)
                {
                    throw new ApplicationException("Failed to get JB list");
                }

                int count = jBEntities.Count;
                var JBBasicDtos = new List<JBBasicDto>(count);
                var listJBInfo = new List<JBInformationModel>(count);
                var dictJBInfo = new Dictionary<string, JBInformationModel>(count);

                foreach (var JBEntity in jBEntities)
                {
                    var dto = MapToBasicDto(JBEntity);
                    var model = new JBInformationModel(dto.Id, dto.Name);

                    JBBasicDtos.Add(dto);
                    listJBInfo.Add(model);
                    dictJBInfo[dto.Name] = model;
                }

                GlobalVariable.temp_List_JBInformationModel = listJBInfo;
                GlobalVariable.temp_Dictionary_JBInformationModel = dictJBInfo;

                return JBBasicDtos;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get JB list", ex);
            }
        }



        //! Dùng IEnumerable<JBGeneralDto thay cho List<JBGeneralDto> vì không cần tạo ra List để sử dụng trong hàm ngay
        public async Task<IEnumerable<JBGeneralDto>> GetListJBInforAsync(string grapperId)
        {
            var jBEntities = await _IJBRepository.GetListJBInformationAsync(grapperId);
            if (jBEntities == null || !jBEntities.Any())
            {
                UnityEngine.Debug.LogWarning("No jBEntities found.");
            }

            return jBEntities.Select(MapToGeneralDto);
        }


        public async Task<JBResponseDto> GetJBByIdAsync(string JBId)
        {
            try
            {
                var jbEntity = await _IJBRepository.GetJBByIdAsync(JBId);

                if (jbEntity == null)
                {
                    throw new ApplicationException("Failed to get JB");
                }
                else
                {
                    var jbResponseDto = MapToResponseDto(jbEntity);
                    return jbResponseDto;
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get JB", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> CreateNewJBAsync(string grapperId, JBRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                // Ánh xạ từ JBRequestDto sang JBEntity để check các nghiệp vụ
                var jbEntity = MapRequestToEntity(requestDto);

                // var requestData = MapToRequestDto(jbEntity);

                if (jbEntity == null)
                {
                    throw new ApplicationException("Failed to create JB cause jbEntity is Null");
                }

                else
                {
                    return await _IJBRepository.CreateNewJBAsync(grapperId, jbEntity);
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create JB", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> UpdateJBAsync(string JBId, JBRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                // Ánh xạ từ JBRequestDto sang JBEntity để check các nghiệp vụ
                var jbEntity = MapRequestToEntity(requestDto);

                // var requestData = MapToRequestDto(jbEntity);

                if (jbEntity == null)
                {
                    throw new ApplicationException("Failed to update JB cause jbEntity is Null");
                }

                else
                {
                    return await _IJBRepository.UpdateJBAsync(JBId, jbEntity);
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create JB", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> DeleteJBAsync(string JBId)
        {
            try
            {
                var deletedJBResult = await _IJBRepository.DeleteJBAsync(JBId);
                return deletedJBResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete JB", ex); // Bao bọc lỗi từ Repository
            }
        }


        //! Dto => Entity
        private JBEntity MapRequestToEntity(JBRequestDto jBRequestDto)
        {
            return new JBEntity(
                jBRequestDto.Name,
                 string.IsNullOrEmpty(jBRequestDto.Location) ? "chưa cập nhật" : jBRequestDto.Location,
                jBRequestDto.DeviceBasicDtos == null ? null : jBRequestDto.DeviceBasicDtos.Select(d => new DeviceEntity(d.Id, d.Code)).ToList(),
                jBRequestDto.ModuleBasicDtos == null ? null : jBRequestDto.ModuleBasicDtos.Select(m => new ModuleEntity(m.Id, m.Name)).ToList(),
                jBRequestDto.OutdoorImageBasicDto == null ? null : new ImageEntity(jBRequestDto.OutdoorImageBasicDto.Id, jBRequestDto.OutdoorImageBasicDto.Name),
                jBRequestDto.ConnectionImageBasicDtos == null ? null : jBRequestDto.ConnectionImageBasicDtos.Select(i => new ImageEntity(i.Id, i.Name)).ToList()
            );
        }


        //! Entity => Dto
        private JBResponseDto MapToResponseDto(JBEntity jBEntity)
        {
            return new JBResponseDto(
                jBEntity.Id,

                jBEntity.Name,

                jBEntity.Location ?? "chưa cập nhật",

               (jBEntity.DeviceEntities == null || (jBEntity.DeviceEntities != null && jBEntity.DeviceEntities.Count <= 0)) ?
                 new List<DeviceBasicDto>() : jBEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),

                (jBEntity.ModuleEntities == null || (jBEntity.ModuleEntities != null && jBEntity.ModuleEntities.Count <= 0)) ?
                 new List<ModuleBasicDto>() : jBEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList(),

                jBEntity.OutdoorImageEntity == null ?
                 null : new ImageBasicDto(jBEntity.OutdoorImageEntity.Id, jBEntity.OutdoorImageEntity.Name
                 //  , jBEntity.OutdoorImageEntity.Url
                 ),

                (jBEntity.ConnectionImageEntities == null || (jBEntity.ConnectionImageEntities != null && jBEntity.ConnectionImageEntities.Count <= 0)) ?
                 new List<ImageBasicDto>() : jBEntity.ConnectionImageEntities.Select(i => new ImageBasicDto(i.Id, i.Name
                 //  , i.Url
                 )).ToList()
            );
        }
        private JBGeneralDto MapToGeneralDto(JBEntity jBEntity)
        {
            return new JBGeneralDto(
                id: jBEntity.Id,

              name: jBEntity.Name,

          location: jBEntity.Location ?? "chưa cập nhật",

         outdoorImageBasicDto: jBEntity.OutdoorImageEntity == null ?
                 null : new ImageBasicDto(jBEntity.OutdoorImageEntity.Id, jBEntity.OutdoorImageEntity.Name),

            connectionImageBasicDtos: (jBEntity.ConnectionImageEntities == null || (jBEntity.ConnectionImageEntities != null && jBEntity.ConnectionImageEntities.Count <= 0)) ?
                 new List<ImageBasicDto>() : jBEntity.ConnectionImageEntities.Select(i => new ImageBasicDto(i.Id, i.Name)).ToList()
             );
        }
        private JBBasicDto MapToBasicDto(JBEntity jBEntity)
        {
            return new JBBasicDto(
             id: jBEntity.Id,

           name: jBEntity.Name
                   );
        }
        // private JBRequestDto MapToRequestDto(JBEntity jBEntity)
        // {
        //     return new JBRequestDto(
        //         jBEntity.Name,
        //         jBEntity.Location ?? "chưa cập nhật",
        //         jBEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),
        //         jBEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList(),
        //         jBEntity.OutdoorImageEntity != null ? new ImageBasicDto(jBEntity.OutdoorImageEntity.Id, jBEntity.OutdoorImageEntity.Name) : null!,
        //         jBEntity.ConnectionImageEntities.Select(i => new ImageBasicDto(i.Id, i.Name)).ToList()
        //     );
        // }


    }
}