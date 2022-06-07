using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
using Xamarin.Essentials;

namespace Mobile_Academic_App.Services
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection _db;

        static async Task Init()
        {
            if (_db != null)
            {
                return;
            }
            else
            {
                // Set the absolute path to the database
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "academic_db.db3");

                // Create the database connection
                _db = new SQLiteAsyncConnection(databasePath);

                // Create the tables
                await _db.CreateTableAsync<Models.Term>();
                await _db.CreateTableAsync<Models.Course>();
                await _db.CreateTableAsync<Models.ObjectiveAssessment>();
                await _db.CreateTableAsync<Models.PerformanceAssessment>();
            }            
        }

        #region Updating Terms

        public static async Task<IEnumerable<Models.Term>> GetTerms()
        {

            // Get a list of terms

            await Init();                                               // Calling the init method asynchronously
            var terms = await _db.Table<Models.Term>().ToListAsync();   // Getting a list of terms
            return terms;                                               // Returning the terms
        }       

        public static async Task AddTerm(DateTime startDate, DateTime endDate, IEnumerable<Models.Course> courses)
        {
            await Init();                           // Calling the init method asynchronously

            Models.Term term = new Models.Term();   // Creating the term

            // Setting the term parameters...
            term.StartDate = startDate;             
            term.EndDate = endDate;
            term.Courses = courses;
                
            await _db.InsertAsync(term);            // Adding the term to the database
        }

        public static async Task RemoveTerm(int id)
        {
            await Init();               // Calling the init method asynchronously
            await _db.DeleteAsync(id);  // Deleting the term with the provided ID
        }

        public static async Task UpdateTerm(int id, DateTime startDate, DateTime endDate, IEnumerable<Models.Course> courses)
        {
            await Init();                                                   // Calling the init method asynchronously

            var UpdateTermQuery = await _db.Table<Models.Term>()            // Querying the database for a Term...
                .Where(i => i.Id == id)                                     // Where the ID is equal to the ID provided
                .FirstOrDefaultAsync();                                     // Get the first result (there should be only one)

            if (UpdateTermQuery == null)                                    // If the result is null, then...
            {
                Console.WriteLine("Term with id " + id + " not found");     // Write to the console
                return;
            }
            else                                                            // If the result is NOT null, then...
            {
                UpdateTermQuery.StartDate = startDate;                      // Set the result's variables according to what was 
                UpdateTermQuery.EndDate = endDate;                          // provided when calling the function.
                UpdateTermQuery.Courses = courses;

                await _db.UpdateAsync(UpdateTermQuery);                     // Write the update to the database

            }
        }
        #endregion

    }
}
