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

            await Init();                                               // 
            var terms = await _db.Table<Models.Term>().ToListAsync();
            return terms;
        }

        public static async Task AddTerm(DateTime startDate, DateTime endDate, IEnumerable<Models.Course> courses)
        {

        }

        public static async Task RemoveTerm(int id)
        {
            await Init();
            await _db.DeleteAsync(id);
        }

        #endregion

    }
}
