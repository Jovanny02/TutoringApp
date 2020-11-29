using AppWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace AppWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        TutoringAppDBEntities ddb = new TutoringAppDBEntities();

        public readonly List<user> Usersssssss = new List<user>{
            new user
            {
                fullName= "kevin",
                averageRating=2.9 
            }
        };

       [HttpGet]
      //  [Route("api/Values")]
        
        // GET api/values
        public IEnumerable<user> Get()
        {
            // reservation reserve = new reservation();
            // reserve.toDateTime = 
          //  List<user> hello = UFID;

            return  Usersssssss;
           // return new string[] { "value1", "value2" };
        }



        [HttpGet]
        //[Route("api/Values/{id}")]
        public string Get(string id)
        {
            
         
            
            return "Value1";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
