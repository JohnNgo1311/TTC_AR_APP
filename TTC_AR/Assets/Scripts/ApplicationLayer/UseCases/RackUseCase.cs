using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Device;
using ApplicationLayer.Dtos.Image;
using ApplicationLayer.Dtos.Rack;
using ApplicationLayer.Dtos.Module;
using Domain.Entities;
using Domain.Interfaces;

namespace ApplicationLayer.UseCases
{
    public class RackUseCase
    {
        private readonly IRackRepository _IRackRepository;

        public RackUseCase(IRackRepository IRackRepository)
        {
            _IRackRepository = IRackRepository;
        }

        public async Task<List<RackBasicDto>> GetListRackAsync(string grapperId)
        {
            try
            {
                // var RackEntities = await _IRackRepository.GetListRackAsync(grapperId) ??
                //     throw new ApplicationException("Failed to get Rack list");
                // return RackEntities.Select(RackEntity => MapToBasicDto(RackEntity)).ToList();

                var RackEntities = await _IRackRepository.GetListRackAsync(grapperId) ??

                throw new ApplicationException("Failed to get Rack list");

                int count = RackEntities.Count;

                var RackBasicDtos = new List<RackBasicDto>(count);

                var listRackInfo = new List<RackInformationModel>(count);

                var dictRackInfo = new Dictionary<string, RackInformationModel>(count);

                foreach (var RackEntity in RackEntities)
                {
                    var dto = MapToBasicDto(RackEntity);
                    var model = new RackInformationModel(dto.Id, dto.Name);

                    RackBasicDtos.Add(dto);
                    listRackInfo.Add(model);
                    dictRackInfo[dto.Name] = model;
                }

                GlobalVariable.temp_List_RackInformationModel = listRackInfo;
                GlobalVariable.temp_Dictionary_RackInformationModel = dictRackInfo;

                return RackBasicDtos;


            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Rack list", ex);
            }
        }

        public async Task<RackResponseDto> GetRackByIdAsync(string RackId)
        {
            try
            {
                var RackEntity = await _IRackRepository.GetRackByIdAsync(RackId) ??
                    throw new ApplicationException("Failed to get Rack");

                return MapToResponseDto(RackEntity);
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get Rack", ex); // Bao bọc lỗi từ Repository
            }
        }

        public async Task<bool> CreateNewRackAsync(string grapperId, RackRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }

                var RackEntity = MapRequestToEntity(requestDto) ??
                    throw new ApplicationException("Failed to create Rack cause RackEntity is Null");


                return await _IRackRepository.CreateNewRackAsync(grapperId, RackEntity);
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create Rack", ex); // Bao bọc lỗi từ Repository
            }
        }

        public async Task<bool> UpdateRackAsync(string RackId, RackRequestDto requestDto)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(requestDto.Name))
                {
                    throw new ArgumentException("Name cannot be empty");
                }

                var RackEntity = MapRequestToEntity(requestDto) ??
                    throw new ApplicationException("Failed to update Rack cause RackEntity is Null");

                return await _IRackRepository.UpdateRackAsync(RackId, RackEntity);
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create Rack", ex); // Bao bọc lỗi từ Repository
            }
        }

        public async Task<bool> DeleteRackAsync(string RackId)
        {
            try
            {
                var deletedRackResult = await _IRackRepository.DeleteRackAsync(RackId);
                return deletedRackResult;
            }
            catch (ArgumentException)
            {
                throw; // Ném lại lỗi validation cho Unity xử lý
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete Rack", ex); // Bao bọc lỗi từ Repository
            }
        }

        private RackEntity MapRequestToEntity(RackRequestDto RackRequestDto)
        {
            int count = RackRequestDto.ModuleBasicDtos?.Count ?? 0; // Lấy số lượng phần tử trước
            return new RackEntity(
                name: RackRequestDto.Name,
                moduleEntities: count == 0
                    ? new List<ModuleEntity>() // Nếu không có dữ liệu, trả về danh sách rỗng
                    : new List<ModuleEntity>(RackRequestDto.ModuleBasicDtos.Select(module => new ModuleEntity(module.Id, module.Name)))
            );
        }



        //! Entity => Dto
        private RackResponseDto MapToResponseDto(RackEntity rackEntity)
        {
            var moduleEntities = rackEntity.ModuleEntities;

            return new RackResponseDto(
                id: rackEntity.Id,
                name: rackEntity.Name,
                moduleBasicDtos: moduleEntities.Any()
                    ? moduleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList()
                    : new()
            );
        }


        private RackBasicDto MapToBasicDto(RackEntity RackEntity)
        {
            return new RackBasicDto(
                id: RackEntity.Id,
                name: RackEntity.Name
            );
        }
    }
}
