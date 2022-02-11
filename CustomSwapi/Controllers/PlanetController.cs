using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomSwapi.DataObjects;
using Flurl.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomSwapi.Controllers
{
    [Route("api/planets")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        // GET: api/<PlanetController>
        [HttpGet]
        public async Task<IEnumerable<Planet>> GetAllPlanets()
        {
            List<Planet> planetCollection = new List<Planet>();

            string apiUri = "https://swapi.dev/api/planets/";
            var apiTask = await apiUri.GetJsonAsync<PlanetCollection>();
            planetCollection.AddRange(apiTask.results);

            while (string.IsNullOrEmpty(apiTask.next) == false)
            {
                apiUri = apiTask.next;
                apiTask = await apiUri.GetJsonAsync<PlanetCollection>();
                planetCollection.AddRange(apiTask.results);
            }

            foreach (var item in planetCollection)
            {
                string[] sections = item.url.Split(@"/");
                item.id = int.Parse(sections[5]);
            }

            return planetCollection;
        }

        // GET api/<PlanetController>/5
        [HttpGet("{id}")]
        public async Task<Planet> GetPlanetById(int id)
        {
            string apiUri = $"https://swapi.dev/api/planets/{id}/";

            Planet planet = await apiUri.GetJsonAsync<Planet>();
            string[] sections = planet.url.Split(@"/");
            planet.id = int.Parse(sections[5]);

            return planet;     
        }

        // POST api/<PlanetController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PlanetController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlanetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
