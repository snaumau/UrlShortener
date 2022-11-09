using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.API.DTOs;
using UrlShortener.Models.Entities;
using UrlShortener.Models.Interfaces;

namespace UrlShortener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        public UrlController(IUrlRepository urlRepository, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUrls()
        {
            var urls = _urlRepository.GetAllUrl;
            var records = _mapper.Map<List<GetUrlsDto>>(urls);
            return Ok(records);
        }

        [HttpGet("{id}")]
        public IActionResult GetUrl(int id)
        {
            var url = _urlRepository.GetUrl(id);
            if (url is null)
            {
                return NotFound();
            }

            var urlDetailsDto = _mapper.Map<GetUrlDetailsDto>(url);
            return Ok(urlDetailsDto);
        }

        [HttpPost]
        public IActionResult PostUrl(CreateUrlDto createUrlDto)
        {
            var url = _mapper.Map<Url>(createUrlDto);
            _urlRepository.PostUrl(url);

            return CreatedAtAction("GetUrl",
                new { id = url.Id },
                url);
        }

        [HttpPut]
        public IActionResult PutUrl(int id, EditUrlDto editUrlDto)
        {
            if (id != editUrlDto.Id)
            {
                return BadRequest();
            }

            var url = _urlRepository.GetUrl(id);
            if (url is null)
            {
                return NotFound();
            }

            _mapper.Map(editUrlDto, url);

            _urlRepository.PutUrl(url);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteUrl(DeleteUrlDto deleteUrlDto)
        {
            var url = _mapper.Map<Url>(deleteUrlDto);
            _urlRepository.DeleteUrl(url);
            return Ok();
        }
    }
}
