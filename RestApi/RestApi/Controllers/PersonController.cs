using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datos.Modelo;

namespace RestApi.Controllers
{
    public class PersonController : ApiController
    {
         TestEntities db = new TestEntities();
        //Get
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            var listado = db.People.ToList();
            return listado;
        }

        //Get
        [HttpGet]
        public Person Get(int id)
        {
            var person = db.People.FirstOrDefault(x => x.id==id);
            return person;
        }

        //Post
        [HttpPost]
        public IHttpActionResult Post([FromBody] Person p )
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

              db.People.Add(p);

              db.SaveChanges();
            
            return Ok(p);
        }


        //Post
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Person p)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var personaExistente = db.People.Count(x => x.id == id) > 0;

            if (personaExistente)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else return NotFound();

            return Ok(p);
        }

        //Delete
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var personaExistente = db.People.Find(id);

            if (personaExistente != null)
            {
                db.People.Remove(personaExistente);
                db.SaveChanges();
            }
            else return NotFound();

            return Ok(personaExistente);
        }

    }
}
