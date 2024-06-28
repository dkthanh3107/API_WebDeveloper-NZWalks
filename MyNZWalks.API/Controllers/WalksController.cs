using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyNZWalks.API.CustomActionFilter;
using MyNZWalks.API.Models.Domain;
using MyNZWalks.API.Models.DTOs;
using MyNZWalks.API.Repositories;
using System.Net;

namespace MyNZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalksRepository walksRepository , IMapper mapper)
        {
            _walksRepository = walksRepository;
            _mapper = mapper;   
        }
        //GET WALK
        //GET:/api/walks?filterOn=Name&filterQuery=Track&sortBy = Name % isAscending = true
        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn , [FromQuery] string? filterQuery ,
            [FromQuery] string? sortBy , [FromQuery] bool? isAscending,
            int pageNumber = 1 , int pageSize = 1000)
        {
                
                var walksDomain = await _walksRepository.GetAllWalksAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            //CREATE an exception
                throw new Exception("this is a new exception");
                return Ok(_mapper.Map<List<WalksDTO>>(walksDomain));
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalks([FromBody] CreateWalksDTO createWalksDTO)
        {
            var walksDomainModel = _mapper.Map<Walks>(createWalksDTO);
            await _walksRepository.CreateWalksAsync(walksDomainModel);
            return Ok(_mapper.Map<WalksDTO>(walksDomainModel));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalksByID(Guid id)
        {
            var walksDomain = await _walksRepository.GetWalksByIDAsync(id);
            if (walksDomain == null)
            {
                return NotFound();
            }
            var walksDTO = _mapper.Map<WalksDTO>(walksDomain);
            return Ok(walksDTO);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute]Guid id,UpdateWalksDTO updateWalksDTO)
        {
            var walkDomain = _mapper.Map<Walks>(updateWalksDTO);
            walkDomain = await _walksRepository.UpdateWalksAsync(id, walkDomain);
            if (walkDomain == null)
                return NotFound();
            var walksDTO = _mapper.Map<WalksDTO>(walkDomain);
            return Ok(walksDTO);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walksDomain = await _walksRepository.DeleteWalksAsync(id);
            if (walksDomain == null)
                return NotFound();
            var walksDTO = _mapper.Map<WalksDTO>(walksDomain);
            return Ok(walksDTO);
        }
    }
}
