using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PratiseAPI.Data;
using PratiseAPI.Models.Domain;
using PratiseAPI.Models.Domain.DTO;
using System.Security.Cryptography.Xml;

namespace PratiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly TestDbContext dbContext;
        public RegionsController(TestDbContext testDbContext) 
        { 
            this.dbContext = testDbContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = dbContext.Regions.ToList();

            //Map Domain to DTO
            var regionsDTO = new List<RegionDTO>();
            foreach(var regionDomain in regionsDomain)
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id) 
        {
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            //Map Domain to DTO
            var regionsDTO = new RegionDTO
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return Ok(regionsDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpadteById([FromRoute]Guid id, [FromBody] UpadateRegionRequestDTO requestDTO)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(y => y.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound(id);
            }
            else
            {
                regionDomainModel.Code = requestDTO.Code;
                regionDomainModel.Name = requestDTO.Name;
                regionDomainModel.RegionImageUrl = requestDTO.RegionImageUrl;

                dbContext.SaveChanges();

                var regionDTO = new RegionDTO
                {
                    Id = regionDomainModel.Id,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl,
                };
            }

            return Ok(regionDomainModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDTO requestDTO)
        {
            var regionDomainModel = new Region
            {
                Code = requestDTO.Code,
                Name = requestDTO.Name,
                RegionImageUrl = requestDTO.RegionImageUrl,
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.Id}, regionDTO);
        }
    }
}
