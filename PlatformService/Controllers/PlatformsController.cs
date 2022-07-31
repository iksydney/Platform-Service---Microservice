using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataService.Http;
using PlatforService.Data;

namespace PlatformService.Controllers
{
    [Route("api/platforms")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandDataClient _commandDataClient;
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandDataClient commandDataClient, IPlatformRepo repository, IMapper mapper)
        {
            _commandDataClient = commandDataClient;
            _repository = repository;
            _mapper = mapper;
        }   
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            System.Console.WriteLine("Getting Platforms");
            
            var platformItem = _repository.GetAllPlatforms();
            if(platformItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatFormById(int id)
        {
            var singlePlatform = _repository.GetPlatformById(id);
            if(singlePlatform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(singlePlatform));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
            
            var PlatformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            try{
                await _commandDataClient.SendPlatformToCommand(PlatformReadDto);
            }catch(Exception ex){
                System.Console.WriteLine(  $"--> Could not send message Synchronously: {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetPlatFormById), new{id=PlatformReadDto.Id}, PlatformReadDto);
        }
    }
}