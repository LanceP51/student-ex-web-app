using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercises.Models
{
    public class StudentExercise
    {
        public int Id { get; set; }

        // This is to hold the actual foreign key integer
        public int StudentId { get; set; }
        public int ExerciseId { get; set; }
        public string Cohort { get; set; }
        public string Exercise { get; set; }
    }
}
