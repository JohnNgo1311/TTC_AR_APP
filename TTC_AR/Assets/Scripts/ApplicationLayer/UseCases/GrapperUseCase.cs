using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.Grapper;
using ApplicationLayer.Dtos.Module;
using Domain.Entities;
using Domain.Interfaces;
using ApplicationLayer.Dtos.Rack;
using ApplicationLayer.Dtos.JB;
using ApplicationLayer.Dtos.Mcc;
using ApplicationLayer.Dtos.FieldDevice;

namespace ApplicationLayer.UseCases
{
    public class GrapperUseCase
    {
        private IGrapperRepository _IGrapperRepository;

        public GrapperUseCase(IGrapperRepository IGrapperRepository)
        {
            _IGrapperRepository = IGrapperRepository;
        }
        public async Task<List<GrapperBasicDto>> GetListGrapperAsync(int companyId)
        {
            try
            {
                var GrapperEntities = await _IGrapperRepository.GetListGrapperAsync(companyId);

                if (GrapperEntities == null)
                {
                    throw new ApplicationException("Failed to get Grapper list");
                }
                else
                {
                    var GrapperResponseDtos = GrapperEntities.Select(GrapperEntity => MapToBasicDto(GrapperEntity)).ToList();
                    return GrapperResponseDtos;
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Grapper list", ex);
            }
        }
        public async Task<GrapperResponseDto> GetGrapperByIdAsync(int GrapperId)
        {
            try
            {
                var GrapperEntity = await _IGrapperRepository.GetGrapperByIdAsync(GrapperId);

                if (GrapperEntity == null)
                {
                    throw new ApplicationException("Failed to get Grapper");
                }
                else
                {
                    var GrapperResponseDto = MapToResponseDto(GrapperEntity);
                    return GrapperResponseDto;
                }
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Grapper", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> CreateNewGrapperAsync(int companyId, GrapperRequestDto requestDto)
        {
            companyId = GlobalVariable.companyId;
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }

                var GrapperEntity = MapRequestToEntity(requestDto);

                if (GrapperEntity == null)
                {
                    throw new ApplicationException("Failed to create Grapper cause GrapperEntity is Null");
                }

                else
                {
                    return await _IGrapperRepository.CreateNewGrapperAsync(companyId, GrapperEntity);
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create Grapper", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> UpdateGrapperAsync(int GrapperId, GrapperRequestDto requestDto)
        {
            GrapperId = GlobalVariable.GrapperId;
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                // Ánh xạ từ GrapperRequestDto sang GrapperEntity để check các nghiệp vụ
                var GrapperEntity = MapRequestToEntity(requestDto);

                // var requestData = MapToRequestDto(GrapperEntity);

                if (GrapperEntity == null)
                {
                    throw new ApplicationException("Failed to update Grapper cause GrapperEntity is Null");
                }

                else
                {
                    return await _IGrapperRepository.UpdateGrapperAsync(GrapperId, GrapperEntity);
                }

            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create Grapper", ex); // Bao bọc lỗi từ Repository
            }
        }
        public async Task<bool> DeleteGrapperAsync(int GrapperId)
        {
            try
            {
                var deletedGrapperResult = await _IGrapperRepository.DeleteGrapperAsync(GrapperId);
                return deletedGrapperResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete Grapper", ex); // Bao bọc lỗi từ Repository
            }
        }


        //! Dto => Entity
        private GrapperEntity MapRequestToEntity(GrapperRequestDto GrapperRequestDto)
        {
            return new GrapperEntity(
                GrapperRequestDto.Name
            );
        }

        //! Entity => Dto

        private GrapperBasicDto MapToBasicDto(GrapperEntity GrapperEntity)
        {
            return new GrapperBasicDto(
                id: GrapperEntity.Id,
                name: GrapperEntity.Name
            );
        }
        private GrapperResponseDto MapToResponseDto(GrapperEntity GrapperEntity)
        {
            return new GrapperResponseDto(
            id: GrapperEntity.Id,
            name: GrapperEntity.Name,
            rackBasicDtos: GrapperEntity.RackEntities.Count > 0 ? GrapperEntity.RackEntities.Select(r => new RackBasicDto(r.Id, r.Name)).ToList() : new List<RackBasicDto>(),
            moduleBasicDtos: GrapperEntity.ModuleEntities.Count > 0 ? GrapperEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList() : new List<ModuleBasicDto>(),
            jBBasicDtos: GrapperEntity.JBEntities.Count > 0 ? GrapperEntity.JBEntities.Select(j => new JBBasicDto(j.Id, j.Name)).ToList() : new List<JBBasicDto>(),
            mccBasicDtos: GrapperEntity.MccEntities.Count > 0 ? GrapperEntity.MccEntities.Select(m => new MccBasicDto(m.Id, m.CabinetCode)).ToList() : new List<MccBasicDto>(),
            deviceBasicDtos: GrapperEntity.DeviceEntities.Count > 0 ? GrapperEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList() : new List<DeviceBasicDto>(),
            fieldDeviceBasicDtos: GrapperEntity.FieldDeviceEntities.Count > 0 ? GrapperEntity.FieldDeviceEntities.Select(fd => new FieldDeviceBasicDto(fd.Id, fd.Name)).ToList() : new List<FieldDeviceBasicDto>()
            );
        }

        // private GrapperRequestDto MapToRequestDto(GrapperEntity GrapperEntity)
        // {
        //     return new GrapperRequestDto(
        //         GrapperEntity.Name,
        //         GrapperEntity.Location ?? "chưa cập nhật",
        //         GrapperEntity.DeviceEntities.Select(d => new DeviceBasicDto(d.Id, d.Code)).ToList(),
        //         GrapperEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList(),
        //         GrapperEntity.OutdoorImageEntity != null ? new ImageBasicDto(GrapperEntity.OutdoorImageEntity.Id, GrapperEntity.OutdoorImageEntity.Name) : null!,
        //         GrapperEntity.ConnectionImageEntities.Select(i => new ImageBasicDto(i.Id, i.Name)).ToList()
        //     );
        // }


    }
}