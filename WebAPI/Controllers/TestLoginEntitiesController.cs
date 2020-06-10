using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TestLoginEntitiesController : ApiController
    {
        private TutoringAppDBEntities db = new TutoringAppDBEntities();

        // GET: api/TestLoginEntities
        public IQueryable<TestLoginEntity> GetTestLoginEntities()
        {
            return db.TestLoginEntities;
        }

        // GET: api/TestLoginEntities/5
        [ResponseType(typeof(TestLoginEntity))]
        public async Task<IHttpActionResult> GetTestLoginEntity(string id)
        {
            TestLoginEntity testLoginEntity = await db.TestLoginEntities.FindAsync(id);
            if (testLoginEntity == null)
            {
                return NotFound();
            }

            return Ok(testLoginEntity);
        }

        // PUT: api/TestLoginEntities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTestLoginEntity(string id, TestLoginEntity testLoginEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != testLoginEntity.userName)
            {
                return BadRequest();
            }

            db.Entry(testLoginEntity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestLoginEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TestLoginEntities
        [ResponseType(typeof(TestLoginEntity))]
        public async Task<IHttpActionResult> PostTestLoginEntity(TestLoginEntity testLoginEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TestLoginEntities.Add(testLoginEntity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TestLoginEntityExists(testLoginEntity.userName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = testLoginEntity.userName }, testLoginEntity);
        }

        // DELETE: api/TestLoginEntities/5
        [ResponseType(typeof(TestLoginEntity))]
        public async Task<IHttpActionResult> DeleteTestLoginEntity(string id)
        {
            TestLoginEntity testLoginEntity = await db.TestLoginEntities.FindAsync(id);
            if (testLoginEntity == null)
            {
                return NotFound();
            }

            db.TestLoginEntities.Remove(testLoginEntity);
            await db.SaveChangesAsync();

            return Ok(testLoginEntity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestLoginEntityExists(string id)
        {
            return db.TestLoginEntities.Count(e => e.userName == id) > 0;
        }
    }
}