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
using Microsoft.Extensions.Configuration;

namespace apitest.Controllers
{
   // [DisableCors]
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
            string ls_sql = "select id,name,image,category,label,price,description from dishes";
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
                        ld = new dish{id= (int) rd.GetValue(0) ,name = (string)rd.GetValue(1), image = (string)rd.GetValue(2),
                            category = (string)rd.GetValue(3), label = (string)rd.GetValue(4),
                            price = (string)rd.GetValue(5),description = (string)rd.GetValue(6) };    
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
        //[Route("{qry:string}")]
        //public ActionResult<string> Getname(string qry)
        //{
        //    return  "hello "+qry;
        //}

        // POST api/values
        [HttpPost]

        public ActionResult Post(dish[] dishes)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                SqlCommand SqlCmd = new SqlCommand();
                conn.Open();
                SqlCmd.Connection = conn;
                foreach (var ldish in dishes)
                {
                    try
                    {
                        string strupdate = "update dishes set name='" + ldish.name + "' , image='" + ldish.image + "' ,category='" + ldish.category + "' , label='" + ldish.label + "' ,price='" + ldish.price + "' ,description= '" + ldish.description + "'";
                        strupdate += " where id= " + ldish.id;
                        ;

                        SqlCmd.CommandText = strupdate;
                        SqlCmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    { }

                }
                return Ok();
            }
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
