using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IQuestionRepository : IDisposable
    {
        Task<CreateQuestionViewModel> CreateQuestionAsync(CreateQuestionViewModel question, CancellationToken ct = default(CancellationToken));
        Task<List<CreateQuestionViewModel>> GetQuestionByQualifierIdAsync(int qualifierId, CancellationToken ct = default(CancellationToken));
        
        Task<Question> GetQuestionByIdAsync(int Id, CancellationToken ct = default(CancellationToken));
        Task<List<QuestionType>> GetQuestionTypesAsync(CancellationToken ct = default(CancellationToken));
        Task UpdateQuestionAsync(CreateQuestionViewModel question, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteQuestionAsync(int Id, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteOptionAsync(int Id, CancellationToken ct = default(CancellationToken));
        Task<bool> OrderingQuestionAsync(QuestionOrderingViewModel questionOrdering, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAllOptionsAsync(List<int> Ids, CancellationToken ct = default(CancellationToken));
        Task<List<CreateQuestionWebViewModel>> CreateQuestionWebAsync(List<CreateQuestionWebViewModel> questions, int usedId, CancellationToken ct = default(CancellationToken));
        Task<List<CreateQuestionWebViewModel>> UpdateQuestionWebAsync(List<CreateQuestionWebViewModel> questions, int qualifierId, List<int> questionDelete, int userId, CancellationToken ct = default(CancellationToken));
    }
}
