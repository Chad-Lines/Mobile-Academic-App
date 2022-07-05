using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite;

namespace MobileAcademicApp.Services
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection _db;

        static async Task Init()
        {
            // Initializing the database.

            // Checking if the _db already exists. If it does NOT (i.e. if it is null) then we'll create it.
            // Otherwise, we just return
            if (_db == null)
            {
                // Get the absolute path of the database file
                var databasePath = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments), "AcademicApp.db");

                // Connect to the database using the path
                _db = new SQLiteAsyncConnection(databasePath);

                // Create the database tables
                await _db.CreateTableAsync<Models.Term>();
                await _db.CreateTableAsync<Models.Course>();
                await _db.CreateTableAsync<Models.Assessment>();
            }
            else 
            {
                // If _db already exists, just return
                return;
            }        
        }

        #region TERM OPERATIONS

        // GET ALL TERMS
        public static async Task<IEnumerable<Models.Term>> GetAllTerms()
        {
            // Initialize the database
            await Init();

            // Getting the list of all terms from the database
            var terms = await _db.Table<Models.Term>().ToListAsync();

            // Return the terms
            return terms;
        }

        // GET TERM BY ID
        public static async Task<Models.Term> GetTermByID(int id)
        {
            // Initialize the database
            await Init();

            // Search for the term by term Id
            var term = await _db.Table<Models.Term>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            // Return the term
            return term;
        }

        // ADD TERM
        public static async Task AddTerm(Models.Term term)
        {
            // Initialize the database
            await Init();

            // Adding the term in the database
            var id = await _db.InsertAsync(term);
        }

        // REMOVE TERM
        public static async Task RemoveTerm(int id)
        {
            // Initialize the database
            await Init();

            // Remove the course from the database
            await _db.DeleteAsync<Models.Term>(id);
        }

        // UPDATE TERM
        public static async Task UpdateTerm(int id, Models.Term term)
        {
            // Initialize the database
            await Init();

            // Querying the database for the matching term
            // Opening the database
            var termQuery = await _db.Table<Models.Term>()
              .Where(i => i.Id == id) // Looking for the ID			                   
              .FirstOrDefaultAsync(); // Getting the first result   

            // Update the term based on all of the provided information
            if (termQuery != null)
            {
                termQuery.Name = term.Name;
                termQuery.StartDate = term.StartDate;
                termQuery.EndDate = term.EndDate;

                // Updating the database with the new information
                await _db.UpdateAsync(termQuery);
            }
        }

        #endregion

        #region COURSE OPERATIONS

        // GET COURSES BY TERM
        public static async Task<IEnumerable<Models.Course>> GetCoursesForTerm(int termId) 
        {
            // Initialize the database
            await Init();

            // Querying the database for the matching course
            var courses = await _db.Table<Models.Course>()  // Opening thd database                
                .Where(i => i.TermId == termId)             // Looking for the ID
                .ToListAsync();                             // Getting the results as a list

            // Return the list
            return courses;
        }

        // GET A SINGLE COURSE BY ID
        public static async Task<Models.Course> GetCourseByID(int id)
        {
            // Initialize the database
            await Init();

            // Search for the course by courseId
            var course = await _db.Table<Models.Course>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            // Return the course
            return course;
        }

        // ADD COURSE
        public static async Task AddCourse(Models.Course course)
        {
            // Initialize the database
            await Init();

            // Adding the course in the database
            var id = await _db.InsertAsync(course);
        }

        // REMOVE COURSE
        public static async Task RemoveCourse(int id)
        {
            // Initialize the database
            await Init();

            // Remove the course from the database
            await _db.DeleteAsync<Models.Course>(id);
        }

        // UPDATE COURSE
        public static async Task UpdateCourse(int id, Models.Course course)
        {
            // Initialize the database
            await Init();

            // Querying the database for the matching course
            var courseQuery = await _db.Table<Models.Course>()  // Opening thd database
                .Where(i => i.Id == id)                         // Looking for the ID
                .FirstOrDefaultAsync();                         // Getting the first result

            // Create the course based on all of the provided information
            if (courseQuery != null)
            {
                courseQuery.TermId = course.TermId;
                courseQuery.Name = course.Name;
                courseQuery.StartDate = course.StartDate;
                courseQuery.EndDate = course.EndDate;
                courseQuery.Status = course.Status;
                courseQuery.InstructorName = course.InstructorName;
                courseQuery.InstructorPhoneNumber = course.InstructorPhoneNumber;
                courseQuery.InstructorEmail = course.InstructorEmail;
                courseQuery.Notes = course.Notes;

                // Updating the database with the new information
                await _db.UpdateAsync(courseQuery);
            };
        }

        #endregion

        #region ASSESSMENT OPERATIONS
        // GET ASSESSMENTS BY COURSE
        public static async Task<IEnumerable<Models.Assessment>> GetAssessmentsForCourse(int courseId)
        {
            // Initialize the database
            await Init();

            // Querying the database for the matching assessment
            var assessments = await _db.Table<Models.Assessment>()  // Opening thd database                
                .Where(i => i.CourseId == courseId)             // Looking for the ID
                .ToListAsync();                                 // Getting the results as a list

            // Return the list
            return assessments;
        }

        // ADD ASSESSMENT
        public static async Task AddAssessment(int courseId, string name, string type, DateTime dueDate)
        {
            // Initialize the database
            await Init();

            // Create the assessment based on all of the provided information
            var assessment = new Models.Assessment
            {
                CourseId = courseId,
                Name = name,
                Type = type,
                DueDate = dueDate
            };

            // Adding the course in the database
            var id = await _db.InsertAsync(assessment);
        }

        // REMOVE ASSESSMENT
        public static async Task RemoveAssessment(int id)
        {
            // Initialize the database
            await Init();

            // Remove the assessment from the database
            await _db.DeleteAsync<Models.Assessment>(id);
        }
        
        // UPDATE ASSESSMENT
        public static async Task UpdateAssessment(int id, int courseId, string name, string type, DateTime dueDate)
        {
            // Initialize the database
            await Init();

            // Querying the database for the matching assessment
            // Opening the database
            var assessmentQuery = await _db.Table<Models.Assessment>()
              .Where(i => i.Id == id) // Looking for the ID			                   
              .FirstOrDefaultAsync(); // Getting the first result 

            // Update the assessment based on the information provided
            if (assessmentQuery != null)
            {
                assessmentQuery.CourseId = courseId;
                assessmentQuery.Name = name;
                assessmentQuery.Type = type;
                assessmentQuery.DueDate = dueDate;

                // Updating the database with the new information
                await _db.UpdateAsync(assessmentQuery);
            };
        }

        #endregion

    }
}
