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
                var RackEntities = await _IRackRepository.GetListRackAsync(grapperId);

                if (RackEntities == null)
                {
                    throw new ApplicationException("Failed to get Rack list");
                }

                var RackResponseDtos = RackEntities.Select(RackEntity => MapToBasicDto(RackEntity)).ToList();
                return RackResponseDtos;
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
                var RackEntity = await _IRackRepository.GetRackByIdAsync(RackId);

                if (RackEntity == null)
                {
                    throw new ApplicationException("Failed to get Rack");
                }

                var RackResponseDto = MapToResponseDto(RackEntity);
                return RackResponseDto;
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

                var RackEntity = MapRequestToEntity(requestDto);

                if (RackEntity == null)
                {
                    throw new ApplicationException("Failed to create Rack cause RackEntity is Null");
                }

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

                var RackEntity = MapRequestToEntity(requestDto);

                if (RackEntity == null)
                {
                    throw new ApplicationException("Failed to update Rack cause RackEntity is Null");
                }

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

        //! Dto => Entity
        private RackEntity MapRequestToEntity(RackRequestDto RackRequestDto)
        {
            return new RackEntity(
                name: RackRequestDto.Name,
                moduleEntities: (RackRequestDto.ModuleBasicDtos == null || RackRequestDto.ModuleBasicDtos.Count <= 0)
                    ? new List<ModuleEntity>()
                    : RackRequestDto.ModuleBasicDtos.Select(module => new ModuleEntity(module.Id, module.Name)).ToList()
            );
        }

        //! Entity => Dto
        private RackResponseDto MapToResponseDto(RackEntity RackEntity)
        {
            return new RackResponseDto(
                id: RackEntity.Id,
                name: RackEntity.Name,
                moduleBasicDtos: RackEntity.ModuleEntities.Count > 0
                    ? RackEntity.ModuleEntities.Select(m => new ModuleBasicDto(m.Id, m.Name)).ToList()
                    : new List<ModuleBasicDto>()
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
