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
    public class DogController : ControllerBase
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;

        public DogController(IDogRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
        }
        [HttpGet]

        public IActionResult GetDogs()
        {
            var dogs =_mapper.Map<List<DogDto>>(_dogRepository.GetDogs());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(dogs);
        }

        [HttpGet("{dogId}")]
        public IActionResult GetDog(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
                return NotFound();
            var dog =_mapper.Map<DogDto>(_dogRepository.GetDog(dogId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(dog);
        }

        [HttpGet("{dogId}/rating")]
        public IActionResult GetDogRating(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
                return NotFound();
            var rating = _dogRepository.GetDogRating(dogId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }

        [HttpPost]
        public IActionResult CreateDog(DogDto dogCreate, int ownerId,int breedId)
        {
            if (dogCreate == null)
                return BadRequest(ModelState);
            var dogs = _dogRepository.GetDogs().Where(d => d.Name.Trim().ToUpper() == dogCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (dogs != null)
            {
                ModelState.AddModelError("", "Данная собака уже существует");
                return StatusCode(422,ModelState);
            };
            var dogMap = _mapper.Map<Dog>(dogCreate);
            if(!_dogRepository.CreateDog(dogMap, ownerId, breedId))
            {
                ModelState.AddModelError("", "что то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            };
            return Ok("Собака успешна добавлена");
        }

        [HttpPut("{dogId}")]
        public IActionResult UpdateDog(int ownerId, int breedId,int dogId, DogDto dogUpdate)
        {
            if (dogUpdate == null || ownerId != dogUpdate.Id)
                return BadRequest(ModelState);
            if (!_dogRepository.DogExists(dogId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var dogMap = _mapper.Map<Dog>(dogUpdate);
            if(!_dogRepository.UpdateDog(dogMap, ownerId, breedId))
            {
                ModelState.AddModelError("", "что то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{dogId}")]
        public IActionResult DeketeDog(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
                return NotFound();
            var dogToDelete = _dogRepository.GetDog(dogId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_dogRepository.DeleteDog(dogToDelete))
            {
                ModelState.AddModelError("", "что то пошло не так при удалении");
                return StatusCode(500,ModelState);
            }
            return NoContent();
        }
    }
}
