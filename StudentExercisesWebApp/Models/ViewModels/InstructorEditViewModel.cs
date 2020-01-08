using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesWebApp.Models.ViewModels
{
    public class InstructorEditViewModel
    {
        private readonly IConfiguration _config;

        public List<SelectListItem> Cohorts { get; set; }
        public Instructor Instructor { get; set; }

        public InstructorEditViewModel() { }

        public InstructorEditViewModel(IConfiguration config)
        {

            /*
                Use the LINQ .Select() method to convert
                the list of Cohort into a list of SelectListItem
                objects
            */
            //using (SqlConnection conn = Connection)
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = conn.CreateCommand())
            //    {
            //        cmd.CommandText = "SELECT Id, Name FROM Cohort";
            //        SqlDataReader reader = cmd.ExecuteReader();

            //        List<Cohort> cohorts = new List<Cohort>();
            //        if (reader.Read())
            //        {
            //            cohorts.Add(new Cohort
            //            {
            //                Id = reader.GetInt32(reader.GetOrdinal("Id")),
            //                Name = reader.GetString(reader.GetOrdinal("Name")),
            //            });
            //        }

            //        reader.Close();

            //        cohorts.Select(cohort => Cohorts);

                    //return cohorts;
                }
            }
        }
    //}
//}
