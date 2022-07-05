using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAcademicApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCourse : ContentPage
    {
        Models.Term _term;

        public AddCourse(Models.Term term)
        {
            InitializeComponent();

            // Capturing the term
            _term = term;
        }

        private async void saveCourseButton_Clicked(object sender, EventArgs e)
        {
            // Create a new course object based on user input
            Models.Course course = new Models.Course();
            course.TermId = _term.Id;
            course.Name = courseName.Text;
            course.StartDate = startDate.Date;
            course.EndDate = endDate.Date;
            course.Status = status.SelectedItem.ToString();
            course.InstructorName = instructorName.Text;
            course.InstructorPhoneNumber = instructorPhone.Text;
            course.InstructorEmail = instructorEmail.Text;
            course.Notes = notes.Text;

            // Add the course to the database
            await Services.DatabaseService.AddCourse(course);

            // Navigate back to the term detail page
            await Navigation.PushAsync(new TermDetail(_term));
        }
    }
}