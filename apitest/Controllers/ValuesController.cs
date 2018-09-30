using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using apitest.model;
using Microsoft.AspNetCore.Cors;

namespace apitest.Controllers
{
    [DisableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        string connstr = "Data Source=ASHLAP\\MSSQLSERVER12;Initial Catalog=nrc_sql;Integrated Security=true";
        // GET api/values
        [HttpGet]
        public ActionResult<List<dish>> Get()//
        {
            List<dish> mylist =new List<dish>();
            string ls_sql = "select name,image,category,label,price,description from dishes";
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                List<dish> mydisharr = new List<dish>()  ;
                try
                {
                    conn.Open();
                    SqlCommand scmd = new SqlCommand(ls_sql, conn);
                    SqlDataReader rd = scmd.ExecuteReader();
                    dish ld;
                    while (rd.Read())
                       {
                        ld = new dish{name = (string)rd.GetValue(0), image = (string)rd.GetValue(1),
                            category = (string)rd.GetValue(2), label = (string)rd.GetValue(3),
                            price = (string)rd.GetValue(4),description = (string)rd.GetValue(5) };    
                        mydisharr.Add(ld);
                    }
                    rd.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }

            
               return mydisharr ;
            }
            //return new string[] { "v 1", "value2" };

        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return id.ToString();
        }

        // GET api/values/5
        [HttpGet ,Route("api/values/name/{qry}")]
        public ActionResult<string> Getname(string qry)
        {
            return  "hello"+qry;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
