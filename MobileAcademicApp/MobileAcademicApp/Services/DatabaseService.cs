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
        public static async Task AddCourse(int termId, string name, DateTime startDate, DateTime endDate, string status, 
            string instructorName, string instructorPhoneNumber, string instructorEmail, string notes, 
            List<Models.Assessment> assessments)
        {
            // Initialize the database
            await Init();

            // Create the course based on all of the provided information
            var course = new Models.Course
            {
                TermId = termId,
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                InstructorName = instructorName,
                InstructorPhoneNumber = instructorPhoneNumber,
                InstructorEmail = instructorEmail,
                Notes = notes,
                Assessments = assessments
            };

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
        public static async Task UpdateCourse(int id, int termId, string name, DateTime startDate, DateTime endDate, string status,
            string instructorName, string instructorPhoneNumber, string instructorEmail, string notes,
            List<Models.Assessment> assessments)
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
                courseQuery.TermId = termId;
                courseQuery.Name = name;
                courseQuery.StartDate = startDate;
                courseQuery.EndDate = endDate;
                courseQuery.Status = status;
                courseQuery.InstructorName = instructorName;
                courseQuery.InstructorPhoneNumber = instructorPhoneNumber;
                courseQuery.InstructorEmail = instructorEmail;
                courseQuery.Notes = notes;
                courseQuery.Assessments = assessments;

                // Updating the database with the new information
                await _db.UpdateAsync(courseQuery);
            };
        }

        #endregion

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
        public static async Task AddTerm(string name, DateTime startDate, DateTime endDate, List<Models.Course> courses)
        {
            // Initialize the database
            await Init();

            // Create the term based on all of the provided information
            var term = new Models.Term
            {
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Courses = courses
            };

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
        public static async Task UpdateTerm(int id, string name, DateTime startDate, DateTime endDate, List<Models.Course> courses)
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
                termQuery.Name = name;
                termQuery.StartDate = startDate;
                termQuery.EndDate = endDate;
                termQuery.Courses = courses;

                // Updating the database with the new information
                await _db.UpdateAsync(termQuery);
            }
        }

        #endregion

        #region ASSESSMENT OPERATIONS
        // GET ASSESSMENTS BY COURSE
        public static async Task GetAssessmentsForCourse(int courseId)
        {

        }

        public static async Task AddAssessment()
        {

        }

        public static async Task RemoveAssessment()
        {

        }

        public static async Task UpdateAssessment()
        {

        }

        #endregion

    }
}
