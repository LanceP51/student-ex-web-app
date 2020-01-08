using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesWebApp.Models.ViewModels
{
    public class StudentEditViewModel
    {
        public Student student { get; set; }
        public List<SelectListItem> allExercises { get; set; } = new List<SelectListItem>();
        public List<int> SelectedExercises { get; set; }
        public List<SelectListItem> cohorts { get; set; } = new List<SelectListItem>();
        protected string _connectionString;
        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public StudentEditViewModel() { }
        public StudentEditViewModel(string connectionString, int id)
        {
            _connectionString = connectionString;
            {

                using (SqlConnection conn = Connection)

                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                        SELECT Student.Id, Student.FirstName, Student.LastName, Student.SlackHandle, Student.CohortId, StudentExercise.Id, StudentExercise.ExerciseId, Exercise.Id AS 'ExerciseId', Exercise.Name FROM Student JOIN StudentExercise ON Student.Id=StudentExercise.StudentId JOIN Exercise ON StudentExercise.ExerciseId=Exercise.Id WHERE Student.Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        SqlDataReader reader = cmd.ExecuteReader();



                        while (reader.Read())
                        {
                            if (student==null) { student = new Student
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                                CohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                            };
                            }
                            student.exercises.Add(new Exercise
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ExerciseId")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            });
                        }
                        reader.Close();
                    }

                    cohorts = GetAllCohorts()
                .Select(cohort => new SelectListItem()
                {
                    Text = cohort.Name,
                    Value = cohort.Id.ToString()
                }).ToList();

                    cohorts.Insert(0, new SelectListItem
                    {
                        Text = "Choose a cohort",
                        Value = "0"
                    });

                    allExercises = GetAllExercises().Select(exercise => new SelectListItem()
                    {
                        Text = exercise.Name,
                        Value = exercise.Id.ToString(),
                        Selected = student.exercises.Any(assignedExercise=> assignedExercise.Id==exercise.Id)
                    }).ToList();
                }
            }
        }


        protected List<Cohort> GetAllCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Cohort";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> cohorts = new List<Cohort>();
                    while (reader.Read())
                    {
                        cohorts.Add(new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        });
                    }
                    reader.Close();
                    return cohorts;
                }
            }
        }


        protected List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT e.Id, e.Name, e.Language FROM Exercise e";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        exercises.Add(new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Language = reader.GetString(reader.GetOrdinal("Language"))
                        });
                    };
                    reader.Close();
                    return exercises;
                }
            }
        }
    }

}
