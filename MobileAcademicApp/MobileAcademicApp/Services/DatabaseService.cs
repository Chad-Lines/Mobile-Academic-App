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
                return;
            }

            
        }

        #region COURSE OPERATIONS
        public static async Task<IEnumerable<Models.Course>> GetCoursesForTerm() 
        {
            // Initialize the database
            await Init();

            // Getting the list of courses from the database
            var courses = await _db.Table<Models.Course>().ToListAsync();

            // Return the list
            return courses;
        }

        public static async Task AddCourse(string name, DateTime startDate, DateTime endDate, string status, 
            string instructorName, string instructorPhoneNumber, string instructorEmail, string notes, 
            List<Models.Assessment> assessments)
        {
            // Initialize the database
            await Init();

            // Create the course based on all of the provided information
            var course = new Models.Course
            {
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

        public static async Task RemoveCourse(int id)
        {
            // Initialize the database
            await Init();

            // Remove the course from the database
            await _db.DeleteAsync<Models.Course>(id);
        }

        public static async Task UpdateCourse(int id, string name, DateTime startDate, DateTime endDate, string status,
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
        public static async Task GetAllTerms()
        {

        }

        public static async Task GetTermByID()
        {

        }

        public static async Task AddTerm()
        {

        }

        public static async Task RemoveTerm()
        {

        }

        public static async Task UpdateTerm()
        {

        }

        #endregion

        #region ASSESSMENT OPERATIONS
        public static async Task GetAssessmentsForCourse()
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
