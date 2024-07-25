using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewDog.Dto;
using ReviewDog.interfaces;
using ReviewDog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;

        public BreedController(IBreedRepository breedRepository, IMapper mapper)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBreeds()
        {
            var breeds = _mapper.Map<List<BreedDto>>(_breedRepository.GetBreeds());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(breeds);
        }

        [HttpGet("{breedId}")]
        public IActionResult GetBreed(int breedId)
        {
            if (!_breedRepository.BreedExists(breedId))
                return NotFound();
            var breed = _mapper.Map<BreedDto>(_breedRepository.GetBreed(breedId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(breed);
        }

        [HttpGet("dog/{breedId}")]
        public IActionResult GetDogsByBreed(int breedId)
        {
            var dog = _mapper.Map<DogDto>(_breedRepository.GetDogsByBreed(breedId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(dog);
        }

        [HttpPost]
        public IActionResult CreateBreed(BreedDto breedCreate)
        {
            if (breedCreate == null)
                return BadRequest(ModelState);
            var breed = _breedRepository.GetBreeds().Where(b => b.Title.Trim().ToUpper() == breedCreate.Title.TrimEnd().ToUpper()).FirstOrDefault();
            if (breed != null)
            {
                ModelState.AddModelError("", "Данная порода уже существует");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var breedMap = _mapper.Map<Breed>(breedCreate);
            if (!_breedRepository.CreateBreed(breedMap))
            {
                ModelState.AddModelError("", "что то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Порода успешна добавлена");

        }
        [HttpPut("{breedId}")]
        public IActionResult UpdateBreed(int breedId, BreedDto breedUpdate)
        {
            if (breedUpdate == null || breedId != breedUpdate.Id)
                return BadRequest(ModelState);
            if (!_breedRepository.BreedExists(breedId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var breedMap = _mapper.Map<Breed>(breedUpdate);
            if (!_breedRepository.UpdateBreed(breedMap))
            {
                ModelState.AddModelError("", "что то пошло не так при обновлении содержимого");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{breedId}")]
        public IActionResult DeleteBreed(int breedId)
        {
            if (!_breedRepository.BreedExists(breedId))
                return NotFound();
            var breedToDelete = _breedRepository.GetBreed(breedId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_breedRepository.DeleteBreed(breedToDelete))
            {
                ModelState.AddModelError("", "что то пошло не так при удалении содержимого");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
