using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatforService.Data;

namespace PlatformService.Controllers
{
    [Route("api/platforms")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ILogger<PlatformsController> _logger;
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ILogger<PlatformsController> logger, IPlatformRepo repository, IMapper mapper)
        {
            _logger = logger;
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
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var PlatformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute(nameof(GetPlatFormById), new{id=PlatformReadDto.Id}, PlatformReadDto);
        }
    }
}