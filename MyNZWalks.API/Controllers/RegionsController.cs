using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNZWalks.API.CustomActionFilter;
using MyNZWalks.API.Data;
using MyNZWalks.API.Models.Domain;
using MyNZWalks.API.Models.DTOs;
using MyNZWalks.API.Repositories;
using System.Reflection;
using System.Text.Json;

namespace MyNZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRegionRepository _regionRepository;
        private readonly ILogger<RegionsController> _logger1;

        public RegionsController(NZWalkDBContext dBContext, IMapper mapper, IRegionRepository regionRepository, ILogger<RegionsController> logger1)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _regionRepository = regionRepository;
            _logger1 = logger1;
        }
        ////GET ALL REGIONS 
        ///GET https://locallhost:portnumber/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllReions()
        {

            //Get Data From Database -  Domain Models
            var regionsDomain = await _regionRepository.GetAllRegionsAsync();

            //Map Domain Model to DTOs

            var regionsDTO = _mapper.Map<List<RegionsDTO>>(regionsDomain);

            //Return DTOs
            return Ok(regionsDTO);

        }
        //[HttpGet("{id}")]
        //Get Regions By ID
        //GET : https://locallhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionsByID([FromRoute] Guid id) 
        {
            var regionsDomain = await _regionRepository.GetRegionsByIDAsync(id);
            if(regionsDomain == null)
            {
                return NotFound();
            }    
            var regionsDTO = _mapper.Map<Regions>(regionsDomain);
            return Ok(regionsDTO);
        }
        //Create To New Regions
        // POST : https://locallhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Write")]
        public async Task<IActionResult> CreateRegions([FromBody] CreateRegionsRequestDTO createRegionsRequestDTO)
        {
            // map or convert DTO to domain model
            var regionsModelDomain = _mapper.Map<Regions>(createRegionsRequestDTO);
            //use domain model to create regions
            regionsModelDomain= await _regionRepository.CreateRegionsAsync(regionsModelDomain);
            //map Domain Model back to DTO
            var regionsDTO = _mapper.Map<Regions>(regionsModelDomain);
            return CreatedAtAction(nameof(GetRegionsByID), new {id = regionsDTO.Id}, regionsDTO);
        }
        //Update to new regions
        //PUT : https://locallhost:portnumber/api/r egions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Write")]
        public async Task<IActionResult> UpdateRegions([FromRoute]Guid id , UpdateRegionsDTO updateRegionsDTO)
        {
            //map DTO to domain model 
            var regionDomainModel= _mapper.Map<Regions>(updateRegionsDTO);
            /*var regionDomainModel = new Regions
            {
                Code = updateRegionsDTO.Code,
                Name = updateRegionsDTO.Name,
                RegionImageURL = updateRegionsDTO.RegionImageURL
            };*/
            //check if regions exist
            regionDomainModel= await _regionRepository.UpdateRegionsAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Lặp qua các thuộc tính trong updateRegionsDTO
            //convert domain  model to DTO
            var regionsDTO = _mapper.Map<RegionsDTO>(regionDomainModel);
            /*var regionsDTO = new RegionsDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };*/
            return Ok(regionsDTO);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Write")]
        public async Task<IActionResult> DeleteRegions([FromRoute]Guid id) 
        {
            var regionsDomainModel = await _regionRepository.DeleteRegionsAsync(id);
            if (regionsDomainModel == null)
                return NotFound();
            
            //map domain model to dto
            var regionDTO = _mapper.Map<Regions>(regionsDomainModel);
            return Ok(regionDTO);
        }
    }
}
