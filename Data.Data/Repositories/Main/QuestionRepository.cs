using AutoMapper;
using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ELIContext _context;
        private readonly IMapper _mapper;
        public QuestionRepository(ELIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<CreateQuestionViewModel> CreateQuestionAsync(CreateQuestionViewModel question, CancellationToken ct = default(CancellationToken))
        {
            var qualifier = await _context.Qualifier.Include(a => a.Questions).Where(a => a.QualifierId == question.QualifierId && a.IsActive == true && a.IsDeleted == false).FirstOrDefaultAsync();
            Question tempQuestion = new Question();
            tempQuestion = _mapper.Map<Question>(question);
            tempQuestion.Sequence = qualifier.Questions.Count() + 1;
            tempQuestion.IsDeleted = false;
            tempQuestion.IsActive = true;
            if(question.DeviceIdentifier != null && question.DeviceIdentifier != "")
            {
                var device = await _context.Device.SingleOrDefaultAsync(a => a.DeviceIdentifier == question.DeviceIdentifier && a.IsDeleted == false && a.IsActive == true);
                if (device != null)
                {
                    tempQuestion.DeviceId = device.DeviceId;
                }
            }
            await _context.Question.AddAsync(tempQuestion, ct);
            await _context.SaveChangesAsync(ct);
            foreach (var option in question.options)
            {
                var tempquestion = await _context.Question.Include(a => a.Responses).SingleOrDefaultAsync(a => a.QuestionId == tempQuestion.QuestionId && a.IsActive == true && a.IsDeleted == false);
                QuestionOption tempOption = new QuestionOption();
                tempOption.option = option.Name;
                tempOption.Sequence = tempquestion.Responses.Count() + 1;
                tempOption.QuestionId = tempQuestion.QuestionId;
                tempOption.IsDeleted = false;
                tempOption.CreatedDate = DateTime.Now;
                tempOption.IsActive = true;
                await _context.QuestionOption.AddAsync(tempOption, ct);
                await _context.SaveChangesAsync(ct);
            }
            question.QuestionId = tempQuestion.QuestionId;
            return question;
        }
        
        public async Task<List<CreateQuestionViewModel>> GetQuestionByQualifierIdAsync(int qualifierId, CancellationToken ct = default(CancellationToken))
        {
            var questions = await _context.Question.Where(x => x.IsActive == true && x.IsDeleted == false).Where(a => a.QualifierId == qualifierId && a.IsActive == true && a.IsDeleted == false).OrderBy(a => a.Sequence).ToListAsync();
            List<CreateQuestionViewModel> QList = new List<CreateQuestionViewModel>();
            foreach (var item in questions)
            {
                CreateQuestionViewModel Q = new CreateQuestionViewModel();
                Q.QualifierId = item.QualifierId;
                Q.QuestionDescription = item.QuestionDescription;
                Q.QuestionId = item.QuestionId;
                Q.QuestionTypeId = item.QuestionTypeId;
                Q.QuestionTypeName = item.QuestionTypeName;
                Q.Sequence = item.Sequence;
                var device = await _context.Device.SingleOrDefaultAsync(a => a.DeviceId == item.DeviceId);
                if (device != null)
                {
                    Q.DeviceIdentifier = device.DeviceIdentifier;
                }
                QList.Add(Q);
            }
            foreach (var q in QList)
            {
                q.Responses = await _context.QuestionOption.OrderBy(a => a.Sequence).Where(a => a.QuestionId == q.QuestionId && a.IsActive == true && a.IsDeleted == false).ToListAsync();
                //foreach (var o in options)
                //{
                //    q.Responses.Add(o);
                //}
            }
            if (QList != null)
                return QList.OrderBy(a => a.Sequence).ToList();
            else
                throw new AppException("No Questions found against this qualifier");
        }
        public async Task<Question> GetQuestionByIdAsync(int Id, CancellationToken ct = default(CancellationToken))
        {
            var question = await _context.Question.SingleOrDefaultAsync(a => a.QuestionId == Id && a.IsActive == true && a.IsDeleted == false);

            question.Responses = new List<QuestionOption>();
            var options = await _context.QuestionOption.Where(a => a.QuestionId == question.QuestionId && a.IsActive == true && a.IsDeleted == false).ToListAsync();
            //foreach (var o in options)
            //{
            //    question.Responses.Add(o);
            //}
            return question;
        }
        public async Task<List<QuestionType>> GetQuestionTypesAsync(CancellationToken ct = default(CancellationToken))
        {
            var questionTypes = await _context.QuestionType.Where(a => a.IsActive == true && a.IsDeleted == false).ToListAsync();
            return questionTypes;
        }
        public async Task UpdateQuestionAsync(CreateQuestionViewModel question, CancellationToken ct = default(CancellationToken))
        {
            var tempQuestion = await _context.Question.SingleOrDefaultAsync(a => a.QuestionId == question.QuestionId && a.IsDeleted == false && a.IsActive == true);
            if (tempQuestion == null) throw new AppException("Question not found");
            tempQuestion.QuestionTypeId = question.QuestionTypeId;
            tempQuestion.QuestionDescription = question.QuestionDescription;
            tempQuestion.Sequence = question.Sequence;
            tempQuestion.UpdatedDate = DateTime.Now;
            if (question.DeviceIdentifier != null && question.DeviceIdentifier != "")
            {
                var device = await _context.Device.SingleOrDefaultAsync(a => a.DeviceIdentifier == question.DeviceIdentifier && a.IsDeleted == false && a.IsActive == true);
                if (device != null)
                {
                    tempQuestion.DeviceId = device.DeviceId;
                }
            }
            _context.Question.Update(tempQuestion);
            foreach (var option in question.options)
            {
                if (option.OptionId == 0)
                {
                    QuestionOption q = new QuestionOption();
                    q.option = option.Name;
                    q.QuestionId = tempQuestion.QuestionId;
                    q.QuestionOptionId = 0;
                    q.Sequence = option.Sequence;
                    await CreateOption(q);
                }
                else
                {
                    var tempOption = await _context.QuestionOption.SingleOrDefaultAsync(a => a.QuestionOptionId == option.OptionId && a.IsDeleted == false && a.IsActive == true);
                    if (tempOption == null) throw new AppException("Option not found");
                    tempOption.option = option.Name;
                    tempOption.Sequence = option.Sequence;
                    tempOption.UpdatedDate = DateTime.Now;
                    _context.QuestionOption.Update(tempOption);
                    await _context.SaveChangesAsync(ct);
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteQuestionAsync(int Id, CancellationToken ct = default(CancellationToken))
        {
            var question = await _context.Question.Include(a => a.Responses).SingleOrDefaultAsync(a => a.QuestionId == Id && a.IsActive == true);
            if (question != null)
            {
                question.IsDeleted = true;
                _context.Question.Update(question);
                await _context.SaveChangesAsync();

                if (question.QuestionTypeId != 3 && question.QuestionTypeId != 4)
                {
                    foreach (var option in question.Responses)
                    {
                        var deletedOption = await DeleteOptionAsync(option.QuestionOptionId);
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new AppException("Question not found");
            }
        }
        public async Task<bool> DeleteOptionAsync(int Id, CancellationToken ct = default(CancellationToken))
        {
            var option = await _context.QuestionOption.SingleOrDefaultAsync(a => a.QuestionOptionId == Id && a.IsActive == true && a.IsDeleted == false);
            if (option != null)
            {
                option.IsDeleted = true;
                _context.QuestionOption.Update(option);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new AppException("Option not found");
            }
        }
        public async Task<QuestionOption> CreateOption(QuestionOption QuestionOption, CancellationToken ct = default(CancellationToken))
        {
            QuestionOption tempOption = new QuestionOption();
            tempOption.option = QuestionOption.option;
            tempOption.QuestionId = QuestionOption.QuestionId;
            tempOption.Sequence = QuestionOption.Sequence;
            tempOption.IsDeleted = false;
            tempOption.CreatedDate = DateTime.Now;
            tempOption.IsActive = true;
            await _context.QuestionOption.AddAsync(tempOption, ct);
            await _context.SaveChangesAsync(ct);
            return tempOption;
        }
        public async Task<bool> OrderingQuestionAsync(QuestionOrderingViewModel questionOrdering, CancellationToken ct = default(CancellationToken))
        {
            if (questionOrdering.Questions != null)
            {
                foreach (var x in questionOrdering.Questions)
                {
                    var question = await _context.Question.Where(a => a.IsActive == true && a.IsDeleted == false && a.QuestionId == x.QuestionId).FirstOrDefaultAsync();
                    if (question != null)
                    {
                        question.Sequence = x.Sequence;
                        _context.Question.Update(question);
                        await _context.SaveChangesAsync(ct);
                    }
                    else
                    {
                        throw new AppException("No Question found.");
                    }

                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> DeleteAllOptionsAsync(List<int> Ids, CancellationToken ct = default(CancellationToken))
        {
            foreach (var op in Ids)
            {
                var option = await _context.QuestionOption.SingleOrDefaultAsync(a => a.QuestionOptionId == op && a.IsActive == true);
                if (option != null)
                {
                    option.IsDeleted = true;
                    _context.QuestionOption.Update(option);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new AppException("Option not found");
                }
            }
            return true;
        }
        public async Task<List<CreateQuestionWebViewModel>> CreateQuestionWebAsync(List<CreateQuestionWebViewModel> questions,int usedId, CancellationToken ct = default(CancellationToken))
        {
            foreach (var item in questions)
            {
                var qualifier = await _context.Qualifier.Include(a => a.Questions).Where(a => a.QualifierId == item.QualifierId && a.IsActive == true && a.IsDeleted == false).FirstOrDefaultAsync();
                Question tempQuestion = new Question();
                tempQuestion = _mapper.Map<Question>(item);
                tempQuestion.Sequence = qualifier.Questions.Count() + 1;
                tempQuestion.IsDeleted = false;
                tempQuestion.QuestionId = 0;
                tempQuestion.IsActive = true;
                tempQuestion.CreatedDate = DateTime.Now;
                await _context.Question.AddAsync(tempQuestion, ct);
                await _context.SaveChangesAsync(ct);
                foreach (var option in item.options)
                {
                    var tempquestion = await _context.Question.Include(a => a.Responses).SingleOrDefaultAsync(a => a.QuestionId == tempQuestion.QuestionId && a.IsActive == true && a.IsDeleted == false);
                    QuestionOption tempOption = new QuestionOption();
                    tempOption.option = option.Name;
                    tempOption.Sequence = tempquestion.Responses.Count() + 1;
                    tempOption.QuestionId = tempQuestion.QuestionId;
                    tempOption.QuestionOptionId = 0;
                    tempOption.IsDeleted = false;
                    tempOption.CreatedDate = DateTime.Now;
                    tempOption.IsActive = true;
                    await _context.QuestionOption.AddAsync(tempOption, ct);
                    await _context.SaveChangesAsync(ct);
                }
                item.QuestionId = tempQuestion.QuestionId;
                if(item.QualifierId > 0 && item.IsExhibitor)
                {
                    QualifierUsers QU = new QualifierUsers();
                    QU.QualifierId = 0;
                    QU.QualifierId = item.QuestionId;
                    QU.UserId = usedId;
                    await _context.QualifierUsers.AddAsync(QU, ct);
                    await _context.SaveChangesAsync(ct);
                }
            }
            return questions;
        }
        public async Task<List<CreateQuestionWebViewModel>> UpdateQuestionWebAsync(List<CreateQuestionWebViewModel> questions, int qualifierId, List<int> questionDelete, int userId, CancellationToken ct = default(CancellationToken))
        {
            foreach (var q in questionDelete)
            {
                await DeleteQuestionAsync(q);
            }
         
            foreach (var item in questions)
            {
                if (item.QuestionId == 0)
                {
                    List<CreateQuestionWebViewModel> qList = new List<CreateQuestionWebViewModel>();
                    CreateQuestionWebViewModel q = new CreateQuestionWebViewModel();

                    q = _mapper.Map(item,q);
                    qList.Add(q);
                    await CreateQuestionWebAsync(qList,userId);
                }
                else
                {
                    var qu = await _context.Question.SingleOrDefaultAsync(a => a.QuestionId == item.QuestionId);
                    qu.Sequence= questions.IndexOf(item) + 1;
                      _context.Question.Update(qu);
                    await _context.SaveChangesAsync();
                }
            }
            return questions;
        }
    }
}
