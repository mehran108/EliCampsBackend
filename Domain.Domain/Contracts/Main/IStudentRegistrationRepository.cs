using ELI.Data.Repositories.Main;
using ELI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IStudentRegistrationRepository : IDisposable
    {
        Task<int> AddStudentAsync(StudentRegistration student);
        //Task<StudentRegistration> GetStudentAsync(int studentID);
        Task<bool> UpdateStudentAsync(StudentRegistration student);
        //Task<AllResponse<StudentRegistration>> GetAllStudentAsync(AllRequest<StudentRegistration> student);
        //Task<bool> ActivateStudentAsync(StudentRegistration student);
    }
}
