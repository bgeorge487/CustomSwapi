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
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET: api/<PersonController>
        [HttpGet]
        public async Task<IEnumerable<Person>> GetAllPeople()
        {
            List<Person> personCollection = new List<Person>();

            string apiUri = "https://swapi.dev/api/people/";
            var apiTask = await apiUri.GetJsonAsync<PersonCollection>();
            personCollection.AddRange(apiTask.results);

            while (string.IsNullOrEmpty(apiTask.next) == false)
            {
                apiUri = apiTask.next; 
                apiTask = await apiUri.GetJsonAsync<PersonCollection>();
                personCollection.AddRange(apiTask.results);
            }

            //"https://swapi.dev/api/people/1/"
            foreach (var item in personCollection)
            {
                string[] sections = item.url.Split(@"/");
                item.id = sections[5];
            }

            return personCollection;
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<Person> GetPersonById(int id)
        {
            string apiUri = $"https://swapi.dev/api/people/{id}/";

            Person person = await apiUri.GetJsonAsync<Person>();
            string[] sections = person.url.Split(@"/");
            person.id = sections[5];

            return person;
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<IEnumerable<Person>> AddAPerson([FromBody] Person newPerson)
        {
            List<Person> repository = await MockRepository();
            repository.Add(newPerson);

            return repository;
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<IEnumerable<Person>> DeletePersonById(int id)
        {
            List<Person> repository = await MockRepository();

            repository.Remove(repository.Where(x => x.id == id.ToString()).First());

            return repository;
        }

        private async Task<List<Person>> MockRepository()
        {
            List<Person> personCollection = new List<Person>();

            string apiUri = "https://swapi.dev/api/people/";
            var apiTask = await apiUri.GetJsonAsync<PersonCollection>();
            personCollection.AddRange(apiTask.results);

            //"https://swapi.dev/api/people/1/"
            foreach (var item in personCollection)
            {
                string[] sections = item.url.Split(@"/");
                item.id = sections[5];
            }

            return personCollection;
        }
    }
}
